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
            this.components = new System.ComponentModel.Container();
            this.InfoBoard = new System.Windows.Forms.RichTextBox();
            this.goButton = new System.Windows.Forms.Button();
            this.holdButton = new System.Windows.Forms.Button();
            this.uploadButton = new System.Windows.Forms.Button();
            this.userMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.outputTypeMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.HTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BBCODEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outputTypeMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // InfoBoard
            // 
            this.InfoBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoBoard.Location = new System.Drawing.Point(12, 13);
            this.InfoBoard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.InfoBoard.Name = "InfoBoard";
            this.InfoBoard.ReadOnly = true;
            this.InfoBoard.Size = new System.Drawing.Size(536, 344);
            this.InfoBoard.TabIndex = 0;
            this.InfoBoard.Text = "";
            // 
            // goButton
            // 
            this.goButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.goButton.Location = new System.Drawing.Point(12, 365);
            this.goButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(87, 33);
            this.goButton.TabIndex = 1;
            this.goButton.Text = "走起";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // holdButton
            // 
            this.holdButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.holdButton.Location = new System.Drawing.Point(461, 365);
            this.holdButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.holdButton.Name = "holdButton";
            this.holdButton.Size = new System.Drawing.Size(87, 33);
            this.holdButton.TabIndex = 2;
            this.holdButton.Text = "停一下";
            this.holdButton.UseVisualStyleBackColor = true;
            this.holdButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.holdButton_MouseUp);
            // 
            // uploadButton
            // 
            this.uploadButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uploadButton.Enabled = false;
            this.uploadButton.Location = new System.Drawing.Point(234, 365);
            this.uploadButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(87, 33);
            this.uploadButton.TabIndex = 3;
            this.uploadButton.Text = "上传";
            this.uploadButton.UseVisualStyleBackColor = true;
            // 
            // userMenuStrip
            // 
            this.userMenuStrip.Name = "userMenuStrip";
            this.userMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // outputTypeMenuStrip
            // 
            this.outputTypeMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HTMLToolStripMenuItem,
            this.BBCODEToolStripMenuItem});
            this.outputTypeMenuStrip.Name = "outputTypeMenuStrip";
            this.outputTypeMenuStrip.Size = new System.Drawing.Size(127, 48);
            this.outputTypeMenuStrip.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.outputTypeMenuStrip_Closed);
            // 
            // HTMLToolStripMenuItem
            // 
            this.HTMLToolStripMenuItem.Checked = true;
            this.HTMLToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HTMLToolStripMenuItem.Name = "HTMLToolStripMenuItem";
            this.HTMLToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.HTMLToolStripMenuItem.Text = "HTML";
            // 
            // BBCODEToolStripMenuItem
            // 
            this.BBCODEToolStripMenuItem.Name = "BBCODEToolStripMenuItem";
            this.BBCODEToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.BBCODEToolStripMenuItem.Text = "BBCODE";
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 409);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.holdButton);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.InfoBoard);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainWindow";
            this.Text = "EasyScrShot 2.1";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragEnter);
            this.outputTypeMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox InfoBoard;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Button holdButton;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.ContextMenuStrip userMenuStrip;
        private System.Windows.Forms.ContextMenuStrip outputTypeMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem HTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BBCODEToolStripMenuItem;
    }
}

