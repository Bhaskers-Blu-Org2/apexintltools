using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    public partial class EulaForm : Form
    {
        const string filename ="Open Test Platform Capture Viewer EULA (final).rtf";
        bool hasAccepted = false;
        public bool HasAccepted
        {
            get
            {
                return hasAccepted;
            }
            set
            {
                hasAccepted = true;
            }
        }
        public EulaForm()
        {
            InitializeComponent();
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            hasAccepted = false;
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            hasAccepted = true;
            this.DialogResult = DialogResult.OK;
        }

        private void EndUserLicense_Load(object sender, EventArgs e)
        {
            using (Stream stream = GenerateStreamFromString(Microsoft.SQL.Loc.OTPCaptureViewer.Properties.Resources.Open_Test_Platform_Capture_Viewer_EULA__final_))
            {
                txtEULA.LoadFile(stream, RichTextBoxStreamType.RichText);
            }
        }

        private static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
