#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.NativeMethods;

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

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

        internal override GainPtr Sdp(float_t[] foci, Amplitude[] amps, ulong size, float_t alpha, uint repeat, float_t lambda, EmissionConstraintPtr constraint)
        {
            unsafe
            {
                fixed (float_t* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloSDP(Ptr, pf, (float_t*)pa, size, alpha, lambda, repeat, constraint);
                }
            }
        }

        internal override GainPtr Gs(float_t[] foci, Amplitude[] amps, ulong size, uint repeat, EmissionConstraintPtr constraint)
        {
            unsafe
            {
                fixed (float_t* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloGS(Ptr, pf, (float_t*)pa, size, repeat, constraint);
                }
            }
        }

        internal override GainPtr Gspat(float_t[] foci, Amplitude[] amps, ulong size, uint repeat, EmissionConstraintPtr constraint)
        {
            unsafe
            {
                fixed (float_t* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloGSPAT(Ptr, pf, (float_t*)pa, size, repeat, constraint);
                }
            }
        }

        internal override GainPtr Naive(float_t[] foci, Amplitude[] amps, ulong size, EmissionConstraintPtr constraint)
        {
            unsafe
            {
                fixed (float_t* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloNaive(Ptr, pf, (float_t*)pa, size, constraint);
                }
            }
        }

        internal override GainPtr Lm(float_t[] foci, Amplitude[] amps, ulong size, float_t eps1, float_t eps2, float_t tau, uint kMax, float_t[] initial, EmissionConstraintPtr constraint)
        {
            unsafe
            {
                fixed (float_t* pf = foci)
                fixed (Amplitude* pa = amps)
                fixed (float_t* pInitial = initial)
                {
                    return NativeMethodsGainHolo.AUTDGainHoloLM(Ptr, pf, (float_t*)pa, size, eps1, eps2, tau, kMax, pInitial, (ulong)initial.Length, constraint);
                }
            }
        }
    }
}
