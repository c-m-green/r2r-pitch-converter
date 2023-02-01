using PitchConverter.Pitch;
using System.Collections.Generic;
using System.Text;

namespace PitchConverter.Encoder
{
    internal class StringConverter
    {
        public static List<MusicSymbol> ByLetter(string input, int startOctave, bool stripLetters, bool useGermanH, bool writeRests)
        {
            List<MusicSymbol> music = new();
            string s = input;
            if (stripLetters)
            {
                string pitchLetters = useGermanH ? "ABCDEFGH" : "ABCDEFG";
                s = StripNonPitchLetters(input, pitchLetters);
            }
            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];
                MusicSymbol ms = CharConverter.LetterToPitchLiteral(ch, startOctave, useGermanH);
                if (writeRests || ms.GetPitchClass() != -1)
                {
                    music.Add(ms);
                }
            }
            return music;
        }

        public static List<MusicSymbol> ByDegree(string input, int startOctave, bool isChromatic, bool writeRests)
        {
            List<MusicSymbol> music = new();
            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                MusicSymbol ms = CharConverter.AlphaNumToPitchDegree(ch, startOctave, isChromatic);
                if (writeRests || ms.GetPitchClass() != -1)
                {
                    music.Add(ms);
                }
            }
            return music;
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
    }
}