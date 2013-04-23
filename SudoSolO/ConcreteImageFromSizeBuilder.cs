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
    class ConcreteImageFromSizeBuilder : ImageBuilder
    {
        private Image<Bgr, Byte> result;
        private Image<Gray, Byte> newImg;
        private Bitmap bmp;

        public ConcreteImageFromSizeBuilder(Bitmap bmp)
        {
            this.bmp = bmp;
        }

        public override void Crop(int i, int j)
        {
            Image<Bgr, Byte> img = new Image<Bgr, byte>(bmp);
            //creates little square pictures of digits
            Rectangle rect = new Rectangle(j * Config.PicSize + Config.Epsilon, i * Config.PicSize + Config.Epsilon, Config.PicSize - 2 * Config.Epsilon, Config.PicSize - 2 * Config.Epsilon);
            img.ROI = rect;
            result = img;
        }

        public override void Invert()
        {
            Image<Gray, Byte> newImg = result.Convert<Gray, byte>(); //to grayscale
            newImg = newImg.Not(); //invert colors
        }

        public override void Normalize()
        {
            newImg = newImg.ThresholdBinary(new Gray(Config.MinGray), new Gray(Config.MaxGray));
        }

        public override Bitmap GetResult()
        {
            return newImg.ToBitmap();
        }
    }
}
