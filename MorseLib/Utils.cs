using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorseLib
{
    public class Utils
    {

        public const string SPACE3 = "   ";
        public const string SPACE1 = " ";
        public const string SPACE5 = "     ";
        public static MorseCode ConvertToMorseCode(string text)
        {

            text = text.ToLower();

            var words = text.Split(' ');

            var ret = new MorseCode
            {
                Text = text
            };

            var sb = new StringBuilder();
            for (var i = 0; i < words.Length; i++)
            {
                if (i > 0)
                {
                    var r = new Range(sb.Length, sb.Length);
                    sb.Append(SPACE5);
                    r.End = sb.Length;
                    ret.ToneInCodesRange.Add(r);
                    ret.CharInCodesRange.Add(r);

                }
                GetWord(words[i],  ref ret, ref sb);
            }

            ret.Codes = sb.ToString();
            return ret;

        }

        private static void GetWord(string word,  ref MorseCode morseCode, ref StringBuilder sb)
        {
            
            for (int i = 0; i < word.Length; i++)
            {
                
                if (i > 0)
                {
                    var r = new Range(sb.Length, sb.Length);
                    sb.Append(SPACE3);
                    r.End = sb.Length;
                    morseCode.ToneInCodesRange.Add(r);
                }
                if (word == "")
                {
  
                    var r = new Range(sb.Length, sb.Length);

                    sb.Append(SPACE5);

                    r.End = sb.Length;
                   
                    morseCode.ToneInCodesRange.Add(r);
                    morseCode.CharInCodesRange.Add(r);
                } else if (word[i] == '<')
                {
                    // Prosign
                    int end = word.IndexOf('>', i);
                    if (end < 0)
                        throw new ArgumentException();
                    var r = new Range(sb.Length, sb.Length);
                    GetCharacter(word.Substring(i, end + 1 - i),ref morseCode,ref sb);
                    r.End = sb.Length;
                    morseCode.CharInCodesRange.Add(r);
                    i = end;
                }
                else
                {
                    var r = new Range(sb.Length, sb.Length);
                    GetCharacter(word[i].ToString(), ref morseCode,ref  sb);
                    r.End = sb.Length;
                    if (word.Length > 1 && i != word.Length - 1) //不是最后一个字符
                    {
                        r.End += 3;
                    }
                    morseCode.CharInCodesRange.Add(r);
                }
            }
        }

        private static void GetCharacter(string character, ref MorseCode morseCode, ref StringBuilder sb)
        {
            string morseSymbol = Characters.Symbols[character];

            for (int i = 0; i < morseSymbol.Length; i++)
            {
                if (i > 0)
                {
                    var r = new Range(sb.Length, sb.Length);
                    sb.Append(SPACE1);
                    r.End = sb.Length;
                    morseCode.ToneInCodesRange.Add(r);
                }
                if (morseSymbol[i] == '-')
                {
                    var r = new Range(sb.Length, sb.Length);
                    sb.Append('-');
                    r.End = sb.Length;
                    morseCode.ToneInCodesRange.Add(r);

                }
                else if (morseSymbol[i] == '.')
                {
                    var r = new Range(sb.Length, sb.Length);
                    sb.Append('.');
                    r.End = sb.Length;
                    morseCode.ToneInCodesRange.Add(r);
                }
            }

        }

    }
}
