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
        private readonly Freq<uint> _sampleRate;

        [Property]
        public char Deliminator { get; private set; } = ',';

        public Csv(string filename, Freq<uint> sampleRate)
        {
            _filename = filename;
            _sampleRate = sampleRate;
        }

        private ModulationPtr ModulationPtr(Geometry _)
        {
            var filenameBytes = System.Text.Encoding.UTF8.GetBytes(_filename);
            unsafe
            {
                fixed (byte* fp = &filenameBytes[0])
                {
                    return NativeMethodsModulationAudioFile.AUTDModulationCsv(fp, _sampleRate.Hz, Convert.ToByte(Deliminator), LoopBehavior).Validate();
                }
            }
        }
    }
}
