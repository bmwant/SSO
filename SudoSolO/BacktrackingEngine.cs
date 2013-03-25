using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudoSolO
{
    class BacktrackingEngine
    {
        private Matrix field;
        private int size;
        private Stack<Matrix> mStack;

        public BacktrackingEngine(Matrix m)
        {
            field = m;
            size = m.Size;
            mStack = new Stack<Matrix>();
        }

        public void Start()
        {
            mStack.Push(field);
            while (mStack.Count != 0)
            {
                Matrix m = mStack.Pop();

                if (isFull(m))
                {
                    copyToField(m);
                    break;
                }
                
                for (int i = 0; i < size; i++)
                {
                    bool flag = false;
                    for (int j = 0; j < size; j++)
                    {
                        if (m[i, j] == 0)
                        {
                            for (int digit = 1; digit <= size; digit++)
                            {
                                if (canBeThere(m, i, j, digit))
                                {
                                    Matrix k = Matrix.DeepClone(m);
                                    k[i, j] = digit;
                                    mStack.Push(k);
                                }
                            }
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                        break;
                }
            }
        }


        private void copyToField(Matrix m)
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    field[i, j] = m[i, j];
        }

        private bool canBeThere(Matrix m, int row, int column, int digit)
        {
            int[] temp = m.GetRow(row);
            foreach (int i in temp)
                if (i == digit)
                    return false;
            temp = m.GetColumn(column);
            foreach (int i in temp)
                if (i == digit)
                    return false;
            temp = m.GetRegion(row, column);
            foreach (int i in temp)
                if (i == digit)
                    return false;

            return true;
        }

        private bool isFull(Matrix m)
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (m[i, j] == 0)
                        return false;
            return true;
        }
    }
}
