using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestInWinForm
{
    public partial class Form1 : Form
    {
        WaveOut _waveOut = null;
        MorseLib.MorseProvider16 _wavPprovider16;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_waveOut != null)
            {
                _waveOut.Stop();
                _waveOut.Dispose();
                _waveOut = null;
            }
            _wavPprovider16 = new MorseLib.MorseProvider16("CW 1234 123");

            _waveOut = new WaveOut();
            _waveOut.Init(_wavPprovider16);
            _waveOut.Play();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
