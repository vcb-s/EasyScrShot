namespace EasyScrShot
{
    partial class MainWindow
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
            this.InfoBoard = new System.Windows.Forms.RichTextBox();
            this.goButton = new System.Windows.Forms.Button();
            this.holdButton = new System.Windows.Forms.Button();
            this.uploadButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InfoBoard
            // 
            this.InfoBoard.Location = new System.Drawing.Point(12, 12);
            this.InfoBoard.Name = "InfoBoard";
            this.InfoBoard.ReadOnly = true;
            this.InfoBoard.Size = new System.Drawing.Size(460, 244);
            this.InfoBoard.TabIndex = 0;
            this.InfoBoard.Text = "";
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(44, 275);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 1;
            this.goButton.Text = "走起";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // holdButton
            // 
            this.holdButton.Location = new System.Drawing.Point(363, 275);
            this.holdButton.Name = "holdButton";
            this.holdButton.Size = new System.Drawing.Size(75, 23);
            this.holdButton.TabIndex = 2;
            this.holdButton.Text = "停一下";
            this.holdButton.UseVisualStyleBackColor = true;
            this.holdButton.Click += new System.EventHandler(this.holdButton_Click);
            // 
            // uploadButton
            // 
            this.uploadButton.Enabled = false;
            this.uploadButton.Location = new System.Drawing.Point(205, 275);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(75, 23);
            this.uploadButton.TabIndex = 3;
            this.uploadButton.Text = "上传";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 319);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.holdButton);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.InfoBoard);
            this.Name = "Form1";
            this.Text = "EasyScrShot 1.8";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox InfoBoard;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Button holdButton;
        private System.Windows.Forms.Button uploadButton;
    }
}

