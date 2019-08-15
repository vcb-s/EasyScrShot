using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using PNGCompression;

namespace EasyScrShot.HelperLib
{
    public static class PNGHelpers
    {
        public static void MultiThreadPNGCompress(string[] fileList)
        {
            MultiThreadPNGCompress(fileList, Environment.ProcessorCount);
        }

        public static void MultiThreadPNGCompress(string[] fileList, int threadsMaxCount)
        {
            int fileCount = fileList.Length;
            PreCompress();
            if (fileCount < threadsMaxCount)
                threadsMaxCount = fileCount;

            Task[] tasks = new Task[threadsMaxCount];
            Action<object> action = (object obj) =>
                                    {
                                        PNGCompress(obj);
                                    };
            for (int i = 0; i < (fileCount - 1) / threadsMaxCount + 1; i++)
            {
                for (int j = 0; j < threadsMaxCount; j++)
                {
                    int k = i * threadsMaxCount + j;
                    if (k < fileCount)
                    {
                        tasks[j] = new Task(action, fileList[k]);
                        tasks[j].Start();
                    }
                    else
                        break;
                }
                Task.WaitAll(tasks);
            }

            RemoveTemp("optipng.exe");
        }

        private static void PNGCompress(object fileNameObject)
        {
            PNGCompress(fileNameObject.ToString());
        }

        private static void PNGCompress(string fileName)
        {
            PNGCompressor compressor = new PNGCompressor();
            LosslessInputSettings inputSettings = new LosslessInputSettings();
            inputSettings.OptimizationLevel = OptimizationLevel.Level1;
            compressor.CompressImageLossLess(fileName, "temp." + fileName, inputSettings);
            FileInfo file = new FileInfo(fileName);
            file.MoveTo(fileName + ".bak");
            file = new FileInfo("temp." + fileName);
            file.MoveTo(fileName);
        }

        private static void PreCompress()
        {
            Bitmap bitmap = new Bitmap(60, 60);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Aquamarine);
            graphics.Save();
            graphics.Dispose();
            bitmap.Save("pre.bmp", ImageFormat.Bmp);
            PNGCompressor compressor = new PNGCompressor();
            LosslessInputSettings inputSettings = new LosslessInputSettings();
            inputSettings.OptimizationLevel = OptimizationLevel.Level0;
            compressor.CompressImageLossLess("pre.bmp", "pre.png", inputSettings);
            RemoveTemp("pre.bmp");
            RemoveTemp("pre.png");
        }

        private static void RemoveTemp(string tempFile)
        {
            FileInfo file = new FileInfo(tempFile);
            file.Delete();
        }

    }
}
