using NUnit.Framework;
using PitchConverter.Encoder;
using PitchConverter.Pitch;

namespace PitchConverterTest
{
    [TestFixture]
    [TestOf(typeof(Encoder))]
    public abstract class EncoderTests
    {
        public struct EncoderTestInput
        {
            public string InputString;
            public int[] ExpectedPitchClasses, ExpectedOctaves;
            public EncoderTestInput(string inputString, int[] expectedPitchClasses, int[] expectedOctaves)
            {
                InputString = inputString;
                ExpectedPitchClasses = expectedPitchClasses;
                ExpectedOctaves = expectedOctaves;
            }
        }
    }

    [TestFixture]
    [TestOf(nameof(Encoder.Encode))]
    public class EncodeTests : EncoderTests
    {
        [TestCaseSource(nameof(TestInputsRests), new object[] { true })]
        public void Encoder_IncludeRestsTrue_WritesRests(EncoderTestInput encoderTestInput)
        {
            AbstractEncoder e = new(OutputFormat.Text);
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

        [TestCaseSource(nameof(TestInputsRests), new object[] { false })]
        public void Encoder_IncludeRestsFalse_OmitsRests(EncoderTestInput encoderTestInput)
        {
            AbstractEncoder e = new(OutputFormat.Text)
            {
                IncludeRests = false
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

        static IEnumerable<EncoderTestInput> TestInputsRests(bool includesRests)
        {
            if (includesRests)
            {
                yield return new EncoderTestInput("Aa Bb Cc Dd Ee", new int[] { 0, 0, -1, 2, 2, -1, 4, 4, -1, 5, 5, -1, 7, 7 }, new int[] { 4, 4, 0, 4, 4, 0, 4, 4, 0, 4, 4, 0, 4, 4 });
                yield return new EncoderTestInput("Abc123", new int[] { 0, 2, 4, 2, 4, 5 }, new int[] { 4, 4, 4, 4, 4, 4 });
                yield return new EncoderTestInput("EFGHI efghi ", new int[] { 7, 9, 11, 0, 2, -1, 7, 9, 11, 0, 2, -1 }, new int[] { 4, 4, 4, 5, 5, 0, 4, 4, 4, 5, 5, 0 });
                yield return new EncoderTestInput("/Q! S? ^ $.", new int[] { -1, 4, -1, -1, 7, -1, -1, -1, -1, -1, -1 }, new int[] { 0, 6, 0, 0, 6, 0, 0, 0, 0, 0, 0 });
            }
            else
            {
                yield return new EncoderTestInput("Aa Bb Cc Dd Ee", new int[] { 0, 0, 2, 2, 4, 4, 5, 5, 7, 7 }, new int[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 });
                yield return new EncoderTestInput("Abc123", new int[] { 0, 2, 4, 2, 4, 5 }, new int[] { 4, 4, 4, 4, 4, 4 });
                yield return new EncoderTestInput("EFGHI efghi ", new int[] { 7, 9, 11, 0, 2, 7, 9, 11, 0, 2 }, new int[] { 4, 4, 4, 5, 5, 4, 4, 4, 5, 5 });
                yield return new EncoderTestInput("/Q! S? ^ $.", new int[] { 4, 7 }, new int[] { 6, 6 });
            }
        }

        [Test]
        public void Encoder_StartOctave_IncrementsAsExpected()
        {
            AbstractEncoder e = new(OutputFormat.Text);
            List<MusicSymbol> music = e.Encode("fghi tuvw");
            List<int> actualOctaves = new();
            foreach (MusicSymbol ms in music)
            {
                actualOctaves.Add(ms.GetOctave());
            }
            Assert.AreEqual(new int[] { 4, 4, 5, 5, 0, 6, 6, 7, 7 }, actualOctaves.ToArray());
        }

        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(9)]
        [TestCase(12)]
        public void Encoder_StartOctave_EnforcesLimits(int startOctave)
        {
            AbstractEncoder e = new(OutputFormat.Text);
            e.StartOctave = startOctave;
            List<MusicSymbol> music = e.Encode("Mr. Jock, TV quiz PhD, bags few lynx");
            List<int> actualOctaves = new();
            foreach (MusicSymbol ms in music)
            {
                actualOctaves.Add(ms.GetOctave());
            }
            Assert.LessOrEqual(actualOctaves.Max(), 10);
            Assert.GreaterOrEqual(actualOctaves.Min(), -1);
        }

    }
}
