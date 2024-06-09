using System;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.NativeMethods;

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

        internal override GainPtr Sdp(float[] foci, Amplitude[] amps, uint size, float alpha, uint repeat, float lambda, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (float* pf = &foci[0])
                fixed (Amplitude* pa = &amps[0])
                {
                    return NativeMethodsGainHolo.AUTDGainHoloSDPSphere(Ptr, pf, (float*)pa, size, alpha, lambda, repeat, constraint);
                }
            }
        }

        internal override GainPtr Gs(float[] foci, Amplitude[] amps, uint size, uint repeat, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (float* pf = &foci[0])
                fixed (Amplitude* pa = &amps[0])
                {
                    return NativeMethodsGainHolo.AUTDGainHoloGSSphere(Ptr, pf, (float*)pa, size, repeat, constraint);
                }
            }
        }

        internal override GainPtr Gspat(float[] foci, Amplitude[] amps, uint size, uint repeat, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (float* pf = &foci[0])
                fixed (Amplitude* pa = &amps[0])
                {
                    return NativeMethodsGainHolo.AUTDGainHoloGSPATSphere(Ptr, pf, (float*)pa, size, repeat, constraint);
                }
            }
        }

        internal override GainPtr Naive(float[] foci, Amplitude[] amps, uint size, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (float* pf = &foci[0])
                fixed (Amplitude* pa = &amps[0])
                {
                    return NativeMethodsGainHolo.AUTDGainHoloNaiveSphere(Ptr, pf, (float*)pa, size, constraint);
                }
            }
        }

        internal override GainPtr Lm(float[] foci, Amplitude[] amps, uint size, float eps1, float eps2, float tau, uint kMax, float[] initial, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                if (initial.Length == 0)
                {
                    fixed (float* pf = &foci[0])
                    fixed (Amplitude* pa = &amps[0])
                    {
                        return NativeMethodsGainHolo.AUTDGainHoloLMSphere(Ptr, pf, (float*)pa, size, eps1, eps2, tau, kMax, (float*)IntPtr.Zero, 0u, constraint);
                    }
                }
                else
                {
                    fixed (float* pf = &foci[0])
                    fixed (Amplitude* pa = &amps[0])
                    fixed (float* pInitial = &initial[0])
                    {
                        return NativeMethodsGainHolo.AUTDGainHoloLMSphere(Ptr, pf, (float*)pa, size, eps1, eps2, tau, kMax, pInitial, (uint)initial.Length, constraint);
                    }
                }
            }
        }
    }
}
