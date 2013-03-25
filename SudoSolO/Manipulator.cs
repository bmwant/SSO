using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;

namespace SudoSolO
{
    class Manipulator
    {
        SerialPort port;
        public Manipulator()
        {
            //add com-port selection menu
            port = new SerialPort("COM3", 9600);
            port.Open();
        }

        public void Send(string str)
        {
            port.Write(str);
        }

        public bool Upload(Matrix m)
        {
            for (int i = 0; i < m.Size; i++)
            {
                for (int j = 0; j < m.Size; j++)
                {
                    if (i % 2 == 0)
                    {
                        string str = m[i, j].ToString();
                        Thread.Sleep(300);
                        port.Write(str);

                        if (j != m.Size - 1)
                        {
                            Thread.Sleep(300);
                            port.Write("r");
                        }
                        if (j == m.Size - 1 && i != m.Size - 1)
                        {
                            Thread.Sleep(300);
                            port.Write("d");
                            //Thread.Sleep(300);
                        }
                    }

                    if (i % 2 == 1)
                    {
                        int k = m.Size - j - 1;
                        string str = m[i, k].ToString();
                        Thread.Sleep(300);
                        port.Write(str);

                        if (k != 0)
                        {
                            Thread.Sleep(300);
                            port.Write("l");
                        }
                        if (k == 0 && i != m.Size - 1)
                        {
                            Thread.Sleep(300);
                            port.Write("d");
                            //Thread.Sleep(300);
                        }
                    }
                }
            }
            return true;
        }

        ~Manipulator()
        {
            if (port.IsOpen)
            {
                port.Close();
            }
        }
    }
}
