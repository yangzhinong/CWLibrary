using NAudio.Wave;
using System;
using System.Collections.Generic;

using System.Runtime.CompilerServices;
using C;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MorseLib
{
    //class Sc12: VolumeWaveProvider16
    //{

    //}

    
    public class MorseProvider16 : WaveProvider16
    {

        
        private string _text = "";
        private int _sampleRate = 8000;
        private int _decodeWaveIndex = 0; //解码索引
        public double Frequency { get; set; }

        private List<Range> _charsRange = new List<Range>();
        private MorseCode _morseCode = null;
        private CircularBuffer<short> _decodesBuf = new CircularBuffer<short>(16000);

        public delegate void PlayingCharIdxDelegate(string text, int charIndex);
        public delegate void PlayCompleteDelegate();

        public event PlayingCharIdxDelegate OnPlaying;
        public event PlayCompleteDelegate OnPlayEnd;

        /// <summary>
        /// 音量 0.0-1.0f
        /// </summary>
        public float Volume { get; set; }

        public int _baseDotMiSeconds;
        private double _baseDotSeconds { get; set; }
        /// <summary>
        /// 基本点占的时长(毫秒）
        /// </summary>
        public int BaseDotMiSeconds { get => _baseDotMiSeconds; set {
                _baseDotMiSeconds = value;
                _baseDotSeconds = ((double)value) / 1000;
            } }
        
        

        public MorseProvider16(string text):base(8000,1)
        {
            _text = text.ToLower();
            _sampleRate = this.WaveFormat.SampleRate;
            Volume = 1.0f;
            BaseDotMiSeconds = 50;
            Frequency = 1000;
            _morseCode = Utils.ConvertToMorseCode(_text);
        }
        public override int Read(short[] buffer, int offset, int sampleCount)
        {
            Stopwatch st = new Stopwatch();
            st.Start();
            int n = 0;
            while (_decodesBuf.Count< sampleCount && _decodeWaveIndex < _morseCode.Codes.Length)
            {
                switch (_morseCode.Codes[_decodeWaveIndex])
                {
                    case (' '):
                        for(var i=0; i< GetSilenceCount(_baseDotSeconds); i++)
                        {
                            _decodesBuf.PushBack(0);
                        }
                        break;
                    case '.':
                       foreach(var sh in GetDot())
                        {
                            _decodesBuf.PushBack(sh);
                        }
                        break;
                    case '-':
                        foreach(var sh in GetSinWave(3* _baseDotSeconds))
                        {
                            _decodesBuf.PushBack(sh);
                        }
                        break;
                }
                if (OnPlaying != null)
                {
                    for(var i=0; i< _morseCode.CharInCodesRange.Count; i++)
                    {
                        if (_decodeWaveIndex >= _morseCode.CharInCodesRange[i].Start) ;
                        OnPlaying.Invoke(_morseCode.Text, i);
                    }
                } 
                _decodeWaveIndex++;
                
            }
            if (_decodesBuf.Count> 0)
            {
                n = Math.Min(_decodesBuf.Count, sampleCount);
                for(int i=0; i< n; i++)
                {
                    buffer[i + offset] = _decodesBuf.PopFront();
                }
            }
            if (OnPlayEnd !=null &&  (n< sampleCount || (n == sampleCount && _decodeWaveIndex == _morseCode.Codes.Length)))
            {
                Task.Run(() =>
                {
                    System.Threading.Thread.Sleep(500);
                    OnPlayEnd.Invoke();
                });
                
            }
            st.Stop();
            Console.WriteLine( $"hao shi={st.ElapsedMilliseconds}");
            return n;
            
        }

        private short[] GetSinWave(double seconds)
        {
            short[] waveArray;
            int samples = (int)(_sampleRate * seconds);

            waveArray = new short[samples];

            for (int i = 0; i < samples; i++)
            {
                waveArray[i] = Convert.ToInt16( Volume * 32760 * Math.Sin(i * 2 * Math.PI * Frequency / _sampleRate));
            }

            return waveArray;
        }

        private short[] GetSilence(double seconds)
        {
            short[] waveArray;
            int samples = (int)(_sampleRate * seconds);

            waveArray = new short[samples];

            return waveArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetSilenceCount(double seconds)
        {
            return (int)(_sampleRate * seconds);
        }



        private short[] GetDot()
        {
            return GetSinWave(_baseDotSeconds);
        }
        private short[] GetInterEltSpace()
        {
            return GetSilence(_baseDotSeconds);
        }
        private short[] GetInterCharSpace()
        {
            return GetSilence(_baseDotSeconds *3);
        }
        private short[] GetInterWordSpace()
        {
            return GetSilence(_baseDotSeconds * 5);
        }

    }
}
