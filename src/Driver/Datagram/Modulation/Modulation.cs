using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    [ComVisible(false)]
    public interface IModulation
    {
        public ModulationPtr ModulationPtr();
        public SamplingConfiguration InternalSamplingConfiguration();
        public LoopBehavior InternalLoopBehavior();
    }
}
