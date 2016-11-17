using System;

namespace EasyScrShot.HelperLib
{
    public abstract class Info : ICloneable
    {
        public From from { get; protected set; }
        public abstract bool IsSource(string filename);
        public abstract string GetIndex(string filename);
        public abstract bool IsRipped(string filename, string frameId);
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
        public string[] pattern { get; set; } = { "src", "source" };
        public AVSInfo() {
            from = From.avs;
        }

        public override void HandelCloned(ref Info clonedCopy)
        {
            AVSInfo clonedAVSInfo = clonedCopy as AVSInfo;
            clonedAVSInfo.pattern = new string[this.pattern.Length];
            for (int i = 0; i < this.pattern.Length; i++)
                clonedAVSInfo.pattern[i] = String.Copy(this.pattern[i]);
        }

        public override bool IsSource(string filename)
        {
            filename = filename.ToLower();
            foreach (string str in pattern)
                if (filename.IndexOf(str) != -1)
                    return true;
            return false;
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
        public int N=2, s=0, r=1;
        public VSInfo(int _N, int _s, int _r)
        {
            from = From.vs;
            N = _N;
            s = _s;
            r = _r;
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
}
