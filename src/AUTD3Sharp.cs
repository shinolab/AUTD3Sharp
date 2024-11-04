using System;
using AUTD3Sharp.NativeMethods;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public static class Tracing
    {
#if UNITY_2020_2_OR_NEWER
        [ExcludeFromCodeCoverage]
        public static void Init(string path)
        {
            var pathBytes = Ffi.toNullTerminatedUtf8(path);
            unsafe
            {
                fixed (byte* pathPtr = &pathBytes[0])
                {
                    NativeMethodsBase.AUTDTracingInitWithFile(pathPtr);
                    NativeMethodsLinkSimulator.AUTDLinkSimulatorTracingInitWithFile(pathPtr);
                    NativeMethodsModulationAudioFile.AUTDModulationAudioFileTracingInitWithFile(pathPtr);
                    NativeMethodsLinkTwinCAT.AUTDLinkTwinCATTracingInitWithFile(pathPtr);
                }
            }
        }
#else
        [ExcludeFromCodeCoverage]
        public static void Init()
        {
            NativeMethodsBase.AUTDTracingInit();
            NativeMethodsLinkSimulator.AUTDLinkSimulatorTracingInit();
            NativeMethodsModulationAudioFile.AUTDModulationAudioFileTracingInit();
            NativeMethodsLinkTwinCAT.AUTDLinkTwinCATTracingInit();
        }
#endif
    }
}
