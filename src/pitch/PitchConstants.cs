namespace PitchConverter.Pitch
{
    internal class PitchConstants
    {
        private const string _pitchClasses = "0123456789te";

        public static int GetNumberOfPitchClasses()
        {
            return _pitchClasses.Length;
        }

        public const int MinOctave = -1;
        public const int MaxOctave = 10;
    }
}