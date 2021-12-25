using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Web;
using EasyScrShot.HelperLib;

namespace EasyScrShot.Uploader
{
    public class Uploader
    {
        private static readonly string UserAgent = "ShareX";

        public const string ContentTypeMultipartFormData = "multipart/form-data";
        public const string ContentTypeJSON = "application/json";
        public const string ContentTypeURLEncoded = "application/x-www-form-urlencoded";
        public const string ContentTypeOctetStream = "application/octet-stream";

        public List<string> Errors { get; private set; }

        public bool IsUploading { get; protected set; }
        public int BufferSize { get; set; }

        public bool StopUploadRequested { get; protected set; }

        private HttpWebRequest currentRequest;

        public bool WebExceptionReturnResponse { get; set; }
        public bool WebExceptionThrow { get; set; }

        public bool AllowReportProgress { get; set; }

        public delegate void ProgressEventHandler(ProgressManager progress);
        public event ProgressEventHandler ProgressChanged;

        protected void OnProgressChanged(ProgressManager progress)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(progress);
            }
        }


        protected UploadResult UploadData(Stream dataStream, string url, string fileName, string fileFormName = "file", Dictionary<string, string> arguments = null,
            NameValueCollection headers = null, CookieCollection cookies = null, ResponseType responseType = ResponseType.Text, HttpMethod method = HttpMethod.POST,
            string contentType = ContentTypeMultipartFormData, string metadata = null)
        {
            UploadResult result = new UploadResult();

            IsUploading = true;
            StopUploadRequested = false;

            try
            {
                string boundary = CreateBoundary();
                contentType += "; boundary=" + boundary;

                byte[] bytesArguments = MakeInputContent(boundary, arguments, false);
                byte[] bytesDataOpen;
                byte[] bytesDataDatafile = { };

                if (metadata != null)
                {
                    bytesDataOpen = MakeFileInputContentOpen(boundary, fileFormName, fileName, metadata);
                    bytesDataDatafile = MakeFileInputContentOpen(boundary, fileFormName, fileName, null);
                }
                else
                {
                    bytesDataOpen = MakeFileInputContentOpen(boundary, fileFormName, fileName);
                }

                byte[] bytesDataClose = MakeFileInputContentClose(boundary);

                long contentLength = bytesArguments.Length + bytesDataOpen.Length + bytesDataDatafile.Length + dataStream.Length + bytesDataClose.Length;
                HttpWebRequest request = PrepareWebRequest(method, url, headers, cookies, contentType, contentLength);

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytesArguments, 0, bytesArguments.Length);
                    requestStream.Write(bytesDataOpen, 0, bytesDataOpen.Length);
                    requestStream.Write(bytesDataDatafile, 0, bytesDataDatafile.Length);
                    if (!TransferData(dataStream, requestStream)) return null;
                    requestStream.Write(bytesDataClose, 0, bytesDataClose.Length);
                }

                result.Response = ResponseToString(request.GetResponse(), responseType);
                result.URL = result.Response.Replace("http://", "https://");
                result.IsSuccess = true;
            }
            catch (Exception e)
            {
                if (!StopUploadRequested)
                {
                    if (WebExceptionThrow && e is WebException)
                    {
                        throw;
                    }

                    string response = AddWebError(e);

                    if (WebExceptionReturnResponse && e is WebException)
                    {
                        result.Response = response;
                    }
                }
            }
            finally
            {
                currentRequest = null;
                IsUploading = false;
            }

            return result;
        }

        #region Helper methods

        private HttpWebRequest PrepareWebRequest(HttpMethod method, string url, NameValueCollection headers = null, CookieCollection cookies = null, string contentType = null, long contentLength = 0)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = method.ToString();

            if (headers != null)
            {
                if (headers["Accept"] != null)
                {
                    request.Accept = headers["Accept"];
                    headers.Remove("Accept");
                }

                request.Headers.Add(headers);
            }

            request.CookieContainer = new CookieContainer();
            if (cookies != null) request.CookieContainer.Add(cookies);
            IWebProxy proxy = HelpersOptions.CurrentProxy.GetWebProxy();
            if (proxy != null) request.Proxy = proxy;
            request.UserAgent = UserAgent;
            request.ContentType = contentType;

            if (contentLength > 0)
            {
                request.AllowWriteStreamBuffering = HelpersOptions.CurrentProxy.IsValidProxy();
                request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                request.ContentLength = contentLength;
                request.Pipelined = false;
                request.Timeout = -1;
            }
            else
            {
                request.KeepAlive = false;
            }

            currentRequest = request;

            return request;
        }

        protected bool TransferData(Stream dataStream, Stream requestStream)
        {
            if (dataStream.CanSeek)
            {
                dataStream.Position = 0;
            }

            ProgressManager progress = new ProgressManager(dataStream.Length);
            int length = (int)Math.Min(BufferSize, dataStream.Length);
            byte[] buffer = new byte[length];
            int bytesRead;

            while (!StopUploadRequested && (bytesRead = dataStream.Read(buffer, 0, length)) > 0)
            {
                requestStream.Write(buffer, 0, bytesRead);

                if (AllowReportProgress && progress.UpdateProgress(bytesRead))
                {
                    OnProgressChanged(progress);
                }
            }

            return !StopUploadRequested;
        }

        private string CreateBoundary()
        {
            return new string('-', 20) + DateTime.Now.Ticks.ToString("x");
        }

        private byte[] MakeInputContent(string boundary, string name, string value)
        {
            string format = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n", boundary, name, value);
            return Encoding.UTF8.GetBytes(format);
        }

        private byte[] MakeInputContent(string boundary, Dictionary<string, string> contents, bool isFinal = true)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (string.IsNullOrEmpty(boundary)) boundary = CreateBoundary();
                byte[] bytes;

                if (contents != null)
                {
                    foreach (KeyValuePair<string, string> content in contents)
                    {
                        if (!string.IsNullOrEmpty(content.Key) && !string.IsNullOrEmpty(content.Value))
                        {
                            bytes = MakeInputContent(boundary, content.Key, content.Value);
                            stream.Write(bytes, 0, bytes.Length);
                        }
                    }

                    if (isFinal)
                    {
                        bytes = MakeFinalBoundary(boundary);
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }

                return stream.ToArray();
            }
        }

        private byte[] MakeFileInputContentOpen(string boundary, string fileFormName, string fileName)
        {
            string format = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                boundary, fileFormName, fileName, Helpers.GetMimeType(fileName));

            return Encoding.UTF8.GetBytes(format);
        }

        private byte[] MakeFileInputContentOpen(string boundary, string fileFormName, string fileName, string metadata)
        {
            string format = "";

            if (metadata != null)
            {
                format = string.Format("--{0}\r\nContent-Type: {1}; charset=UTF-8\r\n\r\n{2}\r\n\r\n", boundary, ContentTypeJSON, metadata);
            }
            else
            {
                format = string.Format("--{0}\r\nContent-Type: {1}\r\n\r\n", boundary, Helpers.GetMimeType(fileName));
            }

            return Encoding.UTF8.GetBytes(format);
        }

        private byte[] MakeFileInputContentClose(string boundary)
        {
            return Encoding.UTF8.GetBytes(string.Format("\r\n--{0}--\r\n", boundary));
        }

        private byte[] MakeFinalBoundary(string boundary)
        {
            return Encoding.UTF8.GetBytes(string.Format("--{0}--\r\n", boundary));
        }

        private string ResponseToString(WebResponse response, ResponseType responseType = ResponseType.Text)
        {
            if (response != null)
            {
                using (response)
                {
                    switch (responseType)
                    {
                        case ResponseType.Text:
                            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                            {
                                return reader.ReadToEnd();
                            }
                        case ResponseType.RedirectionURL:
                            return response.ResponseUri.OriginalString;
                        case ResponseType.Headers:
                            StringBuilder sbHeaders = new StringBuilder();
                            foreach (string key in response.Headers.AllKeys)
                            {
                                string value = response.Headers[key];
                                sbHeaders.AppendFormat("{0}: \"{1}\"{2}", key, value, Environment.NewLine);
                            }
                            return sbHeaders.ToString().Trim();
                        case ResponseType.LocationHeader:
                            return response.Headers["Location"];
                    }
                }
            }

            return null;
        }

        protected string CreateQuery(Dictionary<string, string> args)
        {
            if (args != null && args.Count > 0)
            {
                return string.Join("&", args.Select(x => x.Key + "=" + HttpUtility.UrlEncode(x.Value)).ToArray());
            }

            return "";
        }

        protected string CreateQuery(string url, Dictionary<string, string> args)
        {
            string query = CreateQuery(args);

            if (!string.IsNullOrEmpty(query))
            {
                return url + "?" + query;
            }

            return url;
        }

        protected string CreateQuery(NameValueCollection args)
        {
            if (args != null && args.Count > 0)
            {
                List<string> commands = new List<string>();

                foreach (string key in args.AllKeys)
                {
                    string[] values = args.GetValues(key);
                    string isArray = values.Length > 1 ? "[]" : "";

                    commands.AddRange(values.Select(value => key + isArray + "=" + HttpUtility.UrlEncode(value)));
                }

                return string.Join("&", commands.ToArray());
            }

            return "";
        }

        protected string CreateQuery(string url, NameValueCollection args)
        {
            string query = CreateQuery(args);

            if (!string.IsNullOrEmpty(query))
            {
                return url + "?" + query;
            }

            return url;
        }

        protected NameValueCollection CreateAuthenticationHeader(string username, string password)
        {
            string authInfo = username + ":" + password;
            authInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));
            NameValueCollection headers = new NameValueCollection();
            headers["Authorization"] = "Basic " + authInfo;
            return headers;
        }

        private string AddWebError(Exception e)
        {
            string response = null;

            if (Errors != null && e != null)
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("Message:");
                str.AppendLine(e.Message);

                if (e is WebException)
                {
                    try
                    {
                        response = ResponseToString(((WebException)e).Response);

                        if (!string.IsNullOrEmpty(response))
                        {
                            str.AppendLine();
                            str.AppendLine("Response:");
                            str.AppendLine(response);
                        }
                    }
                    catch
                    {
                    }
                }

                str.AppendLine();
                str.AppendLine("StackTrace:");
                str.AppendLine(e.StackTrace);

                string errorText = str.ToString().Trim();
                Errors.Add(errorText);
                //DebugHelper.WriteLine("AddWebError(): " + errorText);
                Console.WriteLine(e.ToString() + "weberror");

            }

            return response;
        }

        #endregion Helper methods
    }
}
