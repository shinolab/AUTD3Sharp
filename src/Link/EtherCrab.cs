using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Link
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false,
        ThrowOnUnmappableChar = true)]
    internal delegate void ErrHandlerDelegate(IntPtr context, uint slave, NativeMethods.Status status);

    public class EtherCrabOption
    {
        public string? Ifname { get; init; } = null;
        public uint BufSize { get; init; } = 16;
        public Duration StateCheckPeriod { get; init; } = Duration.FromMillis(100);
        public Duration Sync0Period { get; init; } = Duration.FromMillis(2);
        public Duration SyncTolerance { get; init; } = Duration.FromMicros(1);
        public Duration SyncTimeout { get; init; } = Duration.FromSecs(10);
        public ThreadPriority? TxRxThreadBuilder { get; init; } = null;
        public CoreId? TxRxThreadAffinity { get; init; } = null;
        public ThreadPriority? MainThreadBuilder { get; init; } = null;
        public CoreId? MainThreadAffinity { get; init; } = null;

        [ExcludeFromCodeCoverage]
        internal NativeMethods.EtherCrabOption ToNative()
        {
            var option = new NativeMethods.EtherCrabOption
            {
                ifname = null,
                buf_size = BufSize,
                timeouts_state_transition = Duration.FromSecs(10),
                timeouts_pdu = Duration.FromMillis(100),
                timeouts_eeprom = Duration.FromMillis(10),
                timeouts_wait_loop_delay = Duration.Zero,
                timeouts_mailbox_echo = Duration.FromMillis(100),
                timeouts_mailbox_response = Duration.FromMillis(1000),
                main_device_config_dc_static_sync_iterations = 10000,
                main_device_config_retry_behaviour = 0,
                dc_configuration_start_delay = Duration.FromMillis(100),
                dc_configuration_sync0_period = Sync0Period,
                dc_configuration_sync0_shift = Duration.Zero,
                state_check_period = StateCheckPeriod,
                sync_tolerance = SyncTolerance,
                sync_timeout = SyncTimeout,
                tx_rx_thread_builder = TxRxThreadBuilder is null ? new ThreadPriorityPtr { Item1 = IntPtr.Zero } : TxRxThreadBuilder.Ptr,
                tx_rx_thread_affinity = TxRxThreadAffinity is null ? -1 : (int)TxRxThreadAffinity.Id,
                main_thread_builder = MainThreadBuilder is null ? new ThreadPriorityPtr { Item1 = IntPtr.Zero } : MainThreadBuilder.Ptr,
                main_thread_affinity = MainThreadAffinity is null ? -1 : (int)MainThreadAffinity.Id
            };

            unsafe
            {
                if (Ifname is not null)
                {
                    var ifnameBytes = Ffi.ToNullTerminatedUtf8(Ifname);
                    fixed (byte* pIfname = &ifnameBytes[0])
                    {
                        option.ifname = pIfname;
                    }
                }
            }
            return option;
        }
    }

    public sealed class EtherCrab : Driver.Link
    {
        private readonly ErrHandlerDelegate _errHandler;
        private readonly EtherCrabOption _option;

        [ExcludeFromCodeCoverage]
        public EtherCrab(Action<int, Status> errHandler, EtherCrabOption option)
        {
            _errHandler = (_, slave, status) =>
            {
                var msgBytes = new byte[128];
                unsafe
                {
#pragma warning disable CA1806
                    fixed (byte* p = &msgBytes[0]) NativeMethodsLinkEthercrab.AUTDLinkEtherCrabStatusGetMsg(status, p);
#pragma warning restore CA1806
                }
                errHandler((int)slave, new Status(status, System.Text.Encoding.UTF8.GetString(msgBytes).TrimEnd('\0')));
            };
            _option = option;
        }

        [ExcludeFromCodeCoverage]
        public override LinkPtr Resolve() => NativeMethodsLinkEthercrab.AUTDLinkEtherCrab(
                            new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_errHandler) },
                            new ConstPtr { Item1 = IntPtr.Zero },
                            _option.ToNative()).Validate();

        public static class Tracing
        {
#if UNITY_2020_2_OR_NEWER
            public static void Init(string path)
            {
                var pathBytes = Ffi.ToNullTerminatedUtf8(path);
                unsafe
                {
                    fixed (byte* pathPtr = &pathBytes[0])
                    {
                        NativeMethodsLinkEthercrab.AUTDLinkEtherCrabTracingInitWithFile(pathPtr);
                    }
                }
            }
#else
#pragma warning disable CA2255
            [ModuleInitializer]
            public static void Init()
            {
                NativeMethodsLinkEthercrab.AUTDLinkEtherCrabTracingInit();
            }
#pragma warning restore CA2255
#endif
        }
    }

    public class ThreadPriority
    {
        internal readonly ThreadPriorityPtr Ptr;

        private ThreadPriority(ThreadPriorityPtr ptr)
        {
            Ptr = ptr;
        }

        public static ThreadPriority Min = new(NativeMethodsLinkEthercrab.AUTDLinkEtherCrabThreadPriorityMin());
        public static ThreadPriority Max = new(NativeMethodsLinkEthercrab.AUTDLinkEtherCrabThreadPriorityMax());

        public static ThreadPriority Crossplatform(byte value)
        {
            if (value > 99) throw new ArgumentOutOfRangeException(nameof(value), "value must be between 0 and 99");
            return new ThreadPriority(NativeMethodsLinkEthercrab.AUTDLinkEtherCrabThreadPriorityCrossplatform(value));
        }
    }

    public class CoreId
    {
        public uint Id { get; set; }
    }

    public class Status : IEquatable<Status>
    {
        private readonly NativeMethods.Status _inner;
        private readonly string _msg;

        internal Status(NativeMethods.Status status, string msg)
        {
            _inner = status;
            _msg = msg;
        }

        public static Status Lost => new(NativeMethods.Status.Lost, "");
        public static Status StateChanged => new(NativeMethods.Status.StateChanged, "");
        public static Status Error => new(NativeMethods.Status.Error, "");
        public static Status Resumed => new(NativeMethods.Status.Resumed, "");

        public override string ToString() => _msg;

        public static bool operator ==(Status left, Status right) => left.Equals(right);
        public static bool operator !=(Status left, Status right) => !left.Equals(right);
        public bool Equals(Status? other) => other is not null && _inner.Equals(other._inner);
        public override bool Equals(object? obj) => obj is Status other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => _inner.GetHashCode();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
