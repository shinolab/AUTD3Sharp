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
    public sealed class SOEM
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        private delegate void ErrHandlerDelegate(IntPtr context, uint slave, AUTD3Sharp.NativeMethods.Status status);

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

            public SOEMBuilder WithBufSize(uint size)
            {
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithBufSize(_ptr, size);
                return this;
            }

            public SOEMBuilder WithSendCycle(ushort sendCycle)
            {
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithSendCycle(_ptr, sendCycle);
                return this;
            }

            public SOEMBuilder WithSync0Cycle(ushort sync0Cycle)
            {
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithSync0Cycle(_ptr, sync0Cycle);
                return this;
            }

            public SOEMBuilder WithSyncMode(SyncMode syncMode)
            {
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithSyncMode(_ptr, syncMode);
                return this;
            }

            public SOEMBuilder WithTimerStrategy(TimerStrategy timerStrategy)
            {
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithTimerStrategy(_ptr, timerStrategy.Into());
                return this;
            }

            public SOEMBuilder WithErrHandler(Action<int, Status, string> handler)
            {
                _errHandler = (_, slave, status) =>
                {
                    var msgBytes = new byte[128];
                    unsafe
                    {
                        fixed (byte* p = &msgBytes[0]) NativeMethodsLinkSOEM.AUTDLinkSOEMStatusGetMsg(status, p);
                    }
                    handler((int)slave, status.Into(), System.Text.Encoding.UTF8.GetString(msgBytes).TrimEnd('\0'));
                };
                _ptr = NativeMethodsLinkSOEM.AUTDLinkSOEMWithErrHandler(_ptr, Marshal.GetFunctionPointerForDelegate(_errHandler), IntPtr.Zero);
                return this;
            }

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

            SOEM ILinkBuilder<SOEM>.ResolveLink(RuntimePtr _, LinkPtr ptr)
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

    public sealed class RemoteSOEM
    {
        public sealed class RemoteSOEMBuilder : ILinkBuilder<RemoteSOEM>
        {
            private LinkRemoteSOEMBuilderPtr _ptr;

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

            RemoteSOEM ILinkBuilder<RemoteSOEM>.ResolveLink(RuntimePtr _, LinkPtr ptr)
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
