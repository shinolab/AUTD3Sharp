#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain.Holo
{
    /// <summary>
    /// Backend using CUDA
    /// </summary>
    [ComVisible(false)]
    public class CUDABackend : Backend
    {
        public CUDABackend()
        {
            Ptr = NativeMethodsBackendCuda.AUTDCUDABackend().Validate();
        }

        ~CUDABackend()
        {
            if (Ptr.Item1 == IntPtr.Zero) return;
            NativeMethodsBackendCuda.AUTDCUDABackendDelete(Ptr);
            Ptr.Item1 = IntPtr.Zero;
        }

        internal override GainPtr Sdp(float_t[] foci, Amplitude[] amps, ulong size, float_t alpha, uint repeat, float_t lambda, EmissionConstraintPtr constraint)
        {
            unsafe
            {
                fixed (float_t* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDASDP(Ptr, pf, (float_t*)pa, size, alpha, lambda, repeat, constraint);
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
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDAGS(Ptr, pf, (float_t*)pa, size, repeat, constraint);
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
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDAGSPAT(Ptr, pf, (float_t*)pa, size, repeat, constraint);
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
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDANaive(Ptr, pf, (float_t*)pa, size, constraint);
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
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDALM(Ptr, pf, (float_t*)pa, size, eps1, eps2, tau, kMax, constraint, pInitial, (ulong)initial.Length);
                }
            }
        }

    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
