//#define USE_JSON
//#define USE_LB
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyScrShot.HelperLib;
using EasyScrShot.Uploader;
using System.Text;

#if USE_JSON
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
#endif

namespace EasyScrShot
{
    public partial class MainWindow : Form
    {
        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assName = new AssemblyName(args.Name).FullName;
            if (args.Name == "PNGCompressor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")
            {
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EasyScrShot.PNGCompressor.dll"))
                {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    return Assembly.Load(bytes);
                }
            }
            throw new DllNotFoundException(assName);
        }

        private string[] Result { get; set; }
        private List<Frame> FList { get; set; }
        private int N { get; set; }
        private Info FromInfo { get; set; }

        public MainWindow()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            InitializeComponent();
#if USE_JSON
            this.Load += MainWindow_Load;
#endif
            LoadFile();
            Text = $"EasyScrShot v{Assembly.GetExecutingAssembly().GetName().Version}";
        }

        private void LoadFile()
        {
            goButton.Enabled = true;
            uploadButton.Enabled = false;
            holdButton.Text = "停一下";
            try
            {
                GetPNG();
                if (Result.Length > 0)
                {
                    DecidingInfo();
                    MatchPNG();
                }
                else
                    goButton.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // processing methods

        private void GetPNG()
        {
            Result = Directory.GetFiles(Utility.CurrentDir, "*.png");
            InfoBoard.Text += $"当前目录有 {Result.Length} 张 PNG 图片。\n";
            for (int i = 0; i < Result.Length; i++)
                Result[i] = Result[i].Remove(0, Utility.CurrentDir.Length);
        }

        private void DecidingInfo()
        {
            if (Result.Length % 2 == 0 && Result.Any(item => item.Contains(".vpy"))) //from vpy
            {
                var popup = new VSInfoWindow(Result);
                popup.ShowDialog();
                FromInfo = (Info)popup.result.Clone();
                popup.Dispose();
            }
            else if ((Result.Length % 2 == 0 && Result.Any(item => item.Contains("src") || item.Contains("source")))) //from avs
            {
                FromInfo = new AVSInfo();
                InfoBoard.Text += "看起来是AVS截取的图。\n";
            }
            else if (Result.Length % 4  == 0)
            {
                FromInfo = new ProcessedInfo();
                InfoBoard.Text += "看起来是已经处理过的图。\n";
                InfoBoard.Text += $"如果之前上传失败了，记得去图床手动清理啊{Utility.GetHelplessEmotion()}\n";
            }
            else
            {
                InfoBoard.Text += $"完全不明白你这些图怎么来的{Utility.GetHelplessEmotion()}\n";
                InfoBoard.Text += $"去群里求助下正确使用姿势？\n";
            }
        }

        private void MatchPNG()
        {
            N = FromInfo.GetTotalPairCount(Result.Length);
            FList = new List<Frame>();
            int k = 0;
            bool flag = true;
            for (int i = 0; i < Result.Length; i++)
                if (FromInfo.IsSource(Result[i]))
                {
                    string id = FromInfo.GetIndex(Result[i]);
                    flag = true;
                    for (int j = 0; j < Result.Length; j++)
                        if (j != i && FromInfo.IsRipped(Result[j], id))
                        {
                            FList.Add(new Frame(id, Result[i], Result[j]));
                            k++;
                            flag = false;
                            break;
                        }
                    if (flag)
                    {
                        InfoBoard.Text += $"{Result[i]} 没找到对应的成品截图, 帧数检测为: {id}\n";
                        break;
                    }
                }
            if (!flag)
                foreach (Frame tmp in FList)
                    InfoBoard.Text += $"{tmp.SrcName} -> {tmp.RipName}\n";
            if (k < N || flag)
            {
                InfoBoard.Text += $"没能找到所有 {N} 组配对...\n";
                N = 0;
                goButton.Enabled = false;
            }
            else if (FromInfo is ProcessedInfo)
            {
                goButton.Enabled = false;
                uploadButton.Enabled = true;
            }
        }

        private enum OutputType
        {
            Html, Bbcode, Markdown
        }

        private OutputType _outputType = OutputType.Html;

        private string GenerateBbcode()
        {
            FList.Sort();
            var ret = new StringBuilder();
            {
                ret.AppendLine("Comparison (right click on the image and open it in a new tab to see the full-size one)");
                ret.AppendLine("Source________________________________________________Encode");
                ret.AppendLine();
                foreach (var img in FList)
                {
                    var src = img.SrcURL;
                    var rip = img.RipURL;
                    var srctbl = img.SrcThumbnailURL;
                    var riptbl = img.RipThumbnailURL;
                    ret.AppendFormat("[URL={2}][IMG]{0}[/IMG][/URL] [URL={3}][IMG]{1}[/IMG][/URL]", srctbl, riptbl, src, rip);
                    ret.AppendLine();
                }
            }
            return ret.ToString();
        }

        private string GenerateHTML()
        {
            FList.Sort();
            var ret = new StringBuilder();

            ret.Append("<p>");
            ret.AppendLine("Comparison (right click on the image and open it in a new tab to see the full-size one)<br/>");
            ret.AppendLine("Source________________________________________________Encode<br/></p>");
            ret.AppendLine();
            ret.Append("<p>");
            foreach (var img in FList)
            {
                var src = img.SrcURL;
                var rip = img.RipURL;
                var srctbl = img.SrcThumbnailURL;
                var riptbl = img.RipThumbnailURL;
                ret.AppendFormat("<a href=\"{2}\"><img src=\"{0}\"></a> <a href=\"{3}\"><img src=\"{1}\"></a><br/><br/>", srctbl, riptbl, src, rip);
                ret.AppendLine();
            }
            ret.Append("</p>");

            return ret.ToString();
        }

        private string GenerateMarkdown()
        {
            FList.Sort();
            var ret = new StringBuilder();

            ret.AppendLine("Comparison (right click on the image and open it in a new tab to see the full-size one)");
            ret.AppendLine("Source________________________________________________Encode");
            ret.AppendLine();
            foreach (var img in FList)
            {
                var src = img.SrcURL;
                var rip = img.RipURL;
                var srctbl = img.SrcThumbnailURL;
                var riptbl = img.RipThumbnailURL;
                ret.AppendFormat("[![]({0})]({2}) [![]({1})]({3})", srctbl, riptbl, src, rip);
                ret.AppendLine();
            }

            return ret.ToString();
        }

        // event definition

        private void goButton_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < N; i++)
                {
                    FList[i].Resize();
                    FList[i].Rename();
                }
                holdButton.Text = "走人";
                MessageBox.Show("你现在可以继续上传图片了", $"搞定{Utility.GetHappyEmotion()}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"把下面这段截图给LP:\n{ex.Message}\n\n{ex.StackTrace}", $"搞不定啊{Utility.GetHelplessEmotion()}");
            }
            goButton.Enabled = false;
            uploadButton.Enabled = true;
        }

        private void holdButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) Close();
