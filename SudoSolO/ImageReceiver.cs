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
    class ImageReceiver
    {
        private Capture capture;
        private ImageViewer viewer;
        private Image<Bgr, Byte> img;

        public ImageReceiver(int camNumber)
        {
            viewer  = new ImageViewer();
            capture = new Capture(camNumber);

            if (viewer == null || capture == null)
            {
                throw new NullReferenceException("Cannot initialize web-cam.");
            }
        }
        
        public Bitmap GetImage()
        {
            viewer.Image = capture.QueryFrame();
            img = (Image<Bgr, Byte>)viewer.Image;
            img = img.Rotate(Config.Angle, new Bgr(255, 255, 255), false);
            img.ROI = Config.Rect; 
            return img.ToBitmap();
        }
    }
}
