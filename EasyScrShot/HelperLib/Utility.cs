using System;
using System.IO;

namespace EasyScrShot.HelperLib
{
    public enum From {avs, vs, processed};

    public static class Utility
    {
        private static readonly Random RandGen = new Random(DateTime.Now.Minute * 60 + DateTime.Now.Second);
        private static readonly string[] HelplessEmotion = { "_(:3 」∠)_", "_(・ω・｣∠)_", "_(:з)∠)_", "_(┐「ε:)_", "_(:3」∠❀", "_(:зゝ∠)_", "_(:3」[＿]", "_(:3」[＿]", "ヾ(:3ﾉｼヾ)ﾉｼ" };
        private static readonly string[] HappyEmotion = { "٩( ╹▿╹ )۶", "(๑>◡<๑)", "٩(^o^)۶", "┌(^o^)┘", " (ง^o^)", "ヘ( ^o^)ノ", "＼(^_^ )", "(〃⌒▽⌒)〃)" };
        public static string CurrentDir { get; set; } = Directory.GetCurrentDirectory() + "\\";



        public static string GetHelplessEmotion()
        {
            int index = RandGen.Next(HelplessEmotion.Length);
            return HelplessEmotion[index];
        }

        public static string GetHappyEmotion()
        {
            int index = RandGen.Next(HappyEmotion.Length);
            return HappyEmotion[index];
        }


        public static string GetIntStr(string filename)
        {
            string res = "";
            int len = 0, j = 1000;
            for (int i = 0; i < filename.Length; i++)
            {
                if (filename[i] > '9' || filename[i] < '0')
                {
                    j = 1000;
                    continue;
                }
                if (j == 1000) j = i;
                if (i - j + 1 > len)
                {
                    len = i - j + 1;
                    res = filename.Substring(j, len);
                }
            }
            while (res.Length > 1 && res[0] == '0')
                res = res.Substring(1);
            return res;
        }

        public static int GetInt(string filename)
        {
            string res = GetIntStr(filename);
            return int.Parse(res);
        }
    }
}
