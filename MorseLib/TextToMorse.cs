using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MorseLib
{
    public class TextToMorse
    {
        public const int DEFAULT_SAMPLE_RATE = 8000;//11025
        // Character speed in WPM
        public int CharacterSpeed { get; set; }
        // Overall speed in WPM (must be <= character speed)
        public int WordSpeed { get; set; }
        // Tone frequency
        public double Frequency { get; set; }

        

        public TextToMorse(int charSpeed, int wordSpeed, double frequency)
        {
            CharacterSpeed = charSpeed;
            WordSpeed = wordSpeed;
            Frequency = frequency;
        }

        public TextToMorse(int charSpeed, int wordSpeed) : this(charSpeed, wordSpeed, 600.0) { }
        public TextToMorse(int wpm) : this(wpm, wpm) { }
        public TextToMorse() : this(20) { }

        // Return given number of seconds of sine wave
        private short[] GetWave(double seconds)
        {
            short[] waveArray;
            int samples = (int)(DEFAULT_SAMPLE_RATE * seconds);
            
            waveArray = new short[samples];

            for (int i = 0; i < samples; i++)
            {
                waveArray[i] = Convert.ToInt16(32760 * Math.Sin(i * 2 * Math.PI * Frequency / DEFAULT_SAMPLE_RATE));
            }

            return waveArray;
        }

        // Return given number of seconds of flatline. This could also be
        // achieved with slnt chunks inside a wavl chunk, but the resulting
        // file might not be universally readable. If saving space is that
        // important, it would be better to compress the output as mp3 or ogg
        // anyway.
        private short[] GetSilence(double seconds)
        {
            short[] waveArray;
            int samples = (int)(11025 * seconds);

            waveArray = new short[samples];

            return waveArray;
        }

        // Dot -- 1 unit long
        private short[] GetDot()
        {
            return GetWave(1.2 / CharacterSpeed);
        }

        // Dash -- 3 units long
        private short[] GetDash()
        {
            return GetWave(3.6 / CharacterSpeed);
        }

        // Inter-element space -- 1 unit long
        private short[] GetInterEltSpace()
        {
            return GetSilence(1.2 / CharacterSpeed);
        }

        // Space between letters -- nominally 3 units, but adjusted for
        // Farnsworth timing (if word speed is lower than character
        // speed) based on ARRL's Morse code timing standard:
        // http://www.arrl.org/files/file/Technology/x9004008.pdf
        private short[] GetInterCharSpace()
        {
            double delay = (60.0 / WordSpeed) - (32.0 / CharacterSpeed);
            double spaceLength = 3 * delay / 19;
            return GetSilence(spaceLength);
        }

        // Space between words -- nominally 7 units, but adjusted for
        // Farnsworth timing in case word speed is lower than character
        // speed.
        private short[] GetInterWordSpace()
        {
            double delay = (60.0 / WordSpeed) - (32.0 / CharacterSpeed);
            double spaceLength = 7 * delay / 19;
            return GetSilence(spaceLength);
        }

        // Return a single character as a waveform
        private short[] GetCharacter(string character)
        {
            short[] space = GetInterEltSpace();
            short[] dot = GetDot();
            short[] dash = GetDash();
            List<short> morseChar = new List<short>();

            string morseSymbol = Characters.Symbols[character];
            for (int i = 0; i < morseSymbol.Length; i++)
            {
                if (i > 0)
                    morseChar.AddRange(space);
                if (morseSymbol[i] == '-')
                    morseChar.AddRange(dash);
                else if (morseSymbol[i] == '.')
                    morseChar.AddRange(dot);
            }

            return morseChar.ToArray<short>();
        }

        // Return a word as a waveform
        private short[] GetWord(string word)
        {
            List<short> data = new List<short>();

            for (int i = 0; i < word.Length; i++)
            {
                if (i > 0)
                    data.AddRange(GetInterCharSpace());
                if (word[i] == '<')
                {
                    // Prosign
                    int end = word.IndexOf('>', i);
                    if (end < 0)
                        throw new ArgumentException();
                    data.AddRange(GetCharacter(word.Substring(i, end + 1 - i)));
                    i = end;
                }
                else
                {
                    data.AddRange(GetCharacter(word[i].ToString()));
                }
            }

            return data.ToArray<short>();
        }

        // Return a string (lower case text only, unrecognized characters
        // throw an exception -- see Characters.cs for the list of recognized
        // characters) as a waveform wrapped in a DataChunk, ready to by added
        // to a wave file.
        public short[] GetPCM(string text)
        {

            text = text.ToLower();
            List<short> data = new List<short>();

            string[] words = text.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (i > 0)
                    data.AddRange(GetInterWordSpace());
                data.AddRange(GetWord(words[i]));
            }

            // Pad the end with a little bit of silence. Otherwise the last
            // character may sound funny in some media players.
            data.AddRange(GetInterCharSpace());

           return data.ToArray();
        }

       
    }
}
