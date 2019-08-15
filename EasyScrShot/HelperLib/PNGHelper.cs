using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using PNGCompression;

namespace EasyScrShot.HelperLib
{
    public static class PNGHelper
    {
        private static string[] FileList { get; set; }

        public static void PNGCompress()
        {
            int i;
            FileList = Directory.GetFiles(Utility.CurrentDir, "*.png");
            for (i = 0; i < FileList.Length; i++)
            {
                FileList[i] = FileList[i].Remove(0, Utility.CurrentDir.Length);

                PNGCompressor compressor = new PNGCompressor();
                LosslessInputSettings inputSettings = new LosslessInputSettings();
                inputSettings.OptimizationLevel = "3";
                compressor.CompressImageLossLess(FileList[i], Utility.CurrentDir + $"temp." + FileList[i], inputSettings);
                RemoveOptiPng();
            }
        }

        private static void RemoveOptiPng()
        {
            FileInfo file = new FileInfo("optipng.exe");
            try
            {
                file.Delete();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
