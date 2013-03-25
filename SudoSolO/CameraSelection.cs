using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DirectShowLib;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Util;
using Emgu.CV.Structure;

namespace SudoSolO
{
    public partial class CameraSelection : Form
    {

        Video_Device[] WebCams; //List containing all the camera available

        public CameraSelection()
        {
            InitializeComponent();

            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            WebCams = new Video_Device[_SystemCamereas.Length];
            for (int i = 0; i < _SystemCamereas.Length; i++)
            {
                WebCams[i] = new Video_Device(i, _SystemCamereas[i].Name, _SystemCamereas[i].ClassID); //fill web cam array
                comboBox1.Items.Add(WebCams[i].ToString());
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("There is no web-cam\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                Application.Exit();
            }
        }


        private int _camNumber;
        public int camNumber
        {
            get { return this._camNumber; }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _camNumber = comboBox1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _camNumber = comboBox1.SelectedIndex;
        }


    }

}
