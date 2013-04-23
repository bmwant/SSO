using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudoSolO
{
    class SolvingHandler : Handler
    {
        Matrix gameField;
        public override void HandleRequest(Object subject, State request)
        {
            if (request == State.RECOGNIZED)
            {
                gameField = (Matrix)subject;
                Solver solver = new Solver(gameField);
                solver.Solve();
            }
            else if (successor != null)
            {
                successor.HandleRequest(gameField, State.SOLVED);
            }
        }
    }
}
