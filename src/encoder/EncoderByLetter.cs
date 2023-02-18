using PitchConverter.Pitch;
using System.Text;

namespace PitchConverter.Encoder.Impl
{
    /// <summary>
    /// <para>
    /// Class to convert a string into a sequence of musical pitches by
    /// interpreting each character as a pitch class.
    /// </para>
    /// <para>
    /// Characters that directly map to a pitch (by default, A to G) will
    /// do so. Letters that are not converted this way continue the
    /// pattern through the alphabet. (H -> A, I -> B, etc.)
    /// </para>
    /// </summary>
    public class EncoderByLetter : Encoder
    {
        /// <summary>
        /// Determines whether the encoder will treat the letter 'H' as
        /// the pitch class B-natural (<c>11</c>). The letter 'B'
        /// is then treated as A-sharp/B-flat (<c>10</c>).
        /// </summary>
        /// <remarks>
        /// If <c>true</c>, 'H' is treated as a valid pitch class.
        /// </remarks>
        /// <seealso cref="SetUseGermanH(bool)"/>
        public bool UseGermanH { get; private set; }
        /// <summary>
        /// Determines whether the encoder will ignore letters
        /// that are not part of the established pitch classes.
        /// </summary>
        /// <seealso cref="SetUseGermanH(bool)"/>
        public bool OmitNonPitchLetters { get; set; }

        public EncoderByLetter(OutputFormat format) : base(format)
        {
            _allowNumbers = false;
            OmitNonPitchLetters = false;
            SetUseGermanH(false);
        }

        /// <summary>
        /// Overriding method that optionally removes all non-pitch
        /// letters from the input string before converting.
        /// </summary>
        /// <seealso cref="OmitNonPitchLetters"/>
        /// <seealso cref="SetUseGermanH(bool)"/>
        /// <param name="input"></param>a string to convert
        /// <returns>a list of MusicSymbol objects</returns>
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
                if (pitchLetters.Contains(ch) || ch == ' ')
                {
                    output.Append(ch);
                }
            }
            return output.ToString();
        }
        /// <summary>
        /// Sets whether to treat 'H' as a pitch class representing B-natural.
        /// </summary>
        /// <seealso cref="UseGermanH"/>
        /// <param name="useGermanH">whether to use H as a pitch class</param>
        public void SetUseGermanH(bool useGermanH)
        {
            UseGermanH = useGermanH;
            _pitchClasses = UseGermanH ? PitchConstants.PitchClassSets.GermanHPitchClasses
                : PitchConstants.PitchClassSets.NonGermanHPitchClasses;
        }
    }
}