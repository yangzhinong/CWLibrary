using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (_waveOut != null)
            {
                _waveOut.Stop();
                _waveOut.Dispose();
                _waveOut = null;
            }
            TxtDotLine.Text = MorseLib.Utils.ConvertToMorseCode(Txt1.Text).Codes;
            _wavPprovider16 = new MorseLib.MorseProvider16(Txt1.Text);
            _wavPprovider16.OnPlaying += Provider16_OnPlaying;
            
            _waveOut = new WaveOut();
            _waveOut.Init(_wavPprovider16);
            _waveOut.Play();

            _waveOut.PlaybackStopped += WaveOut_PlaybackStopped;

        }

        private void WaveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            LblPlayingTip.Text = $"播放完毕";
            Txt1.Select(Txt1.Text.Length, 1);
        }

        private void Provider16_OnPlaying(string text, int charIndex)
        {
            Debug.WriteLine($"正在播第{charIndex}个字符");

            this.Invoke(new Action(() =>
            {
                Txt1.Select(charIndex, 1);
                Txt1.Focus();
                LblPlayingTip.Text = $"正在播第{charIndex}个字符";
            }));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
