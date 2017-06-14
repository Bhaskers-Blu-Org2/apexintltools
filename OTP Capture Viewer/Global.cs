﻿using Octokit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    class Global
    {
        internal static string RepoName= "Microsoft/LinguisticReview";
        internal static string Reference = ConfigurationManager.AppSettings["BranchName"];
        internal static string GithubUrl = "https://github.com/";
        internal static string HeaderName = "OTP_Capture_Viewer";
        internal static string ProductName = "Open Test Platform Capture Viewer - Preview";
        internal static int RepoId = 91628245;
        //internal static string BranchName = "Test";
        internal static string BranchName = ConfigurationManager.AppSettings["BranchName"];
        internal static string Owner = "Microsoft";
        internal static string Name = "LinguisticReview";
        internal static string issueLink = @"https://github.com/" + Owner + "/" + Name + "/issues/";
        internal static string newIssueLink = @"https://github.com/" + Owner + "/" + Name + "/issues/new";
        internal static List<Issue> RepoIssues = new List<Issue>();
        internal static CaptureFolderInfo ReviewCaptureFolderInfo = null;
        internal static CaptureFolderInfo ReferenceCaptureFolderInfo = null;
        internal static bool SaveReferenceCaptureSxS = true;
        //internal static string ReviewFolderPath = @"\Review";
        static string token = "";
        internal static string GithubToken
        {
            get
            {
                if (string.IsNullOrEmpty(token))
                {
                    return "";
                }
                return OTPUtility.Decrypt(token);
            }
            set
            {
                token = OTPUtility.Encrypt(value);
                Uri urlStr = new Uri(GithubUrl);
                Credentials basicAuth = new Credentials(GithubToken);
                gitHubClient = new GitHubClient(new ProductHeaderValue(HeaderName), urlStr);
                gitHubClient.Credentials = basicAuth;
            }
        }

        internal static GithubTreeItem RootItem;

        internal static GithubTreeItem LocTreeItem;

        internal static GithubTreeItem ENUTreeItem;

        internal static TreeNode RootNode;

        static GitHubClient gitHubClient = null;
        internal static GitHubClient GitHubClient
        {
            get
            {
                if (gitHubClient == null)
                {
                    Uri urlStr = new Uri(GithubUrl);
                    Credentials basicAuth = new Credentials(GithubToken);
                    gitHubClient = new GitHubClient(new ProductHeaderValue(HeaderName), urlStr);
                    gitHubClient.Credentials = basicAuth;
                }
                return gitHubClient;
            }

        }

        internal static void Init(string token)
        {
            GithubToken = token;
            try
            {
                RootItem = OTPUtility.GetGithubTree();
                if (RootItem != null)
                {
                    RootNode = OTPUtility.GetGithubFoldersTreeView(RootItem);
                    RepoIssues = OTPUtility.GetAllIssues();
                    MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.GitHub_Connect_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Global.gitHubClient = null;
                    MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.GitHub_ConnectFail_Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                Global.gitHubClient = null;
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.GitHub_ConnectFail_Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    class GithubFolderOrFile
    {
        public string Name;
        public bool isFolder = false;
        public string Sha = string.Empty;
        public string Path;
    }

    class GithubTreeItem
    {
        internal GithubTreeItem(GithubFolderOrFile item)
        {
            this.Item = item;
        }

        public GithubFolderOrFile Item;

        public List<GithubTreeItem> Children = new List<GithubTreeItem>();
    }
}
