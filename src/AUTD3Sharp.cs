using System;
using AUTD3Sharp.NativeMethods;
using System.Diagnostics.CodeAnalysis;

namespace AUTD3Sharp
{
    public static class Tracing
    {
        [ExcludeFromCodeCoverage]
        public static void Init()
        {
            NativeMethodsBase.AUTDTracingInit();
            NativeMethodsLinkSimulator.AUTDLinkSimulatorTracingInit();
            NativeMethodsModulationAudioFile.AUTDModulationAudioFileTracingInit();
            NativeMethodsLinkTwinCAT.AUTDAUTDLinkTwinCATTracingInit();
            try
            {
                NativeMethodsLinkSOEM.AUTDAUTDLinkSOEMTracingInit();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
