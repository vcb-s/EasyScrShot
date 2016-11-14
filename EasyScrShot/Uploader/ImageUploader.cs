using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upload.BaseUploader
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
