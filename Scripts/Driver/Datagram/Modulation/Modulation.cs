using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    [ComVisible(false)]
    public interface IModulation
    {
        public ModulationPtr ModulationPtr(Geometry geometry);
        public NativeMethods.LoopBehavior LoopBehavior();
        public SamplingConfigWrap SamplingConfig();
    }
}
