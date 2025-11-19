using System.Diagnostics.CodeAnalysis;
using System.Net;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Link
{
    public class RemoteOption
    {
        public Duration? Timeout { get; init; } = null;

        internal NativeMethods.RemoteOption ToNative()
        {
            return new NativeMethods.RemoteOption
            {
                timeout = Timeout.ToNative()
            };
        }
    }

    public sealed class Remote : Driver.Link
    {
        private readonly IPEndPoint _ip;
        private readonly RemoteOption _option;

        public Remote(IPEndPoint ip, RemoteOption option)
        {
            _ip = ip;
            _option = option;
        }

        [ExcludeFromCodeCoverage]
        public override LinkPtr Resolve()
        {
            var ipBytes = Ffi.ToNullTerminatedUtf8(_ip.ToString());
            unsafe
            {
                fixed (byte* ipPtr = &ipBytes[0])
                    return NativeMethodsLinkRemote.AUTDLinkRemote(ipPtr, _option.ToNative()).Validate();
            }
        }
    }

}
