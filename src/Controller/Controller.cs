#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#define DIMENSION_M
#endif

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
using AUTD3Sharp.Driver.Geometry;

namespace AUTD3Sharp
{
    /// <summary>
    /// Controller class for AUTD3
    /// </summary>
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

        private static FirmwareInfo GetFirmwareInfo(FirmwareInfoListPtr handle, uint i)
        {
            var info = new byte[256];
            unsafe
            {
                fixed (byte* p = &info[0])
                {
                    NativeMethodsBase.AUTDControllerFirmwareInfoGet(handle, i, p);
                    return new FirmwareInfo(System.Text.Encoding.UTF8.GetString(info));
                }
            }
        }

        /// <summary>
        /// Get list of FPGA information
        /// </summary>
        /// <exception cref="AUTDException"></exception>
        public async Task<FirmwareInfo[]> FirmwareInfoListAsync()
        {
            var handle = await Task.Run(() => NativeMethodsBase.AUTDControllerFirmwareInfoListPointer(Ptr).Validate());
            var result = Enumerable.Range(0, Geometry.NumDevices).Select(i => GetFirmwareInfo(handle, (uint)i)).ToArray();
            NativeMethodsBase.AUTDControllerFirmwareInfoListPointerDelete(handle);
            return result;
        }

        /// <summary>
        /// Get list of FPGA information
        /// </summary>
        /// <exception cref="AUTDException"></exception>
        public FirmwareInfo[] FirmwareInfoList()
        {
            var handle = NativeMethodsBase.AUTDControllerFirmwareInfoListPointer(Ptr).Validate();
            var result = Enumerable.Range(0, Geometry.NumDevices).Select(i => GetFirmwareInfo(handle, (uint)i)).ToArray();
            NativeMethodsBase.AUTDControllerFirmwareInfoListPointerDelete(handle);
            return result;
        }

        /// <summary>
        /// Close connection
        /// </summary>
        /// <exception cref="AUTDException"></exception>
        public async Task<bool> CloseAsync()
        {
            return await Task.Run(() => NativeMethodsBase.AUTDControllerClose(Ptr).Validate() == NativeMethodsDef.AUTD3_TRUE);
        }

        /// <summary>
        /// Close connection
        /// </summary>
        /// <exception cref="AUTDException"></exception>
        public bool Close()
        {
            return NativeMethodsBase.AUTDControllerClose(Ptr).Validate() == NativeMethodsDef.AUTD3_TRUE;
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


        /// <summary>
        /// List of FPGA state
        /// </summary>
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

        /// <summary>
        /// List of FPGA state
        /// </summary>
        public async Task<FPGAState?[]> FPGAStateAsync()
        {
            return await Task.Run(FPGAState);
        }


        public T Link { get; }

        #endregion

        /// <summary>
        /// Send data to the devices
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="timeout"></param>
        /// <returns> If true, it is confirmed that the data has been successfully transmitted. Otherwise, there are no errors, but it is unclear whether the data has been sent reliably or not.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="AUTDException"></exception>
        public async Task<bool> SendAsync(IDatagram data, TimeSpan? timeout = null)
        {
            return await SendAsync(data, new NullDatagram(), timeout);
        }


        /// <summary>
        /// Send data to the devices
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="timeout"></param>
        /// <returns> If true, it is confirmed that the data has been successfully transmitted. Otherwise, there are no errors, but it is unclear whether the data has been sent reliably or not.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="AUTDException"></exception>
        public bool Send(IDatagram data, TimeSpan? timeout = null)
        {
            return Send(data, new NullDatagram(), timeout);
        }

        /// <summary>
        /// Send data to the devices
        /// </summary>
        /// <param name="data1">First data</param>
        /// <param name="data2">Second data</param>
        /// <param name="timeout"></param>
        /// <returns> If true, it is confirmed that the data has been successfully transmitted. Otherwise, there are no errors, but it is unclear whether the data has been sent reliably or not.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="AUTDException"></exception>
        public async Task<bool> SendAsync(IDatagram data1, IDatagram data2, TimeSpan? timeout = null)
        {
            return await Task.Run(() => NativeMethodsBase.AUTDControllerSend(Ptr, data1.Ptr(Geometry), data2.Ptr(Geometry),
                (long)(timeout?.TotalMilliseconds * 1000 * 1000 ?? -1)).Validate() == NativeMethodsDef.AUTD3_TRUE);
        }


        /// <summary>
        /// Send data to the devices
        /// </summary>
        /// <param name="data1">First data</param>
        /// <param name="data2">Second data</param>
        /// <param name="timeout"></param>
        /// <returns> If true, it is confirmed that the data has been successfully transmitted. Otherwise, there are no errors, but it is unclear whether the data has been sent reliably or not.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="AUTDException"></exception>
        public bool Send(IDatagram data1, IDatagram data2, TimeSpan? timeout = null)
        {
            return NativeMethodsBase.AUTDControllerSend(Ptr, data1.Ptr(Geometry), data2.Ptr(Geometry),
                (long)(timeout?.TotalMilliseconds * 1000 * 1000 ?? -1)).Validate() == NativeMethodsDef.AUTD3_TRUE;
        }

        /// <summary>
        /// Send data to the devices
        /// </summary>
        /// <param name="data">Tuple of data</param>
        /// <param name="timeout"></param>
        /// <returns> If true, it is confirmed that the data has been successfully transmitted. Otherwise, there are no errors, but it is unclear whether the data has been sent reliably or not.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="AUTDException"></exception>
        public async Task<bool> SendAsync((IDatagram, IDatagram) data, TimeSpan? timeout = null)
        {
            var (data1, data2) = data;
            return await SendAsync(data1, data2, timeout);
        }

        /// <summary>
        /// Send data to the devices
        /// </summary>
        /// <param name="data">Tuple of data</param>
        /// <param name="timeout"></param>
        /// <returns> If true, it is confirmed that the data has been successfully transmitted. Otherwise, there are no errors, but it is unclear whether the data has been sent reliably or not.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="AUTDException"></exception>
        public bool Send((IDatagram, IDatagram) data, TimeSpan? timeout = null)
        {
            var (data1, data2) = data;
            return Send(data1, data2, timeout);
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
