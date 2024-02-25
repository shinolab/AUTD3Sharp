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

    public sealed class ChangeModulationSegment : IDatagram
    {
        private readonly Segment _segment;

        public ChangeModulationSegment(Segment segment)
        {
            _segment = segment;
        }

        DatagramPtr IDatagram.Ptr(Geometry _) => NativeMethodsBase.AUTDDatagramChangeModulationSegment(_segment);
    }
}