#if USE_JSON
            if (e.Button == MouseButtons.Right) userMenuStrip.Show(Cursor.Position);
#else
            if (e.Button == MouseButtons.Right) outputTypeMenuStrip.Show(Cursor.Position);
#endif
        }

#if USE_JSON
        private void MainWindow_Load(object sender, EventArgs e)
        {
            const string configFile = "config.json";
            JsonSchema schema = JsonSchema.Parse(Resources.config_schema);
            try
            {
                string configJson = File.ReadAllText(configFile);
                JObject json = JObject.Parse(configJson);
                IList<string> messages;
                bool valid = json.IsValid(schema, out messages);
                if (!valid) throw new Exception(string.Join("\r\n", messages));

                var accounts = json["accounts"] as JArray;
                if (accounts == null) throw new NullReferenceException(nameof(accounts));
                foreach (var account in accounts)
                {
                    var item = userMenuStrip.Items.Add(account.Value<string>("user"));
                    if (account.Value<bool>("default"))
                    {
                        ((ToolStripMenuItem) item).Checked = true;
                    }
                    item.Tag = new CheveretoUploader(account.Value<string>("url"), account.Value<string>("key"));
                    item.Click += (s, a) =>
                    {
                        foreach (ToolStripMenuItem strip in userMenuStrip.Items)
                            strip.Checked = false;
                        ((ToolStripMenuItem) s).Checked = true;
                    };
                }
            }
            catch (FileNotFoundException exception)
            {
                MessageBox.Show(exception.Message, "未找到配置文件");
                Environment.Exit(-1);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "载入配置文件失败");
                Environment.Exit(-1);
            }
        }
