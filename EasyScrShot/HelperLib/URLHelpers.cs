using System;
using System.Linq;

namespace EasyScrShot.HelperLib
{
    public static class URLHelpers
    {
        private static readonly string[] URLPrefixes = new string[] { "http://", "https://", "ftp://", "ftps://", "file://", "//" };

        public static bool HasPrefix(string url)
        {
            return URLPrefixes.Any(x => url.StartsWith(x, StringComparison.InvariantCultureIgnoreCase));
        }

        public static string FixPrefix(string url, string prefix = "http://")
        {
            if (!string.IsNullOrEmpty(url) && !HasPrefix(url))
            {
                return prefix + url;
            }

            return url;
        }

        public static string GetShortURL(string url)
        {
            Uri uri;

            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                string host = uri.Host;

                if (!string.IsNullOrEmpty(host))
                {
                    if (host.StartsWith("www.", StringComparison.InvariantCultureIgnoreCase))
                    {
                        host = host.Substring(4);
                    }

                    return host;
                }
            }

            return url;
        }
    }
}
