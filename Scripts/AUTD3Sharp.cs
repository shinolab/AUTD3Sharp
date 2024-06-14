using System;
using AUTD3Sharp.NativeMethods;
using System.Diagnostics.CodeAnalysis;

namespace AUTD3Sharp
{
    public static class Debug
    {
        public enum Level : byte
        {
            Debug = NativeMethodsBase.TRACE_LEVEL_DEBUG,
            Info = NativeMethodsBase.TRACE_LEVEL_INFO,
            Warn = NativeMethodsBase.TRACE_LEVEL_WARN,
            Error = NativeMethodsBase.TRACE_LEVEL_ERROR,
            Trace = NativeMethodsBase.TRACE_LEVEL_TRACE,
        }

        [ExcludeFromCodeCoverage]
        public static void TracingInit(Level level)
        {
            NativeMethodsBase.AUTDTracingInit((byte)level);
        }
    }
}
