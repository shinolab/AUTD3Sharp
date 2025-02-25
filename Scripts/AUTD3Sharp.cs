using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public static class Tracing
    {
        public static TraceInitAction ExtTracing;

#if UNITY_2020_2_OR_NEWER
        public delegate void TraceInitAction(byte[] pathBytes);

        static Tracing()
        {
            ExtTracing = (_) => { };
        }

        public static void Init(string path)
        {
            var pathBytes = Ffi.ToNullTerminatedUtf8(path);
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
            ExtTracing(pathBytes);
        }
#else
        public delegate void TraceInitAction();

        static Tracing()
        {
            ExtTracing = () => { };
        }

        public static void Init()
        {
            NativeMethodsBase.AUTDTracingInit();
            NativeMethodsLinkSimulator.AUTDLinkSimulatorTracingInit();
            NativeMethodsModulationAudioFile.AUTDModulationAudioFileTracingInit();
            NativeMethodsLinkTwinCAT.AUTDLinkTwinCATTracingInit();
            ExtTracing();
        }
#endif
    }
}
