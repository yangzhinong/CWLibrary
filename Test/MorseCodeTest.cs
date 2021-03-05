using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MorseLib;
using NUnit.Framework;
namespace Test
{
    [TestFixture]
    public class MorseCodeTest
    {
        [Test]
        public void TestIsOK()
        {
            Assert.IsTrue(true);
        }
        [Test]
        public void TestNewAndSigleChar()
        {
            var morseCode = Utils.ConvertToMorseCode("1");
            Assert.IsNotNull(morseCode);
            Assert.AreEqual(". - - - -" ,morseCode.Codes);
        }

        [Test]
        public void TestNewAndTwoChar()
        {
            var morseCode = Utils.ConvertToMorseCode("11");
            Assert.IsNotNull(morseCode);
            Assert.AreEqual(". - - - -   . - - - -", morseCode.Codes);
        }

        [Test]
        public void TestThreeChar()
        {
            var morseCode = Utils.ConvertToMorseCode("111");
            Assert.AreEqual(". - - - -   . - - - -   . - - - -", morseCode.Codes);
        }

        [Test]
        public void TestTwoWord()
        {
            var morseCode = Utils.ConvertToMorseCode("1 1");
            Assert.AreEqual(". - - - -     . - - - -", morseCode.Codes);
        }

        [Test]
        public void TestTwoWord2()
        {
            var morseCode = Utils.ConvertToMorseCode("12 13");
            Assert.AreEqual(". - - - -   . . - - -     . - - - -   . . . - -", morseCode.Codes);
        }

        [Test]
        public void TestTwoWord3()
        {
            var morseCode = Utils.ConvertToMorseCode("12a 13");
            Assert.AreEqual(". - - - -   . . - - -   . -     . - - - -   . . . - -", morseCode.Codes);
        }

        [Test]
        public void TestTwoWord3AndSpace1()
        {
            var morseCode = Utils.ConvertToMorseCode(" 12a 13");
            Assert.AreEqual("     . - - - -   . . - - -   . -     . - - - -   . . . - -", morseCode.Codes);
        }

        [Test]
        public void TestTwoWord3AndSpace2()
        {
            var morseCode = Utils.ConvertToMorseCode(" 12a  13");
            Assert.AreEqual("     . - - - -   . . - - -   . -          . - - - -   . . . - -", morseCode.Codes);
        }

        [Test]
        public void TestTwoWord3AndSpace3()
        {
            var morseCode = Utils.ConvertToMorseCode("  12a  13");
            Assert.AreEqual("          . - - - -   . . - - -   . -          . - - - -   . . . - -", morseCode.Codes);
        }

        [Test]
        public void TestNewAndSigleCharInCodes()
        {
            var morseCode = Utils.ConvertToMorseCode("1");
            Assert.AreEqual(1, morseCode.CharInCodesRange.Count);
            Assert.AreEqual(0, morseCode.CharInCodesRange[0].Start);
            Assert.AreEqual(9, morseCode.CharInCodesRange[0].End);
            //Assert.AreEqual(9, morseCode.ToneInCodesRange.Count);
            //Assert.AreEqual(". - - - -", morseCode.Codes);
        }

        [Test]
        public void TestNewAndSigleCharInCodes2()
        {
            var morseCode = Utils.ConvertToMorseCode("11");
            Assert.AreEqual(2, morseCode.CharInCodesRange.Count);
            Assert.AreEqual(0, morseCode.CharInCodesRange[0].Start);
            Assert.AreEqual(12, morseCode.CharInCodesRange[0].End);
            Assert.AreEqual(12, morseCode.CharInCodesRange[1].Start);
            Assert.AreEqual(21, morseCode.CharInCodesRange[1].End);
            
            //Assert.AreEqual(9, morseCode.ToneInCodesRange.Count);
            //Assert.AreEqual(". - - - -   . - - - -", morseCode.Codes);
        }

        [Test]
        public void TestNewAndSigleCharInCodes3()
        {
            var morseCode = Utils.ConvertToMorseCode("11 1");
            Assert.AreEqual(4, morseCode.CharInCodesRange.Count);
            Assert.AreEqual(0, morseCode.CharInCodesRange[0].Start);
            Assert.AreEqual(12, morseCode.CharInCodesRange[0].End);
            Assert.AreEqual(12, morseCode.CharInCodesRange[1].Start);
            Assert.AreEqual(21, morseCode.CharInCodesRange[1].End);
            Assert.AreEqual(21, morseCode.CharInCodesRange[2].Start);
            Assert.AreEqual(26, morseCode.CharInCodesRange[2].End);
            Assert.AreEqual(26, morseCode.CharInCodesRange[3].Start);
            Assert.AreEqual(35, morseCode.CharInCodesRange[3].End);


            var r = morseCode.CharInCodesRange[0];
            Assert.AreEqual(". - - - -   ", morseCode.Codes.Substring(r.Start, r.Count()));
            r = morseCode.CharInCodesRange[1];
            Assert.AreEqual(". - - - -", morseCode.Codes.Substring(r.Start, r.Count()));

            r = morseCode.CharInCodesRange[2];
            Assert.AreEqual("     ", morseCode.Codes.Substring(r.Start, r.Count()));

            r = morseCode.CharInCodesRange[3];
            Assert.AreEqual(". - - - -", morseCode.Codes.Substring(r.Start, r.Count()));

        }


        [Test]
        public void TestNewAndSigleCharInCodes4()
        {
            var morseCode = Utils.ConvertToMorseCode("  111 4");
            Assert.AreEqual(7, morseCode.CharInCodesRange.Count);
            var r = morseCode.CharInCodesRange[0];
            Assert.AreEqual("     ", morseCode.Codes.Substring(r.Start, r.Count()));
            r = morseCode.CharInCodesRange[1];
            Assert.AreEqual("     ", morseCode.Codes.Substring(r.Start, r.Count()));
            r = morseCode.CharInCodesRange[2];
            Assert.AreEqual(". - - - -   ", morseCode.Codes.Substring(r.Start, r.Count()));
            r = morseCode.CharInCodesRange[3];
            Assert.AreEqual(". - - - -   ", morseCode.Codes.Substring(r.Start, r.Count()));
            r = morseCode.CharInCodesRange[4];
            Assert.AreEqual(". - - - -", morseCode.Codes.Substring(r.Start, r.Count()));
            r = morseCode.CharInCodesRange[5];
            Assert.AreEqual("     ", morseCode.Codes.Substring(r.Start, r.Count()));
            //Assert.AreEqual(0, morseCode.CharInCodesRange[0].Start);
            //Assert.AreEqual(9, morseCode.CharInCodesRange[0].End);
            //Assert.AreEqual(9, morseCode.CharInCodesRange[1].Start);
            //Assert.AreEqual(18, morseCode.CharInCodesRange[1].End);

            //Assert.AreEqual(9, morseCode.ToneInCodesRange.Count);
            //Assert.AreEqual(". - - - -", morseCode.Codes);
        }

    }
}
