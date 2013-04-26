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
            Config config = new Config();
            Decorator newConfig = new AdvancedConfig();
            newConfig.SetConfig(config);
            newConfig.SetGray(val, val + bmp.Size.Width);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    ConcreteImageFromSizeBuilder imageBuilder = new ConcreteImageFromSizeBuilder(bmp);
                    imageBuilder.Crop(i, j);
                    imageBuilder.Invert();
                    imageBuilder.Normalize();
                    digits.Add(imageBuilder.GetResult());
                }
            }
            return digits;
        }
    }
}
