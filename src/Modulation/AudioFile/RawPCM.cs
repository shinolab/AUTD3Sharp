using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation.AudioFile
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class RawPCM
    {
        private readonly string _filename;

        public RawPCM(string filename, SamplingConfig config)
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
                    return NativeMethodsModulationAudioFile.AUTDModulationAudioFileRawPCM(fp, _config.Inner, LoopBehavior).Validate();
                }
            }
        }
    }
}
