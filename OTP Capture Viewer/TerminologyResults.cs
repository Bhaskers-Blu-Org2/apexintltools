using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SQL.Loc.OTPCaptureVeiwer.TerminologyProxy;

namespace Microsoft.SQL.Loc.OTPCaptureVeiwer
{
    public partial class TerminologyResults : Form
    {
        public TerminologyResults()
        {
            InitializeComponent();
        }

        public string ENUTest
        {
            set
            {
                txtENUString.Text = value;
            }
        }

        public Matches MatchResult
        {
            set
            {
                foreach (Match match in value)
                {
                    gvResults.Rows.Add(match.OriginalText, match.Translations[0].TranslatedText, match.Product);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
