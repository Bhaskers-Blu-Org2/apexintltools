using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SQL.Loc.OTPCaptureViewer
{
    public class ElementLocation : IEquatable<ElementLocation>
    {
        public int AbsoluteX;

        public int AbsoluteY;

        public int RelativeX;

        public int RelativeY;

        public int Width;

        public int Height;

        public override string ToString()
        {
            return RelativeX + "_" + RelativeY + "_" + Width + "_" + Height;
        }

        public bool Equals(ElementLocation other)
        {
            return this.RelativeX == other.RelativeX && this.RelativeY == other.RelativeY && this.Width == other.Width && this.Height == other.Height;
        }
    }
}
