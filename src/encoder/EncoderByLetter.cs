using PitchConverter.Pitch;
using System.Text;

namespace PitchConverter.Encoder.Impl
{
    public class EncoderByLetter : Encoder
    {
        public bool UseGermanH { get; private set; }
        public bool OmitNonPitchLetters { get; set; }

        public EncoderByLetter(OutputFormat format) : base(format)
        {
            _allowNumbers = false;
            OmitNonPitchLetters = false;
            SetGermanH(false);
        }

        public override List<MusicSymbol> Encode(string input)
        {
            string strToConvert = OmitNonPitchLetters ?
                StripNonPitchLetters(input, UseGermanH ? "ABCDEFGH" : "ABCDEFG")
                : input;
            return base.Encode(strToConvert);
        }

        private static string StripNonPitchLetters(string input, string pitchLetters)
        {
            StringBuilder output = new();
            for (int i = 0; i < input.Length; i++)
            {
                // TODO Remove diacritics from text
                char ch = char.ToUpper(input[i]);
                if (pitchLetters[ch] != -1 || ch == ' ')
                {
                    output.Append(ch);
                }
            }
            return output.ToString();
        }

        public void SetGermanH(bool useGermanH)
        {
            UseGermanH = useGermanH;
            _pitchClasses = UseGermanH ? PitchConstants.PitchClassSets.GermanHPitchClasses
                : PitchConstants.PitchClassSets.NonGermanHPitchClasses;
        }
    }
}