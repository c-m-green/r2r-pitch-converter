using NUnit.Framework;
using PitchConverter.Encoder;
using PitchConverter.Encoder.Impl;
using PitchConverter.Pitch;

namespace PitchConverterTest
{
    [TestFixture]
    [TestOf(typeof(EncoderByDegree))]
    public class EncoderByDegreeTests : EncoderTests
    {
        [TestCaseSource(nameof(TestInputsChromatic), new object[] { true })]
        public void EncoderByDegree_UseChromatic_UsesChromatic(EncoderTestInput encoderTestInput)
        {
            EncoderByDegree e = new(OutputFormat.Text);
            e.SetUseChromaticScale(true);
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

        [TestCaseSource(nameof(TestInputsChromatic), new object[] { false })]
        public void EncoderByDegree_DoNotUseChromatic_DoesNotUseChromatic(EncoderTestInput encoderTestInput)
        {
            EncoderByDegree e = new(OutputFormat.Text);
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

        static IEnumerable<EncoderTestInput> TestInputsChromatic(bool useChromaticScale)
        {
            const string str1 = "Aa Bb Cc Dd Ee";
            const string str2 = "Abc123";
            const string str3 = "aBcDeFgHiJkLmN";
            const string str4 = "/Q! S? ^ $.";
            if (useChromaticScale)
            {
                yield return new EncoderTestInput(str1, new int[] { 0, 0, -1, 1, 1, -1, 2, 2, -1, 3, 3, -1, 4, 4 }, new int[] { 4, 4, 0, 4, 4, 0, 4, 4, 0, 4, 4, 0, 4, 4 });
                yield return new EncoderTestInput(str2, new int[] { 0, 1, 2, 1, 2, 3 }, new int[] { 4, 4, 4, 4, 4, 4 });
                yield return new EncoderTestInput(str3, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0, 1 }, new int[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5 });
                yield return new EncoderTestInput(str4, new int[] { -1, 4, -1, -1, 6, -1, -1, -1, -1, -1, -1 }, new int[] { 0, 5, 0, 0, 5, 0, 0, 0, 0, 0, 0 });
            }
            else
            {
                yield return new EncoderTestInput(str1, new int[] { 0, 0, -1, 2, 2, -1, 4, 4, -1, 5, 5, -1, 7, 7 }, new int[] { 4, 4, 0, 4, 4, 0, 4, 4, 0, 4, 4, 0, 4, 4 });
                yield return new EncoderTestInput(str2, new int[] { 0, 2, 4, 2, 4, 5 }, new int[] { 4, 4, 4, 4, 4, 4 });
                yield return new EncoderTestInput(str3, new int[] { 0, 2, 4, 5, 7, 9, 11, 0, 2, 4, 5, 7, 9, 11 }, new int[] { 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5 });
                yield return new EncoderTestInput(str4, new int[] { -1, 4, -1, -1, 7, -1, -1, -1, -1, -1, -1 }, new int[] { 0, 6, 0, 0, 6, 0, 0, 0, 0, 0, 0 });
            }
        }
    }
}