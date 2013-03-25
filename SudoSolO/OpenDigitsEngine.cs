using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudoSolO
{
    class OpenDigitsEngine
    {
        private List<int>[,] potential;
        private Matrix field;
        private int size;

        public OpenDigitsEngine(Matrix m)
        {
            field = m;
            size = m.Size;
            potential = new List<int>[size, size];
        }

        public void Start()
        {
            do
            {
                calculatePotential();
            } while (putPotential());
        }

        private void calculatePotential()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    potential[i, j] = new List<int>();
                    if (field[i, j] != 0)
                        continue;
                    for (int digit = 1; digit <= size; digit++)
                        if (!isInRow(i, digit) && !isInColumn(j, digit) && !isInRegion(i, j, digit))
                            potential[i, j].Add(digit);
                }
        }

        private bool putPotential()
        {
            bool flag = false;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (potential[i, j] != null && potential[i, j].Count == 1)
                    {
                        field[i, j] = potential[i, j][0];
                        flag = true;
                    }
            return flag;
        }

        private bool isInRow(int row, int digit)
        {
            int[] arr = field.GetRow(row);
            foreach (int i in arr)
                if (i == digit)
                    return true;
            return false;
        }

        private bool isInColumn(int column, int digit)
        {
            int[] arr = field.GetColumn(column);
            foreach (int i in arr)
                if (i == digit)
                    return true;
            return false;
        }

        private bool isInRegion(int row, int column, int digit)
        {
            int[] arr = field.GetRegion(row, column);
            foreach (int i in arr)
                if (i == digit)
                    return true;
            return false;
        }
    }
}
