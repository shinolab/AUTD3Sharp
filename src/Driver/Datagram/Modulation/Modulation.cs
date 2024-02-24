using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    [ComVisible(false)]
    public abstract class Modulation : IDatagram
    {
        protected internal SamplingConfiguration Config;

        public SamplingConfiguration SamplingConfiguration => new SamplingConfiguration(NativeMethodsBase.AUTDModulationSamplingConfig(ModulationPtr()));

        DatagramPtr IDatagram.Ptr(Geometry.Geometry _) => NativeMethodsBase.AUTDModulationIntoDatagram(ModulationPtr());

        internal abstract ModulationPtr ModulationPtr();

        public LoopBehavior LoopBehavior { get; protected set; } = LoopBehavior.Infinite;

        public int Length => NativeMethodsBase.AUTDModulationSize(ModulationPtr()).Validate();
    }
}
