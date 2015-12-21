using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SubtitlesDownloader.Forms
{
    public partial class MainForm : Form
    {
        private readonly SetupData m_SetupData;
        private readonly FolderBrowserDialog m_FolderDialog = new FolderBrowserDialog();

        public MainForm(SetupData i_SetupData)
        {
            m_SetupData = i_SetupData;
            InitializeComponent();
            RunInBackgroud.Checked = false;
            NoSubFolders.Checked = false;
        }

        private void MainForm_Load(object i_Sender, EventArgs i_E)
        {
            FolderTextBox.Text = m_SetupData.Path;
            m_FolderDialog.SelectedPath = m_SetupData.Path;
            m_FolderDialog.Description = "Choose folder to download subtitles to";
        }

        private void ChooseFolder_Click(object i_Sender, EventArgs i_E)
        {
            if (m_FolderDialog.ShowDialog() == DialogResult.OK)
            {
                m_SetupData.Path = m_FolderDialog.SelectedPath;
                FolderTextBox.Text = m_SetupData.Path;
                m_FolderDialog.SelectedPath = m_SetupData.Path;
            }
        }

        private void RunInBackground_CheckedChanged(object i_Sender, EventArgs i_E)
        {
        }

        private void NoSubFolders_CheckedChanged(object i_Sender, EventArgs i_E)
        {
        }

        private void StartBtn_Click(object i_Sender, EventArgs i_E)
        {
            if (!Directory.Exists(FolderTextBox.Text))
            {
                MessageBox.Show("Invalid path");
                return;
            }


            m_SetupData.Path = FolderTextBox.Text;
            m_SetupData.BackgroundRun = RunInBackgroud.Checked ? SetupData.trueStr : SetupData.falseStr;
            m_SetupData.NoSubFolders = NoSubFolders.Checked ? SetupData.trueStr : SetupData.falseStr;
            m_SetupData.Languages = new List<string> {"hebrew", "english"};
            
            DialogResult = DialogResult.OK;
            
            Close();
        }
    }
}
