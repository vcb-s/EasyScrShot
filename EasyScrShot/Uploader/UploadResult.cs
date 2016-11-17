using System;
using System.Collections.Generic;
using System.Text;

namespace EasyScrShot.Uploader
{
    public class UploadResult
    {
        public string URL { get; set; }
        public string ThumbnailURL { get; set; }
        public string DeletionURL { get; set; }
        public string ShortenedURL { get; set; }

        private bool isSuccess;

        public bool IsSuccess
        {
            get
            {
                return isSuccess && !string.IsNullOrEmpty(Response);
            }
            set
            {
                isSuccess = value;
            }
        }

        public string Response { get; set; }
        public List<string> Errors { get; set; }
        public bool IsURLExpected { get; set; }

        public bool IsError
        {
            get
            {
                return Errors != null && Errors.Count > 0 && (!IsURLExpected || string.IsNullOrEmpty(URL));
            }
        }

        public UploadResult()
        {
            Errors = new List<string>();
            IsURLExpected = true;
        }

        public UploadResult(string source, string url = null)
            : this()
        {
            Response = source;
            URL = url;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(ShortenedURL))
            {
                return ShortenedURL;
            }

            if (!string.IsNullOrEmpty(URL))
            {
                return URL;
            }

            return "";
        }

        public string ErrorsToString()
        {
            if (IsError)
            {
                return string.Join(Environment.NewLine + Environment.NewLine, Errors.ToArray());
            }

            return null;
        }

        public string ToSummaryString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("URL: " + URL);
            sb.AppendLine("Thumbnail URL: " + ThumbnailURL);
            sb.AppendLine("Shortened URL: " + ShortenedURL);
            sb.AppendLine("Deletion URL: " + DeletionURL);
            return sb.ToString();
        }
    }
}
