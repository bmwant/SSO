using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudoSolO
{
    public partial class RecognitionAccepter : Form
    {
        int size;
        public int[,] field;
        public RecognitionAccepter(int sz)
        {
            InitializeComponent();
            size = sz;
                                                                                                                                                                                                    field = new int[6, 6] {{0,4,0,0,0,0},{0,0,6,0,0,2},{1,0,3,0,0,0},{0,0,0,3,0,0},{0,0,0,1,0,0}, {5,0,0,0,0,0},};
        }

        private void RecognitionAccepter_Load(object sender, EventArgs e)
        {
            NumericUpDown[,] nums = new NumericUpDown[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    nums[i, j] = new NumericUpDown();
                    nums[i, j].Location = new System.Drawing.Point(10 + 50*j, 10 + 30*i);
                    nums[i, j].Name = (i*size+j).ToString();
                    nums[i, j].Size = new System.Drawing.Size(30, 20);
                    nums[i, j].Maximum = size;
                    nums[i, j].ValueChanged += new System.EventHandler(this.numericUpDownAll_ValueChanged);
                    nums[i, j].Value = field[i, j];
                    this.Controls.Add(nums[i, j]);
                }
            }
        }

        private void numericUpDownAll_ValueChanged(object sender, EventArgs e)
        {
            int c = Int32.Parse(((NumericUpDown)sender).Name);
            int j = c%size;
            int i = c/size;
            field[i, j] = (int)((NumericUpDown)sender).Value;
        }
    }
}
