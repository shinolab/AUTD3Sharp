using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#endif

namespace AUTD3Sharp.Link
{
    public sealed class TwinCAT : Driver.Link
    {
        public override LinkPtr Resolve() => NativeMethodsLinkTwinCAT.AUTDLinkTwinCAT().Validate();
    }

    public class RemoteTwinCATOption
    {
        public string ServerIp { get; init; } = "";
        public string ClientAmsNetId { get; init; } = "";
    }

    public sealed class RemoteTwinCAT : Driver.Link
    {
        public string ServerAmsNetId;
        public RemoteTwinCATOption Option;

        public RemoteTwinCAT(string serverAmsNetId, RemoteTwinCATOption option)
        {
            ServerAmsNetId = serverAmsNetId;
            Option = option;
        }

        public override LinkPtr Resolve()
        {
            var serverAmsNetIdBytes = Ffi.ToNullTerminatedUtf8(ServerAmsNetId);
            var clientAmsNetIdBytes = Ffi.ToNullTerminatedUtf8(Option.ClientAmsNetId);
            var serverIpBytes = Ffi.ToNullTerminatedUtf8(Option.ServerIp);
            unsafe
            {
                fixed (byte* serverAmsNetIdPtr = &serverAmsNetIdBytes[0])
                fixed (byte* clientAmsNetIdPtr = &clientAmsNetIdBytes[0])
                fixed (byte* serverIpPtr = &serverIpBytes[0])
                    return NativeMethodsLinkTwinCAT.AUTDLinkRemoteTwinCAT(serverAmsNetIdPtr, serverIpPtr, clientAmsNetIdPtr).Validate();
            }
        }
    }
}
