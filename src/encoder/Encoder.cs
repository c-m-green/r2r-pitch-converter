namespace PitchConverter.Encoder
{
    public class Encoder
    {
        public bool UseGermanH { get; set; }
        public bool UseChromaticScale { get; set; }
        public bool StripNonPitchLetters { get; set; }
        public bool IncludeRests { get; set; }

        public Encoder() { }
        public string GetTestMessage()
        {
            return "Hello, Read2Read app!";
        }
    }
}