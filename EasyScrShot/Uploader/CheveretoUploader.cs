using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upload
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
