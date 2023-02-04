using PitchConverter.Pitch;

namespace PitchConverter.Encoder.Impl
{
    public class EncoderByDegree : Encoder
    {
        public bool UseChromaticScale { get; private set; }

        public EncoderByDegree(OutputFormat format) : base(format)
        {
            SetUseChromaticScale(false);
        }

        public override List<MusicSymbol> Encode(string input)
        {
            List<MusicSymbol> music = new();
            for (int i = 0; i < input.Length; i++)
            {
                MusicSymbol ms = CharConverter.CharToPitch(input[i], _pitchClasses, StartOctave, true);
                if (IncludeRests || ms.GetPitchClass() != -1)
                {
                    music.Add(ms);
                }
            }
            return music;
        }

        public void SetUseChromaticScale(bool useChromaticScale)
        {
            UseChromaticScale = useChromaticScale;
            _pitchClasses = UseChromaticScale ?
                PitchConstants.PitchClassSets.ChromaticPitchClasses
                : PitchConstants.PitchClassSets.NonChromaticPitchClasses;
        }
    }
}