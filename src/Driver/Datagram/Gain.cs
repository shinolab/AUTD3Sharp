using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Driver.Datagram
{
    [ComVisible(false)]
    public interface IGain : IDatagram, IDatagramS
    {
        internal GainPtr GainPtr(Geometry geometry);
        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDGainIntoDatagram(GainPtr(geometry));

        DatagramPtr IDatagramS.WithSegmentTransition(Geometry geometry, Segment segment, TransitionMode? transitionMode) => NativeMethodsBase.AUTDGainIntoDatagramWithSegment(GainPtr(geometry), segment.ToNative(), (transitionMode ?? TransitionMode.None).Inner);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
