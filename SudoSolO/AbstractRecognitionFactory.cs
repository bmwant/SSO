using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudoSolO
{
    abstract class AbstractRecognitionFactory
    {
        public abstract Recognizer CreateSudokuRecognizer();
        public abstract TicTacToeRecognizer CreateTicTacToeRecognizer();
    }
}
