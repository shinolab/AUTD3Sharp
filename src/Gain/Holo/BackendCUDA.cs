/*
 * File: CUDABackend.cs
 * Project: src
 * Created Date: 08/06/2023
 * Author: Shun Suzuki
 * -----
 * Last Modified: 11/12/2023
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2023 Shun Suzuki. All rights reserved.
 * 
 */

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

        internal override GainPtr Sdp(float_t[] foci, Amplitude[] amps, ulong size)
        {
            unsafe
            {
                fixed (float_t* pf = &foci[0])
                fixed (Amplitude* pa = &amps[0])
                {
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDASDP(Ptr, pf, (float_t*)pa, size);
                }
            }
        }

        internal override GainPtr SdpWithAlpha(GainPtr ptr, float_t v)
        {
            return NativeMethodsBackendCuda.AUTDGainHoloCUDASDPWithAlpha(ptr, v);
        }

        internal override GainPtr SdpWithRepeat(GainPtr ptr, uint v)
        {
            return NativeMethodsBackendCuda.AUTDGainHoloCUDASDPWithRepeat(ptr, v);
        }

        internal override GainPtr SdpWithLambda(GainPtr ptr, float_t v)
        {
            return NativeMethodsBackendCuda.AUTDGainHoloCUDASDPWithLambda(ptr, v);
        }

        internal override GainPtr SdpWithConstraint(GainPtr ptr, EmissionConstraintPtr v)
        {
            return NativeMethodsBackendCuda.AUTDGainHoloCUDASDPWithConstraint(ptr, v);
        }

        internal override GainPtr Gs(float_t[] foci, Amplitude[] amps, ulong size)
        {
            unsafe
            {
                fixed (float_t* pf = &foci[0])
                fixed (Amplitude* pa = &amps[0])
                {
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDAGS(Ptr, pf, (float_t*)pa, size);
                }
            }
        }

        internal override GainPtr GsWithRepeat(GainPtr ptr, uint v)
        {
            return NativeMethodsBackendCuda.AUTDGainHoloCUDAGSWithRepeat(ptr, v);
        }

        internal override GainPtr GsWithConstraint(GainPtr ptr, EmissionConstraintPtr v)
        {
            return NativeMethodsBackendCuda.AUTDGainHoloCUDAGSWithConstraint(ptr, v);
        }

        internal override GainPtr Gspat(float_t[] foci, Amplitude[] amps, ulong size)
        {
            unsafe
            {
                fixed (float_t* pf = &foci[0])
                fixed (Amplitude* pa = &amps[0])
                {
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDAGSPAT(Ptr, pf, (float_t*)pa, size);
                }
            }
        }

        internal override GainPtr GspatWithRepeat(GainPtr ptr, uint v)
        {
            return NativeMethodsBackendCuda.AUTDGainHoloCUDAGSPATWithRepeat(ptr, v);
        }

        internal override GainPtr GspatWithConstraint(GainPtr ptr, EmissionConstraintPtr v)
        {
            return NativeMethodsBackendCuda.AUTDGainHoloCUDAGSPATWithConstraint(ptr, v);
        }

        internal override GainPtr Naive(float_t[] foci, Amplitude[] amps, ulong size)
        {
            unsafe
            {
                fixed (float_t* pf = &foci[0])
                fixed (Amplitude* pa = &amps[0])
                {
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDANaive(Ptr, pf, (float_t*)pa, size);
                }
            }
        }

        internal override GainPtr NaiveWithConstraint(GainPtr ptr, EmissionConstraintPtr v)
        {
            return NativeMethodsBackendCuda.AUTDGainHoloCUDANaiveWithConstraint(ptr, v);
        }

        internal override GainPtr Lm(float_t[] foci, Amplitude[] amps, ulong size)
        {
            unsafe
            {
                fixed (float_t* pf = &foci[0])
                fixed (Amplitude* pa = &amps[0])
                {
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDALM(Ptr, pf, (float_t*)pa, size);
                }
            }
        }

        internal override GainPtr LmWithEps1(GainPtr ptr, float_t v)
        {
            return NativeMethodsBackendCuda.AUTDGainHoloCUDALMWithEps1(ptr, v);
        }

        internal override GainPtr LmWithEps2(GainPtr ptr, float_t v)
        {
            return NativeMethodsBackendCuda.AUTDGainHoloCUDALMWithEps2(ptr, v);
        }

        internal override GainPtr LmWithTau(GainPtr ptr, float_t v)
        {
            return NativeMethodsBackendCuda.AUTDGainHoloCUDALMWithTau(ptr, v);
        }

        internal override GainPtr LmWithKMax(GainPtr ptr, uint v)
        {
            return NativeMethodsBackendCuda.AUTDGainHoloCUDALMWithKMax(ptr, v);
        }

        internal override GainPtr LmWithInitial(GainPtr ptr, float_t[] v, ulong size)
        {
            unsafe
            {
                fixed (float_t* p = &v[0])
                {
                    return NativeMethodsBackendCuda.AUTDGainHoloCUDALMWithInitial(ptr, p, size);
                }
            }
        }

        internal override GainPtr LmWithConstraint(GainPtr ptr, EmissionConstraintPtr v)
        {
            return NativeMethodsBackendCuda.AUTDGainHoloCUDALMWithConstraint(ptr, v);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
