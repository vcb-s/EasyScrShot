using EasyScrShot.HelperLib;

namespace EasyScrShot
{
    partial class ProgressWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.completedBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // completedBar
            // 
            this.completedBar.Location = new System.Drawing.Point(12, 12);
            this.completedBar.Name = "completedBar";
            this.completedBar.Size = new System.Drawing.Size(300, 37);
            this.completedBar.TabIndex = 2;
            // 
            // ProgressWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(324, 61);
            this.Controls.Add(this.completedBar);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "ProgressWindow";
            this.Text = "努力压缩中......";
            this.Load += new System.EventHandler(this.ProgressWindow_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProgressWindow_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar completedBar;
    }
}