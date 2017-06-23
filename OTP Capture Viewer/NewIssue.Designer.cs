namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    partial class NewIssue
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDes = new Telerik.WinControls.UI.RadRichTextEditor();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toggleButtonBar = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.toggleButtonBold = new Telerik.WinControls.UI.CommandBarToggleButton();
            this.toggleButtonItalic = new Telerik.WinControls.UI.CommandBarToggleButton();
            this.toggleButtonUnderline = new Telerik.WinControls.UI.CommandBarToggleButton();
            this.commandBarSeparator1 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.toggleButtonAlignLeft = new Telerik.WinControls.UI.CommandBarToggleButton();
            this.toggleButtonAlignCenter = new Telerik.WinControls.UI.CommandBarToggleButton();
            this.toggleButtonAlignRight = new Telerik.WinControls.UI.CommandBarToggleButton();
            this.toggleButtonJustify = new Telerik.WinControls.UI.CommandBarToggleButton();
            this.commandBarSeparator2 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.toggleButtonBulletList = new Telerik.WinControls.UI.CommandBarToggleButton();
            this.toggleButtonNumberedList = new Telerik.WinControls.UI.CommandBarToggleButton();
            this.commandBarRowElement2 = new Telerik.WinControls.UI.CommandBarRowElement();
            ((System.ComponentModel.ISupportInitialize)(this.txtDes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleButtonBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.NewIssue_IssueTitle_LabelText;
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(49, 10);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(596, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.NewIssue_IssueDescription_LabelText;
            // 
            // txtDes
            // 
            this.txtDes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            this.txtDes.IsContextMenuEnabled = false;
            this.txtDes.IsSelectionMiniToolBarEnabled = false;
            this.txtDes.Location = new System.Drawing.Point(19, 96);
            this.txtDes.Name = "txtDes";
            this.txtDes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDes.SelectionFill = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(78)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.txtDes.Size = new System.Drawing.Size(627, 366);
            this.txtDes.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(490, 468);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.NewIssue_SubmitButton_Text;
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(571, 468);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.NewIssue_CancelButton_Text;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // toggleButtonBar
            // 
            this.toggleButtonBar.Location = new System.Drawing.Point(16, 60);
            this.toggleButtonBar.Name = "toggleButtonBar";
            this.toggleButtonBar.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement1});
            this.toggleButtonBar.Size = new System.Drawing.Size(658, 55);
            this.toggleButtonBar.TabIndex = 6;
            // 
            // commandBarRowElement1
            // 
            this.commandBarRowElement1.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement1.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement1});
            // 
            // commandBarStripElement1
            // 
            this.commandBarStripElement1.DisplayName = "Elements";
            this.commandBarStripElement1.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.toggleButtonBold,
            this.toggleButtonItalic,
            this.toggleButtonUnderline,
            this.commandBarSeparator1,
            this.toggleButtonAlignLeft,
            this.toggleButtonAlignCenter,
            this.toggleButtonAlignRight,
            this.toggleButtonJustify,
            this.commandBarSeparator2,
            this.toggleButtonBulletList,
            this.toggleButtonNumberedList});
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            // 
            // toggleButtonBold
            // 
            this.toggleButtonBold.DisplayName = "Bold";
            this.toggleButtonBold.Image = global::Microsoft.SQL.Loc.OTPCaptureViewer.Properties.Resources.Bold;
            this.toggleButtonBold.Name = "toggleButtonBold";
            this.toggleButtonBold.Text = "";
            // 
            // toggleButtonItalic
            // 
            this.toggleButtonItalic.DisplayName = "Italic";
            this.toggleButtonItalic.Image = global::Microsoft.SQL.Loc.OTPCaptureViewer.Properties.Resources.Italic;
            this.toggleButtonItalic.Name = "toggleButtonItalic";
            this.toggleButtonItalic.Text = "";
            // 
            // toggleButtonUnderline
            // 
            this.toggleButtonUnderline.DisplayName = "Underline";
            this.toggleButtonUnderline.Image = global::Microsoft.SQL.Loc.OTPCaptureViewer.Properties.Resources.Underline;
            this.toggleButtonUnderline.Name = "toggleButtonUnderline";
            this.toggleButtonUnderline.Text = "";
            // 
            // commandBarSeparator1
            // 
            this.commandBarSeparator1.DisplayName = "Separator1";
            this.commandBarSeparator1.Name = "commandBarSeparator1";
            this.commandBarSeparator1.VisibleInOverflowMenu = false;
            // 
            // toggleButtonAlignLeft
            // 
            this.toggleButtonAlignLeft.DisplayName = "AlignLeft";
            this.toggleButtonAlignLeft.Image = global::Microsoft.SQL.Loc.OTPCaptureViewer.Properties.Resources.AlignLeft;
            this.toggleButtonAlignLeft.Name = "toggleButtonAlignLeft";
            this.toggleButtonAlignLeft.Text = "";
            // 
            // toggleButtonAlignCenter
            // 
            this.toggleButtonAlignCenter.DisplayName = "AlignCenter";
            this.toggleButtonAlignCenter.Image = global::Microsoft.SQL.Loc.OTPCaptureViewer.Properties.Resources.AlignCenter;
            this.toggleButtonAlignCenter.Name = "toggleButtonAlignCenter";
            this.toggleButtonAlignCenter.Text = "";
            // 
            // toggleButtonAlignRight
            // 
            this.toggleButtonAlignRight.DisplayName = "AlignRight";
            this.toggleButtonAlignRight.Image = global::Microsoft.SQL.Loc.OTPCaptureViewer.Properties.Resources.AlignRight;
            this.toggleButtonAlignRight.Name = "toggleButtonAlignRight";
            this.toggleButtonAlignRight.Text = "";
            // 
            // toggleButtonJustify
            // 
            this.toggleButtonJustify.DisplayName = "Justify";
            this.toggleButtonJustify.Image = global::Microsoft.SQL.Loc.OTPCaptureViewer.Properties.Resources.AlignJustify;
            this.toggleButtonJustify.Name = "toggleButtonJustify";
            this.toggleButtonJustify.Text = "";
            // 
            // commandBarSeparator2
            // 
            this.commandBarSeparator2.DisplayName = "Separator2";
            this.commandBarSeparator2.Name = "commandBarSeparator2";
            this.commandBarSeparator2.VisibleInOverflowMenu = false;
            // 
            // toggleButtonBulletList
            // 
            this.toggleButtonBulletList.DisplayName = "BulletList";
            this.toggleButtonBulletList.Image = global::Microsoft.SQL.Loc.OTPCaptureViewer.Properties.Resources.Bullets;
            this.toggleButtonBulletList.Name = "toggleButtonBulletList";
            this.toggleButtonBulletList.Text = "";
            // 
            // toggleButtonNumberedList
            // 
            this.toggleButtonNumberedList.DisplayName = "NumberedList";
            this.toggleButtonNumberedList.Image = global::Microsoft.SQL.Loc.OTPCaptureViewer.Properties.Resources.Numbering;
            this.toggleButtonNumberedList.Name = "toggleButtonNumberedList";
            this.toggleButtonNumberedList.Text = "";
            // 
            // commandBarRowElement2
            // 
            this.commandBarRowElement2.MinSize = new System.Drawing.Size(25, 25);
            // 
            // NewIssue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 506);
            this.Controls.Add(this.toggleButtonBar);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtDes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewIssue";
            this.Text = global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.NewIssue_DialogTitle_Text;
            this.Load += new System.EventHandler(this.NewIssue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleButtonBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadRichTextEditor txtDes;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private Telerik.WinControls.UI.RadCommandBar toggleButtonBar;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private Telerik.WinControls.UI.CommandBarToggleButton toggleButtonBold;
        private Telerik.WinControls.UI.CommandBarToggleButton toggleButtonItalic;
        private Telerik.WinControls.UI.CommandBarToggleButton toggleButtonUnderline;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator1;
        private Telerik.WinControls.UI.CommandBarToggleButton toggleButtonAlignLeft;
        private Telerik.WinControls.UI.CommandBarToggleButton toggleButtonAlignCenter;
        private Telerik.WinControls.UI.CommandBarToggleButton toggleButtonAlignRight;
        private Telerik.WinControls.UI.CommandBarToggleButton toggleButtonJustify;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator2;
        private Telerik.WinControls.UI.CommandBarToggleButton toggleButtonBulletList;
        private Telerik.WinControls.UI.CommandBarToggleButton toggleButtonNumberedList;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement2;
    }
}