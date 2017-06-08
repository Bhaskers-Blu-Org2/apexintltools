using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    public partial class Suggestion : Form
    {
        public Suggestion()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public string OriginalText
        {
            get
            {
                return txtOriginal.Text.Trim();
            }
            set
            {
                txtOriginal.Text = value;
            }
        }

        public string SuggestionText
        {
            get
            {
                return txtSuggestion.Text.Trim();
            }
            set
            {
                txtSuggestion.Text = value;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtOriginal.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Origianl Text is empty!", OTPMessages.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtOriginal.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Suggestion is empty!", OTPMessages.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }


    }
}
