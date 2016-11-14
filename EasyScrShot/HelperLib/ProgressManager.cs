using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upload.HelpersLib;

namespace upload
{
    public class ProgressManager
    {
        public long Position { get; private set; }
        public long Length { get; private set; }

        public double Percentage
        {
            get
            {
                return (double)Position / Length * 100;
            }
        }

        public double Speed { get; private set; }

        public TimeSpan Elapsed
        {
            get
            {
                return startTimer.Elapsed;
            }
        }

        public TimeSpan Remaining
        {
            get
            {
                if (Speed > 0)
                {
                    return TimeSpan.FromSeconds((Length - Position) / Speed);
                }

                return TimeSpan.Zero;
            }
        }

        private Stopwatch startTimer = new Stopwatch();
        private Stopwatch smoothTimer = new Stopwatch();
        private int smoothTime = 250;
        private long speedTest;
        private FixedSizedQueue<double> averageSpeed = new FixedSizedQueue<double>(10);

        public ProgressManager(long length)
        {
            Length = length;
            startTimer.Start();
            smoothTimer.Start();
        }

        public bool UpdateProgress(long bytesRead)
        {
            Position += bytesRead;
            speedTest += bytesRead;

            if (Position >= Length)
            {
                startTimer.Stop();

                return true;
            }

            if (smoothTimer.ElapsedMilliseconds > smoothTime)
            {
                averageSpeed.Enqueue(speedTest / smoothTimer.Elapsed.TotalSeconds);

                Speed = averageSpeed.Average();

                speedTest = 0;
                smoothTimer.Reset();
                smoothTimer.Start();

                return true;
            }

            return false;
        }
    }
}
