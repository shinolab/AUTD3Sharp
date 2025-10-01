using System.Diagnostics.CodeAnalysis;
using System.Net;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Link
{
    public sealed class Remote : Driver.Link
    {
        private readonly IPEndPoint _ip;

        public Remote(IPEndPoint ip)
        {
            _ip = ip;
        }

        [ExcludeFromCodeCoverage]
        public override LinkPtr Resolve()
        {
            var ipBytes = Ffi.ToNullTerminatedUtf8(_ip.Address.ToString());
            unsafe
            {
                fixed (byte* ipPtr = &ipBytes[0])
                    return NativeMethodsLinkRemote.AUTDLinkRemote(ipPtr).Validate();
            }
        }
    }

}
