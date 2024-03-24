using System;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain.Holo
{
    /// <summary>
    /// Backend using <see href="https://nalgebra.org/">Nalgebra</see>
    /// </summary>
    public sealed class NalgebraBackend : Backend
    {
        public NalgebraBackend()
        {
            Ptr = NativeMethodsGainHolo.AUTDNalgebraBackend();
        }

        [ExcludeFromCodeCoverage]
        ~NalgebraBackend()
        {
            NativeMethodsGainHolo.AUTDDeleteNalgebraBackend(Ptr);
            Ptr.Item1 = IntPtr.Zero;
        }

        internal override GainPtr Sdp(double[] foci, Amplitude[] amps, ulong size, double alpha, uint repeat, double lambda, EmissionConstraintPtr constraint)
        {
            unsafe
            {
                fixed (double* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloSDP(Ptr, pf, (double*)pa, size, alpha, lambda, repeat, constraint);
                }
            }
        }

        internal override GainPtr Gs(double[] foci, Amplitude[] amps, ulong size, uint repeat, EmissionConstraintPtr constraint)
        {
            unsafe
            {
                fixed (double* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloGS(Ptr, pf, (double*)pa, size, repeat, constraint);
                }
            }
        }

        internal override GainPtr Gspat(double[] foci, Amplitude[] amps, ulong size, uint repeat, EmissionConstraintPtr constraint)
        {
            unsafe
            {
                fixed (double* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloGSPAT(Ptr, pf, (double*)pa, size, repeat, constraint);
                }
            }
        }

        internal override GainPtr Naive(double[] foci, Amplitude[] amps, ulong size, EmissionConstraintPtr constraint)
        {
            unsafe
            {
                fixed (double* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloNaive(Ptr, pf, (double*)pa, size, constraint);
                }
            }
        }

        internal override GainPtr Lm(double[] foci, Amplitude[] amps, ulong size, double eps1, double eps2, double tau, uint kMax, double[] initial, EmissionConstraintPtr constraint)
        {
            unsafe
            {
                fixed (double* pf = foci)
                fixed (Amplitude* pa = amps)
                fixed (double* pInitial = initial)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloLM(Ptr, pf, (double*)pa, size, eps1, eps2, tau, kMax, pInitial, (ulong)initial.Length, constraint);
                }
            }
        }
    }
}
