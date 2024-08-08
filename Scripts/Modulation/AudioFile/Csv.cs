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

        public Csv(string filename, SamplingConfig config)
        {
            _filename = filename;
            _config = config;
        }

        private ModulationPtr ModulationPtr()
        {
            var filenameBytes = System.Text.Encoding.UTF8.GetBytes(_filename);
            unsafe
            {
                fixed (byte* fp = &filenameBytes[0])
                {
                    return NativeMethodsModulationAudioFile.AUTDModulationAudioFileCsv(fp, _config.Inner, Convert.ToByte(Deliminator), LoopBehavior).Validate();
                }
            }
        }
    }
}
