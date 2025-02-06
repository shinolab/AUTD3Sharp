using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Gain.Holo
{
    public sealed class NalgebraBackend : Backend
    {
        public NalgebraBackend() => Ptr = NativeMethodsGainHolo.AUTDNalgebraBackendSphere();

        [ExcludeFromCodeCoverage]
        ~NalgebraBackend()
        {
            NativeMethodsGainHolo.AUTDDeleteNalgebraBackendSphere(Ptr);
            Ptr.Item1 = IntPtr.Zero;
        }

        internal override GainPtr Gs((Point3, Amplitude)[] foci, NativeMethods.GSOption option)
        {
            var points = foci.Select(f => f.Item1).ToArray();
            var amps = foci.Select(f => f.Item2.Pascal()).ToArray();
            unsafe
            {
                fixed (Point3* pPoints = &points[0])
                fixed (float* pAmps = &amps[0])
                    return NativeMethodsGainHolo.AUTDGainHoloGSSphere(Ptr, pPoints, pAmps, (uint)foci.Length, option);
            }
        }

        internal override GainPtr Gspat((Point3, Amplitude)[] foci, NativeMethods.GSPATOption option)
        {
            var points = foci.Select(f => f.Item1).ToArray();
            var amps = foci.Select(f => f.Item2.Pascal()).ToArray();
            unsafe
            {
                fixed (Point3* pPoints = &points[0])
                fixed (float* pAmps = &amps[0])
                    return NativeMethodsGainHolo.AUTDGainHoloGSPATSphere(Ptr, pPoints, pAmps, (uint)foci.Length, option);
            }
        }

        internal override GainPtr Naive((Point3, Amplitude)[] foci, NativeMethods.NaiveOption option)
        {
            var points = foci.Select(f => f.Item1).ToArray();
            var amps = foci.Select(f => f.Item2.Pascal()).ToArray();
            unsafe
            {
                fixed (Point3* pPoints = &points[0])
                fixed (float* pAmps = &amps[0])
                    return NativeMethodsGainHolo.AUTDGainHoloNaiveSphere(Ptr, pPoints, pAmps, (uint)foci.Length, option);
            }
        }

        internal override GainPtr Lm((Point3, Amplitude)[] foci, NativeMethods.LMOption option)
        {
            var points = foci.Select(f => f.Item1).ToArray();
            var amps = foci.Select(f => f.Item2.Pascal()).ToArray();
            unsafe
            {
                fixed (Point3* pPoints = &points[0])
                fixed (float* pAmps = &amps[0])
                    return NativeMethodsGainHolo.AUTDGainHoloLMSphere(Ptr, pPoints, pAmps, (uint)foci.Length, option);
            }
        }
    }
}
