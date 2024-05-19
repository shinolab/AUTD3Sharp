#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Driver.FPGA.Defined;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public sealed class Controller<T> : IDisposable
    {
        #region field

        private bool _isDisposed;
        internal ControllerPtr Ptr;

        #endregion

        #region Controller

        internal Controller(Geometry geometry, ControllerPtr ptr, T link)
        {
            Ptr = ptr;
            Geometry = geometry;
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

        public async Task<FirmwareVersion[]> FirmwareVersionListAsync()
        {
            var handle = await Task.Run(() => NativeMethodsBase.AUTDControllerFirmwareVersionListPointer(Ptr).Validate());
            var result = Enumerable.Range(0, Geometry.NumDevices).Select(i => GetFirmwareVersion(handle, (uint)i)).ToArray();
            NativeMethodsBase.AUTDControllerFirmwareVersionListPointerDelete(handle);
            return result;
        }

        public FirmwareVersion[] FirmwareVersionList()
        {
            var handle = NativeMethodsBase.AUTDControllerFirmwareVersionListPointer(Ptr).Validate();
            var result = Enumerable.Range(0, Geometry.NumDevices).Select(i => GetFirmwareVersion(handle, (uint)i)).ToArray();
            NativeMethodsBase.AUTDControllerFirmwareVersionListPointerDelete(handle);
            return result;
        }

        public async Task CloseAsync()
        {
            await Task.Run(() => NativeMethodsBase.AUTDControllerClose(Ptr).Validate());
        }

        public void Close()
        {
            NativeMethodsBase.AUTDControllerClose(Ptr).Validate();
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            if (Ptr.Item1 != IntPtr.Zero) NativeMethodsBase.AUTDControllerDelete(Ptr);
            Ptr.Item1 = IntPtr.Zero;

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

        public Geometry Geometry { get; }

        public FPGAState?[] FPGAState()
        {
            var infos = new int[Geometry.NumDevices];
            unsafe
            {
                fixed (int* ptr = &infos[0])
                {
                    NativeMethodsBase.AUTDControllerFPGAState(Ptr, ptr).Validate();
                    return infos.Select(x => x < 0 ? null : new FPGAState((byte)x)).ToArray();
                }
            }
        }

        public async Task<FPGAState?[]> FPGAStateAsync()
        {
            return await Task.Run(FPGAState);
        }

        public T Link { get; }

        #endregion

        public async Task SendAsync(IDatagram data, TimeSpan? timeout = null)
        {
            await SendAsync(data, new NullDatagram(), timeout);
        }

        public void Send(IDatagram data, TimeSpan? timeout = null)
        {
            Send(data, new NullDatagram(), timeout);
        }

        public async Task SendAsync(IDatagram data1, IDatagram data2, TimeSpan? timeout = null)
        {
            await Task.Run(() => NativeMethodsBase.AUTDControllerSend(Ptr, data1.Ptr(Geometry), data2.Ptr(Geometry),
                (long)(timeout?.TotalMilliseconds * 1000 * 1000 ?? -1)).Validate());
        }

        public void Send(IDatagram data1, IDatagram data2, TimeSpan? timeout = null)
        {
            NativeMethodsBase.AUTDControllerSend(Ptr, data1.Ptr(Geometry), data2.Ptr(Geometry),
                (long)(timeout?.TotalMilliseconds * 1000 * 1000 ?? -1)).Validate();
        }

        public async Task SendAsync((IDatagram, IDatagram) data, TimeSpan? timeout = null)
        {
            var (data1, data2) = data;
            await SendAsync(data1, data2, timeout);
        }

        public void Send((IDatagram, IDatagram) data, TimeSpan? timeout = null)
        {
            var (data1, data2) = data;
            Send(data1, data2, timeout);
        }

        public GroupGuard<T> Group(Func<Device, object?> map)
        {
            return new GroupGuard<T>(map, this);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
