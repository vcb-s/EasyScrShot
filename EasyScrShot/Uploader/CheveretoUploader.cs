using EasyScrShot.HelperLib;

namespace EasyScrShot.Uploader
{
    public class CheveretoUploader
    {
        public string UploadURL { get; set; }
        public string APIKey { get; set; }

        public CheveretoUploader()
        {
        }

        public CheveretoUploader(string uploadURL, string apiKey)
        {
            UploadURL = uploadURL;
            APIKey = apiKey;
        }

        public override string ToString()
        {
            return URLHelpers.GetShortURL(UploadURL);
        }
    }
}
