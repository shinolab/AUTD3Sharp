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

        internal override GainPtr Sdp(double[] foci, Amplitude[] amps, ulong size, double alpha, uint repeat, double lambda, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (double* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloSDPSphere(Ptr, pf, (double*)pa, size, alpha, lambda, repeat, constraint);
                }
            }
        }

        internal override GainPtr Gs(double[] foci, Amplitude[] amps, ulong size, uint repeat, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (double* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloGSSphere(Ptr, pf, (double*)pa, size, repeat, constraint);
                }
            }
        }

        internal override GainPtr Gspat(double[] foci, Amplitude[] amps, ulong size, uint repeat, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (double* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloGSPATSphere(Ptr, pf, (double*)pa, size, repeat, constraint);
                }
            }
        }

        internal override GainPtr Naive(double[] foci, Amplitude[] amps, ulong size, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (double* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloNaiveSphere(Ptr, pf, (double*)pa, size, constraint);
                }
            }
        }

        internal override GainPtr Lm(double[] foci, Amplitude[] amps, ulong size, double eps1, double eps2, double tau, uint kMax, double[] initial, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (double* pf = foci)
                fixed (Amplitude* pa = amps)
                fixed (double* pInitial = initial)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloLMSphere(Ptr, pf, (double*)pa, size, eps1, eps2, tau, kMax, pInitial, (ulong)initial.Length, constraint);
                }
            }
        }
    }
}
