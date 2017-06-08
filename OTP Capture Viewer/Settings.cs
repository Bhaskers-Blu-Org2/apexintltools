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
                OTPUtility.GetFolderInfo();
                CaptureSetting.IsReadyForReview = true;
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
                RepoBrowser dialog = new OTPCaptureViewer.RepoBrowser();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtLocFolder.Text = dialog.SelectedPath;
                    Global.LocTreeItem = dialog.SelectedItem;
                    Global.ReviewCaptureFolderInfo = dialog.FolderInfo;
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
                RepoBrowser dialog = new OTPCaptureViewer.RepoBrowser();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtENUFolder.Text = dialog.SelectedPath;
                    Global.ENUTreeItem = dialog.SelectedItem;
                    Global.ReferenceCaptureFolderInfo = dialog.FolderInfo;
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
                Global.Init(sessionKey);
                
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    NewIssue dialog = new OTPCaptureViewer.NewIssue();
        //    dialog.ShowDialog();
        //}
    }
}
