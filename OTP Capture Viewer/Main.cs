
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Globalization;
using System.Drawing.Imaging;

//using ResourceManager;
using System.Collections;


using System.Reflection;
using Octokit;
using System.Diagnostics;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    public partial class Main : Form
    {
        int originWidth = 0;
        int originHeight = 0;
        string currentFolder;
        string captureSettingsFileName = "settings.xml";

        float zoomScale = 1.0f;//Scale for Zoom in and out
        bool isDrawLine = false;
        bool isStartPen = false;
        bool isStartText = false;
        bool isStartRectangle = false;
        bool isDrawText = false;
        bool isDrawRectangle = false;
        bool isStartSuggestion = false;
        //bool canMove = true;
        Point point = new Point();//Cordinate for mouse move
        Image reviewImage = null;
        Image baseImage = null;
        string reviewfolder = string.Empty;
        string basefolder = string.Empty;
        string wrongFolder = string.Empty;
        string editor = Environment.UserName;
        string userFolder = "";
        string tempFolder = "";
        string resultFile = "";
        string csvResultFile = "";
        string reviewGUID = "";
        bool isPictureEdited = false;
        int currentCaptureIndex = -1;

        KeyboardHook hook;

        Dictionary<string, GithubFolderOrFile> locCaptures = new Dictionary<string, GithubFolderOrFile>();
        Dictionary<string, GithubFolderOrFile> enuCaptures = new Dictionary<string, GithubFolderOrFile>();

        List<string> locCaptureNames = new List<string>();

        UIScreenshot locSS;
        UIScreenshot enuSS;
        LocUIElement currentENUElement;
        LocUIElement currentLocElement;

        XmlDocument resultDoc;
        XmlElement root;

        //string currentKey = "";

        public Main()
        {
            InitializeComponent();
            setToolButtons(false);
            btnStart.Enabled = false;
            btnSetting.Enabled = true;
            this.Text = Global.ProductName;            
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            Settings dialog = new Settings();
            dialog.settingFile = currentFolder + @"\" + captureSettingsFileName;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                btnStart.Enabled = true;
                resetState();
                userFolder = CaptureSetting.LogFolder;
                if (!Directory.Exists(userFolder))
                {
                    Directory.CreateDirectory(userFolder);
                }
                resultFile = userFolder + "\\" + "Result.XML";
                csvResultFile = userFolder + "\\"+ "Result.csv";
                if (File.Exists(resultFile))
                {
                    resultDoc = new XmlDocument();
                    resultDoc.Load(resultFile);
                    root = (XmlElement)resultDoc.FirstChild;
                }
                else
                {
                    resultDoc = new XmlDocument();
                    resultDoc.LoadXml("<Captures></Captures>");
                    root = (XmlElement)resultDoc.FirstChild;
                    resultDoc.Save(resultFile);
                }

                setToolButtons(false);
            }

            if (CaptureSetting.IsReadyForReview)
            {
                btnStart.Enabled = true;
            }
            else
            {
                btnStart.Enabled = false;
            }
        }

        private void resetState()
        {
            currentCaptureIndex = 0;
            isStartPen = false;
            isStartRectangle = false;
            isStartSuggestion = false;
            isStartText = false;
            pictureBox_Review.Image = null;
            pictureBox_Base.Image = null;
            lvIssues.Items.Clear();
            this.Cursor = Cursors.Default;
        }

        private void setToolButtons(bool isStarted)
        {
            this.btnDraw.Enabled = isStarted;
            this.btnNext.Enabled = isStarted;
            this.btnPrev.Enabled = isStarted;
            this.btnRect.Enabled = isStarted;
            this.btnRefresh.Enabled = isStarted;
            this.btnSave.Enabled = isStarted;
            this.btnScaleIn.Enabled = isStarted;
            this.btnScaleOut.Enabled = isStarted;
            this.btnStart.Enabled = !isStarted;
            this.btnText.Enabled = isStarted;
            this.btnIssue.Enabled = isStarted;
        }

        private void createTempFolder()
        {
            reviewGUID = Guid.NewGuid().ToString();

            tempFolder = userFolder + "\\" + reviewGUID;
            try
            {
                if (!Directory.Exists(tempFolder))
                {
                    Directory.CreateDirectory(tempFolder);
                }
            }
            catch
            { }
        }

        

        private void showCapture()
        {
            this.Cursor = Cursors.Default;
            isStartSuggestion = false;
            isStartRectangle = false;
            isStartPen = false;
            isStartText = false;
            isPictureEdited = false;
            zoomScale = 1.0f;
            resetButton();
            reviewImage = null;
            baseImage = null;
            originHeight = 0;
            originWidth = 0;

            if (locCaptures.Count>0)
            {
                if (currentCaptureIndex < locCaptures.Count)
                {
                    string key = locCaptureNames[currentCaptureIndex];
                    GithubFolderOrFile item = locCaptures[key];
                    string fileName = System.IO.Path.GetFileName(item.Name);
                    this.Text = Global.ProductName + " - " + fileName;
                    Task<Blob> blobTask = Global.GitHubClient.Git.Blob.Get(Global.RepoId, item.Sha);
                    blobTask.Wait();
                    Blob blob = blobTask.Result;
                    if (blob != null)
                    {
                        using (Stream ms = new MemoryStream(Convert.FromBase64String(blob.Content)))
                        {
                            if (key.ToLower().EndsWith(".otp"))
                            {
                                locSS = new UIScreenshot(ms);
                                if (locSS != null)
                                {
                                    pictureBox_Review.Image = locSS.UIImage;
                                    reviewImage = locSS.UIImage;
                                    originHeight = reviewImage.Height;
                                    originWidth = reviewImage.Width;
                                    setZoonEnable(true);

                                }
                            }
                            else
                            {
                                Image image = new Bitmap(ms);
                                pictureBox_Review.Image = image;
                                reviewImage = image;
                                originHeight = reviewImage.Height;
                                originWidth = reviewImage.Width;
                                setZoonEnable(true);
                            }

                        }
                    }
                    if (enuCaptures.Keys.Contains(key))
                    {
                        GithubFolderOrFile enuItem = enuCaptures[key];
                        Task<Blob> enuBlobTask = Global.GitHubClient.Git.Blob.Get(Global.RepoId, enuItem.Sha);
                        enuBlobTask.Wait();
                        Blob enuBlob = enuBlobTask.Result;
                        {
                            using (Stream ms =new MemoryStream(Convert.FromBase64String(enuBlob.Content)))
                            {
                                if (key.ToLower().EndsWith(".otp"))
                                {
                                    enuSS = new UIScreenshot(ms);
                                    if (enuSS != null)
                                    {
                                        pictureBox_Base.Image = enuSS.UIImage;
                                        baseImage = enuSS.UIImage;

                                    }
                                }
                                else
                                {
                                    Image image = new Bitmap(ms);
                                    pictureBox_Base.Image = image;
                                    baseImage = image;
                                }
                            }                          
                        }
                    }
                    else
                    {
                        pictureBox_Base.Image = Properties.Resources.NotFound;
                    }

                    if (!key.ToLower().EndsWith(".otp"))
                    {
                        pictureBox_Base.MouseMove -= pictureBox_Base_MouseMove;
                        //copyToolStripMenuItem.Enabled = false;
                        copyToolStripMenuItem1.Enabled = false;
                        //menuTerminologyService.Enabled = false;

                    }
                    else
                    {
                        pictureBox_Base.MouseMove += pictureBox_Base_MouseMove;
                        //copyToolStripMenuItem.Enabled = true;
                        copyToolStripMenuItem1.Enabled = true;
                        //menuTerminologyService.Enabled = true;

                    }
                    updateResult();
                    showIssues(fileName);
                    currentTotalLabel.Text = (currentCaptureIndex + 1).ToString() + "/" + enuCaptures.Count.ToString();
                }
            }
            else
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.LoadCapture_NoScreenshotFound_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }

        private void showIssues(string fileName)
        {
            if (Global.RepoIssues!=null)
            {
                var q = from i in Global.RepoIssues
                        where i.Title.Contains("[" + fileName.ToLower() + "]")
                        select i;
                List<Issue> issues = q.ToList<Issue>();
                lvIssues.Items.Clear();
                foreach(Issue issue in issues)
                {
                    ListViewItem lvi = new ListViewItem(issue.Id.ToString());
                    lvi.SubItems.Add(issue.Number.ToString());
                    lvi.SubItems.Add(issue.Title);
                    lvi.SubItems.Add(issue.User.Login);
                    lvi.SubItems.Add(issue.State.ToString());
                    lvi.SubItems.Add(issue.Milestone != null ? issue.Milestone.Title : "None");
                    lvIssues.Items.Add(lvi);
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            resetState();
            ////Remove code for creating temp folder, as temp folder is no need for github version
            ////createTempFolder();
            

            Task task1 = Task.Factory.StartNew(() =>
             {
                 loadLocCaptures();
             });

            Task task2 = Task.Factory.StartNew(() =>
                 {
                     loadENUCaptures();
                 });

            Task.WaitAll(task1, task2);

            currentCaptureIndex = 0;
            showCapture();

            setToolButtons(true);

            setPreNextButtonEnable();
        }

        private void loadLocCaptures()
        {
            locCaptures = new Dictionary<string, GithubFolderOrFile>();
            locCaptureNames = new List<string>();
            if (Global.LocTreeItem != null)
            {
                //foreach (GithubTreeItem child in Global.LocTreeItem.Children)
                //{
                //    if (!child.Item.isFolder)
                //    {
                //        if (System.IO.Path.GetExtension(child.Item.Name).ToLower() == ".otp" || System.IO.Path.GetExtension(child.Item.Name).ToLower() == ".png" ||
                //            System.IO.Path.GetExtension(child.Item.Name).ToLower() == ".jpg" )
                //        {
                //            locCaptures.Add(child.Item.Name.ToLower(), child.Item);
                //        }
                //    }
                //}
                loadCaptures(Global.LocTreeItem, locCaptures, Global.LocTreeItem.Item.Name);
                locCaptureNames = locCaptures.Keys.ToList<string>();
            }
        }

        private void loadCaptures(GithubTreeItem item,Dictionary<string,GithubFolderOrFile> captures, string folder)
        {
            foreach (GithubTreeItem child in item.Children)
            {
                if (!child.Item.isFolder)
                {
                    if (System.IO.Path.GetExtension(child.Item.Name).ToLower() == ".otp" || System.IO.Path.GetExtension(child.Item.Name).ToLower() == ".png" ||
                        System.IO.Path.GetExtension(child.Item.Name).ToLower() == ".jpg")
                    {
                        captures.Add(folder+"\\"+child.Item.Name.ToLower(), child.Item);
                    }
                }
                else
                {
                    loadCaptures(child,captures,folder+"\\"+child.Item.Name);
                }
            }
        }

        private void loadENUCaptures()
        {
            enuCaptures = new Dictionary<string, GithubFolderOrFile>();
            if (Global.ENUTreeItem != null)
            {
                //foreach (GithubTreeItem child in Global.ENUTreeItem.Children)
                //{
                //    if (!child.Item.isFolder)
                //    {
                //        if (System.IO.Path.GetExtension(child.Item.Name).ToLower() == ".otp" || System.IO.Path.GetExtension(child.Item.Name).ToLower() == ".png" ||
                //            System.IO.Path.GetExtension(child.Item.Name).ToLower() == ".jpg" )
                //        {
                //            enuCaptures.Add(child.Item.Name.ToLower(), child.Item);
                //        }

                //    }
                //}
                loadCaptures(Global.ENUTreeItem, enuCaptures, Global.ENUTreeItem.Item.Name);
            }
        }

        private void setPreNextButtonEnable()
        {
            if (currentCaptureIndex == 0)
            {
                btnPrev.Enabled = false;
            }
            else
            {
                btnPrev.Enabled = true;
            }

            if (currentCaptureIndex < locCaptureNames.Count - 1)
            {
                btnNext.Enabled = true;
            }
            else
            {
                btnNext.Enabled = false;
            }
        }

        private void resetButton()
        {
            if (zoomScale != 1.0f)
            {
                this.btnRect.Enabled = false;
                this.btnDraw.Enabled = false;
                this.btnText.Enabled = false;
            }
            else
            {
                this.btnRect.Enabled = true;
                this.btnDraw.Enabled = true;
                this.btnText.Enabled = true;
                this.btnScaleOut.Enabled = true;
                this.btnScaleIn.Enabled = true;
            }
        }
        private void setZoonEnable(bool enable)
        {
            btnScaleIn.Enabled = enable;
            btnScaleOut.Enabled = enable;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            movePre();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            moveNext();
        }

        private void moveNext()
        {
            if (this.isPictureEdited)
            {
                DialogResult result = MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.EditCapture_SaveConfirmation_Message, Global.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    btnSave_Click(this, null);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            if (currentCaptureIndex < locCaptureNames.Count - 1)
            {
                currentCaptureIndex++;
                showCapture();
            }
            else
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.LoadCapture_LastScreenshot_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            setPreNextButtonEnable();
        }

        private void movePre()
        {
            if (this.isPictureEdited)
            {
                DialogResult result = MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.EditCapture_SaveConfirmation_Message, Global.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    btnSave_Click(this, null);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            if (currentCaptureIndex > 0)
            {
                currentCaptureIndex--;
                showCapture();
            }

            setPreNextButtonEnable();
        }

        private void btnRect_Click(object sender, EventArgs e)
        {
            doDrawRect();
        }

        private void doDrawRect()
        {
            if (null == pictureBox_Review.Image)
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.EditCapture_LoadScreenshotFirst_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!isStartRectangle)
            {
                setControlBackColor(btnRect);
                Size iconSize = SystemInformation.SmallIconSize;
                Bitmap myIcon = new Bitmap(iconSize.Width, iconSize.Height);
                using (Graphics g = Graphics.FromImage(myIcon))
                {
                    g.DrawIcon(Properties.Resources.writer, new Rectangle(Point.Empty, iconSize));
                }
                //Bitmap myIcon = Properties.Resources.writer.ToBitmap();
                this.Cursor = CreateCursor(myIcon, 0, 12);
                setZoonEnable(false);
            }
            else
            {
                setControlBackColor();
                this.Cursor = Cursors.Default;
                setZoonEnable(true);
            }
            isStartRectangle = !isStartRectangle;
            isStartPen = false;
            isStartText = false;
            isStartSuggestion = false;
        }

        private void doDrawLine()
        {
            if (null == pictureBox_Review.Image)
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.EditCapture_LoadScreenshotFirst_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!isStartPen)
            {
                //Modify cursor for Pen
                setControlBackColor(btnDraw);
                Size iconSize = SystemInformation.SmallIconSize;
                Bitmap myIcon = new Bitmap(iconSize.Width, iconSize.Height);
                using (Graphics g = Graphics.FromImage(myIcon))
                {
                    g.DrawIcon(Properties.Resources.pen, new Rectangle(Point.Empty, iconSize));
                }
                //Bitmap myIcon = Properties.Resources.pen.ToBitmap();
                this.Cursor = CreateCursor(myIcon, 0, 12);
                setZoonEnable(false);
            }
            else
            {
                setControlBackColor();
                this.Cursor = Cursors.Default;
                setZoonEnable(true);
            }
            isStartPen = !isStartPen;

            isStartRectangle = false;
            isStartText = false;
            isStartSuggestion = false;
        }

        private void doZoomIn()
        {
            if (null == pictureBox_Review.Image)
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.EditCapture_LoadScreenshotFirst_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            zoomScale -= 0.25f;
            int width = Convert.ToInt32(originWidth * zoomScale);
            int height = Convert.ToInt32(originHeight * zoomScale);
            Image srcImage = new Bitmap(width, height);
            zoomPicture(reviewImage, zoomScale, srcImage, pictureBox_Review);

            if (null != pictureBox_Base.Image)
            {
                //Zoom operation for base picture if existing
                Image baseSrcImage = new Bitmap(Convert.ToInt32(baseImage.Width * zoomScale), Convert.ToInt32(baseImage.Height * zoomScale));
                zoomPicture(baseImage, zoomScale, baseSrcImage, pictureBox_Base);
            }

            if (zoomScale == 0.5f)
            {
                this.btnScaleIn.Enabled = false;
            }
            this.btnScaleOut.Enabled = true;

            resetButton();
        }

        private void doZoomOut()
        {
            if (null == pictureBox_Review.Image)
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.EditCapture_LoadScreenshotFirst_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            zoomScale += 0.25f;
            int width = Convert.ToInt32(originWidth * zoomScale);
            int height = Convert.ToInt32(originHeight * zoomScale);
            Image srcImage = new Bitmap(width, height);
            zoomPicture(reviewImage, zoomScale, srcImage, pictureBox_Review);

            if (null != pictureBox_Base.Image)
            {
                //Zoom operation for base picture if existing
                Image baseSrcImage = new Bitmap(Convert.ToInt32(baseImage.Width * zoomScale), Convert.ToInt32(baseImage.Height * zoomScale));
                zoomPicture(baseImage, zoomScale, baseSrcImage, pictureBox_Base);
            }

            if (zoomScale == 1.5f)
            {
                this.btnScaleOut.Enabled = false;
            }
            this.btnScaleIn.Enabled = true;
            resetButton();
        }

       

        public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IconInfo tmp = new IconInfo();
            Win32API.GetIconInfo(bmp.GetHicon(), ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            return new Cursor(Win32API.CreateIconIndirect(ref tmp));
        }

        public void zoomPicture(Image bitmap, float zoomscale, Image graphicImage, PictureBox picturebox)
        {
            Graphics g = Graphics.FromImage(graphicImage);
            g.ScaleTransform(zoomscale, zoomscale, System.Drawing.Drawing2D.MatrixOrder.Append);
            g.DrawImage(bitmap, 0, 0);
            g.Save();
            picturebox.Image = graphicImage;
            picturebox.Width = graphicImage.Width;
            picturebox.Height = graphicImage.Height;
        } 

        private void btnScaleOut_Click(object sender, EventArgs e)
        {
            doZoomOut();
        }

        private void btnScaleIn_Click(object sender, EventArgs e)
        {
            doZoomIn();
        }

        private void pictureBox_Review_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isStartPen || isStartRectangle || isStartSuggestion || isStartText)
                {
                    isPictureEdited = true;
                }

                if (isStartPen)
                {
                    point = new Point(e.X, e.Y);
                    isDrawLine = true;
                }
                else
                {
                    isDrawLine = false;
                }

                if (isStartText)
                {
                    //Pop up text form to input text
                    TextForm tForm = new TextForm();
                    //canMove = false;
                    if (tForm.ShowDialog() == DialogResult.OK)
                    {
                        isDrawText = tForm.IsDrawText;
                        point = new Point(e.X, e.Y);
                        Graphics g = pictureBox_Review.CreateGraphics();
                        Graphics g1 = Graphics.FromImage(pictureBox_Review.Image);
                        g.DrawString(tForm.TextInput, tForm.TextFont, new SolidBrush(Color.Red), new RectangleF(point, tForm.TextSize));
                        g1.DrawString(tForm.TextInput, tForm.TextFont, new SolidBrush(Color.Red), new RectangleF(point, tForm.TextSize));
                        this.isPictureEdited = true;
                    }
                    //canMove = true;
                }



                if (isStartRectangle)
                {
                    point = new Point(e.X, e.Y);
                    isDrawRectangle = true;
                }
                else
                {
                    isDrawRectangle = false;
                }
            }
        }

        private void pictureBox_Review_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawLine = false;
            Point endPoint = new Point(e.X, e.Y);

            #region Drawing Rectagle
            if (isDrawRectangle)
            {
                Graphics g = pictureBox_Review.CreateGraphics();
                Graphics g1 = Graphics.FromImage(pictureBox_Review.Image);
                Pen pen = new Pen(Color.Red);
                g.DrawRectangle(pen, new Rectangle(point, new Size(endPoint.X - point.X, endPoint.Y - point.Y)));
                g1.DrawRectangle(pen, new Rectangle(point, new Size(endPoint.X - point.X, endPoint.Y - point.Y)));
                isPictureEdited = true;
            }

            isDrawRectangle = false;
            #endregion
        }

        private void pictureBox_Review_MouseMove(object sender, MouseEventArgs e)
        {
            #region Drawing Lines
            if (isDrawLine)
            {
                Graphics g = pictureBox_Review.CreateGraphics();
                Graphics g1 = Graphics.FromImage(pictureBox_Review.Image);
                Pen pen = new Pen(Color.Red);
                g.DrawLine(pen, point, new Point(e.X, e.Y));
                g1.DrawLine(pen, point, new Point(e.X, e.Y));
                point = new Point(e.X, e.Y);

                reviewImage = pictureBox_Review.Image;
                this.isPictureEdited = true;
            }
            else if (isDrawRectangle)
            {
                pictureBox_Review.Refresh();
                Graphics g = pictureBox_Review.CreateGraphics();
                Pen pen = new Pen(Color.Black);
                g.DrawRectangle(pen, new Rectangle(point, new Size(e.X - point.X, e.Y - point.Y)));
                this.isPictureEdited = true;
                reviewImage = pictureBox_Review.Image;
            }
            #endregion
            else if (locSS != null)
            {
                LocUIElement element = locSS.UIElement;
                currentLocElement = element.FindChildFromPoint(new Point(e.X, e.Y));
                if (currentLocElement!=null)
                {
                    

                }


            }
            //else
            //{
            //    menuTerminologyService2.Enabled = false;
            //}
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            doDrawLine();
        }

        private void btnText_Click(object sender, EventArgs e)
        {
            doAddComment();
        }

        private void doAddComment()
        {
            if (null == pictureBox_Review.Image)
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.EditCapture_LoadScreenshotFirst_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!isStartText)
            {
                setControlBackColor(btnText);
                this.Cursor = Cursors.IBeam;
                setZoonEnable(false);
            }
            else
            {
                setControlBackColor();
                this.Cursor = Cursors.Default;
                setZoonEnable(true);
            }
            isStartText = !isStartText;

            isStartRectangle = false;
            isStartPen = false;

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            showCapture();
        }

        private void createResultNote(bool passed)
        {
            if (currentCaptureIndex >= 0)
            {
                string fileName = locCaptureNames[currentCaptureIndex];
                //string language = Global.ReviewCaptureFolderInfo.LanguageId.ToString();
                string language = Global.ReviewCaptureFolderInfo.LanguageName.ToString();
               
                XmlElement existedNode = (XmlElement)root.SelectSingleNode("./Capture[@FileName='" + fileName + "' and @Language='" + language + "']");

                if (existedNode != null)
                {
                    existedNode.SetAttribute("Result", passed ? "Pass" : "Fail");
                    existedNode.SetAttribute("Date", DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                }
                else
                {
                    XmlElement ele = resultDoc.CreateElement("Capture");

                    ele.SetAttribute("FileName", fileName);
                    ele.SetAttribute("Date", DateTime.Now.ToString("MM/dd/yyyy hh:mm"));
                    ele.SetAttribute("Result", passed ? "Pass" : "Fail");
                    ele.SetAttribute("Language", language);
                    root.AppendChild(ele);
                }
                lblResult.Text = passed ? "Pass" : "Fail";
                if (!Directory.Exists(Path.GetDirectoryName(resultFile)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(resultFile));
                }

                resultDoc.Save(resultFile);
                saveResultCSVFile(resultDoc);
            }
        }

        private void updateResult()
        {
            string fileName = locCaptureNames[currentCaptureIndex];
            string language = Global.ReviewCaptureFolderInfo.LanguageName.ToString();
            
            

            XmlElement existedNode = (XmlElement)root.SelectSingleNode("./Capture[@FileName='" + fileName + "' and @Language='" + language + "']");

            if (existedNode != null)
            {
                lblResult.Text = existedNode.Attributes["Result"].Value;
                if(lblResult.Text.ToLower() == global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.Main_CaptureResult_Pass_LabelText)
                {
                    lblResult.ForeColor = Color.Green;
                }
                else if (lblResult.Text.ToLower() == global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.Main_CaptureResult_Fail_LabelText)
                {
                    lblResult.ForeColor = Color.Red;
                }
            }
            else
            {
                lblResult.Text = global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.Main_CaptureResult_NotReviewed_LabelText;
                lblResult.ForeColor = Color.Gray;
            }
        }

        private void saveResultCSVFile(XmlDocument doc)
        {
            XmlNodeList nodes = doc.SelectNodes("Captures/Capture");

            if (nodes.Count > 0)
            {
                try
                {
                    FileStream fs = File.Open(this.csvResultFile, System.IO.FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                    try
                    {
                        StringBuilder header = new StringBuilder();
                        header.Append("FileName");
                        header.Append("\t");
                        header.Append("Language");
                        header.Append("\t");
                        header.Append("Result");
                        header.Append("\t");
                        header.Append("Date");
                        sw.WriteLine(header.ToString());

                        foreach (XmlNode node in nodes)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(node.Attributes["FileName"].Value);
                            sb.Append("\t");
                            sb.Append(node.Attributes["Language"].Value);
                            sb.Append("\t");
                            sb.Append(node.Attributes["Result"].Value);
                            sb.Append("\t");
                            sb.Append(node.Attributes["Date"].Value);
                            sw.WriteLine(sb.ToString());
                        }
                    }
                    catch
                    {
                        MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.ReviewCapture_SaveResultFail_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }
                    finally
                    {
                        sw.Close();
                        fs.Close();
                    }
                }
                catch
                {
                    MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.ReviewCapture_OpenResultFileFail_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox_Good_Click_1(object sender, EventArgs e)
        {
            createResultNote(true);
            moveNext();
        }

        private void pictureBox_Wrong_Click(object sender, EventArgs e)
        {
            if (createIssue())
            {
                //openNewIssuePage();
                createResultNote(false);
                moveNext();
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            
        }

        private void btnBug_Click(object sender, EventArgs e)
        {

        }

        private void openNewIssuePage()
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(Global.newIssueLink);
            p.Start();
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pictureBox_Review.Image != null)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Image (.png)|*.png";

                dialog.FileName = "Updated_" + locCaptureNames[currentCaptureIndex];
                dialog.InitialDirectory = userFolder;
                dialog.RestoreDirectory = true;
                if (!(dialog.FileName.ToString().ToLower().EndsWith(".png")))
                {
                    dialog.FileName += ".png";
                }
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (Global.SaveReferenceCaptureSxS && pictureBox_Base.Image!=null)
                    {
                        Image combinedImage = ImageUtility.CombineImages(new Image[] { pictureBox_Review.Image, pictureBox_Base.Image });
                        combinedImage.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    else
                    {
                        pictureBox_Review.Image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    this.isPictureEdited = false;
                }
            }
        }

        private void pictureBox_Base_MouseMove(object sender, MouseEventArgs e)
        {
            if (enuSS != null)
            {
                LocUIElement element = enuSS.UIElement;
                currentENUElement = element.FindChildFromPoint(new Point(e.X, e.Y));

                //if (currentENUElement != null && currentENUElement.WindowText != "")
                //{
                //    menuTerminologyService.Enabled = true;
                //}
                //else
                //{
                //    menuTerminologyService.Enabled = false;
                //}
            }
            //else
            //{
            //    menuTerminologyService.Enabled = false;
            //}
        }
        //private void searchResourceToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (currentLocElement != null && currentLocElement.WindowText != string.Empty)
        //    {
        //        ResourceSearch dialog = new ResourceSearch();
        //        dialog.Search(currentLocElement.WindowText);
        //    }
        //}

        private void searchResource(string text)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentLocElement != null && currentLocElement.WindowText != string.Empty)
            {
                Clipboard.SetText(currentLocElement.WindowText);
            }
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentENUElement != null && currentENUElement.WindowText != string.Empty)
            {
                Clipboard.SetText(currentENUElement.WindowText);
            }
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            //if (currentLocElement == null || currentLocElement.WindowText == string.Empty)
            //{
            //    copyToolStripMenuItem.Enabled = false;
            //}
            //else
            //{
            //    copyToolStripMenuItem.Enabled = true;
            //}
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (currentENUElement == null || currentENUElement.WindowText == string.Empty)
            {
                copyToolStripMenuItem1.Enabled = false;
            }
            else
            {
                copyToolStripMenuItem1.Enabled = true;
            }
        }

        private void linkToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (lvIssues.SelectedItems != null && lvIssues.SelectedItems.Count > 0 
                && lvIssues.SelectedItems[0].SubItems != null && lvIssues.SelectedItems[0].SubItems.Count > 0)
            {
                string number = lvIssues.SelectedItems[0].SubItems[1].Text;
                string link = Global.issueLink + number;
                Process.Start(link);
            }
            else
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.OpenIssue_MustSelectAnIssue_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void btnSuggestion_Click(object sender, EventArgs e)
        {
            createIssue();
        }

        private bool createIssue()
        {
            NewIssue dialog = new OTPCaptureViewer.NewIssue();
            string key = locCaptureNames[currentCaptureIndex];
            GithubFolderOrFile item = locCaptures[key];
            string fileName = System.IO.Path.GetFileName(item.Name).ToLower();
            dialog.CaptureFileName = fileName;

            if (dialog.ShowDialog()==DialogResult.OK)
            {
                showIssues(fileName);
            }
            else
            {
                return false;
            }
            return true;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        

       

        private void Main_Load(object sender, EventArgs e)
        {
            currentFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //initCaptureSettings();

            hook = new KeyboardHook(new System.Windows.Forms.Keys[] { System.Windows.Forms.Keys.PageDown, System.Windows.Forms.Keys.PageUp });
            hook.OnKeyPressActivity += new System.Windows.Forms.KeyEventHandler(hook_OnKeyPressActivity);
        }

        //private void initCaptureSettings()
        //{
        //    string settingFile = currentFolder + @"\" + captureSettingsFileName;

        //    if (File.Exists(settingFile))
        //    {
        //        try
        //        {
        //            XmlDocument xmlDoc = new XmlDocument();
        //            xmlDoc.Load(settingFile);
        //            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/Settings");

        //            foreach (XmlNode node in nodeList)
        //            {
        //                CaptureSetting.LocCaptureFolder = node.SelectSingleNode("LocCaptureFolder").InnerText;
        //                CaptureSetting.ENUCaptureFolder = node.SelectSingleNode("ENUFolder").InnerText;
        //                CaptureSetting.LogFolder = node.SelectSingleNode("LogFolder").InnerText;
        //                CaptureSetting.IsReadyForReview = bool.Parse(node.SelectSingleNode("IsReadyForReview").InnerText);
                        
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        private void setControlBackColor(ToolStripButton highlightButton = null)
        {
            List<ToolStripButton> buttons = new List<ToolStripButton>();
            buttons.Add(btnRect);
            buttons.Add(btnDraw);
            buttons.Add(btnText);

            foreach (ToolStripButton button in buttons)
            {
                if (button == highlightButton)
                {
                    button.BackColor = Color.Aqua;
                }
                else
                {
                    button.BackColor = Color.Transparent;
                }
            }
        }

        private void thisCursorChanged(object sender, EventArgs e)
        {
            List<ToolStripButton> buttons = new List<ToolStripButton>();
            buttons.Add(btnRect);
            buttons.Add(btnDraw);
            buttons.Add(btnText);

            if (this.Cursor == Cursors.Default)
            {
                foreach (ToolStripButton button in buttons)
                {
                    button.BackColor = Color.Transparent;
                }
            }

        }

        void hook_OnKeyPressActivity(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.PageUp || e.KeyCode == System.Windows.Forms.Keys.Oemcomma)
            {
                movePre();
                e.Handled = true;
            }
            if (e.KeyCode == System.Windows.Forms.Keys.OemPeriod || e.KeyCode == System.Windows.Forms.Keys.PageDown)
            {
                moveNext();
                e.Handled = true;
            }
        }

        private void listViewItemDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvIssues.SelectedItems != null && lvIssues.SelectedItems.Count > 0
                && lvIssues.SelectedItems[0].SubItems != null && lvIssues.SelectedItems[0].SubItems.Count > 0)
            {
                string number = lvIssues.SelectedItems[0].SubItems[1].Text;
                string link = @"https://github.com/Microsoft/linguisticreview/issues/" + number;
                Process.Start(link);
            }
            else
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.OpenIssue_MustSelectAnIssue_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void contextMenuStrip3_Opening(object sender, CancelEventArgs e)
        {
            if (currentLocElement == null)
            {
                copyToolStripMenuItem3.Enabled = false;
            }
            else
            {
                copyToolStripMenuItem3.Enabled = true;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About dialog = new About();
            dialog.ShowDialog();
        }

        private void endUserLisenceAgreementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EulaForm frm = new EulaForm();
            frm.ViewMode = true;
            frm.ShowDialog();
        }
    }


}
