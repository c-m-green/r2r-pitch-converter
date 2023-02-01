using PitchConverter.Pitch;

namespace PitchConverter.Encoder
{
    internal class CharConverter
    {
        public static MusicSymbol LetterToPitchLiteral(char ch, int octaveStart, bool useGermanH)
        {
            if (!char.IsLetter(ch))
            { // if non-letter is passed in
                return new MusicSymbol();
            }
            else
            {
                // TODO: Remove diacritics from text
                char cIn = char.ToUpper(ch);
                string pitchClasses = useGermanH ? PitchConstants.GermanHPitchClasses : PitchConstants.NonGermanHPitchClasses;
                int charValue = FindCharValue(cIn);
                return ObtainPitch(pitchClasses, charValue, octaveStart);
            }
        }

        public static MusicSymbol AlphaNumToPitchDegree(char ch, int octaveStart, bool isChromatic)
        {
            if (!char.IsLetterOrDigit(ch))
            { // if non-alphanumeric character is passed in
                return new MusicSymbol();
            }
            else
            {
                string pitchClasses = (isChromatic) ? PitchConstants.ChromaticPitchClasses : PitchConstants.NonChromaticPitchClasses;
                int charValue = FindCharValue(ch);
                return ObtainPitch(pitchClasses, charValue, octaveStart);
            }
        }

        private static int FindCharValue(char c)
        {
            // TODO: Remove diacritics from text
            char input = char.ToUpper(c);
            int charValue = -1;
            if (char.IsLetter(input))
            {
                charValue = input - 65; // Bring down to 0-25 range
            }
            else if (char.IsDigit(input))
            {
                charValue = input - 0;
            }
            return charValue;
        }

        private static MusicSymbol ObtainPitch(string pitchClasses, int charValue, int registerStart)
        {
            if (charValue == -1)
            {
                return new MusicSymbol();
            }
            else
            {
                int pitchIndex = charValue % pitchClasses.Length;
                int register = charValue / pitchClasses.Length + registerStart;
                return new MusicSymbol(pitchClasses[pitchIndex], register);
            }
        }
    }
}