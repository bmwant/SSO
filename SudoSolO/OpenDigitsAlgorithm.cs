using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudoSolO
{
    class OpenDigitsAlgorithm: SolvingStrategy
    {
        public override void SolvingAlgorithm(Matrix field)
        {
            OpenDigitsEngine engine1 = new OpenDigitsEngine((Matrix)field);
            engine1.Start();
        }
    }
}
