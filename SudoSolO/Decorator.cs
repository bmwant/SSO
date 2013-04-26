using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudoSolO
{
    abstract class Decorator : Config
    {
        protected Config config;

        public void SetConfig(Config config)
        {
            this.config = config;
        }

        public override void SetGray(int min, int max)
        {
            config.SetGray(20, 40);
        }
    }
}
