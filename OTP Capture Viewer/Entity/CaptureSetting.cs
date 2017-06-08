using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    class CaptureSetting
    {
        
        static string currentFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        internal static string CurrentFolder
        {
            get
            {
                return currentFolder;
            }
        }


        internal static string LocCaptureFolder
        {
            get;
            set;
        }

        internal static string ENUCaptureFolder
        {
            get;
            set;
        }

        private static string logFolder = currentFolder + "\\Log";
        internal static string LogFolder
        {
            get
            {
                return logFolder;
            }
            set
            {
                logFolder = value;
            }
        }
            

        internal static bool IsReadyForReview
        {
            get;
            set;
        }

        
    }

    enum CaptureFormats
    {
        OTPFile=0,
        JPGFile=1,
        PNGFile=2
    }
}
