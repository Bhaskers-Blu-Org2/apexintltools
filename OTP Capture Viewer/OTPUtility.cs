using Microsoft.Win32;
using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    class OTPUtility
    {
        static OTPUtility()
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] key = new byte[24]; // For a 192-bit key
            rng.GetBytes(key);
            EncryptKey = key;

        }
        internal static byte[] EncryptKey
        {
            get;set;
        }

        internal static string Encrypt(string toEncrypt)
        {
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = EncryptKey;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);

        }

        internal static string Decrypt(string cipherString)
        {
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = EncryptKey;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        internal static GithubTreeItem GetGithubTree()
        {
            try
            {
                Task<TreeResponse> task= Global.GitHubClient.Git.Tree.Get(Global.Owner, Global.Name,Global.BranchName);
                //TreeResponse treeResponses = await Global.GitHubClient.Git.Tree.Get(Global.RepoId,Global.ReviewFolderHSA);
                task.Wait();
                TreeResponse treeResponses = task.Result;
                GithubFolderOrFile rootFolder = new OTPCaptureViewer.GithubFolderOrFile();
                rootFolder.Name = Global.BranchName;
                rootFolder.isFolder = true;
                rootFolder.Path = "";
                rootFolder.Sha = treeResponses.Sha;
                GithubTreeItem root = new OTPCaptureViewer.GithubTreeItem(rootFolder);
                createTreeItem(treeResponses,root);
                return root;
            }
            catch
            {
                throw;
            }


        }

        static void createTreeItem(TreeResponse treeResponses,GithubTreeItem parent)
        {
            if (treeResponses != null && treeResponses.Tree.Count > 0)
            {
                for (int i = 0; i < treeResponses.Tree.Count; i++)
                {
                    if (treeResponses.Tree[i].Type == TreeType.Blob)
                    {
                        GithubFolderOrFile file = new GithubFolderOrFile();
                        file.Name = treeResponses.Tree[i].Path;
                        if (isSupportedFile(file.Name))
                        {
                            file.Path = Path.Combine(parent.Item.Path, treeResponses.Tree[i].Path);
                            file.Sha = treeResponses.Tree[i].Sha;
                            parent.Children.Add(new GithubTreeItem(file));
                        }
                    }
                    else if (treeResponses.Tree[i].Type == TreeType.Tree)
                    {
                        GithubFolderOrFile folder = new GithubFolderOrFile();
                        folder.Name= treeResponses.Tree[i].Path;
                        folder.Path = parent.Item.Path + "\\" + folder.Name;
                        folder.isFolder = true;
                        folder.Sha = treeResponses.Tree[i].Sha;
                        GithubTreeItem item = new GithubTreeItem(folder);
                        parent.Children.Add(item);
                        createSubTreeItem(treeResponses.Tree[i],item);
                    }
                    else
                    {

                    }

                    
                }
            }
        }

        private static bool isSupportedFile(string name)
        {
            string lowerName = name.ToLower();
            if (lowerName.EndsWith(".otp") || lowerName.EndsWith(".png") || lowerName.EndsWith(".jpg") || lowerName.EndsWith(".xml"))
            {
                return true;
            }

            return false;
        }

        static void createSubTreeItem(TreeItem parentTreeItem, GithubTreeItem parent)
        {
            if (parentTreeItem != null && !string.IsNullOrEmpty(parentTreeItem.Sha))
            {
                Task<TreeResponse> treeResponse =
                        Global.GitHubClient.Git.Tree.Get(Global.Owner,Global.Name, parentTreeItem.Sha);
                treeResponse.Wait();
                var treeResponses = treeResponse.Result;

                if (treeResponses != null && treeResponses.Tree.Count > 0)
                {
                    for (int i = 0; i < treeResponses.Tree.Count; i++)
                    {
                        if (treeResponses.Tree[i].Type == TreeType.Blob)
                        {
                            GithubFolderOrFile file = new GithubFolderOrFile();
                            file.Name = treeResponses.Tree[i].Path;
                            if (isSupportedFile(file.Name))
                            {
                                file.Path = Path.Combine(parent.Item.Path, treeResponses.Tree[i].Path);
                                file.Sha = treeResponses.Tree[i].Sha;
                                parent.Children.Add(new GithubTreeItem(file));
                            }
                        }
                        else if (treeResponses.Tree[i].Type == TreeType.Tree)
                        {
                            GithubFolderOrFile folder = new GithubFolderOrFile();
                            folder.Name = treeResponses.Tree[i].Path;
                            folder.Path = parent.Item.Path + "\\" + folder.Name;
                            folder.isFolder = true;
                            folder.Sha = treeResponses.Tree[i].Sha;
                            GithubTreeItem item = new GithubTreeItem(folder);
                            parent.Children.Add(item);
                            createSubTreeItem(treeResponses.Tree[i], item);
                        }
                    }
                }
            }
        }

        internal static TreeNode GetGithubFoldersTreeView(GithubTreeItem root)
        {
            TreeNode rootItem = new TreeNode(root.Item.Name);
            rootItem.Tag = root;
            foreach(GithubTreeItem child in root.Children)
            {
                if (child.Item.isFolder)
                {
                    setupSubFolderTreeView(rootItem, child);
                }
            }

            for(int i=rootItem.Nodes.Count-1;i>=0;i--)
            {
                if (rootItem.Nodes[i].Nodes.Count==0)
                {
                    rootItem.Nodes[i].Remove();
                }
            }
            return rootItem;

        }

        static void setupSubFolderTreeView(TreeNode parent, GithubTreeItem item)
        {
            TreeNode node = new TreeNode(item.Item.Name);
            node.Tag = item;
            foreach(GithubTreeItem child in item.Children)
            {
                if (child.Item.isFolder)
                {
                    setupSubFolderTreeView(node, child);
                }
            }
            parent.Nodes.Add(node);
        }

        internal static Issue CreateIssue(string language, string title, string description, string captureName)
        {
            try
            {
                title = string.Format("{0}:{3}:{1} [{2}]",language, title, captureName,Global.ReviewCaptureFolderInfo.ProductName);
                Octokit.NewIssue newIssue = new Octokit.NewIssue(title);
                newIssue.Body = description;
                Task<Issue> createIssueTask = Global.GitHubClient.Issue.Create(Global.Owner, Global.Name, newIssue);
                createIssueTask.Wait();
                Issue createdIssue = createIssueTask.Result;
                return createdIssue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static List<Issue> GetAllIssues()
        {
            RepositoryIssueRequest request = new RepositoryIssueRequest()
            {
                State = ItemStateFilter.All
            };

            var issues = Global.GitHubClient.Issue.GetAllForRepository(Global.RepoId, request).Result;
            return issues.ToList<Issue>();
        }

        public static void SaveSetting(string name,
    object value)
        {
            RegistryKey reg_key =
                Registry.CurrentUser.OpenSubKey(@"Software\Microsoft", true);
            RegistryKey sub_key = reg_key.CreateSubKey(Global.ProductName);
            sub_key.SetValue(name, value);
        }

        // Get a value.
        public static object GetSetting(string name,
            object default_value)
        {
            RegistryKey reg_key =
                Registry.CurrentUser.OpenSubKey(@"Software\Microsoft", true);
            RegistryKey sub_key = reg_key.CreateSubKey(Global.ProductName);
            return sub_key.GetValue(name, default_value);
        }

        internal static void GetFolderInfo()
        {

        }

        internal static string GetUIString(string key)
        {
            ResourceManager rm = new ResourceManager("Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings", Assembly.GetEntryAssembly());
            return rm.GetString(key);
        }

        internal static string GetMessage(string key)
        {
            ResourceManager rm = new ResourceManager("Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages", Assembly.GetEntryAssembly());
            return rm.GetString(key);
        }
    }
}
