using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using AUTD3Sharp.Driver;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Link
{
    /// <summary>
    /// Link using <see href="https://github.com/OpenEtherCATsociety/SOEM">SOEM</see>
    /// </summary>
    public sealed class SOEM
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        private delegate void ErrHandlerDelegate(uint slave, byte status, string str);

        private readonly ErrHandlerDelegate? _errHandler;

        private SOEM(ErrHandlerDelegate? errHandler)
        {
            _errHandler = errHandler;
        }

        public sealed class SOEMBuilder : ILinkBuilder<SOEM>
        {
            private LinkSOEMBuilderPtr _ptr;
            private ErrHandlerDelegate? _errHandler;

            internal SOEMBuilder()
            {
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEM();
                _errHandler = null;
            }

            /// <summary>
            /// Set network interface name
            /// </summary>
            /// <param name="ifname">Interface name. If empty, this link will automatically find the network interface that is connected to AUTD3 devices.</param>
            /// <returns></returns>
            public SOEMBuilder WithIfname(string ifname)
            {
                var ifnameBytes = System.Text.Encoding.UTF8.GetBytes(ifname);
                unsafe
                {
                    fixed (byte* p = &ifnameBytes[0])
                        _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithIfname(_ptr, p);
                }

                return this;
            }

            /// <summary>
            /// Set buffer size
            /// </summary>
            /// <param name="size"></param>
            /// <returns></returns>
            public SOEMBuilder WithBufSize(uint size)
            {
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithBufSize(_ptr, size);
                return this;
            }

            /// <summary>
            /// Set send cycle (the unit is 500us)
            /// </summary>
            /// <param name="sendCycle"></param>
            /// <returns></returns>
            public SOEMBuilder WithSendCycle(ushort sendCycle)
            {
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithSendCycle(_ptr, sendCycle);
                return this;
            }

            /// <summary>
            /// Set sync0 cycle (the unit is 500us)
            /// </summary>
            /// <param name="sync0Cycle"></param>
            /// <returns></returns>
            public SOEMBuilder WithSync0Cycle(ushort sync0Cycle)
            {
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithSync0Cycle(_ptr, sync0Cycle);
                return this;
            }

            /// <summary>
            /// Set sync mode
            /// </summary>
            /// <remarks>See <see href="https://infosys.beckhoff.com/content/1033/ethercatsystem/2469122443.html">Beckhoff's site</see> for more details.</remarks>
            /// <param name="syncMode"></param>
            /// <returns></returns>
            public SOEMBuilder WithSyncMode(SyncMode syncMode)
            {
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithSyncMode(_ptr, syncMode.Into());
                return this;
            }

            /// <summary>
            /// Set timer strategy
            /// </summary>
            /// <param name="timerStrategy"></param>
            /// <returns></returns>
            public SOEMBuilder WithTimerStrategy(TimerStrategy timerStrategy)
            {
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithTimerStrategy(_ptr, timerStrategy);
                return this;
            }

            /// <summary>
            /// Set callback function when some error occurs
            /// </summary>
            /// <param name="handler"></param>
            /// <returns></returns>
            public SOEMBuilder WithErrHandler(Action<int, Status, string> handler)
            {
                _errHandler = (uint slave, byte status, string msg) => handler((int)slave, (Status)status, msg);
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithErrHandler(_ptr, Marshal.GetFunctionPointerForDelegate(_errHandler));
                return this;
            }

            /// <summary>
            /// Set state check interval
            /// </summary>
            /// <param name="interval"></param>
            /// <returns></returns>
            public SOEMBuilder WithStateCheckInterval(TimeSpan interval)
            {
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithStateCheckInterval(_ptr, (uint)interval.TotalMilliseconds);
                return this;
            }

            public SOEMBuilder WithTimeout(TimeSpan timeout)
            {
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithTimeout(_ptr, (ulong)(timeout.TotalMilliseconds * 1000 * 1000));
                return this;
            }

            LinkBuilderPtr ILinkBuilder<SOEM>.Ptr()
            {
                return NativeMethodsLinkSOEM.AUTDLinkSOEMIntoBuilder(_ptr);
            }

            SOEM ILinkBuilder<SOEM>.ResolveLink(LinkPtr ptr)
            {
                return new SOEM(_errHandler);
            }
        }

        public static SOEMBuilder Builder()
        {
            return new SOEMBuilder();
        }

        private static EtherCATAdapter GetAdapter(EthernetAdaptersPtr handle, uint i)
        {
            unsafe
            {
                var sbDesc = new byte[128];
                var sbName = new byte[128];
                fixed (byte* dp = &sbDesc[0])
                fixed (byte* np = &sbName[0])
                {
                    NativeMethodsLinkSOEM.AUTDAdapterGetAdapter(handle, i, dp, np);
                }
                return new EtherCATAdapter(System.Text.Encoding.UTF8.GetString(sbDesc), System.Text.Encoding.UTF8.GetString(sbName));
            }
        }

        public static IEnumerable<EtherCATAdapter> EnumerateAdapters()
        {
            var handle = NativeMethodsLinkSOEM.AUTDAdapterPointer();
            var len = NativeMethodsLinkSOEM.AUTDAdapterGetSize(handle);
            for (uint i = 0; i < len; i++)
                yield return GetAdapter(handle, i);
            NativeMethodsLinkSOEM.AUTDAdapterPointerDelete(handle);
        }
    }

    /// <summary>
    /// Link to connect to remote SOEMServer
    /// </summary>
    public sealed class RemoteSOEM
    {
        public sealed class RemoteSOEMBuilder : ILinkBuilder<RemoteSOEM>
        {
            private LinkRemoteSOEMBuilderPtr _ptr;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="ip">IP address and port of SOEMServer (e.g., "127.0.0.1:8080")</param>
            /// <exception cref="AUTDException"></exception>
            internal RemoteSOEMBuilder(IPEndPoint ip)
            {
                var ipStr = ip.ToString();
                var ipBytes = System.Text.Encoding.UTF8.GetBytes(ipStr);
                unsafe
                {
                    fixed (byte* ipPtr = &ipBytes[0])
                    {
                        _ptr = NativeMethodsLinkSOEM.AUTDLinkRemoteSOEM(ipPtr).Validate();
                    }
                }
            }

            public RemoteSOEMBuilder WithTimeout(TimeSpan timeout)
            {
                _ptr = NativeMethodsLinkSOEM.AUTDLinkRemoteSOEMWithTimeout(_ptr, (ulong)(timeout.TotalMilliseconds * 1000 * 1000));
                return this;
            }

            LinkBuilderPtr ILinkBuilder<RemoteSOEM>.Ptr()
            {
                return NativeMethodsLinkSOEM.AUTDLinkRemoteSOEMIntoBuilder(_ptr);
            }

            RemoteSOEM ILinkBuilder<RemoteSOEM>.ResolveLink(LinkPtr ptr)
            {
                return new RemoteSOEM();
            }
        }

        public static RemoteSOEMBuilder Builder(IPEndPoint ip)
        {
            return new RemoteSOEMBuilder(ip);
        }
    }

    public readonly struct EtherCATAdapter : IEquatable<EtherCATAdapter>
    {
        public string Desc { get; }
        public string Name { get; }

        internal EtherCATAdapter(string desc, string name)
        {
            Desc = desc;
            Name = name;
        }

        public override string ToString() => $"{Desc}, {Name}";
        public bool Equals(EtherCATAdapter other) => Desc.Equals(other.Desc) && Name.Equals(other.Name);
        public static bool operator ==(EtherCATAdapter left, EtherCATAdapter right) => left.Equals(right);
        public static bool operator !=(EtherCATAdapter left, EtherCATAdapter right) => !left.Equals(right);
        public override bool Equals(object? obj) => obj is EtherCATAdapter adapter && Equals(adapter);
        public override int GetHashCode() => Desc.GetHashCode() ^ Name.GetHashCode();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
