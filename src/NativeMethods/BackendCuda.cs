// <auto-generated>
// This code is generated by csbindgen.
// DON'T CHANGE THIS DIRECTLY.
// </auto-generated>
#pragma warning disable CS8500
#pragma warning disable CS8981
using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.Utils;


namespace AUTD3Sharp.NativeMethods
{
    public static unsafe partial class NativeMethodsBackendCuda
    {
        const string __DllName = "autd3capi_backend_cuda";



        [DllImport(__DllName, EntryPoint = "AUTDCUDABackend", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultBackend AUTDCUDABackend();

        [DllImport(__DllName, EntryPoint = "AUTDCUDABackendDelete", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDCUDABackendDelete(BackendPtr backend);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloCUDAGS", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainHoloCUDAGS(BackendPtr backend, Vector3* points, float* amps, uint size, uint repeat, EmissionConstraintWrap constraint);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloCUDAGSPAT", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainHoloCUDAGSPAT(BackendPtr backend, Vector3* points, float* amps, uint size, uint repeat, EmissionConstraintWrap constraint);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloCUDANaive", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainHoloCUDANaive(BackendPtr backend, Vector3* points, float* amps, uint size, EmissionConstraintWrap constraint);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloCUDALM", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainHoloCUDALM(BackendPtr backend, Vector3* points, float* amps, uint size, float eps_1, float eps_2, float tau, uint k_max, EmissionConstraintWrap constraint, float* initial_ptr, ulong initial_len);


    }



}
    