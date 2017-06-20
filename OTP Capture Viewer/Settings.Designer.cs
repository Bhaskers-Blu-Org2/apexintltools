using Microsoft.SQL.Loc.OTPCaptureViewer.Resx;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    partial class Settings
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSaveSxS = new System.Windows.Forms.CheckBox();
            this.btnBrowser_Log = new System.Windows.Forms.Button();
            this.txtLogFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBrowseENU = new System.Windows.Forms.Button();
            this.txtENUFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowseLoc = new System.Windows.Forms.Button();
            this.txtLocFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkShowToken = new System.Windows.Forms.CheckBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtSessionKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(456, 233);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.Settings_OKButton_Text;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(537, 233);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.Settings_CancelButton_Text;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSaveSxS);
            this.groupBox1.Controls.Add(this.btnBrowser_Log);
            this.groupBox1.Controls.Add(this.txtLogFolder);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnBrowseENU);
            this.groupBox1.Controls.Add(this.txtENUFolder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBrowseLoc);
            this.groupBox1.Controls.Add(this.txtLocFolder);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 123);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Capture Info";
            // 
            // chkSaveSxS
            // 
            this.chkSaveSxS.AutoSize = true;
            this.chkSaveSxS.Checked = true;
            this.chkSaveSxS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveSxS.Location = new System.Drawing.Point(144, 92);
            this.chkSaveSxS.Name = "chkSaveSxS";
            this.chkSaveSxS.Size = new System.Drawing.Size(291, 17);
            this.chkSaveSxS.TabIndex = 22;
            this.chkSaveSxS.Text = global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.Settings_CheckBoxSaveSxS_Text;
            this.chkSaveSxS.UseVisualStyleBackColor = true;
            // 
            // btnBrowser_Log
            // 
            this.btnBrowser_Log.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowser_Log.Location = new System.Drawing.Point(550, 66);
            this.btnBrowser_Log.Name = "btnBrowser_Log";
            this.btnBrowser_Log.Size = new System.Drawing.Size(32, 21);
            this.btnBrowser_Log.TabIndex = 21;
            this.btnBrowser_Log.Text = global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.Settings_BrowserButton_Text;
            this.btnBrowser_Log.UseVisualStyleBackColor = true;
            this.btnBrowser_Log.Click += new System.EventHandler(this.btnBrowser_Log_Click);
            // 
            // txtLogFolder
            // 
            this.txtLogFolder.Location = new System.Drawing.Point(144, 65);
            this.txtLogFolder.Name = "txtLogFolder";
            this.txtLogFolder.Size = new System.Drawing.Size(400, 20);
            this.txtLogFolder.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Log Folder:";
            // 
            // btnBrowseENU
            // 
            this.btnBrowseENU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseENU.Location = new System.Drawing.Point(550, 39);
            this.btnBrowseENU.Name = "btnBrowseENU";
            this.btnBrowseENU.Size = new System.Drawing.Size(32, 21);
            this.btnBrowseENU.TabIndex = 18;
            this.btnBrowseENU.Text = global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.Settings_BrowserButton_Text;
            this.btnBrowseENU.UseVisualStyleBackColor = true;
            this.btnBrowseENU.Click += new System.EventHandler(this.btnBrowseENU_Click);
            // 
            // txtENUFolder
            // 
            this.txtENUFolder.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtENUFolder.Location = new System.Drawing.Point(144, 39);
            this.txtENUFolder.Name = "txtENUFolder";
            this.txtENUFolder.ReadOnly = true;
            this.txtENUFolder.Size = new System.Drawing.Size(400, 20);
            this.txtENUFolder.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Reference Capture Folder:";
            // 
            // btnBrowseLoc
            // 
            this.btnBrowseLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseLoc.Location = new System.Drawing.Point(550, 13);
            this.btnBrowseLoc.Name = "btnBrowseLoc";
            this.btnBrowseLoc.Size = new System.Drawing.Size(32, 21);
            this.btnBrowseLoc.TabIndex = 15;
            this.btnBrowseLoc.Text = global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.Settings_BrowserButton_Text;
            this.btnBrowseLoc.UseVisualStyleBackColor = true;
            this.btnBrowseLoc.Click += new System.EventHandler(this.btnBrowseLoc_Click);
            // 
            // txtLocFolder
            // 
            this.txtLocFolder.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtLocFolder.Location = new System.Drawing.Point(144, 13);
            this.txtLocFolder.Name = "txtLocFolder";
            this.txtLocFolder.ReadOnly = true;
            this.txtLocFolder.Size = new System.Drawing.Size(400, 20);
            this.txtLocFolder.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Review Capture Folder:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkShowToken);
            this.groupBox2.Controls.Add(this.btnConnect);
            this.groupBox2.Controls.Add(this.txtSessionKey);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(598, 70);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Authentication";
            // 
            // chkShowToken
            // 
            this.chkShowToken.AutoSize = true;
            this.chkShowToken.Location = new System.Drawing.Point(146, 44);
            this.chkShowToken.Name = "chkShowToken";
            this.chkShowToken.Size = new System.Drawing.Size(101, 17);
            this.chkShowToken.TabIndex = 3;
            this.chkShowToken.Text = global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.Setting_ShowTheTokenCheckBox_Text;
            this.chkShowToken.UseVisualStyleBackColor = true;
            this.chkShowToken.CheckedChanged += new System.EventHandler(this.chkShowToken_CheckedChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(507, 15);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.UIStrings.Settings_ConnectButton_Text;
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtSessionKey
            // 
            this.txtSessionKey.Location = new System.Drawing.Point(144, 17);
            this.txtSessionKey.Name = "txtSessionKey";
            this.txtSessionKey.Size = new System.Drawing.Size(359, 20);
            this.txtSessionKey.TabIndex = 1;
            this.txtSessionKey.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Github Access Token:";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 265);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrowser_Log;
        private System.Windows.Forms.TextBox txtLogFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBrowseENU;
        private System.Windows.Forms.TextBox txtENUFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowseLoc;
        private System.Windows.Forms.TextBox txtLocFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSessionKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.CheckBox chkShowToken;
        private System.Windows.Forms.CheckBox chkSaveSxS;
    }
}