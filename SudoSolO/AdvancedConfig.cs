using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudoSolO
{
    class AdvancedConfig : Decorator
    {
        public override void SetGray(int min, int max)
        {
            config.SetGray(min, max);
        }
    }
}
