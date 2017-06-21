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
    public partial class LookupCapture : Form
    {
        public LookupCapture()
        {
            InitializeComponent();
        }
        
        public List<string> LocCaptureNames
        {
            get;set;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtFileName.Text.Trim() != string.Empty)
            {
                string name = txtFileName.Text.Trim();
                using (WaitForm frm = new WaitForm(SearchCapture))
                {
                    frm.Parameter = name;
                    frm.ShowDialog(this);
                }
            }
        }

        public string SelectedName
        {
            get;set;
        }

        private void SearchCapture(string fileName)
        {
            var v = from name in LocCaptureNames
                    where name.ToLower().Contains(fileName.ToLower())
                    select name;
            List<string> findNames = v.ToList<string>();
            if (findNames.Count==0)
            {
                MessageBox.Show("", Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SelectedName = findNames[0];
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
