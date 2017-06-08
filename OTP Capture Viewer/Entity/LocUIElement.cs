using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    [XmlRoot("LocUIElement")]
    public class LocUIElement
    {
        #region Fields
        private string _windowText = string.Empty;
        private string _accesibleName = string.Empty;
        private string _controlName = string.Empty;
        private string _hotKey = string.Empty;
        private int _lcid = CultureInfo.CurrentCulture.LCID;
        private int _hashCode;
        private ElementLocation _location = new ElementLocation();
        private Guid _guid = new Guid();
        private List<LocUIElement> _children = new List<LocUIElement>();
        private int _textWidth;
        private int _textHeight;
        private bool _visible = false;
        private bool _enabled = false;

        private int dpi = 96;
        private UIFontCategory fontCategory = UIFontCategory.Windows;
        private int osMajorVersion;
        private int osMinorVersion;
        private bool is64BitEnvironment;
        private bool isWindowsServer;
        #endregion

        #region Properties
        public string WindowText
        {
            get
            {
                return _windowText;
            }
            set { _windowText = value; }
        }

        public string AccesibleName
        {
            get { return _accesibleName; }
            set { _accesibleName = value; }
        }

        public string ControlName
        {
            get { return _controlName; }
            set { _controlName = value; }
        }

        public string HotKey
        {
            get { return _hotKey; }
            set { _hotKey = value; }

        }

        public int LCID
        {
            get { return _lcid; }
            set { _lcid = value; }
        }

        public Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        public int HashCode
        {
            get { return _hashCode; }
            set { _hashCode = value; }
        }

        public ElementLocation Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public List<LocUIElement> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        public int TextWidth
        {
            get { return _textWidth; }
            set { _textWidth = value; }
        }

        public int TextHeight
        {
            get { return _textHeight; }
            set { _textHeight = value; }
        }
        #endregion

        /// <summary>
        /// Default is False
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        /// <summary>
        /// Default is False
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public int DPI
        {
            get
            {
                return dpi;
            }
            set
            {
                dpi = value;
            }
        }

        public UIFontCategory FontCategory
        {
            get
            {
                return fontCategory;
            }
            set
            {
                fontCategory = value;
            }
        }

        public int OSMajorVersion
        {
            get
            {
                return osMajorVersion;
            }
            set
            {
                osMajorVersion = value;
            }

        }

        public int OSMinorVersion
        {
            get
            {
                return osMinorVersion;
            }
            set
            {
                osMinorVersion = value;
            }
        }

        public bool Is64BitEnvironment
        {
            get
            {
                return is64BitEnvironment;
            }
            set
            {
                is64BitEnvironment = value;
            }
        }

        public bool IsWindowsServer
        {
            get
            {
                return isWindowsServer;
            }
            set
            {
                isWindowsServer = value;
            }
        }

        private bool isWebElement = false;
        public bool IsWebElement
        {
            get
            {
                return isWebElement;
            }
            set
            {
                isWebElement = value;
            }
        }


        private bool _readOnly;

        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
            }
        }
        #region Constructor
        /// <summary>
        /// Blank constructor for serialize method
        /// </summary>
        public LocUIElement()
        {
            OSVer osVer = OSUtility.GetOSVersion();
            osMajorVersion = osVer.Major;
            osMinorVersion = osVer.Minor;
            is64BitEnvironment = Environment.Is64BitOperatingSystem;
            isWindowsServer = OSUtility.IsWindowsServer();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get element according to the given point
        /// </summary>
        public LocUIElement FindChildFromPoint(Point point)
        {
            LocUIElement uiElement = null;
            if (this._location.RelativeX <= point.X && (this._location.RelativeX + this._location.Width) >= point.X && this._location.RelativeY <= point.Y && (this._location.RelativeY + this._location.Height) >= point.Y)
            {
                findControlWithLocation(point, this, ref uiElement);
                if (uiElement == null)
                {
                    uiElement = this;
                }
            }
            return uiElement;
        }

        public LocUIElement FindControlWithRectangle(Rectangle rect)
        {
            LocUIElement uiElement = null;

            if (this._location.RelativeX == rect.X && this._location.RelativeY == rect.Y && this._location.Width == rect.Width && this._location.Height == rect.Height)
            {
                uiElement = this;
            }
            else
            {
                if (this._location.RelativeX <= rect.X && this._location.Width >= rect.Width && this._location.RelativeY <= rect.Y && this._location.Height >= rect.Height)
                {
                    foreach (LocUIElement child in this.Children)
                    {
                        uiElement = child.FindControlWithRectangle(rect);
                        if (uiElement != null)
                        {
                            break;
                        }
                    }
                }
            }


            return uiElement;
        }

        private void findControlWithLocation(Point point, LocUIElement uiElement, ref LocUIElement foundElement)
        {
            if (uiElement.Children.Count > 0)
            {
                foreach (LocUIElement child in uiElement.Children)
                {
                    findControlWithLocation(point, child, ref foundElement);
                }
            }
            else
            {
                if (uiElement._location.RelativeX <= point.X && (uiElement._location.RelativeX + uiElement._location.Width) >= point.X && uiElement._location.RelativeY <= point.Y && (uiElement._location.RelativeY + uiElement._location.Height) >= point.Y)
                {
                    if (foundElement == null)
                    {
                        foundElement = uiElement;
                    }
                    else
                    {
                        Rectangle rect1 = new Rectangle(foundElement.Location.RelativeX, foundElement.Location.RelativeY, foundElement.Location.Width, foundElement.Location.Height);
                        Rectangle rect2 = new Rectangle(uiElement.Location.RelativeX, uiElement.Location.RelativeY, uiElement.Location.Width, uiElement.Location.Height);
                        if (rect1.Contains(rect2))
                        {
                            foundElement = uiElement;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Draw rectangles for all elements in the window,except list items in combo box
        /// </summary>
        public void Traversal()
        {
            drowElement();
            if (this.Children.Count > 0 && this._controlName != "combo box")
            {
                foreach (LocUIElement child in this.Children)
                {
                    child.Traversal();
                }
            }
        }

        /// <summary>
        /// Draw rectangle according to coordinate of element
        /// </summary>
        private void drowElement()
        {
            Thread.Sleep(1000);
            System.IntPtr desktopHandle = Win32API.GetDC(System.IntPtr.Zero);
            Graphics g = System.Drawing.Graphics.FromHdc(desktopHandle);
            g.DrawRectangle(new Pen(Color.Red), new Rectangle(this._location.AbsoluteX, this._location.AbsoluteY, this._location.Width, this._location.Height));
            //g.DrawRectangle(new Pen(Color.Red), new Rectangle(this._location.RelativeX, this._location.RelativeY, this._location.Width, this._location.Height));
        }

        //public static LocUIElement DeserializeFromXml(string xmlFile)
        //{
        //    try
        //    {
        //        LocUIElement locUI;
        //        XmlSerializer serializer = new XmlSerializer(typeof(LocUIElement));
        //        using (FileStream xmlStream = new FileStream(xmlFile, FileMode.Open, FileAccess.Read))
        //        {
        //            object obj=serializer.Deserialize(xmlStream);
        //            locUI = obj as LocUIElement;
        //        }
        //        return locUI;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public static LocUIElement DeserializeFromOTPFile(string otpFile)
        //{
        //    try
        //    {
        //        LocUIElement locUI;
        //        locUI = ZipUtility.ExtractMetadata(otpFile);
        //        return locUI;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        #endregion
    }
}
