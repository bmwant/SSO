using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SudoSolO
{
    class ImageMemento
    {
        private readonly Bitmap _pictureState;
        public ImageMemento(Bitmap image)
        {
            _pictureState = image;
        }
        public Bitmap GetState()
        {
            return _pictureState;
        }
    }
}
