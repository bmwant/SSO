using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SudoSolO
{
    class RecognizingHandler : Handler
    {
        private int[,] tempMatrix;
        Matrix gameField;
        public override void HandleRequest(Object subject, State request)
        {
            if (request == State.CAPTURED)
            {
                tempMatrix = new int[Config.Size, Config.Size];
                Bitmap bmpPicture = (Bitmap)subject;
                AbstractRecognitionFactory factory = new IntRecognition();
                Recognizer recognizer = factory.CreateSudokuRecognizer();
                ImageProcessor imageProcessor = new ImageProcessor(Config.Size);


                List<Bitmap> digits = imageProcessor.Process(bmpPicture, Config.MinGray);
                BitmapCollection bitmapDigits = new BitmapCollection();
                for (int i = 0; i < Config.Size * Config.Size; i++)
                {
                    bitmapDigits[i] = digits[i];
                }

                BitmapIterator bitIter = new BitmapIterator(bitmapDigits);
                int k = 0;
                for (Bitmap item = bitIter.First(); !bitIter.IsDone; item = bitIter.Next(), k++)
                {
                    int i = k / Config.Size;
                    int j = k % Config.Size;
                    if (tempMatrix[i, j] == 0)
                    {
                        tempMatrix[i, j] = recognizer.Recognize(item);
                    }
                }
                gameField = new Matrix(Config.Size, tempMatrix);
            }
            else if (successor != null)
            {
                successor.HandleRequest(gameField, State.RECOGNIZED);
            }
        }
    }
}
