namespace PitchConverter.Pitch
{
    public class MusicSymbol
    {
        private int _pitchClass;
        private int _octave;

        internal MusicSymbol()
        {
            _pitchClass = -1;
        }

        internal MusicSymbol(int pitchClass, int octave)
        {
            if (pitchClass < -1 || pitchClass > 11)
            {
                throw new ArgumentOutOfRangeException($"{pitchClass} is not a valid pitch class value");
            }
            _pitchClass = pitchClass;
            _octave = octave;
            _octave = Math.Min(_octave, PitchConstants.MaxOctave);
            _octave = Math.Max(_octave, PitchConstants.MinOctave);
        }

        public int GetPitchClass()
        {
            return _pitchClass;
        }

        public int GetOctave()
        {
            return _octave;
        }

        public void Transpose(int interval)
        {
            if (_pitchClass > -1)
            {
                int numPitchClasses = PitchConstants.GetNumberOfPitchClasses();
                int shiftedPitch = _pitchClass + interval;
                _pitchClass = shiftedPitch % numPitchClasses;
                if (_pitchClass < 0)
                {
                    _pitchClass += numPitchClasses;
                }
                int octaveShift = shiftedPitch / numPitchClasses;
                _octave += (shiftedPitch < 0) ? octaveShift - 1 : octaveShift;
                _octave = Math.Min(_octave, PitchConstants.MaxOctave);
                _octave = Math.Max(_octave, PitchConstants.MinOctave);
            }
        }

        public override string ToString()
        {
            return $"{GetPitchClass()}:{GetOctave()}";
        }
    }
}