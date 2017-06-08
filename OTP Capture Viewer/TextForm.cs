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
    public partial class TextForm : Form
    {
        public TextForm()
        {
            InitializeComponent();
        }

        #region Fields
        string _textInput = string.Empty;
        bool _isDrawText = false;
        SizeF _textSize;
        Font _textFont = null;
        #endregion
        #region Properties

        public string TextInput
        {
            get { return _textInput; }
        }

        public bool IsDrawText
        {
            get { return _isDrawText; }
        }

        public Font TextFont
        {
            get { return _textFont; }
        }

        public SizeF TextSize
        {
            get { return _textSize; }
        }
        #endregion
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (string.Empty == tb_Input.Text)
            {
                MessageBox.Show(global::Microsoft.SQL.Loc.OTPCaptureViewer.Resx.Messages.TextForm_NotTextInput_Message, Global.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _textInput = tb_Input.Text;
            _textFont = tb_Input.Font;
            _isDrawText = true;
            getTextSize(tb_Input.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void getTextSize(string text)
        {
            int textLines = 0;
            Font font = tb_Input.Font;
            Graphics g = Graphics.FromHwnd(tb_Input.Handle);
            int textWidth = Convert.ToInt32(g.MeasureString(tb_Input.Text, font).Width);
            int textHeight = Convert.ToInt32(g.MeasureString(tb_Input.Text, font).Height);
            if (0 == textWidth % tb_Input.Width)
            {
                //Get string lines in text box
                textLines = textWidth / tb_Input.Width;
            }
            else
            {
                textLines = textWidth / tb_Input.Width + 1;
            }
            _textSize = new SizeF(tb_Input.Width, textLines * textHeight);//Get string rectangle
        }
    }
}
