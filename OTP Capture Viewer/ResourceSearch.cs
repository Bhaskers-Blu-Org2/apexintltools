using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResourceManager;

namespace Microsoft.SQL.Loc.OTPCaptureVeiwer
{
    public partial class ResourceSearch : Form
    {
        static ResType Flags = ResType.Managed | ResType.String | ResType.MessageString | ResType.Baml | ResType.ManagedCTMenu;

        public ResourceSearch()
        {
            InitializeComponent();
        }

        public void Search(string text)
        {
            txtText.Text=text;
            txtResFolder.Text=CaptureSetting.ResourceFolder;

            this.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private Dictionary<string, List<string>> GetResources(string fileName)
        {
            ResourceFetcher findRes = new ResourceFetcher();
            ArrayList results = new ArrayList();
            string text=txtText.Text.Trim();
            Dictionary<string, List<string>> resources = new Dictionary<string, List<string>>();
            //if (fileName.ToLower().Contains(".resources.dll"))
            //{
            //    results = RetrieveManagedAssemblyResources(fileName, text, 1033, 1033);
            //}
            //else
            //{
            try
            {
                results = findRes.WalkResources(fileName, (uint)Flags, text, null, null);
            }
            catch
            {
                try
                {
                    results = RetrieveManagedAssemblyResources(fileName, text, 1033, 1033);
                }
                catch
                { return resources; }
            }
            //}
            //if (results.Count == 0)
            //{
            //    results = findRes.WalkResources(fileName, (uint)Flags, "", null, null);
            //}

            
            foreach (string resStr in results)
            {
                string[] stringItems = resStr.Split(';');
                if (resStr.StartsWith(";") && (stringItems.Length == 5 || stringItems.Length == 6))
                {
                    //ResourceElement element = new ResourceElement();
                    string key = stringItems[stringItems.Length - 1];
                    string value = stringItems[1];
                    if (!resources.Keys.Contains<string>(key))
                    {
                        resources.Add(key, new List<string>() { value });
                    }
                    else
                    {
                        resources[key].Add(value);
                    }
                }
                else if (resStr.StartsWith("<"))
                {
                    string[] items = resStr.Split('<');
                    if (items.Length == 5 || items.Length == 6)
                    {
                        string key = items[items.Length - 1];
                        string value = items[1];
                        if (!resources.Keys.Contains<string>(key))
                        {
                            resources.Add(key, new List<string>() { value });
                        }
                        else
                        {
                            resources[key].Add(value);
                        }
                    }
                }
                else if (resStr.StartsWith("="))
                {
                    string[] items = resStr.Split('=');
                    string key = items[items.Length - 1];
                    string value = items[1];
                    if (!resources.Keys.Contains<string>(key))
                    {
                        resources.Add(key, new List<string>() { value });
                    }
                    else
                    {
                        resources[key].Add(value);
                    }
                }
                //else
                //{
                //    MessageBox.Show("New format was found, some loc resources will not be checked. This issue need to be fixed or the testing result will be incorrect");
                //}
            }
            return resources;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtText.Text.Trim() != "" && Directory.Exists(txtResFolder.Text))
            {
                if (CaptureSetting.ResourceFolder != "")
                {
                    if (Directory.Exists(CaptureSetting.ResourceFolder))
                    {
                        string[] files = Directory.GetFiles(CaptureSetting.ResourceFolder, "*.*", SearchOption.AllDirectories);
                        foreach (string fileName in files)
                        {
                            Task.Factory.StartNew(() =>
                                {
                                    Dictionary<string, List<string>> resources = GetResources(fileName);
                                    if (resources.Keys.Count > 0)
                                    {
                                        foreach (string key in resources.Keys)
                                        {
                                            foreach (string value in resources[key])
                                            {
                                                gvResult.Rows.Add(Path.GetFileName(fileName), key, value);
                                                gvResult.Refresh();
                                            }
                                        }
                                    }
                                },
                                CancellationToken.None, 
                                TaskCreationOptions.None, 
                                TaskScheduler.FromCurrentSynchronizationContext()
                                );
                        }
                    }
                }
            }
        }

