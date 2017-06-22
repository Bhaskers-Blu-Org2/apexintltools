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
        public Action<string> WorkerWithParameter { get; set; }

        public Action Worker { get; set; }

        public WaitForm(Action worker)
        {
            InitializeComponent();
            if (worker == null)
            {
                throw new ArgumentNullException();
            }
            Worker = worker;
        }
        public WaitForm(Action<string> worker)
        {
            InitializeComponent();
            if (worker==null)
            {
                throw new ArgumentNullException();
            }
            WorkerWithParameter = worker;
        }

        public string Parameter { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (WorkerWithParameter != null)
            {
                Task.Factory.StartNew(() => { WorkerWithParameter(Parameter); }).ContinueWith(t => { this.DialogResult = DialogResult.OK; this.Close(); }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            if (Worker!=null)
            {
                Task.Factory.StartNew(() => { Worker(); }).ContinueWith(t => { this.DialogResult = DialogResult.OK; this.Close();  }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
