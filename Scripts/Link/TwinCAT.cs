using AUTD3Sharp.NativeMethods;
using System;
using System.Net;
using AUTD3Sharp.Driver;

namespace AUTD3Sharp.Link
{
    public sealed class TwinCAT
    {
        public sealed class TwinCATBuilder : ILinkBuilder<TwinCAT>
        {
            private LinkTwinCATBuilderPtr _ptr;

            internal TwinCATBuilder()
            {
                _ptr = NativeMethodsLinkTwinCAT.AUTDLinkTwinCAT();
            }

            public TwinCATBuilder WithTimeout(TimeSpan timeout)
            {
                _ptr = NativeMethodsLinkTwinCAT.AUTDLinkTwinCATWithTimeout(_ptr, (ulong)(timeout.TotalMilliseconds * 1000 * 1000));
                return this;
            }

            LinkBuilderPtr ILinkBuilder<TwinCAT>.Ptr()
            {
                return NativeMethodsLinkTwinCAT.AUTDLinkTwinCATIntoBuilder(_ptr);
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

    public sealed class RemoteTwinCAT
    {
        public sealed class RemoteTwinCATBuilder : ILinkBuilder<RemoteTwinCAT>
        {
            private LinkRemoteTwinCATBuilderPtr _ptr;

            public RemoteTwinCATBuilder(string serverAmsNetId)
            {
                var serverAmsNetIdBytes = System.Text.Encoding.UTF8.GetBytes(serverAmsNetId);
                unsafe
                {
                    fixed (byte* ap = &serverAmsNetIdBytes[0])
                    {
                        _ptr = NativeMethodsLinkTwinCAT.AUTDLinkRemoteTwinCAT(ap).Validate();
                    }
                }
            }

            public RemoteTwinCATBuilder WithServerIp(IPAddress serverIp)
            {
                var serverIpBytes = serverIp.GetAddressBytes();
                unsafe
                {
                    fixed (byte* ap = &serverIpBytes[0])
                        _ptr = NativeMethodsLinkTwinCAT.AUTDLinkRemoteTwinCATWithServerIP(_ptr, ap);
                }

                return this;
            }

            public RemoteTwinCATBuilder WithClientAmsNetId(string clientAmsNetId)
            {
                var clientAmsNetIdBytes = System.Text.Encoding.UTF8.GetBytes(clientAmsNetId);
                unsafe
                {
                    fixed (byte* ap = &clientAmsNetIdBytes[0])
                        _ptr = NativeMethodsLinkTwinCAT.AUTDLinkRemoteTwinCATWithClientAmsNetId(_ptr, ap);
                }
                return this;
            }

            public RemoteTwinCATBuilder WithTimeout(TimeSpan timeout)
            {
                _ptr = NativeMethodsLinkTwinCAT.AUTDLinkRemoteTwinCATWithTimeout(_ptr, (ulong)(timeout.TotalMilliseconds * 1000 * 1000));
                return this;
            }

            LinkBuilderPtr ILinkBuilder<RemoteTwinCAT>.Ptr()
            {
                return NativeMethodsLinkTwinCAT.AUTDLinkRemoteTwinCATIntoBuilder(_ptr);
            }

            RemoteTwinCAT ILinkBuilder<RemoteTwinCAT>.ResolveLink(RuntimePtr _, LinkPtr ptr)
            {
                return new RemoteTwinCAT();
            }
        }

        public static RemoteTwinCATBuilder Builder(string serverAmsNetId)
        {
            return new RemoteTwinCATBuilder(serverAmsNetId);
        }
    }
}
