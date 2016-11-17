using System.IO;
using upload;

namespace EasyScrShot.Uploader
{
    public abstract class GenericUploader : Uploader
    {
        public abstract UploadResult Upload(Stream stream, string fileName);
    }
}
