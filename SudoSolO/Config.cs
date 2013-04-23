using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SudoSolO
{
    class Config
    {
        public static double Angle = 0;
        public static Rectangle Rect;
        public static int PicSize = 40;
        public static int Epsilon = 8;
        public static int MinGray = 41;
        public static int MaxGray = 255;
        public static int ImageSize = 240;
        public static int CaptureTrying = 3;
        public static int AwakePress = 13;
        public static int Size = 6;

        public static string COMPort {get; set;}

        static Config()
        {
            Rect = new Rectangle(18, 0, ImageSize, ImageSize);
        }
    }
}
