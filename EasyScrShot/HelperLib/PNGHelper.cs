using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;
using LibZopfliSharp;
using LibZopfliSharp.Native;

namespace EasyScrShot.HelperLib
{
    public static class PNGHelper
    {
        private static string[] FileList { get; set; }
        // private static int[] FileSize { get; set; }
        // private static int[] FileSizeCompressed { get; set; }

        public static void PNGCompress()
        {
            int i;
            FileList = Directory.GetFiles(Utility.CurrentDir, "*.png");
            for (i = 0; i < FileList.Length; i++)
            {
                FileList[i] = FileList[i].Remove(0, Utility.CurrentDir.Length);
                ZopfliPNG.compress(FileList[i], Utility.CurrentDir + $"yahaha" + FileList[i]);
            }
        }

    }
}
