using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation.AudioFile
{
    public sealed class Wav : Driver.Datagram.Modulation
    {
        public string Path;

        public Wav(string path) => Path = path;

        internal override ModulationPtr ModulationPtr()
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
