using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    class ImageUtility
    {
        public static Bitmap CombineImages(Image[] bitmaps)
        {
            //read all images into memory
            
            System.Drawing.Bitmap finalImage = null;

            int width = 0;
            int height = 0;

            foreach (Image bitmap in bitmaps)
            {
                width += bitmap.Width;
                height = bitmap.Height > height ? bitmap.Height : height;
            }

            //create a bitmap to hold the combined image
            finalImage = new System.Drawing.Bitmap(width, height);

            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalImage))
            {
                //set background color
                g.Clear(System.Drawing.Color.Black);

                //go through each image and draw it on the final image
                int offset = 0;
                foreach (Image screenshot in bitmaps)
                {
                    g.DrawImage(screenshot,
                     new System.Drawing.Rectangle(offset, 0, screenshot.Width, screenshot.Height));
                    offset += screenshot.Width;
                }
            }


            return finalImage;
        }
    }
}
