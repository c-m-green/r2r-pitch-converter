using NUnit.Framework;
using PitchConverter.Encoder;
using PitchConverter.Encoder.Impl;
using PitchConverter.Pitch;
using static PitchConverterTest.EncoderTests;

namespace PitchConverterTest
{
    public class EncoderByLetterTests : EncoderTests
    {
        [TestCaseSource(nameof(TestInputsStripLetters), new object[] { true })]
        public void EncoderByLetter_OmitNonPitchLettersTrue_StripsLetters(EncoderTestInput encoderTestInput)
        {
            EncoderByLetter e = new(OutputFormat.Text)
            {
                OmitNonPitchLetters = true
            };
            List<MusicSymbol> music = e.Encode(encoderTestInput.InputString);
            List<int> actualPitches = new();
            List<int> actualOctaves = new();
            foreach (MusicSymbol ms in music)
            {
                actualPitches.Add(ms.GetPitchClass());
                actualOctaves.Add(ms.GetOctave());
            }
            Assert.AreEqual(encoderTestInput.ExpectedPitchClasses, actualPitches.ToArray());
            Assert.AreEqual(encoderTestInput.ExpectedOctaves, actualOctaves.ToArray());
        }

        [TestCaseSource(nameof(TestInputsStripLetters), new object[] { false })]
        public void EncoderByLetter_OmitNonPitchLettersFalse_KeepsLetters(EncoderTestInput encoderTestInput)
        {
            EncoderByLetter e = new(OutputFormat.Text);
            List<MusicSymbol> music = e.Encode(encoderTestInput.InputString);
            List<int> actualPitches = new();
            List<int> actualOctaves = new();
            foreach (MusicSymbol ms in music)
            {
                actualPitches.Add(ms.GetPitchClass());
                actualOctaves.Add(ms.GetOctave());
            }
            Assert.AreEqual(encoderTestInput.ExpectedPitchClasses, actualPitches.ToArray());
            Assert.AreEqual(encoderTestInput.ExpectedOctaves, actualOctaves.ToArray());
        }

        static IEnumerable<EncoderTestInput> TestInputsStripLetters(bool stripNonPitchLetters)
        {
            const string str1 = "Aa Bb Cc Dd Ee";
            const string str2 = "Abc123";
            const string str3 = "EFGHI efghi ";
            const string str4 = "/Q! S? ^ $.";
            if (stripNonPitchLetters)
            {
                yield return new EncoderTestInput(str1, new int[] { 9, 9, -1, 11, 11, -1, 0, 0, -1, 2, 2, -1, 4, 4 }, new int[] { 4, 4, 0, 4, 4, 0, 4, 4, 0, 4, 4, 0, 4, 4 });
                yield return new EncoderTestInput(str2, new int[] { 9, 11, 0 }, new int[] { 4, 4, 4 });
                yield return new EncoderTestInput(str3, new int[] { 4, 5, 7, -1, 4, 5, 7, -1 }, new int[] { 4, 4, 4, 0, 4, 4, 4, 0 });
                yield return new EncoderTestInput(str4, new int[] { -1, -1, -1 }, new int[] { 0, 0, 0 });
            }
            else
            {
                yield return new EncoderTestInput(str1, new int[] { 9, 9, -1, 11, 11, -1, 0, 0, -1, 2, 2, -1, 4, 4 }, new int[] { 4, 4, 0, 4, 4, 0, 4, 4, 0, 4, 4, 0, 4, 4 });
                yield return new EncoderTestInput(str2, new int[] { 9, 11, 0, -1, -1, -1 }, new int[] { 4, 4, 4, 0, 0, 0 });
                yield return new EncoderTestInput(str3, new int[] { 4, 5, 7, 9, 11, -1, 4, 5, 7, 9, 11, -1 }, new int[] { 4, 4, 4, 5, 5, 0, 4, 4, 4, 5, 5, 0 });
                yield return new EncoderTestInput(str4, new int[] { -1, 0, -1, -1, 4, -1, -1, -1, -1, -1, -1 }, new int[] { 0, 6, 0, 0, 6, 0, 0, 0, 0, 0, 0 });
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public void EncoderByLetter_UseGermanH_UsesCorrectPitches(bool useGermanH)
        {
            const string input = "ABCDEFGHI";
            EncoderByLetter e = new(OutputFormat.Text);
            e.SetUseGermanH(useGermanH);
            List<MusicSymbol> music = e.Encode(input);
            int[] expectedPitches = useGermanH ? new int[] { 9, 10, 0, 2, 4, 5, 7, 11, 9 } : new int[] { 9, 11, 0, 2, 4, 5, 7, 9, 11 };
            int[] expectedOctaves = useGermanH ? new int[] { 4, 4, 4, 4, 4, 4, 4, 4, 5 } : new int[] { 4, 4, 4, 4, 4, 4, 4, 5, 5 };
            List<int> actualPitches = new();
            List<int> actualOctaves = new();
            foreach (MusicSymbol ms in music)
            {
                actualPitches.Add(ms.GetPitchClass());
                actualOctaves.Add(ms.GetOctave());
            }
            Assert.AreEqual(expectedPitches, actualPitches.ToArray());
            Assert.AreEqual(expectedOctaves, actualOctaves.ToArray());
        }

        [Test]
        public void EncoderByLetter_DoesNotStripGermanH()
        {
            const string input = "ABCDEFGHI";
            EncoderByLetter e = new(OutputFormat.Text);
            e.SetUseGermanH(true);
            e.OmitNonPitchLetters = true;
            List<MusicSymbol> music = e.Encode(input);
            int[] expectedPitches = new int[] { 9, 10, 0, 2, 4, 5, 7, 11 };
            int[] expectedOctaves = new int[] { 4, 4, 4, 4, 4, 4, 4, 4 };
            List<int> actualPitches = new();
            List<int> actualOctaves = new();
            foreach (MusicSymbol ms in music)
            {
                actualPitches.Add(ms.GetPitchClass());
                actualOctaves.Add(ms.GetOctave());
            }
            Assert.AreEqual(expectedPitches, actualPitches.ToArray());
            Assert.AreEqual(expectedOctaves, actualOctaves.ToArray());
        }
    }
}