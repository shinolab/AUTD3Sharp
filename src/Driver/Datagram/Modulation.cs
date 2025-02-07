using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram
{
    [ComVisible(false)]
    public abstract class Modulation : IDatagram, IDatagramS, IDatagramL
    {
        internal abstract ModulationPtr ModulationPtr();
        public SamplingConfig SamplingConfig() => new(NativeMethodsBase.AUTDModulationSamplingConfig(ModulationPtr()).Validate());

        DatagramPtr IDatagramS.WithSegmentTransition(Geometry _, Segment segment, TransitionMode? transitionMode) => NativeMethodsBase.AUTDModulationIntoDatagramWithSegment(ModulationPtr(), segment.ToNative(), (transitionMode ?? TransitionMode.None).Inner);
        DatagramPtr IDatagramL.WithLoopBehavior(Geometry _, Segment segment, TransitionMode? transitionMode, LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationIntoDatagramWithLoopBehavior(ModulationPtr(), segment.ToNative(), (transitionMode ?? TransitionMode.None).Inner, loopBehavior.Inner);
        DatagramPtr IDatagram.Ptr(Geometry _) => NativeMethodsBase.AUTDModulationIntoDatagram(ModulationPtr());
    }
}
