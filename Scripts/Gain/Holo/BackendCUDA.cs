#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

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

        internal override GainPtr Gs(Vector3[] foci, Amplitude[] amps, uint size, uint repeat, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                fixed (Vector3* pf = &foci[0])
                fixed (Amplitude* pa = &amps[0])
                {
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDAGS(Ptr, pf, (float*)pa, size, repeat, constraint);
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
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDAGSPAT(Ptr, pf, (float*)pa, size, repeat, constraint);
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
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDANaive(Ptr, pf, (float*)pa, size, constraint);
                }
            }
        }

        internal override GainPtr Lm(Vector3[] foci, Amplitude[] amps, uint size, float eps1, float eps2, float tau, uint kMax, float[] initial, EmissionConstraintWrap constraint)
        {
            unsafe
            {
                if (initial.Length == 0)
                {
                    fixed (Vector3* pf = &foci[0])
                    fixed (Amplitude* pa = &amps[0])
                    {
                        return NativeMethodsBackendCuda.AUTDGainHoloCUDALM(Ptr, pf, (float*)pa, size, eps1, eps2, tau, kMax, constraint, (float*)IntPtr.Zero, 0u);
                    }
                }
                else
                {
                    fixed (Vector3* pf = &foci[0])
                    fixed (Amplitude* pa = &amps[0])
                    fixed (float* pInitial = &initial[0])
                    {
                        return NativeMethodsBackendCuda.AUTDGainHoloCUDALM(Ptr, pf, (float*)pa, size, eps1, eps2, tau, kMax, constraint, pInitial, (ulong)initial.Length);
                    }
                }
            }
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
