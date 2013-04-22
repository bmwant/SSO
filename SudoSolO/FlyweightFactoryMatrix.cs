using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudoSolO
{
    class FlyweightFactoryMatrix
    {
        private Hashtable flyweights = new Hashtable();

        public FlyweightFactoryMatrix()
        {
            flyweights.Add(6, new Matrix(6));
            flyweights.Add(9, new Matrix(9));
            flyweights.Add(4, new Matrix(4));
        }

        public FlyweightMatrix GetFlyweight(string key)
        {
            return ((FlyweightMatrix)this.flyweights[key]);
        }
    }
}
