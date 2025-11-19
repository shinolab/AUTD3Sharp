using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public class SenderOption
    {
        public Duration? SendInterval = Duration.FromMillis(1);
        public Duration? ReceiveInterval = Duration.FromMillis(1);
        public Duration? Timeout = null;

        internal NativeMethods.SenderOption ToNative() => new()
        {
            send_interval = SendInterval.ToNative(),
            receive_interval = ReceiveInterval.ToNative(),
            timeout = Timeout.ToNative()
        };
    }

    public class Sender
    {
        internal SenderPtr Ptr;
        internal readonly Geometry Geometry;

        internal Sender(SenderPtr ptr, Geometry geometry)
        {
            Ptr = ptr;
            Geometry = geometry;
        }

        public void Send<TD>(TD d) where TD : IDatagram => NativeMethodsBase.AUTDSenderSend(Ptr, d.Ptr(Geometry)).Validate();
        public void Send<TD1, TD2>((TD1, TD2) d) where TD1 : IDatagram where TD2 : IDatagram => Send(new DatagramTuple<TD1, TD2>(d));
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
