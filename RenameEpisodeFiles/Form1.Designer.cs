namespace RenameEpisodeFiles
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            folderBrowserDialog1 = new FolderBrowserDialog();
            label1 = new Label();
            txtFolderPath = new TextBox();
            label2 = new Label();
            txtFirstEpisode = new TextBox();
            label3 = new Label();
            txtSeasonNumber = new TextBox();
            label4 = new Label();
            txtEpisodeData = new TextBox();
            btnRename = new Button();
            lblErr = new Label();
            label5 = new Label();
            txtShowName = new TextBox();
            btnCleanEpisodeData = new Button();
            btnFindFolder = new Button();
            btnFindEpisodeData = new Button();
            openFileDialog1 = new OpenFileDialog();
            txtCopyFilesTo = new TextBox();
            label6 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            folderBrowserDialog1.InitialDirectory = "E:\\Video\\";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(122, 9);
            label1.Name = "label1";
            label1.Size = new Size(101, 25);
            label1.TabIndex = 0;
            label1.Text = "Folder Path";
            // 
            // txtFolderPath
            // 
            txtFolderPath.AllowDrop = true;
            txtFolderPath.Location = new Point(229, 6);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new Size(510, 31);
            txtFolderPath.TabIndex = 1;
            txtFolderPath.DragDrop += txtFolderPath_DragDrop;
            txtFolderPath.DragEnter += txtFolderPath_DragEnter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 91);
            label2.Name = "label2";
            label2.Size = new Size(211, 25);
            label2.TabIndex = 2;
            label2.Text = "Starting Episode Number";
            // 
            // txtFirstEpisode
            // 
            txtFirstEpisode.Location = new Point(229, 91);
            txtFirstEpisode.Name = "txtFirstEpisode";
            txtFirstEpisode.Size = new Size(65, 31);
            txtFirstEpisode.TabIndex = 3;
            txtFirstEpisode.Text = "1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(83, 52);
            label3.Name = "label3";
            label3.Size = new Size(139, 25);
            label3.TabIndex = 4;
            label3.Text = "Season Number";
            // 
            // txtSeasonNumber
            // 
            txtSeasonNumber.Location = new Point(228, 49);
            txtSeasonNumber.Name = "txtSeasonNumber";
            txtSeasonNumber.Size = new Size(66, 31);
            txtSeasonNumber.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(105, 134);
            label4.Name = "label4";
            label4.Size = new Size(117, 25);
            label4.TabIndex = 6;
            label4.Text = "Episode Data";
            // 
            // txtEpisodeData
            // 
            txtEpisodeData.AllowDrop = true;
            txtEpisodeData.Location = new Point(229, 131);
            txtEpisodeData.Name = "txtEpisodeData";
            txtEpisodeData.Size = new Size(510, 31);
            txtEpisodeData.TabIndex = 4;
            txtEpisodeData.DragDrop += txtEpisodeData_DragDrop;
            txtEpisodeData.DragEnter += txtEpisodeData_DragEnter;
            // 
            // btnRename
            // 
            btnRename.Location = new Point(676, 404);
            btnRename.Name = "btnRename";
            btnRename.Size = new Size(112, 34);
            btnRename.TabIndex = 6;
            btnRename.Text = "Rename Files";
            btnRename.UseVisualStyleBackColor = true;
            btnRename.Click += btnRename_Click;
            // 
            // lblErr
            // 
            lblErr.AutoSize = true;
            lblErr.Location = new Point(229, 409);
            lblErr.Name = "lblErr";
            lblErr.Size = new Size(157, 25);
            lblErr.TabIndex = 9;
            lblErr.Text = "Enter Data to Start";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(114, 181);
            label5.Name = "label5";
            label5.Size = new Size(108, 25);
            label5.TabIndex = 10;
            label5.Text = "Show Name";
            // 
            // txtShowName
            // 
            txtShowName.Location = new Point(229, 178);
            txtShowName.Name = "txtShowName";
            txtShowName.Size = new Size(510, 31);
            txtShowName.TabIndex = 5;
            // 
            // btnCleanEpisodeData
            // 
            btnCleanEpisodeData.Location = new Point(17, 404);
            btnCleanEpisodeData.Name = "btnCleanEpisodeData";
            btnCleanEpisodeData.Size = new Size(206, 34);
            btnCleanEpisodeData.TabIndex = 11;
            btnCleanEpisodeData.Text = "Clean Episode Data";
            btnCleanEpisodeData.UseVisualStyleBackColor = true;
            btnCleanEpisodeData.Click += btnCleanEpisodeData_Click;
            // 
            // btnFindFolder
            // 
            btnFindFolder.Location = new Point(745, 6);
            btnFindFolder.Name = "btnFindFolder";
            btnFindFolder.Size = new Size(43, 34);
            btnFindFolder.TabIndex = 12;
            btnFindFolder.Text = "?";
            btnFindFolder.UseVisualStyleBackColor = true;
            btnFindFolder.Click += btnFindFolder_Click;
            // 
            // btnFindEpisodeData
            // 
            btnFindEpisodeData.Location = new Point(745, 131);
            btnFindEpisodeData.Name = "btnFindEpisodeData";
            btnFindEpisodeData.Size = new Size(43, 34);
            btnFindEpisodeData.TabIndex = 13;
            btnFindEpisodeData.Text = "?";
            btnFindEpisodeData.UseVisualStyleBackColor = true;
            btnFindEpisodeData.Click += button1_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtCopyFilesTo
            // 
            txtCopyFilesTo.AllowDrop = true;
            txtCopyFilesTo.Location = new Point(229, 224);
            txtCopyFilesTo.Name = "txtCopyFilesTo";
            txtCopyFilesTo.Size = new Size(510, 31);
            txtCopyFilesTo.TabIndex = 14;
            txtCopyFilesTo.DragDrop += txtCopyFilesTo_DragDrop;
            txtCopyFilesTo.DragEnter += txtCopyFilesTo_DragEnter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(105, 227);
            label6.Name = "label6";
            label6.Size = new Size(116, 25);
            label6.TabIndex = 15;
            label6.Text = "Copy Files To";
            // 
            // button1
            // 
            button1.Location = new Point(745, 224);
            button1.Name = "button1";
            button1.Size = new Size(43, 34);
            button1.TabIndex = 16;
            button1.Text = "?";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(label6);
            Controls.Add(txtCopyFilesTo);
            Controls.Add(btnFindEpisodeData);
            Controls.Add(btnFindFolder);
            Controls.Add(btnCleanEpisodeData);
            Controls.Add(txtShowName);
            Controls.Add(label5);
            Controls.Add(lblErr);
            Controls.Add(btnRename);
            Controls.Add(txtEpisodeData);
            Controls.Add(label4);
            Controls.Add(txtSeasonNumber);
            Controls.Add(label3);
            Controls.Add(txtFirstEpisode);
            Controls.Add(label2);
            Controls.Add(txtFolderPath);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Rename Episodes";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FolderBrowserDialog folderBrowserDialog1;
        private Label label1;
        private TextBox txtFolderPath;
        private Label label2;
        private TextBox txtFirstEpisode;
        private Label label3;
        private TextBox txtSeasonNumber;
        private Label label4;
        private TextBox txtEpisodeData;
        private Button btnRename;
        private Label lblErr;
        private Label label5;
        private TextBox txtShowName;
        private Button btnCleanEpisodeData;
        private Button btnFindFolder;
        private Button btnFindEpisodeData;
        private OpenFileDialog openFileDialog1;
        private TextBox txtCopyFilesTo;
        private Label label6;
        private Button button1;
    }
}
