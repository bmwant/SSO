using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudoSolO
{
    class ManipulatorFacade
    {
        private Manipulator manipulator;
        public ManipulatorFacade(string portName)
        {
            this.manipulator = new Manipulator(portName);
        }

        public void Up()
        {
            manipulator.Send("u");
        }
        public void Down()
        {
            manipulator.Send("d");
        }
        public void Right()
        {
            manipulator.Send("r");
        }
        public void Left()
        {
            manipulator.Send("l");
        }
        public void LeftSoft()
        {
            manipulator.Send("f");
        }
        public void RightSoft()
        {
            manipulator.Send("k");
        }
        public void TurnOnFlashLight()
        {
            manipulator.Send("0");
        }
        public void UploadGameField(Matrix gameField)
        {
            manipulator.Upload(gameField);
        }

    }
}
