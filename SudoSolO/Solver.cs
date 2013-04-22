using System;
using System.Collections.Generic;
using System.Text;

namespace SudoSolO
{
    class Solver
    {
        private int size;
        private FlyweightFactoryMatrix Factory = new FlyweightFactoryMatrix();
        private FlyweightMatrix field;
        

        //creates a solver from a two-dimensional array
        public Solver(int[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new Exception("Not squared matrix");
            }
            size = matrix.GetLength(0);
            field = Factory.GetFlyweight(size.ToString());
            field.SetField(matrix);
            
        }

        public Solver(Matrix m)
        {
            field = m;
            size = m.Size;
        }

        public bool Solve()
        {
            //open digits method
            OpenDigitsEngine engine1 = new OpenDigitsEngine((Matrix)field);
            engine1.Start();
            if (correctSolution())
            {
                return true;
            }
            //hidden digits method

            //backtracking method
            BacktrackingEngine engine3 = new BacktrackingEngine((Matrix)field);
            engine3.Start();
            if (correctSolution())
            {
                return true;
            }

            if (!correctSolution())
            {
                throw new Exception("Cannot solve current sudoku or incorrect input.");
            }
            return false;
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

        private bool isFullBoard(ref Matrix m)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (m[i, j] == 0)
                        return false;
                }
            }
            return true;
        }

        //check if current solution response to sudoku rules
        private bool correctSolution()
        {
            List<int> correct = new List<int>();
            for (int i = 0; i < size; i++)
                correct.Add(i + 1);

            for (int i = 0; i < size; i++)
            {
                //correct column
                List<int> temp = new List<int>(field.GetColumn(i));
                temp.Sort();
                if (!equalArr(temp, correct))
                    return false;
                //correct row
                temp = new List<int>(field.GetRow(i));
                temp.Sort();
                if (!equalArr(temp, correct))
                    return false;
                //correct zone
                temp = new List<int>(field.GetRegion((i/2)*2, (i%2 == 0 ? 0 : 3)));
                temp.Sort();
                if (!equalArr(temp, correct))
                    return false;
            }
            return true;
        }


        //compare two sorted arrays
        private bool equalArr(List<int> one, List<int> another)
        {
            if (one.Count != another.Count)
            {
                return false;
            }
            for (int i = 0; i < one.Count; i++)
            {
                if (one[i] != another[i])
                {
                    return false;
                }
            }
            return true;        
        }

        public override string ToString()
        {
            string str = "Current sudoku field:\n" + field.ToString();
            return str;
        }
    }
}
