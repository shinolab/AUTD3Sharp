using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;
using System;

namespace AUTD3Sharp.Modulation.AudioFile
{
    [Builder]
    [Modulation(ConfigNoChange = true)]
    public sealed partial class Csv
    {
        private readonly string _filename;

        [Property]
        public char Deliminator { get; private set; } = ',';

        private readonly (float, DynSincInterpolator)? _resample;

        public Csv(string filename, SamplingConfig config)
        {
            _filename = filename;
            _config = config;
            _resample = null;
        }

        public Csv(string filename, Freq<float> source, SamplingConfig target, SincInterpolation resampler)
        {
            _filename = filename;
            _config = target;
            _resample = (source.Hz, resampler.DynResampler());
        }

        private ModulationPtr ModulationPtr()
        {
            var filenameBytes = Ffi.ToNullTerminatedUtf8(_filename);
            unsafe
            {
                fixed (byte* fp = &filenameBytes[0])
                {
                    return _resample.HasValue ?
                        NativeMethodsModulationAudioFile.AUTDModulationAudioFileCsvWithResample(fp, Convert.ToByte(Deliminator), LoopBehavior, _resample.Value.Item1, _config, _resample.Value.Item2).Validate()
                    : NativeMethodsModulationAudioFile.AUTDModulationAudioFileCsv(fp, _config, Convert.ToByte(Deliminator), LoopBehavior).Validate();
                }
            }
        }
    }
}
