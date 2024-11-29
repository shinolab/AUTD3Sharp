using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Modulation.AudioFile
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class Wav
    {
        private readonly string _filename;
        private readonly (SamplingConfig, DynSincInterpolator)? _resample;

        public Wav(string filename)
        {
            _filename = filename;
            _resample = null;
        }

        public Wav(string filename, SamplingConfig target, SincInterpolation resampler)
        {
            _filename = filename;
            _resample = (target, resampler.DynResampler());
        }

        private ModulationPtr ModulationPtr()
        {
            var filenameBytes = Ffi.toNullTerminatedUtf8(_filename);
            unsafe
            {
                fixed (byte* fp = &filenameBytes[0])
                {
                    return _resample.HasValue ?
                        NativeMethodsModulationAudioFile.AUTDModulationAudioFileWavWithResample(fp, LoopBehavior, _resample.Value.Item1, _resample.Value.Item2).Validate()
                    : NativeMethodsModulationAudioFile.AUTDModulationAudioFileWav(fp, LoopBehavior).Validate();
                }
            }
        }
    }
}
