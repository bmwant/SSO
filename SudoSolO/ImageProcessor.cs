using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;

namespace SudoSolO
{
    class ImageProcessor
    {
        private int size;
        private List<Bitmap> digits;
        public ImageProcessor(int size)
        {
            digits = new List<Bitmap>();
            this.size = size;
        }

        public List<Bitmap> Process(Bitmap bmp, int val)
        { 
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Image<Bgr, Byte> img = new Image<Bgr,byte>(bmp);
                    //creates little square pictures of digits
                    Rectangle rect = new Rectangle(j * Config.PicSize + Config.Epsilon, i * Config.PicSize + Config.Epsilon, Config.PicSize - 2*Config.Epsilon, Config.PicSize - 2*Config.Epsilon);
                    img.ROI = rect;
                    Image<Gray, Byte> newImg = img.Convert<Gray, byte>(); //to grayscale
                    newImg = newImg.Not(); //invert colors
                    //normalize to b/w picture
                    newImg = newImg.ThresholdBinary(new Gray(val), new Gray(Config.MaxGray));
                    digits.Add(newImg.ToBitmap());
                }
            }
            return digits;
        }
    }
}
