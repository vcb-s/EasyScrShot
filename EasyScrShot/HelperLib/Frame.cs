using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EasyScrShot.HelperLib
{
    public class Frame : IComparable
    {
        public string frameId { get; set; }
        public string srcName { get; set; }
        public string ripName { get; set; }

        bool resized = false;
        bool renamed = false;


        public Frame(string x, string y, string z)
        {
            frameId = x;
            srcName = y;
            ripName = z;
        }

        public int CompareTo(Object obj)
        {
            Frame x = obj as Frame;
            if (this.frameId.Length < x.frameId.Length)
                return -1;
            if (this.frameId.Length > x.frameId.Length)
                return 1;
            return this.frameId.CompareTo(x.frameId);
        }

        public void Resize(int width = 400)
        {
            if (resized)
                throw (new Exception("The frame indexed as "+frameId + "has already been resized."));
            string path = Utility.currentDir + srcName;
            BitmapImage sourceImg = new BitmapImage();
            sourceImg.BeginInit();
            sourceImg.UriSource = new Uri(path, UriKind.Absolute);
            sourceImg.CacheOption = BitmapCacheOption.OnLoad;
            sourceImg.EndInit();
            int w = sourceImg.PixelWidth, h = sourceImg.PixelHeight;
            BitmapSource thumbnail = new TransformedBitmap(sourceImg, new ScaleTransform((double) width/ w, (double)width / w));
            path = Utility.currentDir + frameId + "s.png";
            using (var file = new FileStream(path, FileMode.OpenOrCreate))
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(thumbnail));
                encoder.Save(file);
            }
            resized = true;
        }

        public void Rename()
        {
            if (renamed)
                throw (new Exception("The frame indexed as " + frameId + "has already been renamed."));
            string oldName = Utility.currentDir + srcName,
                   newName = Utility.currentDir + frameId + ".png";
            File.Move(oldName, newName);
            srcName = frameId + ".png";
            oldName = Utility.currentDir + ripName;
            newName = Utility.currentDir + frameId + "v.png";
            File.Move(oldName, newName);
            ripName = frameId + "v.png";
            renamed = true;
        }
    }
}
