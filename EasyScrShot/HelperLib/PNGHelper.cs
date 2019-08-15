using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using PNGCompression;

namespace EasyScrShot.HelperLib
{
    public static class PNGHelper
    {
        private static string[] FileList { get; set; }

        private static Thread thread;

        public static void MultiThreadPNGCompress(string[] fileList)
        {
            int processorCount = Environment.ProcessorCount;

            MultiThreadPNGCompress(fileList, processorCount);
        }

        public static void MultiThreadPNGCompress(string[] fileList, int processorCount)
        {
            int fileCount = fileList.Length;


        }

        private static void PNGCompress(string fileName)
        {
            PNGCompressor compressor = new PNGCompressor();
            LosslessInputSettings inputSettings = new LosslessInputSettings();
            // inputSettings.OptimizationLevel = "3";
            compressor.CompressImageLossLess(fileName, Utility.CurrentDir + $"temp." + fileName, inputSettings);
            RemoveOptiPng();
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
