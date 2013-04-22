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

        ImageReceiver imageReceiver;
        Manipulator manipulator;

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

            //select camera to get real-time image from phone display
            CameraSelection cselect = new CameraSelection();
            DialogResult dresult = cselect.ShowDialog();
            if (dresult == DialogResult.OK)
            {
                try
                {
                    imageReceiver = ImageReceiver.GetInstanceOnCamNumber(cselect.camNumber);
                }
                catch (NullReferenceException exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                getStreamVideoFromWebCam();
            }
            else
            {
                this.Close();
            }

            //select COM-port for phone manipulation
            COMSelection cmselect = new COMSelection();
            dresult = cmselect.ShowDialog();
            if (dresult == DialogResult.OK)
            {
                manipulator = new Manipulator(cmselect.portName);
            }
            else
            {
                this.Close();
            }
        }

        private void getStreamVideoFromWebCam()
        {
            Application.Idle += new EventHandler(delegate(object sender, EventArgs e)
            {
                pictureBox7.Image = imageReceiver.GetImage();
            });
        }
        
        //CAPTURE   
        private void button1_Click(object sender, EventArgs e)
        {
            bmpPicture = imageReceiver.GetImage();
            pictureBox2.Visible = true;
            bmpPicture.Save("c:/file.bmp");
            button2.Enabled = true;
            pictureBox1.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
        }

        
        //RECOGNIZE
        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Cursor = Cursors.WaitCursor;
                Recognizer recognizer = new Recognizer();
                ImageProcessor imageProcessor = new ImageProcessor(boardSize);
                List<Bitmap> digits = imageProcessor.Process(bmpPicture, Config.MinGray);
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        if(tempMatrix[i, j] == 0)
                            tempMatrix[i, j] = recognizer.Recognize(digits[i * boardSize + j]);
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
            manipulator.Send("f");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            manipulator.Send("k");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            manipulator.Send("u");

        }

        private void button8_Click(object sender, EventArgs e)
        {
            manipulator.Send("l");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            manipulator.Send("r");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            manipulator.Send("d");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            manipulator.Upload(gameField);
        }

        private void FlashLighter_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < Config.AwakePress; i++)
            {
                manipulator.Send("0");
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
