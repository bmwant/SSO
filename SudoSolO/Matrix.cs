using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SudoSolO
{
    [Serializable]
    class Matrix
    {
        private int [ , ] field;
        private int size;

        public Matrix()
        { 
        
        }

        //creates an empty size x size matrix
        public Matrix(int _size)
        {
            size = _size;
            field = new int[size, size];

        }

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public int Size { get { return this.size; } }

        //create matrix from two-dimensional squared array
        public Matrix(int sz, int[,] array)
        {
            size = sz;
            field = array;
        }

        //indexer
        public int this[int row, int column]
        {
            get
            {
                if (row >= 0 && row < size && column >= 0 && column < size)
                {
                    return field[row, column];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }

            set
            {
                if (row >= 0 && row < size && column >= 0 && column < size)
                {
                    field[row, column] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public int[] GetRow(int row)
        {
            if (row < 0 || row >= size)
            {
                throw new IndexOutOfRangeException();
            }
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = field[row, i];
            }
            return arr;
        }

        public int[] GetColumn(int column)
        {
            if (column < 0 || column >= size)
            {
                throw new IndexOutOfRangeException();
            }
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = field[i, column];
            }
            return arr;
        }

        public int[] GetRegion(int row, int column)
        {
            if (row < 0 || row >= size || column < 0 || column >= size)
            {
                throw new IndexOutOfRangeException();
            }

            int[] arr = new int[size];

            //for 6x6

            int _scol = column < 3 ? 0 : 3;
            int _ecol = _scol + 3;
            int _srow = (row / 2) * 2;
            int _erow = _srow + 2;

            int k = 0;
            for (int i = _srow; i < _erow; i++)
            {
                for (int j = _scol; j < _ecol; j++)
                {
                    arr[k++] = field[i, j];
                }
            }
            return arr;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    str += field[i, j] + (j == size - 1 ? "" : " ");
                }
                str += "\n";
            }
            return str;
        }
    }
}
