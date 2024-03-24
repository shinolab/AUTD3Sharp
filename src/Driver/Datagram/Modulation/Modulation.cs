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

namespace AUTD3Sharp
{
    public sealed class ChangeModulationSegment : Driver.Datagram.IDatagram
    {
        private readonly Segment _segment;

        public ChangeModulationSegment(Segment segment)
        {
            _segment = segment;
        }

        DatagramPtr Driver.Datagram.IDatagram.Ptr(Geometry _) => NativeMethodsBase.AUTDDatagramChangeModulationSegment(_segment);
    }
}
