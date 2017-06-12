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
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using Telerik.WinForms.Documents.FormatProviders.Html;
using Telerik.WinForms.Documents.Layout;
using Telerik.WinForms.Documents.RichTextBoxCommands;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    public partial class NewIssue : Form
    {
        public NewIssue()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void WireEvents()
        {
            this.toggleButtonBold.ToggleStateChanged += this.toggleButtonBold_ToggleStateChanged;
            this.toggleButtonItalic.ToggleStateChanged += this.toggleButtonItalic_ToggleStateChanged;
            this.toggleButtonUnderline.ToggleStateChanged += this.toggleButtonUnderline_ToggleStateChanged;
           
            this.toggleButtonAlignLeft.ToggleStateChanged += this.toggleButtonAlignLeft_ToggleStateChanged;
            this.toggleButtonAlignCenter.ToggleStateChanged += this.toggleButtonAlignCenter_ToggleStateChanged;
            this.toggleButtonAlignRight.ToggleStateChanged += this.toggleButtonAlignRight_ToggleStateChanged;
            this.toggleButtonJustify.ToggleStateChanged += this.toggleButtonJustify_ToggleStateChanged;
            this.toggleButtonBulletList.ToggleStateChanged += this.toggleButtonBulletList_ToggleStateChanged;
            this.toggleButtonNumberedList.ToggleStateChanged += this.toggleButtonNumberedList_ToggleStateChanged;
           
        }

       

        private void toggleButtonBold_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ToggleBoldCommand command = new ToggleBoldCommand(this.txtDes.RichTextBoxElement.ActiveEditor);
            command.Execute();
        }

        private void toggleButtonItalic_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ToggleItalicCommand command = new ToggleItalicCommand(this.txtDes.RichTextBoxElement.ActiveEditor);
            command.Execute();
        }

        private void toggleButtonUnderline_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ToggleUnderlineCommand command = new ToggleUnderlineCommand(this.txtDes.RichTextBoxElement.ActiveEditor);
            command.Execute();
        }

        private void toggleButtonAlignLeft_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ChangeTextAlignmentCommand command = new ChangeTextAlignmentCommand(this.txtDes.RichTextBoxElement.ActiveEditor);
                command.Execute(RadTextAlignment.Left);
            }
        }

        private void toggleButtonAlignCenter_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ChangeTextAlignmentCommand command = new ChangeTextAlignmentCommand(this.txtDes.RichTextBoxElement.ActiveEditor);
                command.Execute(RadTextAlignment.Center);
            }
        }

        private void toggleButtonAlignRight_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ChangeTextAlignmentCommand command = new ChangeTextAlignmentCommand(this.txtDes.RichTextBoxElement.ActiveEditor);
                command.Execute(RadTextAlignment.Right);
            }
        }

        private void toggleButtonJustify_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ChangeTextAlignmentCommand command = new ChangeTextAlignmentCommand(this.txtDes.RichTextBoxElement.ActiveEditor);
                command.Execute(RadTextAlignment.Justify);
            }
        }

        private void toggleButtonBulletList_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ToggleBulletsCommand command = new ToggleBulletsCommand(this.txtDes.RichTextBoxElement.ActiveEditor);
            command.Execute();
        }

        private void toggleButtonNumberedList_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ToggleNumberedCommand command = new ToggleNumberedCommand(this.txtDes.RichTextBoxElement.ActiveEditor);
            command.Execute();
        }

        public string getHTMLDescription()
        {
            HtmlFormatProvider provider = new HtmlFormatProvider();
            HtmlExportSettings settings = new HtmlExportSettings();
            settings.DocumentExportLevel = DocumentExportLevel.Fragment;
            settings.ExportLocalOrStyleValueSource = false;
            settings.ExportStyleMetadata = false;
            settings.StylesExportMode = StylesExportMode.Inline;
            settings.StyleRepositoryExportMode = StyleRepositoryExportMode.DontExportStyles;
            settings.ExportFontStylesAsTags = true;
            //settings.PropertiesToIgnore["span"].Add("color");
            settings.PropertiesToIgnore["span"].Add("text-decoration");
            //settings.PropertiesToIgnore["span"].Add("font-weight");
            //settings.PropertiesToIgnore["span"].Add("font-style");
            //settings.PropertiesToIgnore["span"].Add("font-family");
            //settings.PropertiesToIgnore["span"].Add("font-size");
            settings.PropertiesToIgnore["span"].Add("dir");

            settings.PropertiesToIgnore["p"].Add("margin-top");
            settings.PropertiesToIgnore["p"].Add("margin-bottom");
            settings.PropertiesToIgnore["p"].Add("margin-left");
            settings.PropertiesToIgnore["p"].Add("margin-right");
            settings.PropertiesToIgnore["p"].Add("line-height");
            settings.PropertiesToIgnore["p"].Add("text-indent");
            settings.PropertiesToIgnore["p"].Add("text-align");
            settings.PropertiesToIgnore["p"].Add("direction");

            provider.ExportSettings = settings;

            return provider.Export(this.txtDes.Document);
        }

        private void NewIssue_Load(object sender, EventArgs e)
        {
            this.WireEvents();
            this.txtDes.ChangeFontSize(14);
            this.txtDes.ChangeFontFamily(new Telerik.WinControls.RichTextEditor.UI.FontFamily("Arial"));
            this.txtDes.ChangeParagraphLineSpacing(0.5);
            this.txtDes.ChangeParagraphAutomaticSpacingAfter(false);
            this.txtDes.ChangeParagraphAutomaticSpacingBefore(false);
            this.txtDes.ChangeParagraphLineSpacingType(Telerik.WinForms.Documents.Model.LineSpacingType.Auto);
            this.txtDes.DocumentInheritsDefaultStyleSettings = true;

        }

        public string CaptureFileName
        {
            get;
            set;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                try
                {
                    Issue createdIssue=OTPUtility.CreateIssue(Global.ReviewCaptureFolderInfo.LanguageShortName,txtTitle.Text, getHTMLDescription(), CaptureFileName);
                    if (createdIssue != null)
                    {
                        Global.RepoIssues.Add(createdIssue);
                        MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.NewIssue_SubmitSuccess_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.NewIssue_SubmitFail_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.NewIssue_SubmitFailError_Message, ex.ToString()), Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool isValid()
        {
            if (txtTitle.Text.Trim()==string.Empty)
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.NewIssue_EmptyTitle_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtDes.Text.Trim()==string.Empty)
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.NewIssue_EmptyDescription_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
