#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Driver.FPGA.Defined;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public sealed class Controller<T> : Geometry, IDisposable
    {
        #region field

        private bool _isDisposed;
        internal RuntimePtr Runtime;
        internal HandlePtr Handle;
        internal ControllerPtr Ptr;

        #endregion

        #region Controller

        internal Controller(GeometryPtr geometry, RuntimePtr runtime, HandlePtr handle, ControllerPtr ptr, T link) : base(geometry)
        {
            Runtime = runtime;
            Handle = handle;
            Ptr = ptr;
            Link = link;
        }

        private static FirmwareVersion GetFirmwareVersion(FirmwareVersionListPtr handle, uint i)
        {
            var info = new byte[256];
            unsafe
            {
                fixed (byte* p = &info[0])
                {
                    NativeMethodsBase.AUTDControllerFirmwareVersionGet(handle, i, p);
                    return new FirmwareVersion(System.Text.Encoding.UTF8.GetString(info));
                }
            }
        }

        public async Task<FirmwareVersion[]> FirmwareVersionAsync()
        {
            var future = NativeMethodsBase.AUTDControllerFirmwareVersionListPointer(Ptr);
            var result = await Task.Run(() => NativeMethodsBase.AUTDWaitResultFirmwareVersionList(Handle, future));
            var handle = result.Validate();
            var list = Enumerable.Range(0, Geometry.NumDevices).Select(i => GetFirmwareVersion(handle, (uint)i)).ToArray();
            NativeMethodsBase.AUTDControllerFirmwareVersionListPointerDelete(handle);
            return list;
        }

        public FirmwareVersion[] FirmwareVersion()
        {
            var future = NativeMethodsBase.AUTDControllerFirmwareVersionListPointer(Ptr);
            var result = NativeMethodsBase.AUTDWaitResultFirmwareVersionList(Handle, future);
            var handle = result.Validate();
            var list = Enumerable.Range(0, Geometry.NumDevices).Select(i => GetFirmwareVersion(handle, (uint)i)).ToArray();
            NativeMethodsBase.AUTDControllerFirmwareVersionListPointerDelete(handle);
            return list;
        }

        public async Task CloseAsync()
        {
            if (Ptr.Item1 == IntPtr.Zero) return;
            var future = NativeMethodsBase.AUTDControllerClose(Ptr);
            var result = await Task.Run(() => NativeMethodsBase.AUTDWaitResultStatus(Handle, future));
            Ptr.Item1 = IntPtr.Zero;
            NativeMethodsBase.AUTDDeleteRuntime(Runtime);
            Runtime.Item1 = IntPtr.Zero;
            Handle.Item1 = IntPtr.Zero;
            result.Validate();
        }

        public void Close()
        {
            if (Ptr.Item1 == IntPtr.Zero) return;
            var future = NativeMethodsBase.AUTDControllerClose(Ptr);
            var result = NativeMethodsBase.AUTDWaitResultStatus(Handle, future);
            Ptr.Item1 = IntPtr.Zero;
            NativeMethodsBase.AUTDDeleteRuntime(Runtime);
            Runtime.Item1 = IntPtr.Zero;
            Handle.Item1 = IntPtr.Zero;
            result.Validate();
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            Close();

            _isDisposed = true;
            GC.SuppressFinalize(this);
        }

        [ExcludeFromCodeCoverage]
        ~Controller()
        {
            Dispose();
        }

        #endregion

        #region Property

        public Geometry Geometry => this;

        public FPGAState?[] FPGAState()
        {
            var future = NativeMethodsBase.AUTDControllerFPGAState(Ptr);
            var result = NativeMethodsBase.AUTDWaitResultFPGAStateList(Handle, future);
            var p = result.Validate();
            var states = Enumerable.Range(0, Geometry.NumDevices).Select(i => NativeMethodsBase.AUTDControllerFPGAStateGet(p, (uint)i)).Select(
                x => x < 0 ? null : new FPGAState((byte)x)).ToArray();
            NativeMethodsBase.AUTDControllerFPGAStateDelete(p);
            return states;
        }

        public async Task<FPGAState?[]> FPGAStateAsync()
        {
            var future = NativeMethodsBase.AUTDControllerFPGAState(Ptr);
            var result = await Task.Run(() => NativeMethodsBase.AUTDWaitResultFPGAStateList(Handle, future));
            var p = result.Validate();
            var states = Enumerable.Range(0, Geometry.NumDevices).Select(i => NativeMethodsBase.AUTDControllerFPGAStateGet(p, (uint)i)).Select(
                x => x < 0 ? null : new FPGAState((byte)x)).ToArray();
            NativeMethodsBase.AUTDControllerFPGAStateDelete(p);
            return states;
        }

        public T Link { get; }

        #endregion

        public async Task SendAsync<TD>(TD d)
        where TD : IDatagram
        {
            var future = NativeMethodsBase.AUTDControllerSend(Ptr, d.Ptr(Geometry));
            var result = await Task.Run(() => NativeMethodsBase.AUTDWaitResultStatus(Handle, future));
            result.Validate();
        }

        public async Task SendAsync<TD1, TD2>((TD1, TD2) d)
        where TD1 : IDatagram
        where TD2 : IDatagram
        {
            await SendAsync(new DatagramTuple<TD1, TD2>(d));
        }

        public void Send<TD>(TD d)
        where TD : IDatagram
        {
            var future = NativeMethodsBase.AUTDControllerSend(Ptr, d.Ptr(Geometry));
            var result = NativeMethodsBase.AUTDWaitResultStatus(Handle, future);
            result.Validate();
        }

        public void Send<TD1, TD2>((TD1, TD2) d)
        where TD1 : IDatagram
        where TD2 : IDatagram
        {
            Send(new DatagramTuple<TD1, TD2>(d));
        }

        public GroupGuard<T> Group(Func<Device, object?> map)
        {
            return new GroupGuard<T>(map, this);
        }
    }

    public static class Controller
    {
        public static ControllerBuilder Builder(IEnumerable<AUTD3> iter) => new(iter);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
