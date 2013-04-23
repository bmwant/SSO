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
        private int boardSize;
        private int[,] tempMatrix;
        private Matrix gameField;
        private Bitmap bmpPicture;
        ManipulatorFacade manipulatorFacade;

        public Form1()
        {
            InitializeComponent();
            boardSize = 6;
            tempMatrix = new int[boardSize, boardSize];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //turn on the lights
            FlashLighter.RunWorkerAsync();

            Handler webCameraHandler = new CaptureHandler();
            webCameraHandler.HandleRequest(State.NONE);
            getStreamVideoFromWebCam();

            //select COM-port for phone manipulation
            COMSelection cmselect = new COMSelection();
            DialogResult dresult = cmselect.ShowDialog();
            if (dresult == DialogResult.OK)
            {
                manipulatorFacade = new ManipulatorFacade(cmselect.portName);
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
            Handler newCameraHandler = new CaptureHandler();
            newCameraHandler.HandleRequest(State.INITIALIZED);

            //bmpPicture.Save("c:/file.bmp");
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
            AbstractRecognitionFactory factory = new IntRecognition();
            Recognizer recognizer = factory.CreateSudokuRecognizer();
            ImageProcessor imageProcessor = new ImageProcessor(boardSize);


            List<Bitmap> digits = imageProcessor.Process(bmpPicture, Config.MinGray);
            BitmapCollection bitmapDigits = new BitmapCollection();
            for (int i = 0; i < boardSize * boardSize; i++)
            {
                bitmapDigits[i] = digits[i];
            }

            BitmapIterator bitIter = new BitmapIterator(bitmapDigits);
            int k = 0;
            for (Bitmap item = bitIter.First(); !bitIter.IsDone; item = bitIter.Next(), k++)
            {
                int i = k / boardSize;
                int j = k % boardSize;
                if (tempMatrix[i, j] == 0)
                {
                    tempMatrix[i, j] = recognizer.Recognize(item);
                }
            }

            this.Cursor = Cursors.Arrow;
            //after successful recognition turn on LED light on phone
            if (!FlashLighter.IsBusy)
                FlashLighter.RunWorkerAsync();

            //check and correct recognized digits
            RecognitionAccepter ra = new RecognitionAccepter(boardSize);
            ra.ShowDialog();

            //creates game field from recognized digits
            gameField = new Matrix(boardSize, ra.field);

            pictureBox6.Visible = true;
            button3.Enabled = true;
            //RecognitionWorker.RunWorkerAsync();
        }

        //SOLVE
        private void button3_Click(object sender, EventArgs e)
        {
            Solver solver = new Solver(gameField);

            if (solver.Solve())
            {
                pictureBox5.Visible = true;
            }
            //turn on the lights
            if (!FlashLighter.IsBusy)
                FlashLighter.RunWorkerAsync();
            button4.Enabled = true;
        }

        //UPLOAD
        private void button4_Click(object sender, EventArgs e)
        {
            FlashLighter.CancelAsync();
            Uploader.RunWorkerAsync();
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            manipulatorFacade.UploadGameField(gameField);
        }

        private void FlashLighter_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < Config.AwakePress; i++)
            {
                manipulatorFacade.TurnOnFlashLight();
                Thread.Sleep(278);
            }
        }

        private void RecognitionWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void RecognitionWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //prg = new Progress();
            //prg.ShowDialog();
        }


        private void button11_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                bmpPicture = new Bitmap(openFileDialog1.FileName);
            }
            pictureBox2.Image = bmpPicture;
            button2.Enabled = true;
        }
    }
}
