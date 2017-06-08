namespace Microsoft.SQL.Loc.OTPCaptureVeiwer
{
    partial class ResourceSearch
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtResFolder = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gvResult = new System.Windows.Forms.DataGridView();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtResFolder);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtText);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(819, 101);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Text:";
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(109, 13);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(698, 20);
            this.txtText.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Resource Folder:";
            // 
            // txtResFolder
            // 
            this.txtResFolder.Location = new System.Drawing.Point(109, 42);
            this.txtResFolder.Name = "txtResFolder";
            this.txtResFolder.ReadOnly = true;
            this.txtResFolder.Size = new System.Drawing.Size(698, 20);
            this.txtResFolder.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 404);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(819, 41);
            this.panel2.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(732, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gvResult);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 101);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(819, 303);
            this.panel3.TabIndex = 2;
            // 
            // gvResult
            // 
            this.gvResult.AllowUserToAddRows = false;
            this.gvResult.AllowUserToDeleteRows = false;
            this.gvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFileName,
            this.colKey,
            this.colValue});
            this.gvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvResult.Location = new System.Drawing.Point(0, 0);
            this.gvResult.Name = "gvResult";
            this.gvResult.ReadOnly = true;
            this.gvResult.Size = new System.Drawing.Size(819, 303);
            this.gvResult.TabIndex = 0;
            // 
            // colFileName
            // 
            this.colFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colFileName.HeaderText = "File Name";
            this.colFileName.Name = "colFileName";
            this.colFileName.ReadOnly = true;
            this.colFileName.Width = 150;
            // 
            // colKey
            // 
            this.colKey.HeaderText = "Key";
            this.colKey.Name = "colKey";
            this.colKey.ReadOnly = true;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colValue.HeaderText = "Resource";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(732, 68);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ResourceSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 445);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ResourceSearch";
            this.Text = "ResourceSearch";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtResFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView gvResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.Button btnSearch;
    }
}