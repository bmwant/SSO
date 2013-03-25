using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace SudoSolO
{
    public partial class COMSelection : Form
    {
        public COMSelection()
        {
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
                portName = comboBox1.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("There is no devices, connected to COM-port\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                Application.Exit();
            }
            
        }

        public string portName;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            portName = comboBox1.SelectedItem.ToString();
        }

        
    }
}
