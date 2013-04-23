using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SudoSolO
{
    class CaptureHandler : Handler
    {
        private int cameraId;
        ImageReceiver imageReceiver;
        Bitmap bmpPicture;

        public override void HandleRequest(Object subj, State request)
        {
            if (request == State.NONE)
            {
                //select camera to get real-time image from phone display
                CameraSelection cselect = new CameraSelection();
                DialogResult dresult = cselect.ShowDialog();
                if (dresult == DialogResult.OK)
                {
                    try
                    {
                        cameraId = cselect.camNumber;
                        imageReceiver = ImageReceiver.GetInstanceOnCamNumber(cameraId);
                    }
                    catch (NullReferenceException exception)
                    {
                        MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
                    //getStreamVideoFromWebCam();
                }
                else
                {
                    Application.Exit();
                }
            }
            else if (request == State.INITIALIZED)
            {
                Stack<ImageMemento> imageSaves = new Stack<ImageMemento>();
                imageReceiver = ImageReceiver.GetInstance();
                imageSaves.Push(imageReceiver.SaveImageState());
                bmpPicture = imageSaves.Pop().GetState();
            }
            else if (successor != null)
            {
                successor.HandleRequest(bmpPicture, State.CAPTURED);
            }
        }

    }
}
