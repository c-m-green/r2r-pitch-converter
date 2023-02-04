using PitchConverter.Pitch;

namespace PitchConverter.Encoder
{
    public abstract class Encoder
    {
        private protected string _pitchClasses;
        public int StartOctave { get; set; }
        public bool IncludeRests { get; set; }
        public OutputFormat Format { get; init; }

        public Encoder(OutputFormat format)
        {
            _pitchClasses = PitchConstants.PitchClassSets.NonChromaticPitchClasses;
            StartOctave = 4;
            IncludeRests = true;
            Format = format;
        }

        public abstract List<MusicSymbol> Encode(string input);

        public virtual void EncodeToFile(string input, OutputFormat outputFormat)
        {
            List<MusicSymbol> music = Encode(input);
            // Do file writing here
            throw new NotImplementedException();
        }

        public enum OutputFormat
        {
            Text,
            Midi,
            MusicXml
        }

    }
}