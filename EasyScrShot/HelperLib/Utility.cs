using System;
using System.IO;

namespace EasyScrShot
{
    public enum From {avs, vs};

    public static class Utility
    {

        static Random randGen = new Random(DateTime.Now.Minute * 60 + DateTime.Now.Second);
        static string[] helplessEmotion = { "_(:3 」∠)_", "_(・ω・｣∠)_", "_(:з)∠)_", "_(┐「ε:)_", "_(:3」∠❀", "_(:зゝ∠)_", "_(:3」[＿]", "_(:3」[＿]", "ヾ(:3ﾉｼヾ)ﾉｼ" };
        static string[] happyEmotion = { "٩( ╹▿╹ )۶", "(๑>◡<๑)", "٩(^o^)۶", "┌(^o^)┘", " (ง^o^)", "ヘ( ^o^)ノ", "＼(^_^ )", "(〃⌒▽⌒)〃)" };
        public static string CurrentDir { get; set; } = Directory.GetCurrentDirectory();



        public static string GetHelplessEmotion()
        {
            int index = randGen.Next(helplessEmotion.Length);
            return helplessEmotion[index];
        }

        public static string GetHappyEmotion()
        {
            int index = randGen.Next(happyEmotion.Length);
            return happyEmotion[index];
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
                else
                {
                    if (j == 1000) j = i;
                    if (i - j + 1 > len)
                    {
                        len = i - j + 1;
                        res = filename.Substring(j, len);
                    }
                }

            }
            while (res.Length > 0 && res[0] == '0')
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
