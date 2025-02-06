using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram
{
    [ComVisible(false)]
    public interface IDatagramL
    {
        internal DatagramPtr WithLoopBehavior(Geometry geometry, Segment segment, TransitionMode? transitionMode, LoopBehavior loopBehavior);
    }

    public sealed class WithLoopBehavior : IDatagram
    {
        public IDatagramL Inner;
        public LoopBehavior LoopBehavior;
        public Segment Segment;
        public TransitionMode? TransitionMode;

        public WithLoopBehavior(IDatagramL inner, LoopBehavior loopBehavior, Segment segment, TransitionMode? transitionMode)
        {
            Inner = inner;
            LoopBehavior = loopBehavior;
            Segment = segment;
            TransitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => Inner.WithLoopBehavior(geometry, Segment, TransitionMode, LoopBehavior);
    }
}
