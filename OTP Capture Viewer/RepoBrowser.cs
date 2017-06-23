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
            
            
            
        }

        //private void expandNodes(string[] nodes,int index,TreeNode node)
        //{
        //    if (index<nodes.Length)
        //    {
        //        string text = nodes[index];
        //        foreach(TreeNode child in node.Nodes)
        //        {
        //            if (string.Compare(child.Text,text,true)==0)
        //            {
        //                if (index==nodes.Length-1)
        //                {
                            
                            
        //                }
        //                else
        //                {
        //                    //node.Expand();
        //                    expandNodes(nodes, index + 1, child);
        //                }
        //                break;
        //            }
        //        }
        //    }
        //}

        public TreeNode SelectedNode
        {
            get;set;
        }

        private void RepoBrowser_Load(object sender, EventArgs e)
        {
            githubTreeView.Nodes.Add(Global.RootNode);
            if (SelectedNode!=null)
            {
                githubTreeView.CollapseAll();
                SelectedNode.TreeView.SelectedNode = SelectedNode;
                githubTreeView.Select();
            }
            else
            {
                if (githubTreeView.Nodes.Count > 0)
                {
                    foreach (TreeNode tn in githubTreeView.Nodes)
                    {
                        tn.Expand();
                    }
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
                SelectedNode = githubTreeView.SelectedNode;
                GithubTreeItem item = githubTreeView.SelectedNode.Tag as GithubTreeItem;
                SelectedItem = item;
                SelectedPath = item.Item.Path;
                bool foundInfoFile = false;
                foreach (GithubTreeItem child in item.Children)
                {
                    if (System.IO.Path.GetFileName(child.Item.Name).ToLower()==global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.RepoBrowser_InfoFileName)
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

        private void githubTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //TreeNode selectedNode = e.Node;
            //if (selectedNode.Nodes.Count == 1 && selectedNode.Nodes[0].Text == "Opening...")
            //{
            //    selectedNode.Nodes.Clear();
            //    GithubTreeItem selectedItem = selectedNode.Tag as GithubTreeItem;
            //    if (selectedItem!=null)
            //    {
            //        OTPUtility.SetupSubFolderTreeView(selectedNode, selectedItem);
            //        //githubTreeView.Refresh();
                    
            //    }
            //}
        }

        private void githubTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            if (selectedNode.Nodes.Count == 1 && string.Compare(selectedNode.Nodes[0].Text, global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.RepoBrowser_Opening_LblText, true) ==0)
            {
                //selectedNode.Nodes.Clear();
                GithubTreeItem selectedItem = selectedNode.Tag as GithubTreeItem;
                if (selectedItem != null)
                {
                    OTPUtility.SetupSubFolderTreeView(selectedNode, selectedItem);
                    selectedNode.Nodes.RemoveAt(0);
                    //githubTreeView.Refresh();
                    //Global.RootNode = githubTreeView.Nodes[0].Clone() as TreeNode;
                }
            }
            
            
        }

        private void RepoBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            githubTreeView.Nodes.Remove(Global.RootNode);
        }
    }
}
