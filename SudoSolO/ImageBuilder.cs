using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;

namespace SudoSolO
{
    abstract class ImageBuilder
    {
        public abstract void Crop(int i, int j);
        public abstract void Invert();
        public abstract void Normalize();
        public abstract Bitmap GetResult();
    }
}
