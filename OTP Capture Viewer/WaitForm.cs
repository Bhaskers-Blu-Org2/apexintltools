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
    public partial class WaitForm : Form
    {
        public Action<string> Worker { get; set; }
        public WaitForm(Action<string> worker)
        {
            InitializeComponent();
            if (worker==null)
            {
                throw new ArgumentNullException();
            }
            Worker = worker;
        }

        public string Parameter { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Task.Factory.StartNew(()=> { Worker(Parameter); }).ContinueWith(t => { this.Close(); },TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
