#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain.Holo
{
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

        internal override GainPtr Sdp(float[] foci, Amplitude[] amps, uint size, float alpha, uint repeat, float lambda, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (float* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDASDP(Ptr, pf, (float*)pa, size, alpha, lambda, repeat, constraint);
                }
            }
        }

        internal override GainPtr Gs(float[] foci, Amplitude[] amps, uint size, uint repeat, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (float* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDAGS(Ptr, pf, (float*)pa, size, repeat, constraint);
                }
            }
        }

        internal override GainPtr Gspat(float[] foci, Amplitude[] amps, uint size, uint repeat, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (float* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDAGSPAT(Ptr, pf, (float*)pa, size, repeat, constraint);
                }
            }
        }

        internal override GainPtr Naive(float[] foci, Amplitude[] amps, uint size, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (float* pf = foci)
                fixed (Amplitude* pa = amps)
                {
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDANaive(Ptr, pf, (float*)pa, size, constraint);
                }
            }
        }

        internal override GainPtr Lm(float[] foci, Amplitude[] amps, uint size, float eps1, float eps2, float tau, uint kMax, float[] initial, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (float* pf = foci)
                fixed (Amplitude* pa = amps)
                fixed (float* pInitial = initial)
                {
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDALM(Ptr, pf, (float*)pa, size, eps1, eps2, tau, kMax, constraint, pInitial, (ulong)initial.Length);
                }
            }
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
