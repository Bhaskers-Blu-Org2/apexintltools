using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    public class UIScreenshot
    {
        LocUIElement element = null;
        byte[] imageBytes = null;
        //byte[] thumbnailImageBytes=null;

        internal UIScreenshot(string captureFileName)
        {
            if (!File.Exists(captureFileName))
            {
                throw new FileNotFoundException();
            }

            try
            {
                //element = ZipUtility.ExtractMetadata(captureFileName);
                //imageBytes = ZipUtility.ExtractImage(captureFileName);
                //thumbnailImageBytes = ZipUtility.ExtractThumbnailImage(captureFileName);
                using (FileStream fs = File.Open(captureFileName, FileMode.Open))
                {
                    getFromStream(fs);
                }
            }
            catch
            {
                //throw new Exception("Format is not supported!");
                throw;
            }
        }

        internal UIScreenshot(Stream stream)
        {
            getFromStream(stream);
        }

        private void getFromStream(Stream stream)
        {
            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    element = ZipUtility.ExtractMetadata(archive);
                    imageBytes = ZipUtility.ExtractImage(archive);
                    //thumbnailImageBytes = ZipUtility.ExtractThumbnailImage(archive);
                }
            }
            catch 
            {
                throw new Exception("Format is not supported!");
            }
        }

        internal LocUIElement UIElement
        {
            get
            {
                return element;
            }
        }

        internal byte[] UIImageBytes
        {
            get
            {
                return imageBytes;
            }
        }

        //public byte[] ThumbnailImageBytes
        //{
        //    get
        //    {
        //        return thumbnailImageBytes;
        //    }
        //}

        internal Image UIImage
        {
            get
            {
                MemoryStream ms = new MemoryStream(imageBytes);
                ms.Seek(0, SeekOrigin.Begin);
                Image image = Image.FromStream(ms);
                return image;
            }
        }

    }
}
