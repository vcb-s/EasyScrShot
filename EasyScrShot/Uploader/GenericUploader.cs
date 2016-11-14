using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upload.BaseUploader
{
    public abstract class GenericUploader : Uploader
    {
        public abstract UploadResult Upload(Stream stream, string fileName);
    }
}
