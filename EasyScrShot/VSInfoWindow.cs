using System;
using System.Windows.Forms;
using EasyScrShot.HelperLib;

namespace EasyScrShot
{
    public partial class VSInfoWindow : Form
    {
        private string[] fileList;

        public VSInfoWindow(string[] fileList)
        {
            InitializeComponent();
            this.fileList = fileList;
        }

        public Info result { get; private set; }

        private void YesButton_Click(object sender, EventArgs e)
        {
            int N = int.Parse(BoxN.Text),
                s = int.Parse(Boxs.Text),
                r = int.Parse(Boxr.Text);
            result = new VSInfo(N, s, r);
            this.Close();
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            result = new AVSInfo();
            this.Close();
        }

        private void CompButton_Click(object sender, EventArgs e)
        {
            PNGHelpers.MultiThreadPNGCompress(fileList);
            int N = int.Parse(BoxN.Text),
                s = int.Parse(Boxs.Text),
                r = int.Parse(Boxr.Text);
            result = new VSInfo(N, s, r);
            this.Close();
        }
    }
}
