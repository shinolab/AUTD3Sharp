using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Driver.Datagram
{
    [ComVisible(false)]
    public abstract class Modulation : IDatagram, IDatagramS, IDatagramL
    {
        internal abstract ModulationPtr ModulationPtr();
        public SamplingConfig SamplingConfig() => new(NativeMethodsBase.AUTDModulationSamplingConfig(ModulationPtr()));

        DatagramPtr IDatagramS.WithSegmentTransition(Geometry _, Segment segment, IInfiniteTransitionMode transitionMode) => NativeMethodsBase.AUTDModulationIntoDatagramWithSegment(ModulationPtr(), segment.ToNative(), transitionMode.Inner);
        DatagramPtr IDatagramL.WithFiniteLoop(Geometry _, Segment segment, IFiniteTransitionMode transitionMode, ushort loopCount) => NativeMethodsBase.AUTDModulationIntoDatagramWithFiniteLoop(ModulationPtr(), segment.ToNative(), transitionMode.Inner, loopCount);
        DatagramPtr IDatagram.Ptr(Geometry _) => NativeMethodsBase.AUTDModulationIntoDatagram(ModulationPtr());
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
