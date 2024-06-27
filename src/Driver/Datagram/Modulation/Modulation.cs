using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    [ComVisible(false)]
    public interface IModulation
    {
        public ModulationPtr ModulationPtr();
        public NativeMethods.LoopBehavior LoopBehavior { get; }
        public SamplingConfig SamplingConfig { get; }
    }
}
