using System;
using AUTD3Sharp.NativeMethods;
using System.Diagnostics.CodeAnalysis;

namespace AUTD3Sharp
{
    public static class Tracing
    {
#if UNITY_2020_2_OR_NEWER
        [ExcludeFromCodeCoverage]
        public static void Init(string path)
        {
            var pathBytes = System.Text.Encoding.UTF8.GetBytes(path);
            unsafe
            {
                fixed (byte* pathPtr = &pathBytes[0])
                {
                    NativeMethodsBase.AUTDTracingInitWithFile(pathPtr);
                    NativeMethodsLinkSimulator.AUTDLinkSimulatorTracingInitWithFile(pathPtr);
                    NativeMethodsModulationAudioFile.AUTDModulationAudioFileTracingInitWithFile(pathPtr);
                    NativeMethodsLinkTwinCAT.AUTDLinkTwinCATTracingInitWithFile(pathPtr);
                    try
                    {
                        NativeMethodsLinkSOEM.AUTDLinkSOEMTracingInitWithFile(pathPtr);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
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
            try
            {
                NativeMethodsLinkSOEM.AUTDLinkSOEMTracingInit();
            }
            catch (Exception)
            {
                // ignored
            }
        }
#endif
    }
}
