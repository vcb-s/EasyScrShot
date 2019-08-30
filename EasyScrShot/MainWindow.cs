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
                if (N > 0)
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
            if (Result.Length%2 == 1)
            {
                InfoBoard.Text += $"奇数张图没法继续啊{Utility.GetHelplessEmotion()}\n";
                N = 0;
            }
            else
            {
                for (int i = 0; i < Result.Length; i++)
                    Result[i] = Result[i].Remove(0, Utility.CurrentDir.Length);
                N = Result.Length/2;
            }
        }

        private void DecidingInfo()
        {
            bool isVpy = Result.Any(item => item.Contains(".vpy"));
            if (isVpy) //from vpy
            {
                var popup = new VSInfoWindow(Result);
                popup.ShowDialog();
                FromInfo = (Info) popup.result.Clone();
                popup.Dispose();
            }
            else
            {
                FromInfo = new AVSInfo();
            }
        }

        private void MatchPNG()
        {
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
        }

        private enum OutputType
        {
            Html, Bbcode, Markdown
        }

        private OutputType _outputType = OutputType.Html;

        private string GenerateBbcode()
        {
            FList.Sort();
            string url = "https://img.2222.moe/images/" + DateTime.UtcNow.AddHours(9).ToString("yyyy/MM/dd/");
            var ret = new StringBuilder();
            {
                ret.AppendLine("Comparison (right click on the image and open it in a new tab to see the full-size one)");
                ret.AppendLine("Source________________________________________________Encode");
                ret.AppendLine();
                for (int i = 0; i < N; i++)
                {
                    string src = url + FList[i].SrcName,
                        rip = url + FList[i].RipName,
                        tbl = url + FList[i].FrameId + "s.png";
                    ret.AppendFormat("[URL={1}][IMG]{0}[/IMG][/URL] [URL={2}][IMG]{0}[/IMG][/URL]", tbl, src, rip);
                    ret.AppendLine();
                }
            }
            return ret.ToString();
        }

        private string GenerateHTML()
        {
            FList.Sort();
            var baseUrl = "https://img.2222.moe/images/" + DateTime.UtcNow.AddHours(9).ToString("yyyy/MM/dd/");
            var ret = new StringBuilder();

            ret.Append("<p>");
            ret.AppendLine("Comparison (right click on the image and open it in a new tab to see the full-size one)<br/>");
            ret.AppendLine("Source________________________________________________Encode<br/></p>");
            ret.AppendLine();
            ret.Append("<p>");
            foreach (var img in FList)
            {
                var src = baseUrl + img.SrcName;
                var rip = baseUrl + img.RipName;
                var tbl = baseUrl + img.FrameId + "s.png";
                ret.AppendFormat("<a href=\"{1}\"><img src=\"{0}\"></a> <a href=\"{2}\"><img src=\"{0}\"></a><br/>", tbl, src, rip);
                ret.AppendLine();
            }
            ret.Append("</p>");

            return ret.ToString();
        }

        private string GenerateMarkdown()
        {
            FList.Sort();
            var baseUrl = "https://img.2222.moe/images/" + DateTime.UtcNow.AddHours(9).ToString("yyyy/MM/dd/");
            var ret = new StringBuilder();

            ret.AppendLine("Comparison (right click on the image and open it in a new tab to see the full-size one)");
            ret.AppendLine("Source________________________________________________Encode");
            ret.AppendLine();
            foreach (var img in FList)
            {
                var src = baseUrl + img.SrcName;
                var rip = baseUrl + img.RipName;
                var tbl = baseUrl + img.FrameId + "s.png";
                ret.AppendFormat("[![]({0})]({1}) [![]({0})]({2})", tbl, src, rip);
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
            var lb = new CheveretoUploader("http://img.2222.moe/littlebakas/1/upload/", "0f563a641610160a32a1f87d364269f0");
            Chevereto imgUploader = new Chevereto(lb);
#else
            var vcb_s = new CheveretoUploader("http://img.2222.moe/api/1/upload", "0f653a641610160a23a1f87d364926f9");
            Chevereto imgUploader = new Chevereto(vcb_s);
#endif
            InfoBoard.AppendText($"开始上传，耐心等一会儿......\n");
            Application.DoEvents();
            int count = 0;
            bool flag = false;
            foreach (Frame f in FList)
            {
                flag = await Task.Run(() =>
                {
                    if (!imgUploader.UploadImage(f.SrcName, Path.Combine(Utility.CurrentDir, f.SrcName))) return false;
                    if (!imgUploader.UploadImage(f.RipName, Path.Combine(Utility.CurrentDir, f.RipName))) return false;
                    return imgUploader.UploadImage(f.FrameId + "s.png", Path.Combine(Utility.CurrentDir, f.FrameId + "s.png"));
                });
                if (!flag) break;
                count++;
                InfoBoard.AppendText($"已经上传完第 {count}/{FList.Count} 组截图。\n");
                Application.DoEvents();
            }
            if (!flag)
                MessageBox.Show("自己登录图床把上传一半的删了，然后手动上传所有图吧。同目录下的截图代码应该还可以用。", "上传跪了" + Utility.GetHelplessEmotion());

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
                if (flag) MessageBox.Show("截图代码已经写在url.txt里", "去丢发布组吧" + Utility.GetHappyEmotion());
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
