using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    public partial class Settings : Form
    {

        public string settingFile = string.Empty;
        RepoBrowser browseDialog;

        public TreeNode selectedLocNode = null;
        public TreeNode selectedRefNode = null;

        public Settings()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                CaptureSetting.LogFolder = txtLogFolder.Text.Trim();
                CaptureSetting.ENUCaptureFolder = txtENUFolder.Text.Trim();
                CaptureSetting.LocCaptureFolder = txtLocFolder.Text.Trim();
                Global.SelectedLocNode = selectedLocNode;
                Global.SelectedRefNode = selectedRefNode;
                OTPUtility.GetFolderInfo();
                CaptureSetting.IsReadyForReview = true;
                if (chkSaveSxS.Checked)
                {
                    Global.SaveReferenceCaptureSxS = true;
                }
                else
                {
                    Global.SaveReferenceCaptureSxS = false;
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool isValid()
        {
            if (txtLocFolder.Text.Trim() == string.Empty)
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.Settings_ReviewFolderNotSet_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.txtENUFolder.Text.Trim() == string.Empty)
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.Settings_ReferenceFolderNotSet_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.txtLogFolder.Text.Trim() == string.Empty)
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.Settings_LogFolderNotSet_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Directory.Exists(txtLogFolder.Text.Trim()))
            {
                if (MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.Settings_LogFolderNotExist_Message, Global.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Directory.CreateDirectory(txtLogFolder.Text.Trim());

                    }
                    catch //(Exception ex)
                    {
                        MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.Settings_LogFolderCreateFail_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            
               
            return true;
        }

        
        private void btnBrowseLoc_Click(object sender, EventArgs e)
        {
            if (Global.RootItem != null && Global.RootNode != null)
            {
                browseDialog.SelectedNode = selectedLocNode;
                if (this.browseDialog.ShowDialog() == DialogResult.OK)
                {
                    txtLocFolder.Text = browseDialog.SelectedPath;
                    Global.LocTreeItem = browseDialog.SelectedItem;
                    Global.ReviewCaptureFolderInfo = browseDialog.FolderInfo;
                    selectedLocNode = browseDialog.SelectedNode;
                }
            }
            else
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.GitHub_NotConnected_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }     

        private void btnBrowseENU_Click(object sender, EventArgs e)
        {
            if (Global.RootItem != null && Global.RootNode != null)
            {
                browseDialog.SelectedNode = selectedRefNode;
                if (browseDialog.ShowDialog() == DialogResult.OK)
                {
                    txtENUFolder.Text = browseDialog.SelectedPath;
                    Global.ENUTreeItem = browseDialog.SelectedItem;
                    Global.ReferenceCaptureFolderInfo = browseDialog.FolderInfo;
                    selectedRefNode = browseDialog.SelectedNode;
                }
            }
            else
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.GitHub_NotConnected_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowser_Log_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog()==DialogResult.OK)
            {
                txtLogFolder.Text = dialog.SelectedPath;
            }
        }      

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string sessionKey = txtSessionKey.Text.Trim();
            if (string.IsNullOrEmpty(sessionKey))
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.GitHub_EmptyToken_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (WaitForm frm = new WaitForm(Global.Init))
                {
                    frm.Parameter = sessionKey;
                    frm.ShowDialog(this);
                    if (Global.RootNode!=null)
                    {
                        browseDialog = new RepoBrowser();
                    }
                }
                
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            this.txtSessionKey.Text = Global.GithubToken;
            this.txtLocFolder.Text = CaptureSetting.LocCaptureFolder;
            this.txtENUFolder.Text = CaptureSetting.ENUCaptureFolder;
            this.txtLogFolder.Text = CaptureSetting.LogFolder;

            if (Global.RootItem!=null)
            {
                browseDialog = new RepoBrowser();
                selectedLocNode = Global.SelectedLocNode;
                selectedRefNode = Global.SelectedRefNode;
            }
        }

        private void chkShowToken_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowToken.Checked==true)
            {
                txtSessionKey.UseSystemPasswordChar = false;
            }
            else
            {
                txtSessionKey.UseSystemPasswordChar = true;
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    NewIssue dialog = new OTPCaptureViewer.NewIssue();
        //    dialog.ShowDialog();
        //}
    }
}
