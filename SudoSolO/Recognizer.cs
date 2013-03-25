using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SudoSolO
{
    class Recognizer
    {
        private int recognizedDigit;
        private static int number = 0;
        public int Recognize(Bitmap bmpFile)
        {
            Bitmap bmpNew = new Bitmap(200, 200);
            Graphics gfx = Graphics.FromImage(bmpNew);
            gfx.FillRectangle(Brushes.White, 0, 0, 200, 200);
            gfx.DrawImage(bmpFile, 100, 100, bmpFile.Width, bmpFile.Height);
            string fileName = string.Format("{0}.tiff", number++);
            bmpNew.Save(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
			MODI.Document md = new MODI.Document();
            try
            {
                md.Create(fileName);
                md.OCR(MODI.MiLANGUAGES.miLANG_ENGLISH, true, true);
            }
            catch (Exception e)
            {
                //if cannot recognize there is must be empty image
                return 0;
            }

            MODI.Image image = (MODI.Image)md.Images[0];
            try
            {
                recognizedDigit = Int32.Parse(image.Layout.Text);
            }
            catch (Exception e)
            {
                return 0;
            }
            return recognizedDigit;
        }

        ~Recognizer()
        {
            for (int i = 0; i < number; i++)
            {
                //File.Delete(string.Format("{0}.tiff", i));
            }
        }

        
    }
}
