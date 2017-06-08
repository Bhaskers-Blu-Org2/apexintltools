using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    internal class ZipUtility
    {
        static ImageFormat imageFormat = ImageFormat.Png;
        static string imageExtension = ".png";
        static string metadataExtension = ".xml";
        static int thumbnailWidth = 150;
        static string thunbmailName = "_Thumbnail";

        internal static byte[] ExtractImage(ZipArchive archive)
        {
            return getImage(archive);
        }

        internal static LocUIElement ExtractMetadata(ZipArchive archive)
        {
            return getLocUIElement(archive);
        }


        private static LocUIElement getLocUIElement(ZipArchive archive)
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (string.Compare(Path.GetExtension(entry.Name), metadataExtension) == 0)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        entry.Open().CopyTo(stream);
                        stream.Seek(0, System.IO.SeekOrigin.Begin);
                        XmlSerializer serializer = new XmlSerializer(typeof(LocUIElement));
                        LocUIElement element = serializer.Deserialize(stream) as LocUIElement;
                        return element;
                    }
                }
            }

            return null;
        }

        private static byte[] getImage(ZipArchive archive)
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (string.Compare(Path.GetExtension(entry.Name), imageExtension) == 0 && Path.GetFileNameWithoutExtension(entry.Name).IndexOf(thunbmailName) < 0)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        entry.Open().CopyTo(stream);
                        stream.Seek(0, System.IO.SeekOrigin.Begin);
                        //Image image = Image.FromStream(stream);
                        byte[] thedata = new byte[stream.GetBuffer().Length];
                        stream.GetBuffer().CopyTo(thedata, 0);
                        return thedata;
                    }
                }
            }

            return null;
        }

        

        private static bool ThumbnailCallback()
        {
            return false;
        }



    }
}
