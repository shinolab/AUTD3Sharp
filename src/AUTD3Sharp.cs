using System;
using AUTD3Sharp.NativeMethods;
using System.Diagnostics.CodeAnalysis;

namespace AUTD3Sharp
{
    public static class Debug
    {
        public enum Level : byte
        {
            Debug = NativeMethodsDriver.TRACE_LEVEL_DEBUG,
            Info = NativeMethodsDriver.TRACE_LEVEL_INFO,
            Warn = NativeMethodsDriver.TRACE_LEVEL_WARN,
            Error = NativeMethodsDriver.TRACE_LEVEL_ERROR,
            Trace = NativeMethodsDriver.TRACE_LEVEL_TRACE,
        }

        [ExcludeFromCodeCoverage]
        public static void TracingInit(Level level)
        {
            NativeMethodsBase.AUTDTracingInit((byte)level);
            try
            {
                NativeMethodsLinkSOEM.AUTDAUTDLinkSOEMTracingInit((byte)level);
            }
            catch (Exception)
            {
            }
        }
    }
}
