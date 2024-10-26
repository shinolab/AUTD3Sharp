using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Driver;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Link
{

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
    delegate void ErrHandlerDelegate(IntPtr context, uint slave, AUTD3Sharp.NativeMethods.Status status);

    [Builder]
    public sealed partial class SOEMBuilder : ILinkBuilder<SOEM>
    {
        [Property]
        public string Ifname { get; private set; } = string.Empty;

        [Property]
        public uint BufSize { get; private set; } = 32;

        [Property]
        public TimeSpan SendCycle { get; private set; } = TimeSpan.FromMilliseconds(1);

        [Property]
        public TimeSpan Sync0Cycle { get; private set; } = TimeSpan.FromMilliseconds(1);

        [Property]
        public TimerStrategy TimerStrategy { get; private set; } = TimerStrategy.SpinSleep;

        [Property]
        public SyncMode SyncMode { get; private set; } = SyncMode.DC;

        [Property]
        public TimeSpan SyncTolerance { get; private set; } = TimeSpan.FromMilliseconds(0.001);

        [Property]
        public TimeSpan SyncTimeout { get; private set; } = TimeSpan.FromSeconds(10);

        [Property]
        public TimeSpan StateCheckInterval { get; private set; } = TimeSpan.FromMilliseconds(100);

        [Property]
        public ThreadPriorityPtr ThreadPriority { get; private set; } = Link.ThreadPriority.Max;

        [Property]
        public ProcessPriority ProcessPriority { get; private set; } = ProcessPriority.High;

        [Property]
        public Action<int, Status> ErrHandler { get; private set; } = (_, _) => { };

        private ErrHandlerDelegate? _errHandler;

        internal SOEMBuilder()
        {
            _errHandler = null;
        }

        LinkBuilderPtr ILinkBuilder<SOEM>.Ptr()
        {
            _errHandler = (_, slave, status) =>
            {
                var msgBytes = new byte[128];
                unsafe
                {
#pragma warning disable CA1806
                    fixed (byte* p = &msgBytes[0]) NativeMethodsLinkSOEM.AUTDLinkSOEMStatusGetMsg(status, p);
#pragma warning restore CA1806
                }
                ErrHandler((int)slave, new Link.Status(status, System.Text.Encoding.UTF8.GetString(msgBytes).TrimEnd('\0')));
            };
            var ifnameBytes = System.Text.Encoding.UTF8.GetBytes(Ifname);
            unsafe
            {
                fixed (byte* ifnamePtr = ifnameBytes)
                {
                    return NativeMethodsLinkSOEM.AUTDLinkSOEM(
                        ifnamePtr,
                        BufSize,
                        (ulong)(SendCycle.TotalMilliseconds * 1000 * 1000),
                        (ulong)(Sync0Cycle.TotalMilliseconds * 1000 * 1000),
                        new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_errHandler) },
                        new ConstPtr { Item1 = IntPtr.Zero },
                        SyncMode,
                        ProcessPriority,
                        ThreadPriority,
                        (ulong)(StateCheckInterval.TotalMilliseconds * 1000 * 1000),
                        TimerStrategy,
                        (ulong)(SyncTolerance.TotalMilliseconds * 1000 * 1000),
                        (ulong)(SyncTimeout.TotalMilliseconds * 1000 * 1000)
                    ).Validate();
                }
            }
        }

        SOEM ILinkBuilder<SOEM>.ResolveLink(RuntimePtr _, LinkPtr ptr)
        {
            return new SOEM(_errHandler);
        }
    }

    public sealed class SOEM
    {
        private readonly ErrHandlerDelegate? _errHandler;

        internal SOEM(ErrHandlerDelegate? errHandler)
        {
            _errHandler = errHandler;
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
            public IPEndPoint Ip { get; }

            internal RemoteSOEMBuilder(IPEndPoint ip)
            {
                Ip = ip;
            }

            LinkBuilderPtr ILinkBuilder<RemoteSOEM>.Ptr()
            {
                var ipStr = Ip.ToString();
                var ipBytes = System.Text.Encoding.UTF8.GetBytes(ipStr);
                unsafe
                {
                    fixed (byte* ipPtr = &ipBytes[0])
                    {
                        return NativeMethodsLinkSOEM.AUTDLinkRemoteSOEM(ipPtr).Validate();
                    }
                }
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

    public static class ThreadPriority
    {
        public static ThreadPriorityPtr Min = NativeMethodsLinkSOEM.AUTDLinkSOEMThreadPriorityMin();
        public static ThreadPriorityPtr Max = NativeMethodsLinkSOEM.AUTDLinkSOEMThreadPriorityMax();
        public static ThreadPriorityPtr Crossplatform(byte value)
        {
            if (value > 99) throw new ArgumentOutOfRangeException(nameof(value), "value must be between 0 and 99");
            return NativeMethodsLinkSOEM.AUTDLinkSOEMThreadPriorityCrossplatform(value);
        }
    }

    public class Status : IEquatable<Status>
    {
        private readonly NativeMethods.Status _inner;
        private readonly string _msg;

        public Status(NativeMethods.Status status, string msg)
        {
            _inner = status;
            _msg = msg;
        }

        public static Status Lost => new Status(NativeMethods.Status.Lost, "");
        public static Status StateChanged => new Status(NativeMethods.Status.StateChanged, "");
        public static Status Error => new Status(NativeMethods.Status.Error, "");

        public override string ToString()
        {
            return _msg;
        }

        [ExcludeFromCodeCoverage]
        public bool Equals(Status? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _inner == other._inner;
        }

        [ExcludeFromCodeCoverage]
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Status)obj);
        }

        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            return HashCode.Combine((int)_inner);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
