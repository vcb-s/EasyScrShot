namespace EasyScrShot
{
    partial class VSInfoWindow
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
            this.Intro = new System.Windows.Forms.Label();
            this.NLable = new System.Windows.Forms.Label();
            this.BoxN = new System.Windows.Forms.TextBox();
            this.sLable = new System.Windows.Forms.Label();
            this.Boxs = new System.Windows.Forms.TextBox();
            this.Boxr = new System.Windows.Forms.TextBox();
            this.rLable = new System.Windows.Forms.Label();
            this.YesButton = new System.Windows.Forms.Button();
            this.NoButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Intro
            // 
            this.Intro.AutoSize = true;
            this.Intro.Location = new System.Drawing.Point(25, 20);
            this.Intro.Name = "Intro";
            this.Intro.Size = new System.Drawing.Size(221, 48);
            this.Intro.TabIndex = 0;
            this.Intro.Text = "似乎你的文件是从vsedit截图而来，\n在这里设置一下源和成品截图标号规则。\n假定源的标号符合 k*N+s，\n成品截图的标号为 k*N+r。\n";
            // 
            // NLable
            // 
            this.NLable.AutoSize = true;
            this.NLable.Location = new System.Drawing.Point(74, 96);
            this.NLable.Name = "NLable";
            this.NLable.Size = new System.Drawing.Size(23, 12);
            this.NLable.TabIndex = 1;
            this.NLable.Text = "N: ";
            // 
            // BoxN
            // 
            this.BoxN.Location = new System.Drawing.Point(93, 93);
            this.BoxN.Name = "BoxN";
            this.BoxN.Size = new System.Drawing.Size(100, 21);
            this.BoxN.TabIndex = 2;
            this.BoxN.Text = "2";
            // 
            // sLable
            // 
            this.sLable.AutoSize = true;
            this.sLable.Location = new System.Drawing.Point(74, 127);
            this.sLable.Name = "sLable";
            this.sLable.Size = new System.Drawing.Size(23, 12);
            this.sLable.TabIndex = 3;
            this.sLable.Text = "s: ";
            // 
            // Boxs
            // 
            this.Boxs.Location = new System.Drawing.Point(93, 124);
            this.Boxs.Name = "Boxs";
            this.Boxs.Size = new System.Drawing.Size(100, 21);
            this.Boxs.TabIndex = 4;
            this.Boxs.Text = "0";
            // 
            // Boxr
            // 
            this.Boxr.Location = new System.Drawing.Point(93, 157);
            this.Boxr.Name = "Boxr";
            this.Boxr.Size = new System.Drawing.Size(100, 21);
            this.Boxr.TabIndex = 6;
            this.Boxr.Text = "1";
            // 
            // rLable
            // 
            this.rLable.AutoSize = true;
            this.rLable.Location = new System.Drawing.Point(74, 160);
            this.rLable.Name = "rLable";
            this.rLable.Size = new System.Drawing.Size(23, 12);
            this.rLable.TabIndex = 5;
            this.rLable.Text = "r: ";
            // 
            // YesButton
            // 
            this.YesButton.Location = new System.Drawing.Point(12, 215);
            this.YesButton.Name = "YesButton";
            this.YesButton.Size = new System.Drawing.Size(75, 23);
            this.YesButton.TabIndex = 7;
            this.YesButton.Text = "确定";
            this.YesButton.UseVisualStyleBackColor = true;
            this.YesButton.Click += new System.EventHandler(this.YesButton_Click);
            // 
            // NoButton
            // 
            this.NoButton.Location = new System.Drawing.Point(119, 215);
            this.NoButton.Name = "NoButton";
            this.NoButton.Size = new System.Drawing.Size(153, 23);
            this.NoButton.TabIndex = 8;
            this.NoButton.Text = "蛤，这是avs截来的啊";
            this.NoButton.UseVisualStyleBackColor = true;
            this.NoButton.Click += new System.EventHandler(this.NoButton_Click);
            // 
            // VSInfoWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.NoButton);
            this.Controls.Add(this.YesButton);
            this.Controls.Add(this.Boxr);
            this.Controls.Add(this.rLable);
            this.Controls.Add(this.Boxs);
            this.Controls.Add(this.sLable);
            this.Controls.Add(this.BoxN);
            this.Controls.Add(this.NLable);
            this.Controls.Add(this.Intro);
            this.Name = "VSInfoWindow";
            this.Text = "VSInfoWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Intro;
        private System.Windows.Forms.Label NLable;
        private System.Windows.Forms.TextBox BoxN;
        private System.Windows.Forms.Label sLable;
        private System.Windows.Forms.TextBox Boxs;
        private System.Windows.Forms.TextBox Boxr;
        private System.Windows.Forms.Label rLable;
        private System.Windows.Forms.Button YesButton;
        private System.Windows.Forms.Button NoButton;
    }
}