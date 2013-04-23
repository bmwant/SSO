using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudoSolO
{
    class UploadingHandler : Handler
    {
        Matrix gameField;
        ManipulatorFacade manipulatorFacade;

        public override void HandleRequest(Object subject, State request)
        {
            if (request == State.SOLVED)
            {
                manipulatorFacade = new ManipulatorFacade(Config.COMPort);
                gameField = (Matrix)subject;
                Solver solver = new Solver(gameField);
                solver.Solve();
                manipulatorFacade.UploadGameField(gameField);
            }
        }
    }
}
