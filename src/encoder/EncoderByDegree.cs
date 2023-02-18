using PitchConverter.Pitch;

namespace PitchConverter.Encoder.Impl
{
    /// <summary>
    /// <para>
    /// Class to convert text into a sequence of musical pitches by
    /// interpreting each character as a scale degree.
    /// </para>
    /// <para>
    /// Letters closer to the beginning of the alphabet will have a lower
    /// pitch than letters in the middle or at the end of the alphabet.
    /// </para>
    /// </summary>
    public class EncoderByDegree : Encoder
    {
        /// <summary>
        /// Determines whether the encoder will utilize notes from
        /// the full chromatic scale for conversion.
        /// </summary>
        /// <seealso cref="SetUseChromaticScale(bool)"/>
        public bool UseChromaticScale { get; private set; }

        public EncoderByDegree(OutputFormat format) : base(format)
        {
            SetUseChromaticScale(false);
        }

        /// <summary>
        /// Sets whether to use the full chromatic scale when converting
        /// by degree.
        /// </summary>
        /// <param name="useChromaticScale">Sets whether the encoder
        /// will utilize notes from the full chromatic scale for conversion.
        /// </param>
        public void SetUseChromaticScale(bool useChromaticScale)
        {
            UseChromaticScale = useChromaticScale;
            _pitchClasses = UseChromaticScale ?
                PitchConstants.PitchClassSets.ChromaticPitchClasses
                : PitchConstants.PitchClassSets.NonChromaticPitchClasses;
        }
    }
}