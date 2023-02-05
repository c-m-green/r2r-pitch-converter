using PitchConverter.Pitch;

namespace PitchConverter.Encoder
{
    public interface IEncoder
    {
        List<MusicSymbol> Encode(string input);
        void EncodeToFile(string input, OutputFormat outputFormat);
    }
}
