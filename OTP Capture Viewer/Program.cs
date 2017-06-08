using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            initializeSettings();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (OTPUtility.GetSetting(
         "EulaIsAccepted", "False").ToString()
            != "True")
            {
                // Display the EULA.
                EulaForm frm = new EulaForm();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Remember that the user accepted the EULA.
                    OTPUtility.SaveSetting(
                        "EulaIsAccepted", "True");

                }
                else
                {
                    // The user declined. Close the program.
                    
                    return;
                }
            }

            Application.Run(new Main());

        }

        static void initializeSettings()
        {
            CaptureSetting.LocCaptureFolder = "";
            CaptureSetting.ENUCaptureFolder = "";
            CaptureSetting.IsReadyForReview = false;
        }
    }
}
