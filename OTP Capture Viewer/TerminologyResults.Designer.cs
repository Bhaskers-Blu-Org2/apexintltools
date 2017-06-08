namespace Microsoft.SQL.Loc.OTPCaptureVeiwer
{
    partial class TerminologyResults
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
            this.txtENUString = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gvResults = new System.Windows.Forms.DataGridView();
            this.colOriginalText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTranslation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ENU String:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtENUString
            // 
            this.txtENUString.Location = new System.Drawing.Point(78, 9);
            this.txtENUString.Name = "txtENUString";
            this.txtENUString.Size = new System.Drawing.Size(944, 20);
            this.txtENUString.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(947, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtENUString);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1034, 40);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 442);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1034, 44);
            this.panel2.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gvResults);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1034, 402);
            this.panel3.TabIndex = 6;
            // 
            // gvResults
            // 
            this.gvResults.AllowUserToAddRows = false;
            this.gvResults.AllowUserToDeleteRows = false;
            this.gvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOriginalText,
            this.colTranslation,
            this.colProduct});
            this.gvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvResults.Location = new System.Drawing.Point(0, 0);
            this.gvResults.Name = "gvResults";
            this.gvResults.ReadOnly = true;
            this.gvResults.Size = new System.Drawing.Size(1034, 402);
            this.gvResults.TabIndex = 3;
            // 
            // colOriginalText
            // 
            this.colOriginalText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colOriginalText.HeaderText = "Original Text";
            this.colOriginalText.Name = "colOriginalText";
            this.colOriginalText.ReadOnly = true;
            this.colOriginalText.Width = 300;
            // 
            // colTranslation
            // 
            this.colTranslation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTranslation.HeaderText = "Translation";
            this.colTranslation.Name = "colTranslation";
            this.colTranslation.ReadOnly = true;
            // 
            // colProduct
            // 
            this.colProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colProduct.HeaderText = "Product";
            this.colProduct.Name = "colProduct";
            this.colProduct.ReadOnly = true;
            this.colProduct.Width = 150;
            // 
            // TerminologyResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 486);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "TerminologyResults";
            this.Text = "TerminologyResults";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtENUString;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView gvResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOriginalText;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTranslation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProduct;
    }
}