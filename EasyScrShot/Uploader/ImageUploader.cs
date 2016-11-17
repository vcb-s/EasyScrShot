using System.Drawing;
using System.IO;

namespace EasyScrShot.Uploader
{
    public abstract class ImageUploader : FileUploader
    {
        public UploadResult UploadImage(Image image, string fileName)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, image.RawFormat);

                return Upload(stream, fileName);
            }
        }
    }
}