        private static ArrayList RetrieveManagedAssemblyResources(string resFileName, string searchString, int sourceLCID, int targetLCID)
        {

            Assembly a = null;
            ArrayList m_ResKeyValues = new ArrayList();

            try
            {
                a = Assembly.ReflectionOnlyLoadFrom(resFileName);
            } //TODO: do you want to limit to dll extensins only
            catch { } // if assembly cannot be loaded just return

            if (a != null)
            {
                foreach (string ResName in a.GetManifestResourceNames())
                {
                    string ResNameLower = ResName.ToLower();
                    if (ResNameLower.EndsWith(".resources"))
                    {
                        Stream rs = a.GetManifestResourceStream(ResName);
                        ResourceReader rdr = new ResourceReader(rs);
                        IDictionaryEnumerator dicEnum = rdr.GetEnumerator();

                        string ShortResName = ResName.Substring(0, ResName.Length - 10);
                        int i = ShortResName.LastIndexOf('.');
                        if (i > 0)
                        {
                            CultureInfo ci1 = CultureInfo.GetCultureInfo(sourceLCID);
                            CultureInfo ci2 = CultureInfo.GetCultureInfo(targetLCID);
                            string LastPart = ShortResName.Substring(i + 1);
                            if (LastPart.Equals(ci1.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase) ||
                                LastPart.Equals(ci1.TwoLetterISOLanguageName + "-" + ci1.ThreeLetterWindowsLanguageName, StringComparison.OrdinalIgnoreCase) ||
                                LastPart.Equals(ci1.Name, StringComparison.OrdinalIgnoreCase) ||
                                LastPart.Equals(ci2.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase) ||
                                LastPart.Equals(ci2.TwoLetterISOLanguageName + "-" + ci1.ThreeLetterWindowsLanguageName, StringComparison.OrdinalIgnoreCase) ||
                                LastPart.Equals(ci2.Name, StringComparison.OrdinalIgnoreCase))
                                ShortResName = ShortResName.Substring(0, ShortResName.Length - LastPart.Length - 1);
                        }

                        while (dicEnum.MoveNext())
                        {
                            if (dicEnum.Value == null || dicEnum.Value.GetType().ToString() != "System.String" ||
                                ((string)dicEnum.Value).Length == 0 ||
                                (searchString != null && searchString.Length > 0 && searchString.Replace("&", string.Empty) != ((string)dicEnum.Value).Replace("&", string.Empty)))
                                continue;

                            string value = dicEnum.Value.ToString();
                            
                            if (value.Contains(";"))
                            {
                                m_ResKeyValues.Add("<" + (string)dicEnum.Value + "<ManagedString<" + resFileName + "<" + (string)dicEnum.Key);
                            }
                            else
                            {
                                m_ResKeyValues.Add(";" + (string)dicEnum.Value + ";ManagedString;" + resFileName + ";" + (string)dicEnum.Key);
                            }
                        }
                        rdr.Close();
                        rs.Dispose();
                    }
                }

                
            }

            return m_ResKeyValues;

        }
    }

    [Flags]
    public enum ResType : ulong
    {
        Baml = 0x00000008,
        Html = 0x00000010,
        CTMenu = 0x00000020,
        Dialog = 0x00000040,
        String = 0x00000080,
        Managed = 0x00000100,
        DialogItem = 0x00000200,
        NativeMenu = 0x00000400,
        Inf = 0x00000800,
        MessageString = 0x00001000,
        SdmDialog = 0x00002000,
        SdmDialogItem = 0x00004000,
        OfficeString = 0x00008000,
        Msi = 0x00010000,
        Xml = 0x00020000,
        ManagedCTMenu = 0x00040000,
        Version = 0x00080000,

        // UIAF are upper part of 64bit value
        UIAFNewTypes = 0x00000001 << 32, // always set to know that is used in new format
        UIAFManagedString = 0x00000002 << 32,
        UIAFXmlString = 0x00000004 << 32,

        MaskRPFOnly = 0x00EFFF8, // eliminated JustIn1033 from RPF options to not apply ever, because user is choosing files anyway
        MaskUIAFOnly = 0x00000007 << 32,
        MaskAll = 0x009FFFFF | MaskUIAFOnly
    }

    enum ResTypeSearch : uint
    {
        GenerateRelativePathDll = 0x00200000,
        GenerateFullPathDll = 0x00400000,
        ExtractResource = 0x01000000
    }
}
