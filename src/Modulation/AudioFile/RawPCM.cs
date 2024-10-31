using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Modulation.AudioFile
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class RawPCM
    {
        private readonly string _filename;
        private readonly (float, DynSincInterpolator)? _resample;

        public RawPCM(string filename, SamplingConfig config)
        {
            _filename = filename;
            _config = config;
        }

        public RawPCM(string filename, Freq<float> source, SamplingConfig target, SincInterpolation resampler)
        {
            _filename = filename;
            _config = target;
            _resample = (source.Hz, resampler.DynResampler());
        }

        private ModulationPtr ModulationPtr()
        {
            var filenameBytes = Ffi.toNullTerminatedUtf8(_filename);
            unsafe
            {
                fixed (byte* fp = &filenameBytes[0])
                {
                    return _resample.HasValue ? NativeMethodsModulationAudioFile.AUTDModulationAudioFileRawPCMWithResample(fp, LoopBehavior, _resample.Value.Item1, _config, _resample.Value.Item2).Validate()
                    : NativeMethodsModulationAudioFile.AUTDModulationAudioFileRawPCM(fp, _config, LoopBehavior).Validate();
                }
            }
        }
    }
}
