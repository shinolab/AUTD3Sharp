using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain
{
    /// <summary>
    /// Gain to set amp and phase uniformly
    /// </summary>

    [Gain]
    public sealed partial class TransducerTest
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public unsafe delegate void TransducerTestDelegate(ContextPtr context, GeometryPtr geometryPtr, uint devIdx, byte trIdx, DriveRaw* raw);

        private readonly TransducerTestDelegate _f;

        public TransducerTest(Func<Device, Transducer, Drive?> f)
        {
            var nullDrive = new Drive(new Phase(0x00), 0x00);
            unsafe
            {
                _f = (context, geometryPtr, devIdx, trIdx, raw) =>
                {
                    var dev = new Device((int)devIdx, NativeMethodsBase.AUTDDevice(geometryPtr, devIdx));
                    var tr = new Transducer(trIdx, dev.Ptr);
                    var d = f(dev, tr) ?? nullDrive;
                    raw->phase = d.Phase.Value;
                    raw->intensity = d.Intensity.Value;
                };
            }
        }
        private GainPtr GainPtr(Geometry geometry)
        {
            return NativeMethodsBase.AUTDGainTransducerTest(Marshal.GetFunctionPointerForDelegate(_f), new ContextPtr { Item1 = IntPtr.Zero }, geometry.Ptr);
        }
    }
}
