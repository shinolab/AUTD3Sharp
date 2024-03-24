#if UNITY_2020_2_OR_NEWER
#nullable enable
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

        internal override GainPtr Sdp(double[] foci, Amplitude[] amps, ulong size, double alpha, uint repeat, double lambda, EmissionConstraintPtr constraint)
        {
            unsafe
            {
                fixed (double* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDASDP(Ptr, pf, (double*)pa, size, alpha, lambda, repeat, constraint);
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
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDAGS(Ptr, pf, (double*)pa, size, repeat, constraint);
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
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDAGSPAT(Ptr, pf, (double*)pa, size, repeat, constraint);
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
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDANaive(Ptr, pf, (double*)pa, size, constraint);
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
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDALM(Ptr, pf, (double*)pa, size, eps1, eps2, tau, kMax, constraint, pInitial, (ulong)initial.Length);
                }
            }
        }

    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
