using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SudoSolO
{
    class BitmapIterator : IAbstractIterator
    {
        private BitmapCollection _collection;
        private int _current = 0;
        private int _step = 1;

        public BitmapIterator(BitmapCollection collection)
        {
            _collection = collection;
        }

        public Bitmap First()
        {
            _current = 0;
            return _collection[_current] as Bitmap;
        }

        public Bitmap Next()
        {
            _current += _step;
            if (!IsDone)
                return _collection[_current] as Bitmap;
            else
                return null;
        }

        public int Step
        {
            get { return _step; }
            set { _step = value; }
        }

        public Bitmap CurrentItem
        {
            get { return _collection[_current] as Bitmap; }
        }

        public bool IsDone
        {
            get { return _current >= _collection.Count; }
        }
    }
}
