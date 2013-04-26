using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudoSolO
{
    class IntRecognition : AbstractRecognitionFactory
    {
        public override Recognizer CreateSudokuRecognizer()
        {
            return new Recognizer();
        }
        public override TicTacToeRecognizer CreateTicTacToeRecognizer()
        {
            return new TicTacToeRecognizer();
        }

    }
}
