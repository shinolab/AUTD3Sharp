using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    [ComVisible(false)]
    public interface IModulation : IWithSampling
    {
        public ModulationPtr ModulationPtr();
        public LoopBehavior LoopBehavior { get; }
        public SamplingConfig SamplingConfig { get; }
        SamplingConfig IWithSampling.SamplingConfigIntensity() => SamplingConfig;
        SamplingConfig? IWithSampling.SamplingConfigPhase() => null;
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
