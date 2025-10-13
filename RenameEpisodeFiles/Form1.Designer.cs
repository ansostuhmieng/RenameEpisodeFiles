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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            ModeGroup = new GroupBox();
            radioModeAI = new RadioButton();
            radioModeDefault = new RadioButton();
            progressRename = new ProgressBar();
            ModeGroup.SuspendLayout();
            SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            folderBrowserDialog1.InitialDirectory = "E:\\Video\\";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(121, 8);
            label1.Name = "label1";
            label1.Size = new Size(101, 25);
            label1.TabIndex = 0;
            label1.Text = "Folder Path";
            // 
            // txtFolderPath
            // 
            txtFolderPath.AllowDrop = true;
            txtFolderPath.Location = new Point(229, 7);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new Size(510, 31);
            txtFolderPath.TabIndex = 1;
            txtFolderPath.DragDrop += txtFolderPath_DragDrop;
            txtFolderPath.DragEnter += txtFolderPath_DragEnter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 140);
            label2.Name = "label2";
            label2.Size = new Size(211, 25);
            label2.TabIndex = 2;
            label2.Text = "Starting Episode Number";
            // 
            // txtFirstEpisode
            // 
            txtFirstEpisode.Location = new Point(229, 140);
            txtFirstEpisode.Name = "txtFirstEpisode";
            txtFirstEpisode.Size = new Size(65, 31);
            txtFirstEpisode.TabIndex = 3;
            txtFirstEpisode.Text = "1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(83, 100);
            label3.Name = "label3";
            label3.Size = new Size(139, 25);
            label3.TabIndex = 4;
            label3.Text = "Season Number";
            // 
            // txtSeasonNumber
            // 
            txtSeasonNumber.Location = new Point(229, 96);
            txtSeasonNumber.Name = "txtSeasonNumber";
            txtSeasonNumber.Size = new Size(65, 31);
            txtSeasonNumber.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(106, 190);
            label4.Name = "label4";
            label4.Size = new Size(117, 25);
            label4.TabIndex = 6;
            label4.Text = "Episode Data";
            // 
            // txtEpisodeData
            // 
            txtEpisodeData.AllowDrop = true;
            txtEpisodeData.Location = new Point(229, 185);
            txtEpisodeData.Name = "txtEpisodeData";
            txtEpisodeData.Size = new Size(510, 31);
            txtEpisodeData.TabIndex = 4;
            txtEpisodeData.DragDrop += txtEpisodeData_DragDrop;
            txtEpisodeData.DragEnter += txtEpisodeData_DragEnter;
            // 
            // btnRename
            // 
            btnRename.Location = new Point(640, 390);
            btnRename.Name = "btnRename";
            btnRename.Size = new Size(149, 47);
            btnRename.TabIndex = 6;
            btnRename.Text = "Rename Files";
            btnRename.UseVisualStyleBackColor = true;
            btnRename.Click += btnRename_ClickAsync;
            // 
            // lblErr
            // 
            lblErr.AutoSize = true;
            lblErr.Location = new Point(229, 402);
            lblErr.Name = "lblErr";
            lblErr.Size = new Size(157, 25);
            lblErr.TabIndex = 9;
            lblErr.Text = "Enter Data to Start";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(106, 55);
            label5.Name = "label5";
            label5.Size = new Size(108, 25);
            label5.TabIndex = 10;
            label5.Text = "Show Name";
            // 
            // txtShowName
            // 
            txtShowName.Location = new Point(229, 50);
            txtShowName.Name = "txtShowName";
            txtShowName.Size = new Size(510, 31);
            txtShowName.TabIndex = 5;
            // 
            // btnCleanEpisodeData
            // 
            btnCleanEpisodeData.Location = new Point(583, 229);
            btnCleanEpisodeData.Name = "btnCleanEpisodeData";
            btnCleanEpisodeData.Size = new Size(206, 47);
            btnCleanEpisodeData.TabIndex = 11;
            btnCleanEpisodeData.Text = "Clean Episode Data";
            btnCleanEpisodeData.UseVisualStyleBackColor = true;
            btnCleanEpisodeData.Click += btnCleanEpisodeData_Click;
            // 
            // btnFindFolder
            // 
            btnFindFolder.Image = Properties.Resources.matt_icons_folder_yellow_small;
            btnFindFolder.Location = new Point(746, 1);
            btnFindFolder.Name = "btnFindFolder";
            btnFindFolder.Size = new Size(43, 38);
            btnFindFolder.TabIndex = 12;
            btnFindFolder.Text = "?";
            btnFindFolder.UseVisualStyleBackColor = true;
            btnFindFolder.Click += btnFindFolder_Click;
            // 
            // btnFindEpisodeData
            // 
            btnFindEpisodeData.Location = new Point(746, 185);
            btnFindEpisodeData.Name = "btnFindEpisodeData";
            btnFindEpisodeData.Size = new Size(43, 38);
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
            txtCopyFilesTo.Location = new Point(229, 315);
            txtCopyFilesTo.Name = "txtCopyFilesTo";
            txtCopyFilesTo.Size = new Size(510, 31);
            txtCopyFilesTo.TabIndex = 14;
            txtCopyFilesTo.DragDrop += txtCopyFilesTo_DragDrop;
            txtCopyFilesTo.DragEnter += txtCopyFilesTo_DragEnter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(106, 320);
            label6.Name = "label6";
            label6.Size = new Size(116, 25);
            label6.TabIndex = 15;
            label6.Text = "Copy Files To";
            // 
            // button1
            // 
            button1.Location = new Point(746, 315);
            button1.Name = "button1";
            button1.Size = new Size(43, 39);
            button1.TabIndex = 16;
            button1.Text = "?";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // ModeGroup
            // 
            ModeGroup.Controls.Add(radioModeAI);
            ModeGroup.Controls.Add(radioModeDefault);
            ModeGroup.Location = new Point(477, 89);
            ModeGroup.Name = "ModeGroup";
            ModeGroup.Size = new Size(270, 78);
            ModeGroup.TabIndex = 17;
            ModeGroup.TabStop = false;
            ModeGroup.Visible = false;
            // 
            // radioModeAI
            // 
            radioModeAI.AutoSize = true;
            radioModeAI.Location = new Point(155, 30);
            radioModeAI.Name = "radioModeAI";
            radioModeAI.Size = new Size(106, 29);
            radioModeAI.TabIndex = 1;
            radioModeAI.Text = "AI Mode";
            radioModeAI.UseVisualStyleBackColor = true;
            radioModeAI.CheckedChanged += radioModeAI_CheckedChanged;
            // 
            // radioModeDefault
            // 
            radioModeDefault.AutoSize = true;
            radioModeDefault.Checked = true;
            radioModeDefault.Location = new Point(11, 30);
            radioModeDefault.Name = "radioModeDefault";
            radioModeDefault.Size = new Size(146, 29);
            radioModeDefault.TabIndex = 0;
            radioModeDefault.TabStop = true;
            radioModeDefault.Text = "Default Mode";
            radioModeDefault.UseVisualStyleBackColor = true;
            radioModeDefault.CheckedChanged += radioModeDefault_CheckedChanged;
            // 
            // progressRename
            // 
            progressRename.Location = new Point(403, 398);
            progressRename.Name = "progressRename";
            progressRename.Size = new Size(231, 33);
            progressRename.Style = ProgressBarStyle.Marquee;
            progressRename.TabIndex = 18;
            progressRename.Visible = false;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(progressRename);
            Controls.Add(ModeGroup);
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
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Rename Episodes";
            ModeGroup.ResumeLayout(false);
            ModeGroup.PerformLayout();
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
        private GroupBox ModeGroup;
        private RadioButton radioModeDefault;
        private RadioButton radioModeAI;
        private ProgressBar progressRename;
    }
}
