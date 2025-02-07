using AUTD3Sharp.NativeMethods;
using System;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#endif

namespace AUTD3Sharp.Modulation.AudioFile
{
    public readonly struct CsvOption
    {
        public char Deliminator { get; init; } = ',';

        public CsvOption() { }
    }

    public sealed class Csv : Driver.Datagram.Modulation
    {
        public string Path;
        public SamplingConfig Config;
        public CsvOption Option;

        public Csv(string path, SamplingConfig config, CsvOption option)
        {
            Path = path;
            Config = config;
            Option = option;
        }

        internal override ModulationPtr ModulationPtr()
        {
            var filenameBytes = Ffi.ToNullTerminatedUtf8(Path);
            unsafe
            {
                fixed (byte* fp = &filenameBytes[0])
                    return NativeMethodsModulationAudioFile.AUTDModulationAudioFileCsv(fp, Config.Inner, Convert.ToByte(Option.Deliminator)).Validate();
            }
        }
    }
}
