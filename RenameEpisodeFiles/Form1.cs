using Microsoft.Extensions.Logging;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RenameEpisodeFiles
{
    public partial class Form1 : Form
    {
        private enum RenameMode
        {
            Default,
            AI
        }

        private RenameMode _renameMode = RenameMode.Default;

        public Form1()
        {
            InitializeComponent();

            if (Program.OpenAIService != null)
            {
                ModeGroup.Visible = true;
            }
            
            // Initially hide the undo button
            btnUndo.Visible = false;
        }

        private async void btnRename_ClickAsync(object sender, EventArgs e)
        {

            if (_renameMode == RenameMode.Default)
            {
                #region basic validation
                lblErr.Text = "";

                string folderPath = txtFolderPath.Text;
                if (string.IsNullOrEmpty(folderPath))
                {
                    lblErr.Text = "Fix folder path";
                    return;
                }

                if (!int.TryParse(txtFirstEpisode.Text, out int episodeNumber))
                {
                    lblErr.Text = "Fix episode number";
                    return;
                }

                if (!int.TryParse(txtSeasonNumber.Text, out int seasonNumber))
                {
                    lblErr.Text = "Fix season number";
                    return;
                }

                string dataPath = txtEpisodeData.Text;
                if (string.IsNullOrEmpty(dataPath))
                {
                    lblErr.Text = "Fix data path";
                    return;
                }

                string showName = txtShowName.Text;
                if (string.IsNullOrEmpty(showName))
                {
                    lblErr.Text = "Fix show Name";
                    return;
                }
                #endregion

                int lastEpisode = FileRenamer.RenameEpisodes(folderPath, showName, seasonNumber, episodeNumber, dataPath);

                // Show undo button after successful rename
                btnUndo.Visible = true;

                // copy the files over to the destination after the rename
                // but only if the field is populated
                if (!string.IsNullOrEmpty(txtCopyFilesTo.Text))
                {
                    string destinationDirectory = txtCopyFilesTo.Text;
                    string sourceDirectory = txtFolderPath.Text;

                    Directory.CreateDirectory(destinationDirectory); // Ensure destination exists

                    foreach (var filePath in Directory.GetFiles(sourceDirectory))
                    {
                        string fileName = Path.GetFileName(filePath);
                        string destPath = Path.Combine(destinationDirectory, fileName);
                        File.Copy(filePath, destPath, overwrite: true);
                    }
                }

                // update the episode number so next path can be run easier
                txtFirstEpisode.Text = (lastEpisode + 1).ToString();
                txtFolderPath.Text = "";
            }
            else if (_renameMode == RenameMode.AI)
            {
                lblErr.Text = "";
                string folderPath = txtFolderPath.Text;
                if (string.IsNullOrEmpty(folderPath))
                {
                    lblErr.Text = "Fix folder path";
                    return;
                }
                string showName = txtShowName.Text;
                if (string.IsNullOrEmpty(showName))
                {
                    lblErr.Text = "Fix show Name";
                    return;
                }

                btnRename.Enabled = false; // Disable button to prevent multiple clicks
                progressRename.Visible = true; // Show progress bar
                
                // Call the AI renaming method
                await FileRenamer.RenameEpisodesWithAI(folderPath, showName);
                
                progressRename.Visible = false; // Hide progress bar after operation
                btnRename.Enabled = true; // Re-enable button after operation
                btnUndo.Visible = true; // Show undo button after successful rename
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            lblErr.Text = "";
            try
            {
                btnUndo.Enabled = false;
                if (FileRenamer.UndoLastRename())
                {
                    btnUndo.Visible = false;
                }
                else
                {
                    lblErr.Text = "No rename operation to undo";
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = "Error while undoing rename";
                Program.Logger.LogError(ex, "Error in undo operation");
            }
            finally
            {
                btnUndo.Enabled = true;
            }
        }

        private void btnCleanEpisodeData_Click(object sender, EventArgs e)
        {
            string dataPath = txtEpisodeData.Text;
            if (string.IsNullOrEmpty(dataPath))
            {
                lblErr.Text = "Fix data path";
                return;
            }

            string newPath = FileRenamer.ExtractAndWriteEpisodeNames(dataPath);

            txtEpisodeData.Text = newPath;
        }

        private void btnFindFolder_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                txtFolderPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                txtEpisodeData.Text = openFileDialog1.FileName;
            }
        }

        private void txtEpisodeData_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string filePath in files)
                {
                    txtEpisodeData.Text = filePath;
                }
            }
        }

        private void txtEpisodeData_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void txtFolderPath_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (paths.Length > 0)
                {
                    txtFolderPath.Text = paths[0]; // This could be a file or a folder path
                }
            }
        }

        private void txtFolderPath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                txtCopyFilesTo.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void txtCopyFilesTo_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void txtCopyFilesTo_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (paths.Length > 0)
                {
                    txtCopyFilesTo.Text = paths[0]; // This could be a file or a folder path
                }
            }
        }

        private void radioModeAI_CheckedChanged(object sender, EventArgs e)
        {
            if (radioModeAI.Checked)
            {
                _renameMode = RenameMode.AI;
                btnRename.Text = "Rename with AI";
            }
            else
            {
                _renameMode = RenameMode.Default;
                btnRename.Text = "Rename Files";
            }
        }

        private void radioModeDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (radioModeDefault.Checked)
            {
                _renameMode = RenameMode.Default;
                btnRename.Text = "Rename Files";
            }
            else
            {
                _renameMode = RenameMode.AI;
                btnRename.Text = "Rename with AI";
            }
        }
    }
}
