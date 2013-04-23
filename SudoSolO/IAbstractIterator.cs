using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SudoSolO
{
    interface IAbstractIterator
    {
        Bitmap First();
        Bitmap Next();
        bool IsDone { get; }
        Bitmap CurrentItem { get; }
    }
}
