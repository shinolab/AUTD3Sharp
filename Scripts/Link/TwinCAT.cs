using AUTD3Sharp.NativeMethods;
using System;
using System.Net;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Driver;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Link
{
    public sealed class TwinCAT
    {
        public sealed class TwinCATBuilder : ILinkBuilder<TwinCAT>
        {
            internal TwinCATBuilder()
            {
            }

            LinkBuilderPtr ILinkBuilder<TwinCAT>.Ptr()
            {
                return NativeMethodsLinkTwinCAT.AUTDLinkTwinCAT();
            }

            TwinCAT ILinkBuilder<TwinCAT>.ResolveLink(RuntimePtr _, LinkPtr ptr)
            {
                return new TwinCAT();
            }
        }

        public static TwinCATBuilder Builder()
        {
            return new TwinCATBuilder();
        }
    }

    [Builder]
    public sealed partial class RemoteTwinCATBuilder : ILinkBuilder<RemoteTwinCAT>
    {
        public string ServerAmsNetId { get; }

        [Property]
        public string ServerIp { get; private set; }

        [Property]
        public string ClientAmsNetId { get; private set; }

        public RemoteTwinCATBuilder(string serverAmsNetId)
        {
            ServerAmsNetId = serverAmsNetId;
            ServerIp = string.Empty;
            ClientAmsNetId = string.Empty;
        }

        LinkBuilderPtr ILinkBuilder<RemoteTwinCAT>.Ptr()
        {
            var serverAmsNetIdBytes = Ffi.toNullTerminatedUtf8(ServerAmsNetId);
            var serverIpBytes = Ffi.toNullTerminatedUtf8(ServerIp);
            var clientAmsNetIdBytes = Ffi.toNullTerminatedUtf8(ClientAmsNetId);
            unsafe
            {
                fixed (byte* sp = &serverAmsNetIdBytes[0])
                fixed (byte* ip = serverIpBytes)
                fixed (byte* cp = clientAmsNetIdBytes)
                    return NativeMethodsLinkTwinCAT.AUTDLinkRemoteTwinCAT(sp, ip, cp).Validate();
            }
        }

        RemoteTwinCAT ILinkBuilder<RemoteTwinCAT>.ResolveLink(RuntimePtr _, LinkPtr ptr)
        {
            return new RemoteTwinCAT();
        }
    }

    public sealed class RemoteTwinCAT
    {
        public static RemoteTwinCATBuilder Builder(string serverAmsNetId)
        {
            return new RemoteTwinCATBuilder(serverAmsNetId);
        }
    }
}
