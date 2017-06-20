using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    public partial class RepoBrowser : Form
    {
        public RepoBrowser()
        {
            InitializeComponent();
            //githubTreeView.Nodes.Clear();
            githubTreeView.Nodes.Add(Global.RootNode.Clone() as TreeNode);
        }

        private void RepoBrowser_Load(object sender, EventArgs e)
        {
            if (githubTreeView.Nodes.Count>0)
            {
                foreach (TreeNode tn in githubTreeView.Nodes)
                {
                    tn.Expand();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        internal string SelectedPath
        {
            get;set;
        }

        internal GithubTreeItem SelectedItem
        {
            get;set;
        }

        internal CaptureFolderInfo FolderInfo
        {
            get;set;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (githubTreeView.SelectedNode==null)
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.RepoBrowser_FolderNotSelected_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                GithubTreeItem item = githubTreeView.SelectedNode.Tag as GithubTreeItem;
                SelectedItem = item;
                SelectedPath = item.Item.Path;
                bool foundInfoFile = false;
                foreach (GithubTreeItem child in item.Children)
                {
                    if (System.IO.Path.GetFileName(child.Item.Name).ToLower()=="info.xml")
                    {
                        foundInfoFile = true;
                        Task<Blob> blobTask = Global.GitHubClient.Git.Blob.Get(Global.RepoId, child.Item.Sha);
                        blobTask.Wait();
                        Blob blob = blobTask.Result;
                        if (blob != null)
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(Encoding.UTF8.GetString(Convert.FromBase64String(blob.Content)));
                            XmlNode productNode = doc.SelectSingleNode("/Product");
                            CaptureFolderInfo info = new CaptureFolderInfo();
                            info.CaptureAmount = int.Parse(productNode.SelectSingleNode("TotalUINumber").InnerText);
                            info.LanguageId = int.Parse(productNode.SelectSingleNode("LanguageID").InnerText);
                            info.LanguageName = productNode.SelectSingleNode("LanguageName").InnerText;
                            info.MilestoneNumber = productNode.SelectSingleNode("MilestoneNumber").InnerText;
                            info.ProductName = productNode.SelectSingleNode("Name").InnerText;
                            info.LanguageShortName = productNode.SelectSingleNode("ShortName").InnerText;
                            FolderInfo = info;
                        }
                    }
                }
                if (!foundInfoFile)
                {
                    MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.RepoBrowser_FolderNotReady_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            
        }
    }
}
