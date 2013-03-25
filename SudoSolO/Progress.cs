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
    public partial class Progress : Form
    {
        public Progress()
        {
            InitializeComponent();
        }
        private int percent;
        public int Percentage
        {
            get
            {
                return percent;
            }
            set 
            {
                percent = value;
            }
        }

        private void Progress_Paint(object sender, PaintEventArgs e)
        {
            progressBar1.Value = percent;
        }
    }
}
