using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation.AudioFile
{
    public sealed class Wav : IModulation
    {
        public string Path;

        public Wav(string path) => Path = path;

        ModulationPtr IModulation.ModulationPtr()
        {
            var filenameBytes = Ffi.ToNullTerminatedUtf8(Path);
            unsafe
            {
                fixed (byte* fp = &filenameBytes[0])
                    return NativeMethodsModulationAudioFile.AUTDModulationAudioFileWav(fp).Validate();
            }
        }
    }
}
