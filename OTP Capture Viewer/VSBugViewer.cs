/*---------------------------------------------------------------------------------------
 * Copyright(c) 2013 Microsoft Corporation
 * All rights reserved.
 * 
 * ClassName   : VSBugViewer
 * Author      : Weiyan Zhong
 * Create Date : 2011/08/31
 * 
 * Change History:
 * Date               Author                  ChangeList      Comments
 * 2011/08/31         Weiyan Zhong            Created         UI Design
 *  
 * 
 * 
 * ---------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.TeamFoundation.WorkItemTracking.Client;
//using Microsoft.TeamFoundation.WorkItemTracking.Controls;
using Microsoft.TeamFoundation.WorkItemTracking.WpfControls;

namespace Microsoft.SQL.Loc.OTPCaptureVeiwer
{
    public partial class VSBugViewer : Form
    {
        WorkItemControl control;
        public VSBugViewer()
        {
            InitializeComponent();

            //WorkItemFormControl fc = new WorkItemFormControl();
            //fc.Dock = DockStyle.Fill;
            //fc.BackColor = System.Drawing.SystemColors.Window;

            //fc.ReadOnly = false;

            //workItemControl = fc;
            //pViewer.Controls.Add(workItemControl);
            control = new WorkItemControl();
            control.ReadOnly = false;
            workItemControlHost.Child = control;

        }

        private WorkItem item;
        //private WorkItemFormControl workItemControl = null;
        //private WorkItemInformationBar infoBar = null;
        private bool itemChanged = false;

        public WorkItem CurrrentWorkItem
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }

        public string AttachedFile
        {
            set
            {
                if (item != null)
                {
                    item.Attachments.Add(new Attachment(value));
                }
            }
        }

        public bool ItemChanged
        {
            get
            {
                return itemChanged;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VSBugViewer_Load(object sender, EventArgs e)
        {
            //workItemControl.Item = item;
            control.Item = item;
        }

        public bool IsDirty
        {
            //get { return (workItemControl != null && workItemControl.Item.IsDirty); }
            get { return (control != null && control.Item.IsDirty); }
        }

        private void VSTFBugViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.IsDirty)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "OTP Capture Viewer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    btnSave_Click(null, null);
                    e.Cancel = true;
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (control == null || control.Item == null || control.Item.IsDirty == false)
            {
                return;
            }

            try
            {

                WorkItem wi = control.Item;
                foreach (Field f in wi.Fields)
                {
                    if (f.IsValid == false)
                    {
                        MessageBox.Show(string.Format("Invalid field {0} with value {1}", f.Name, f.Value), "OTP Capture Viewer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                wi.Save();
                itemChanged = true;
                item = wi;
            }
            catch (ClientException cex)
            {
                MessageBox.Show(string.Format("Save failed because of error {0}", cex.Message), "OTP Capture Viewer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Save failed because of error {0}", ex.Message), "OTP Capture Viewer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
