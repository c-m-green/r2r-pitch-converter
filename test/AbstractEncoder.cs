using PitchConverter.Encoder;

namespace PitchConverterTest
{
    internal sealed class AbstractEncoder : Encoder
    {
        public AbstractEncoder(OutputFormat format) : base(format) { }       
    }
}
