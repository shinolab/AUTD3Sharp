using System;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Gain.Holo
{
    public sealed class NalgebraBackend : Backend
    {
        public NalgebraBackend()
        {
            Ptr = NativeMethodsGainHolo.AUTDNalgebraBackendSphere();
        }

        [ExcludeFromCodeCoverage]
        ~NalgebraBackend()
        {
            NativeMethodsGainHolo.AUTDDeleteNalgebraBackendSphere(Ptr);
            Ptr.Item1 = IntPtr.Zero;
        }

        internal override GainPtr Gs(Vector3[] foci, Amplitude[] amps, uint size, uint repeat, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (Vector3* pf = &foci[0])
                fixed (Amplitude* pa = &amps[0])
                {
                    return NativeMethodsGainHolo.AUTDGainHoloGSSphere(Ptr, pf, (float*)pa, size, repeat, constraint);
                }
            }
        }

        internal override GainPtr Gspat(Vector3[] foci, Amplitude[] amps, uint size, uint repeat, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (Vector3* pf = &foci[0])
                fixed (Amplitude* pa = &amps[0])
                {
                    return NativeMethodsGainHolo.AUTDGainHoloGSPATSphere(Ptr, pf, (float*)pa, size, repeat, constraint);
                }
            }
        }

        internal override GainPtr Naive(Vector3[] foci, Amplitude[] amps, uint size, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (Vector3* pf = &foci[0])
                fixed (Amplitude* pa = &amps[0])
                {
                    return NativeMethodsGainHolo.AUTDGainHoloNaiveSphere(Ptr, pf, (float*)pa, size, constraint);
                }
            }
        }

        [ExcludeFromCodeCoverage]
        internal override GainPtr Lm(Vector3[] foci, Amplitude[] amps, uint size, float eps1, float eps2, float tau, uint kMax, float[] initial, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (Vector3* pf = &foci[0])
                fixed (Amplitude* pa = &amps[0])
                fixed (float* pInitial = initial)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloLMSphere(Ptr, pf, (float*)pa, size, eps1, eps2, tau, kMax, pInitial, (uint)initial.Length, constraint);
                }
            }
        }
    }
}
