using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation.AudioFile
{
    [Modulation]
    public sealed partial class RawPCM
    {
        private readonly string _filename;
        private readonly Freq<uint> _sampleRate;

        public RawPCM(string filename, Freq<uint> sampleRate)
        {
            _filename = filename;
            _sampleRate = sampleRate;
        }

        private ModulationPtr ModulationPtr(Geometry _)
        {
            var filenameBytes = System.Text.Encoding.ASCII.GetBytes(_filename);
            unsafe
            {
                fixed (byte* fp = filenameBytes)
                {
                    return NativeMethodsModulationAudioFile.AUTDModulationRawPCM(fp, _sampleRate.Hz, _config, LoopBehavior).Validate();
                }
            }
        }
    }
}
