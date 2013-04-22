using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudoSolO
{
    class BacktrackingAlgorithm:SolvingStrategy
    {
        public override void SolvingAlgorithm(Matrix field)
        {
            BacktrackingEngine engine3 = new BacktrackingEngine((Matrix)field);
            engine3.Start();
        }
    }
}
