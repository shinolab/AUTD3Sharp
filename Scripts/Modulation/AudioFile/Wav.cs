using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation.AudioFile
{
    [Modulation]
    public sealed partial class Wav
    {
        private readonly string _filename;

        public Wav(string filename)
        {
            _filename = filename;
        }

        private ModulationPtr ModulationPtr(Geometry _)
        {
            var filenameBytes = System.Text.Encoding.UTF8.GetBytes(_filename);
            unsafe
            {
                fixed (byte* fp = &filenameBytes[0])
                {
                    return NativeMethodsModulationAudioFile.AUTDModulationWav(fp, _config, LoopBehavior).Validate();
                }
            }
        }
    }
}