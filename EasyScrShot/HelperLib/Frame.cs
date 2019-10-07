using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EasyScrShot.HelperLib
{
    public class Frame : IComparable
    {
        public string FrameId { get; private set; }
        public string SrcName { get; private set; }
        public string RipName { get; private set; }

        private bool _resized = false;
        private bool _renamed = false;


        public Frame(string x, string y, string z)
        {
            FrameId = x == "" ? "0" : x;
            SrcName = y;
            RipName = z;
        }

        public int CompareTo(object obj)
        {
            Frame x = obj as Frame;
            if (x == null)
                throw new NullReferenceException(nameof(x));
            if (FrameId.Length < x.FrameId.Length)
                return -1;
            if (FrameId.Length > x.FrameId.Length)
                return 1;
            return string.Compare(FrameId, x.FrameId, StringComparison.Ordinal);
        }

        public void Resize(int width = 384)
        {
            if (_resized)
                throw (new Exception("The frame indexed as "+FrameId + "has already been resized."));
            string path = Utility.CurrentDir + SrcName;
            BitmapImage sourceImg = new BitmapImage();
            sourceImg.BeginInit();
            sourceImg.UriSource = new Uri(path, UriKind.Absolute);
            sourceImg.CacheOption = BitmapCacheOption.OnLoad;
            sourceImg.EndInit();
            int w = sourceImg.PixelWidth, h = sourceImg.PixelHeight;
            BitmapSource thumbnail = new TransformedBitmap(sourceImg, new ScaleTransform((double) width/ w, (double)width / w));
            path = Utility.CurrentDir + FrameId + "s.png";
            using (var file = new FileStream(path, FileMode.OpenOrCreate))
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(thumbnail));
                encoder.Save(file);
            }
            _resized = true;
        }

        public void Rename()
        {
            if (_renamed)
                throw (new Exception("The frame indexed as " + FrameId + "has already been renamed."));
            string oldName = Utility.CurrentDir + SrcName,
                   newName = Utility.CurrentDir + FrameId + ".png";
            File.Move(oldName, newName);
            SrcName = FrameId + ".png";
            oldName = Utility.CurrentDir + RipName;
            newName = Utility.CurrentDir + FrameId + "v.png";
            File.Move(oldName, newName);
            RipName = FrameId + "v.png";
            _renamed = true;
        }
    }
}
