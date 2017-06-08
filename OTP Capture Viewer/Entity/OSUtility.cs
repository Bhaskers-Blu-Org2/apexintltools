using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    class OSUtility
    {
        const int OS_ANYSERVER = 29;

        [DllImport("shlwapi.dll", SetLastError = true, EntryPoint = "#437")]
        private static extern bool IsOS(int os);

        public static bool IsWindowsServer()
        {
            return IsOS(OS_ANYSERVER);
        }

        public static OSVer GetOSVersion()
        {
            string ver = "";

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Version FROM Win32_OperatingSystem");
                foreach (ManagementObject os in searcher.Get())
                {
                    ver = os["Version"].ToString();
                    break;
                }
            }
            catch//(Exception ex)
            { }

            OSVer winVer = new OSVer();
            if (ver != "")
            {
                string[] vers = ver.Split('.');
                winVer.Major = int.Parse(vers[0]);
                winVer.Minor = int.Parse(vers[1]);
            }
            else
            {
                winVer.Major = Environment.OSVersion.Version.Major;
                winVer.Minor = Environment.OSVersion.Version.Minor;
            }

            return winVer;
        }
    }

    internal class OSVer
    {
        public int Major { get; set; }
        public int Minor { get; set; }
    }
}
