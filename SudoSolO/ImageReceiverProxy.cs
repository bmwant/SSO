using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SudoSolO
{
    //Proxy connection to web-camera
    class ImageReceiverProxy
    {
        private ImageReceiver imgReceiver;

        public ImageReceiverProxy()
        {
            imgReceiver = ImageReceiver.GetInstance();
        }

        private void EnsureConnectedWebCam()
        {
            if (ImageReceiver.GetInstance() == null)
            {
                throw new NullReferenceException("There is no web-camera instance to connect");
            }
        }

        public virtual Bitmap GetProxiedImage()
        {
            EnsureConnectedWebCam();
            return imgReceiver.GetImage();
        }
    }
}
