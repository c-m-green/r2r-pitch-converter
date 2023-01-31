using System;
using System.ComponentModel.DataAnnotations;

namespace PitchConverter.Pitch
{
    internal class MusicSymbol
    {
        [Range(-1, 11,
            ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        private int _pitchClass;
        [Range(PitchConstants.MinOctave, PitchConstants.MaxOctave,
            ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        private int _octave;

        public MusicSymbol()
        {
            _pitchClass = -1;
        }

        public MusicSymbol(int pitchClass, int octave)
        {
            _pitchClass = pitchClass;
            _octave = octave;
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
    }
}