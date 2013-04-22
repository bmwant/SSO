using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudoSolO
{
    abstract class FlyweightMatrix
    {
        protected int size;
        abstract public int[] GetRow(int row);
        abstract public int[] GetColumn(int column);
        abstract public int[] GetRegion(int row, int column);
        abstract public void SetField(int[,] array);
    }
}