#endif

        private async void uploadButton_Click(object sender, EventArgs e)
        {
#if USE_JSON
            Chevereto imgUploader;
            try
            {
                var uploader = userMenuStrip.Items.Cast<ToolStripMenuItem>().First(item => item.Checked).Tag as CheveretoUploader;
                imgUploader = new Chevereto(uploader);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("请在配置文件中设定默认账号或手动选中账号", Utility.GetHelplessEmotion());
                return;
            }
#elif USE_LB
            var service = new CheveretoUploader("http://img.2222.moe/littlebakas/1/upload/", "0f563a641610160a32a1f87d364269f0");
#else
            var service = new CheveretoUploader("http://img.2222.moe/api/1/upload", "0f653a641610160a23a1f87d364926f9");
#endif
            var spinlock = new object();
            Action<string> appendText = (string s) =>
            {
                lock (spinlock) {
                    if (InfoBoard.InvokeRequired)
                    {
                        Action a = delegate () {
                            InfoBoard.AppendText(s);
                            InfoBoard.SelectionStart = InfoBoard.Text.Length;
                            InfoBoard.ScrollToCaret();
                        };
                        InfoBoard.Invoke(a);
                    }
                    else
                    {
                        InfoBoard.AppendText(s);
                        InfoBoard.SelectionStart = InfoBoard.Text.Length;
                        InfoBoard.ScrollToCaret();
                    }
                }
            };
            appendText($"开始上传，耐心等一会儿......\n");
            Application.DoEvents();
            var tasks = new List<Task<bool>>();
            foreach (Frame f in FList)
            {
                tasks.Add(Task.Run(() =>
                {
                    Chevereto imgUploader = new Chevereto(service);
                    do
                    {
                        appendText($"开始上传第{f.FrameId}帧源图片...\n");
                        f.SrcURL = imgUploader.UploadImage(f.SrcName, Path.Combine(Utility.CurrentDir, f.SrcName));
                    } while (f.SrcURL == "");
                    do
                    {
                        appendText($"开始上传第{f.FrameId}帧压制成品图片...\n");
                        f.RipURL = imgUploader.UploadImage(f.RipName, Path.Combine(Utility.CurrentDir, f.RipName));
                    } while (f.RipURL == "");
                    do
                    {
                        appendText($"开始上传第{f.FrameId}帧源缩略图...\n");
                        f.SrcThumbnailURL = imgUploader.UploadImage(f.FrameId + "s0.png", Path.Combine(Utility.CurrentDir, f.FrameId + "s0.png"));
                    } while (f.SrcThumbnailURL == "");
                    do
                    {
                        appendText($"开始上传第{f.FrameId}帧压制成品缩略图...\n");
                        f.RipThumbnailURL = imgUploader.UploadImage(f.FrameId + "s1.png", Path.Combine(Utility.CurrentDir, f.FrameId + "s1.png"));
                    } while (f.RipThumbnailURL == "");
                    return true;
                }));
                Application.DoEvents();
            }
            var tasksArr = tasks.ToArray();
            while (!Task.WaitAll(tasksArr, 100))
                Application.DoEvents();

            try
            {
                var urltxt = Path.Combine(Utility.CurrentDir, "url.txt");
                using (var file = new StreamWriter(urltxt, false))
                {
                    file.WriteLine(GenerateHTML());
                    file.WriteLine();
                    file.WriteLine(GenerateBbcode());
                    file.WriteLine();
                    file.WriteLine(GenerateMarkdown());
                }
                MessageBox.Show("截图代码已经写在url.txt里", "去丢发布组吧" + Utility.GetHappyEmotion());
            }
            catch (Exception exception)
            {
                MessageBox.Show($"保存文件出现异常: {exception.Message}\n调用栈：{exception.StackTrace}");
            }
            uploadButton.Enabled = false;
        }

        private void MainWindow_DragDrop(object sender, DragEventArgs e)
        {
            var ret = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (ret == null || ret.Length == 0) return;
            if (string.IsNullOrEmpty(ret[0])) return;
            if (!Directory.Exists(ret[0])) return;
            Utility.CurrentDir = ret[0] + "\\";
            InfoBoard.Text += new string('-', 48) + '\n';
            LoadFile();
        }

        private void MainWindow_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void outputTypeMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            switch (_outputType)
            {
                case OutputType.Html:
                    HTMLToolStripMenuItem.Checked = false;
                    BBCODEToolStripMenuItem.Checked = true;
                    _outputType = OutputType.Bbcode;
                    break;
                case OutputType.Bbcode:
                    HTMLToolStripMenuItem.Checked = true;
                    BBCODEToolStripMenuItem.Checked = false;
                    _outputType = OutputType.Html;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
