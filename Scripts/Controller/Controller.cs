using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.FPGA.Defined;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;
using AUTD3Sharp.Driver.Datagram;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public sealed class Controller : Geometry, IDisposable
    {
        private bool _isDisposed;
        internal ControllerPtr Ptr;
        private readonly LinkPtr _linkPtr;

        internal Controller(GeometryPtr geometry, ControllerPtr ptr, LinkPtr linkPtr) : base(geometry)
        {
            Ptr = ptr;
            _linkPtr = linkPtr;
        }

        public static Controller Open<T>(IEnumerable<AUTD3> devices, T link) where T : Driver.Link => OpenWithOption(devices, link, new SenderOption());

        public static Controller OpenWithOption<T>(IEnumerable<AUTD3> devices, T link, SenderOption option)
            where T : Driver.Link
        {
            var devicesArray = devices as AUTD3[] ?? devices.ToArray();
            var pos = devicesArray.Select(d => d.Pos).ToArray();
            var rot = devicesArray.Select(d => d.Rot).ToArray();
            var linkPtr = link.Resolve();
            unsafe
            {
                fixed (Point3* pPos = &pos[0])
                fixed (Quaternion* pRot = &rot[0])
                {
                    var ptr = NativeMethodsBase.AUTDControllerOpen(pPos, pRot, (ushort)devicesArray.Length, linkPtr, option.ToNative()).Validate();
                    var geometryPtr = NativeMethodsBase.AUTDGeometry(ptr);
                    return new Controller(geometryPtr, ptr, NativeMethodsBase.AUTDLinkGet(ptr));
                }
            }
        }

        public Sender Sender(SenderOption option) => new(NativeMethodsBase.AUTDSender(Ptr, option.ToNative()), Geometry());

        public void Send<TD>(TD d) where TD : IDatagram => Sender(new SenderOption()).Send(d);
        public void Send<TD1, TD2>((TD1, TD2) d) where TD1 : IDatagram where TD2 : IDatagram => Sender(new SenderOption()).Send(d);
        public void GroupSend(Func<Device, object?> keyMap, GroupDictionary datagramMap) => Sender(new SenderOption()).GroupSend(keyMap, datagramMap);

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

        public FirmwareVersion[] FirmwareVersion()
        {
            var result = NativeMethodsBase.AUTDControllerFirmwareVersionListPointer(Ptr);
            var handle = result.Validate();
            var list = Enumerable.Range(0, NumDevices()).Select(i => GetFirmwareVersion(handle, (uint)i)).ToArray();
            NativeMethodsBase.AUTDControllerFirmwareVersionListPointerDelete(handle);
            return list;
        }

        public FPGAState?[] FPGAState()
        {
            var result = NativeMethodsBase.AUTDControllerFPGAState(Ptr);
            var p = result.Validate();
            var states = Enumerable.Range(0, NumDevices()).Select(i => NativeMethodsBase.AUTDControllerFPGAStateGet(p, (uint)i)).Select(x => x < 0 ? null : new FPGAState((byte)x)).ToArray();
            NativeMethodsBase.AUTDControllerFPGAStateDelete(p);
            return states;
        }

        public void Close()
        {
            if (Ptr.Item1 == IntPtr.Zero) return;
            var result = NativeMethodsBase.AUTDControllerClose(Ptr);
            Ptr.Item1 = IntPtr.Zero;
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
        ~Controller() { Dispose(); }

        public Geometry Geometry() => this;

        public T Link<T>() where T: Driver.ILink , new()
        {
            var link = new T();
            link.Resolve(_linkPtr);
            return link;
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
