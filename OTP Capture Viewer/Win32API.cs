using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    internal static class Win32API
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr GetDC(IntPtr hwnd);
    }
}
