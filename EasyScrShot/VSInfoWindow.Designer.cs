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
            this.CompButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Intro
            // 
            this.Intro.AutoSize = true;
            this.Intro.Location = new System.Drawing.Point(35, 25);
            this.Intro.Name = "Intro";
            this.Intro.Size = new System.Drawing.Size(224, 68);
            this.Intro.TabIndex = 0;
            this.Intro.Text = "似乎你的文件是从vsedit截图而来，\n在这里设置一下源和成品截图标号规则。\n假定源的标号符合 k*N+s，\n成品截图的标号为 k*N+r。\n";
            // 
            // NLable
            // 
            this.NLable.AutoSize = true;
            this.NLable.Location = new System.Drawing.Point(83, 121);
            this.NLable.Name = "NLable";
            this.NLable.Size = new System.Drawing.Size(25, 17);
            this.NLable.TabIndex = 1;
            this.NLable.Text = "N: ";
            // 
            // BoxN
            // 
            this.BoxN.Location = new System.Drawing.Point(105, 117);
            this.BoxN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BoxN.Name = "BoxN";
            this.BoxN.Size = new System.Drawing.Size(116, 23);
            this.BoxN.TabIndex = 2;
            this.BoxN.Text = "2";
            // 
            // sLable
            // 
            this.sLable.AutoSize = true;
            this.sLable.Location = new System.Drawing.Point(83, 165);
            this.sLable.Name = "sLable";
            this.sLable.Size = new System.Drawing.Size(21, 17);
            this.sLable.TabIndex = 3;
            this.sLable.Text = "s: ";
            // 
            // Boxs
            // 
            this.Boxs.Location = new System.Drawing.Point(105, 161);
            this.Boxs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Boxs.Name = "Boxs";
            this.Boxs.Size = new System.Drawing.Size(116, 23);
            this.Boxs.TabIndex = 4;
            this.Boxs.Text = "0";
            // 
            // Boxr
            // 
            this.Boxr.Location = new System.Drawing.Point(105, 207);
            this.Boxr.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Boxr.Name = "Boxr";
            this.Boxr.Size = new System.Drawing.Size(116, 23);
            this.Boxr.TabIndex = 6;
            this.Boxr.Text = "1";
            // 
            // rLable
            // 
            this.rLable.AutoSize = true;
            this.rLable.Location = new System.Drawing.Point(83, 212);
            this.rLable.Name = "rLable";
            this.rLable.Size = new System.Drawing.Size(20, 17);
            this.rLable.TabIndex = 5;
            this.rLable.Text = "r: ";
            // 
            // YesButton
            // 
            this.YesButton.Location = new System.Drawing.Point(12, 255);
            this.YesButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.YesButton.Name = "YesButton";
            this.YesButton.Size = new System.Drawing.Size(50, 33);
            this.YesButton.TabIndex = 7;
            this.YesButton.Text = "确定";
            this.YesButton.UseVisualStyleBackColor = true;
            this.YesButton.Click += new System.EventHandler(this.YesButton_Click);
            // 
            // NoButton
            // 
            this.NoButton.Location = new System.Drawing.Point(68, 255);
            this.NoButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NoButton.Name = "NoButton";
            this.NoButton.Size = new System.Drawing.Size(140, 33);
            this.NoButton.TabIndex = 8;
            this.NoButton.Text = "蛤，这是avs截来的啊";
            this.NoButton.UseVisualStyleBackColor = true;
            this.NoButton.Click += new System.EventHandler(this.NoButton_Click);
            // 
            // NoButton
            // 
            this.CompButton.Location = new System.Drawing.Point(214, 255);
            this.CompButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CompButton.Name = "CompButton";
            this.CompButton.Size = new System.Drawing.Size(70, 33);
            this.CompButton.TabIndex = 9;
            this.CompButton.Text = " 压！缩！";
            this.CompButton.UseVisualStyleBackColor = true;
            this.CompButton.Click += new System.EventHandler(this.CompButton_Click);
            // 
            // VSInfoWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 301);
            this.Controls.Add(this.CompButton);
            this.Controls.Add(this.NoButton);
            this.Controls.Add(this.YesButton);
            this.Controls.Add(this.Boxr);
            this.Controls.Add(this.rLable);
            this.Controls.Add(this.Boxs);
            this.Controls.Add(this.sLable);
            this.Controls.Add(this.BoxN);
            this.Controls.Add(this.NLable);
            this.Controls.Add(this.Intro);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(310, 340);
            this.MinimumSize = new System.Drawing.Size(310, 340);
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
        private System.Windows.Forms.Button CompButton;
    }
}