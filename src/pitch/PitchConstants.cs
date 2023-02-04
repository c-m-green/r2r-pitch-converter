namespace PitchConverter.Pitch
{
    internal class PitchConstants
    {
        public class PitchClassSets
        {
            public const string NonChromaticPitchClasses = "024579e";
            public const string ChromaticPitchClasses = "0123456789te";
            public const string GermanHPitchClasses = "9t02457e";
            public const string NonGermanHPitchClasses = "9e02457";
        }

        public static int GetNumberOfPitchClasses()
        {
            return PitchClassSets.ChromaticPitchClasses.Length;
        }

        public const int MinOctave = -1;
        public const int MaxOctave = 10;
    }
}