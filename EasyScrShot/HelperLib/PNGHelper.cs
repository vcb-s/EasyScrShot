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
    public static class PNGHelper
    {
        public static void MultiThreadPNGCompress(string[] fileList)
        {
            MultiThreadPNGCompress(fileList, Environment.ProcessorCount);
        }

        public static void MultiThreadPNGCompress(string[] fileList, int processorCount)
        {
            int i, j;
            int fileCount = fileList.Length;
            Task[] tasks = new Task[fileCount];
            Action<object> action = (object obj) =>
                                    {
                                        PNGCompress(obj);
                                    };

            j = 0;
            for (i = 0; i < fileCount; i++)
            {
                tasks[i] = new Task(action, fileList[i]);
                if (i < processorCount)
                {
                    tasks[i].Start();
                }
                else
                {
                    tasks[i - processorCount].Wait();
                    tasks[i].Start();
                }
            }
            for (i = 0; i < fileCount; i++)
            {
                tasks[i].Wait();
            }

            /*
            WaitCallback wait = fileName =>
            {
                PNGCompress(fileName);
            };
            ThreadPool.SetMaxThreads(processorCount, processorCount * 10);
            for (i = 0; i < fileCount; i++)
            {
                ThreadPool.QueueUserWorkItem(wait, fileList[i]);
            }
            ThreadPool.GetAvailableThreads(out i, out j);
            while (i < processorCount)
            {
                ThreadPool.GetAvailableThreads(out i, out j);
                // Thread.Sleep(1000);
            }
            */

            RemoveOptiPng();
        }

        private static void PNGCompress(object fileNameObject)
        {
            PNGCompress(fileNameObject.ToString());
        }

        private static void PNGCompress(string fileName)
        {
            PNGCompressor compressor = new PNGCompressor();
            LosslessInputSettings inputSettings = new LosslessInputSettings();
            // inputSettings.OptimizationLevel = "3";
            compressor.CompressImageLossLess(fileName, Utility.CurrentDir + $"temp." + fileName, inputSettings);
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
