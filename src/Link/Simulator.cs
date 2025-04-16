using System.Net;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Link
{
    public sealed class Simulator : Driver.Link
    {
        public IPEndPoint Addr { get; }

        public Simulator(IPEndPoint addr) => Addr = addr;

        public override LinkPtr Resolve()
        {
            var addr = Ffi.ToNullTerminatedUtf8(Addr.ToString());
            unsafe
            {
                fixed (byte* addrPtr = &addr[0])
                    return NativeMethodsLinkSimulator.AUTDLinkSimulator(addrPtr).Validate();
            }
        }
    }
}
