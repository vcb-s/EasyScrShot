using System;
using System.IO;
using Microsoft.Win32;

namespace EasyScrShot.HelperLib
{
    public static class Helpers
    {
        public const string Numbers = "0123456789"; // 48 ... 57
        public const string AlphabetCapital = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // 65 ... 90
        public const string Alphabet = "abcdefghijklmnopqrstuvwxyz"; // 97 ... 122
        public const string Alphanumeric = Numbers + AlphabetCapital + Alphabet;
        public const string AlphanumericInverse = Numbers + Alphabet + AlphabetCapital;
        public const string Hexadecimal = Numbers + "ABCDEF";
        public const string URLCharacters = Alphanumeric + "-._~"; // 45 46 95 126
        public const string URLPathCharacters = URLCharacters + "/"; // 47
        public const string ValidURLCharacters = URLPathCharacters + ":?#[]@!$&'()*+,;= ";

        public static readonly string[] ImageFileExtensions = { "jpg", "jpeg", "png", "gif", "bmp", "ico", "tif", "tiff" };
        public static readonly string[] TextFileExtensions = { "txt", "log", "nfo", "c", "cpp", "cc", "cxx", "h", "hpp", "hxx", "cs", "vb", "html", "htm", "xhtml", "xht", "xml", "css", "js", "php", "bat", "java", "lua", "py", "pl", "cfg", "ini", "dart" };
        public static readonly string[] VideoFileExtensions = { "mp4", "webm", "mkv", "avi", "vob", "ogv", "ogg", "mov", "qt", "wmv", "m4p", "m4v", "mpg", "mp2", "mpeg", "mpe", "mpv", "m2v", "m4v", "flv", "f4v" };

        public static readonly Version OSVersion = Environment.OSVersion.Version;



        public static string GetMimeType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return MimeTypes.DefaultMimeType;
            string ext = Path.GetExtension(fileName).ToLower();

            if (string.IsNullOrEmpty(ext)) return MimeTypes.DefaultMimeType;
            string mimeType = MimeTypes.GetMimeType(ext);

            if (!string.IsNullOrEmpty(mimeType))
            {
                return mimeType;
            }

            using (RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(ext))
            {
                if (regKey?.GetValue("Content Type") != null)
                {
                    mimeType = regKey.GetValue("Content Type").ToString();
                    if (!string.IsNullOrEmpty(mimeType))
                    {
                        return mimeType;
                    }
                }
            }
            return MimeTypes.DefaultMimeType;
        }

    }
}
