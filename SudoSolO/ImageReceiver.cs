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
    //Singleton
    class ImageReceiver
    {
        private static ImageReceiver _instance;
        public static ImageReceiver GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                throw new NullReferenceException("There is no image receiver instance");
            }
        }

        //on first use
        public static ImageReceiver GetInstanceOnCamNumber(int camNumber)
        {
            if (_instance == null)
            {
                _instance = new ImageReceiver(camNumber);
            }
            return _instance;
        }

        private Capture capture;
        private ImageViewer viewer;
        private Image<Bgr, Byte> img;
        
        protected ImageReceiver(int camNumber)
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

        public ImageMemento SaveImageState()
        {
            return new ImageMemento(this.GetImage());
        }
    }
}
