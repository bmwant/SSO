using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace SudoSolO
{
    class BitmapCollection : IAbstractCollection
    {
        private ArrayList _items = new ArrayList();

        public BitmapIterator CreateIterator()
        {
            return new BitmapIterator(this);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public object this[int index]
        {
            get { return _items[index]; }
            set { _items.Add(value); }
        }
    }
}
