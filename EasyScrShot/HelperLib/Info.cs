using System;
using System.IO;
using System.Linq;

namespace EasyScrShot.HelperLib
{
    public abstract class Info : ICloneable
    {
        public From from { get; protected set; }
        public abstract bool IsSource(string filename);
        public abstract string GetIndex(string filename);
        public abstract bool IsRipped(string filename, string frameId);

        public virtual int GetTotalPairCount(int totalFileCount)
        {
            return totalFileCount / 2;
        }
        public virtual object Clone()
        {
            var clonedCopy = this.MemberwiseClone() as Info;
            HandelCloned(ref clonedCopy);
            return clonedCopy;
        }
        public virtual void HandelCloned(ref Info clonedCopy) { }
    }

    class AVSInfo : Info
    {
        public string[] Pattern { get; set; } = { "src", "source" };
        public AVSInfo() {
            from = From.avs;
        }

        public override void HandelCloned(ref Info clonedCopy)
        {
            AVSInfo clonedAVSInfo = clonedCopy as AVSInfo;
            if (clonedAVSInfo == null)
                throw new NullReferenceException(nameof(clonedAVSInfo));
            clonedAVSInfo.Pattern = new string[Pattern.Length];
            for (int i = 0; i < Pattern.Length; i++)
                clonedAVSInfo.Pattern[i] = string.Copy(this.Pattern[i]);
        }

        public override bool IsSource(string filename)
        {
            filename = filename.ToLower();
            return Pattern.Any(str => filename.Contains(str));
        }
        public override string GetIndex(string filename)
        {
            return Utility.GetIntStr(filename);
        }
        public override bool IsRipped(string filename, string frameId)
        {
            return (!IsSource(filename)) && (filename.IndexOf(frameId) != -1);
        }
    }
    class VSInfo : Info
    {
        public readonly int N=2, s=0, r=1;

        public VSInfo(int N, int s, int r)
        {
            from = From.vs;
            this.N = N;
            this.s = s;
            this.r = r;
        }

        public override bool IsSource(string filename)
        {
            int frameId = Utility.GetInt(filename);
            return frameId % N == s;
        }
        public override string GetIndex(string filename)
        {
            int frameId = Utility.GetInt(filename);
            return (frameId / N).ToString();
        }
        public override bool IsRipped(string filename, string frameId)
        {
            if (IsSource(filename))
                return false;
            int _frameId = Utility.GetInt(filename);
            return (_frameId / N == int.Parse(frameId)) && (_frameId % N == r);
        }
    }

    class ProcessedInfo : Info
    {
        public ProcessedInfo()
        {
            from = From.processed;
        }

        public override bool IsSource(string filename)
        {
            return Path.GetFileNameWithoutExtension(filename).All(char.IsDigit);
        }

        public override string GetIndex(string filename)
        {
            return Utility.GetIntStr(filename);
        }

        public override bool IsRipped(string filename, string frameId)
        {
            return filename == frameId + "v.png";
        }

        public override int GetTotalPairCount(int totalFileCount)
        {
            return totalFileCount / 3;
        }
    }
}
