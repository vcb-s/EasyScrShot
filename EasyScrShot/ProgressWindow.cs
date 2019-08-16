using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using EasyScrShot.HelperLib;

namespace EasyScrShot
{
    public partial class ProgressWindow : Form
    {
        public ProgressWindow(int maximum)
        {
            InitializeComponent();
            this.completedBar.Maximum = maximum;
            this.completedBar.Value = 0;
        }

        // private delegate void SetBarValue(int completed);

        public void SetBar(int completed)
        {
            //if (this.completedBar.InvokeRequired)
            //{
            //    SetBarValue setBarValue = new SetBarValue(SetBar);
            //    this.completedBar.Invoke(setBarValue, new object[] { completed });
            //}
            //else
            //{
            this.completedBar.Value = completed;
            Application.DoEvents();
            //}
        }

        private void ProgressWindow_Load(object sender, EventArgs e)
        {
            this.Owner.Enabled = false;
        }

        private void ProgressWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Enabled = true;
        }
    }
}
