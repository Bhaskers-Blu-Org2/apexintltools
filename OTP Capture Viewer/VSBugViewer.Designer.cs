namespace Microsoft.SQL.Loc.OTPCaptureVeiwer
{
    partial class VSBugViewer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pViewer = new System.Windows.Forms.Panel();
            this.workItemControlHost = new System.Windows.Forms.Integration.ElementHost();
            this.panel1.SuspendLayout();
            this.pViewer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 693);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 39);
            this.panel1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(840, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(921, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pViewer
            // 
            this.pViewer.Controls.Add(this.workItemControlHost);
            this.pViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pViewer.Location = new System.Drawing.Point(0, 0);
            this.pViewer.Name = "pViewer";
            this.pViewer.Size = new System.Drawing.Size(1008, 693);
            this.pViewer.TabIndex = 1;
            // 
            // workItemControlHost
            // 
            this.workItemControlHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workItemControlHost.Location = new System.Drawing.Point(0, 0);
            this.workItemControlHost.Name = "workItemControlHost";
            this.workItemControlHost.Size = new System.Drawing.Size(1008, 693);
            this.workItemControlHost.TabIndex = 0;
            this.workItemControlHost.Text = "elementHost1";
            this.workItemControlHost.Child = null;
            // 
            // VSBugViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 732);
            this.Controls.Add(this.pViewer);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VSBugViewer";
            this.Text = "VSTF Bug Viewer";
            this.Load += new System.EventHandler(this.VSBugViewer_Load);
            this.panel1.ResumeLayout(false);
            this.pViewer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pViewer;
        private System.Windows.Forms.Integration.ElementHost workItemControlHost;
    }
}