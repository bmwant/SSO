using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using DirectShowLib;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Util;
using Emgu.CV.Structure;

using MODI;

namespace SudoSolO
{
    public partial class Form1 : Form
    {
        
        ManipulatorFacade manipulatorFacade;
        Handler startingHandler;
        Handler recognitionHandler;
        Handler solvingHandler;
        Handler uploadingHandler;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //turn on the lights
            FlashLighter.RunWorkerAsync();

            //Create a chain of responsibility for process
            startingHandler = new CaptureHandler();
            recognitionHandler = new RecognizingHandler();
            solvingHandler = new SolvingHandler();
            uploadingHandler = new UploadingHandler();

            //Set chain sequence
            startingHandler.SetSuccessor(recognitionHandler);
            recognitionHandler.SetSuccessor(solvingHandler);
            solvingHandler.SetSuccessor(uploadingHandler);

            Handler webCameraHandler = new CaptureHandler();
            webCameraHandler.HandleRequest(null, State.NONE);
            getStreamVideoFromWebCam();

            //select COM-port for real time phone manipulation
            COMSelection cmselect = new COMSelection();
            DialogResult dresult = cmselect.ShowDialog();
            if (dresult == DialogResult.OK)
            {
                manipulatorFacade = new ManipulatorFacade(cmselect.portName);
                Config.COMPort = cmselect.portName;
            }
            else
            {
                this.Close();
            }
        }

        private void getStreamVideoFromWebCam()
        {
            //Get image from web-cam only if proxy ensures that there is appropriate
            ImageReceiverProxy imgReceiver = new ImageReceiverProxy();
            Application.Idle += new EventHandler(delegate(object sender, EventArgs e)
            {
                pictureBox7.Image = imgReceiver.GetProxiedImage();
            });
        }
        
        //CAPTURE   
        private void button1_Click(object sender, EventArgs e)
        {
            startingHandler.HandleRequest(null, State.INITIALIZED);
            button2.Enabled = true;
            pictureBox2.Visible = true;
            pictureBox1.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
        }

        
        //RECOGNIZE
        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Cursor = Cursors.WaitCursor;

            startingHandler.HandleRequest(null, State.CAPTURED);
            
            this.Cursor = Cursors.Arrow;
            //after successful recognition turn on LED light on phone
            if (!FlashLighter.IsBusy)
                FlashLighter.RunWorkerAsync();

            pictureBox6.Visible = true;
            button3.Enabled = true;
        }

        //SOLVE
        private void button3_Click(object sender, EventArgs e)
        {
            startingHandler.HandleRequest(null, State.RECOGNIZED);
            pictureBox5.Visible = true;

            //turn on the lights
            if (!FlashLighter.IsBusy)
                FlashLighter.RunWorkerAsync();
            button4.Enabled = true;
        }

        //UPLOAD
        private void button4_Click(object sender, EventArgs e)
        {
            FlashLighter.CancelAsync();
            startingHandler.HandleRequest(null, State.SOLVED);
            pictureBox1.Visible = true;
        }

        //control section
        private void button5_Click(object sender, EventArgs e)
        {
            manipulatorFacade.LeftSoft();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            manipulatorFacade.RightSoft();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            manipulatorFacade.Up();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            manipulatorFacade.Left();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            manipulatorFacade.Right();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            manipulatorFacade.Down();
        }

        
        private void FlashLighter_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < Config.AwakePress; i++)
            {
                manipulatorFacade.TurnOnFlashLight();
                Thread.Sleep(278);
            }
        }
    }
}
