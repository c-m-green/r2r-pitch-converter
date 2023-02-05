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

        public void SetUseChromaticScale(bool useChromaticScale)
        {
            UseChromaticScale = useChromaticScale;
            _pitchClasses = UseChromaticScale ?
                PitchConstants.PitchClassSets.ChromaticPitchClasses
                : PitchConstants.PitchClassSets.NonChromaticPitchClasses;
        }
    }
}