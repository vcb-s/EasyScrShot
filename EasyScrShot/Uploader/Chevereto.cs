using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using upload.BaseUploader;

namespace upload
{
    public class Chevereto : ImageUploader
    {


        public CheveretoUploader Uploader { get; private set; }
        public Chevereto(CheveretoUploader uploader)
        {
            Uploader = uploader;
        }



        public override UploadResult Upload(Stream stream, string fileName)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("key", Uploader.APIKey);
            args.Add("format", "txt");


            string url = URLHelpers.FixPrefix(Uploader.UploadURL);

            UploadResult result = UploadData(stream, url, fileName, "source", args);

            if (result.IsSuccess)
            {

                Console.WriteLine("result :" + result.ToSummaryString());

                //CheveretoResponse response = JsonConvert.DeserializeObject<CheveretoResponse>(result.Response);

                //if (response != null && response.Image != null)
                //{
                //    result.URL = DirectURL ? response.Image.URL : response.Image.URL_Viewer;

                //    if (response.Image.Thumb != null)
                //    {
                //        result.ThumbnailURL = response.Image.Thumb.URL;
                //    }
                //}
            }
            else {
                Console.WriteLine("fail :" + result.ToSummaryString());
            }

            return result;
        }

        private class CheveretoResponse
        {
            public CheveretoImage Image { get; set; }
        }

        private class CheveretoImage
        {
            public string URL { get; set; }
            public string URL_Viewer { get; set; }
            public CheveretoThumb Thumb { get; set; }
        }

        private class CheveretoThumb
        {
            public string URL { get; set; }
        }

        private static void cheveroteTest2()
        {
            // NEED EDIT
            string filename = @"QQ截图.png"; // 服务器上的名字
            string pathname = @"d:\QQ截图20161113011610.png"; // 本地 文件的名字和路径 
            //ImageFormat format = ImageFormat.Png;// 这个好像可以不用写，要是出问题，就写一个自己解析后缀的函数就完了

            CheveretoUploader uploader = new CheveretoUploader("http://img.2222.moe/api/1/upload",
                       "0f653a641610160a23a1f87d364926f9");
            try {
                Chevereto chevereto = new Chevereto(uploader);
                Image image = new Bitmap(pathname, true);

                chevereto.BufferSize = 4096;

                chevereto.UploadImage(image, filename);

            }
            catch(Exception e){
                Console.WriteLine( e.ToString());
            }
        }

        public bool UploadImage(string serverFileName, string filename)
        {
            Image image = new Bitmap(filename, true);
            this.BufferSize = 4096;
            var result = this.UploadImage(image, serverFileName);
            return result.IsSuccess;
        }

    }
}
