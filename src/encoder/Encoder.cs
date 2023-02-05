using PitchConverter.Pitch;

namespace PitchConverter.Encoder
{
    public abstract class Encoder : IEncoder
    {
        private protected string _pitchClasses;
        private protected bool _allowNumbers;
        public int StartOctave { get; set; }
        public bool IncludeRests { get; set; }
        public OutputFormat Format { get; init; }

        public Encoder(OutputFormat format)
        {
            _pitchClasses = PitchConstants.PitchClassSets.NonChromaticPitchClasses;
            _allowNumbers = true;
            StartOctave = 4;
            IncludeRests = true;
            Format = format;
        }

        public virtual List<MusicSymbol> Encode(string input)
        {
            List<MusicSymbol> music = new();
            for (int i = 0; i < input.Length; i++)
            {
                MusicSymbol ms = CharConverter.CharToPitch(input[i], _pitchClasses, StartOctave, _allowNumbers);
                if (IncludeRests || ms.GetPitchClass() != -1)
                {
                    music.Add(ms);
                }
            }
            return music;
        }

        public void EncodeToFile(string input, OutputFormat outputFormat)
        {
            List<MusicSymbol> music = Encode(input);
            // Do file writing here
            throw new NotImplementedException();
        }
    }
}