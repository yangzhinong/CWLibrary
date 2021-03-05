using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorseLib
{
    public class Range
    {
        public int Start { get; set; }

        public int End { get; set; }

        public Range(int start, int end)
        {
            Start = start;
            End = end;
        }
        public int Count()
        {
            return End - Start;
        }
    }
    public class MorseCode
    {
        /// <summary>
        /// 原文
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// MorseCode
        /// </summary>
        public string Codes { get; set; }


        /// <summary>
        /// 每个字符在Codes的范围（包含每个字符间的尾随3个空长度）
        /// </summary>
        public List<Range> CharInCodesRange { get; set; }
        /// <summary>
        /// 音节在Codes的范围
        /// </summary>
        public List<Range> ToneInCodesRange { get; set; }

        public MorseCode()
        {
            CharInCodesRange = new List<Range>();
            ToneInCodesRange = new List<Range>();
        }
    }
}
