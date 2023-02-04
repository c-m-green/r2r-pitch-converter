using PitchConverter.Pitch;

namespace PitchConverter.Encoder
{
    internal class CharConverter
    {
        public static MusicSymbol CharToPitch(char ch, string pitchClasses, int octaveStart, bool allowNumbers)
        {
            if (!char.IsLetterOrDigit(ch) ||
                !allowNumbers && char.IsNumber(ch))
            {
                return new MusicSymbol();
            }
            else if (char.IsLetter(ch))
            {
                ch = char.ToUpper(ch);
            }
            int charValue = FindCharValue(ch);
            return ObtainPitch(pitchClasses, charValue, octaveStart);
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