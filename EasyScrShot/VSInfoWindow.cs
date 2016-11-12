using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyScrShot
{
    public partial class VSInfoWindow : Form
    {
        public VSInfoWindow()
        {
            InitializeComponent();
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
    }
}
