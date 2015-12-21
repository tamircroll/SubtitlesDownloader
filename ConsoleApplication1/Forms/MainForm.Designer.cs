namespace SubtitlesDownloader.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.RunInBackgroud = new System.Windows.Forms.CheckBox();
            this.ChooseFolder = new System.Windows.Forms.Button();
            this.NoSubFolders = new System.Windows.Forms.CheckBox();
            this.StartBtn = new System.Windows.Forms.Button();
            this.FolderTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // RunInBackgroud
            // 
            this.RunInBackgroud.AutoSize = true;
            this.RunInBackgroud.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RunInBackgroud.Location = new System.Drawing.Point(35, 230);
            this.RunInBackgroud.Name = "RunInBackgroud";
            this.RunInBackgroud.Size = new System.Drawing.Size(137, 17);
            this.RunInBackgroud.TabIndex = 0;
            this.RunInBackgroud.Text = "To Run In BackGround";
            this.RunInBackgroud.UseVisualStyleBackColor = true;
            this.RunInBackgroud.CheckedChanged += new System.EventHandler(this.RunInBackground_CheckedChanged);
            // 
            // ChooseFolder
            // 
            this.ChooseFolder.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChooseFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ChooseFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChooseFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ChooseFolder.Location = new System.Drawing.Point(201, 96);
            this.ChooseFolder.Name = "ChooseFolder";
            this.ChooseFolder.Size = new System.Drawing.Size(180, 36);
            this.ChooseFolder.TabIndex = 1;
            this.ChooseFolder.Text = "Choose Folder";
            this.ChooseFolder.UseVisualStyleBackColor = false;
            this.ChooseFolder.Click += new System.EventHandler(this.ChooseFolder_Click);
            // 
            // NoSubFolders
            // 
            this.NoSubFolders.AutoSize = true;
            this.NoSubFolders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NoSubFolders.Location = new System.Drawing.Point(35, 194);
            this.NoSubFolders.Name = "NoSubFolders";
            this.NoSubFolders.Size = new System.Drawing.Size(117, 17);
            this.NoSubFolders.TabIndex = 3;
            this.NoSubFolders.Text = "Without sub-folders";
            this.NoSubFolders.UseVisualStyleBackColor = true;
            this.NoSubFolders.CheckedChanged += new System.EventHandler(this.NoSubFolders_CheckedChanged);
            // 
            // StartBtn
            // 
            this.StartBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.StartBtn.Font = new System.Drawing.Font("Minion Pro", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartBtn.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.StartBtn.Location = new System.Drawing.Point(127, 301);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(312, 55);
            this.StartBtn.TabIndex = 4;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = false;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // FolderTextBox
            // 
            this.FolderTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FolderTextBox.Location = new System.Drawing.Point(12, 50);
            this.FolderTextBox.Name = "FolderTextBox";
            this.FolderTextBox.Size = new System.Drawing.Size(554, 29);
            this.FolderTextBox.TabIndex = 5;
            this.FolderTextBox.Text = "Folder";
            this.FolderTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 368);
            this.Controls.Add(this.FolderTextBox);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.NoSubFolders);
            this.Controls.Add(this.ChooseFolder);
            this.Controls.Add(this.RunInBackgroud);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox RunInBackgroud;
        private System.Windows.Forms.Button ChooseFolder;
        private System.Windows.Forms.CheckBox NoSubFolders;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.TextBox FolderTextBox;
    }
}