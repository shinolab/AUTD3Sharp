using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    [ComVisible(false)]
    public interface IModulation : IWithSampling
    {
        public ModulationPtr ModulationPtr();
        public NativeMethods.LoopBehavior LoopBehavior { get; }
        public SamplingConfig SamplingConfig { get; }
        SamplingConfig IWithSampling.SamplingConfigIntensity() => SamplingConfig;
        SamplingConfig? IWithSampling.SamplingConfigPhase() => null;
    }
}
