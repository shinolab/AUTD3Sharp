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
    public static unsafe partial class NativeMethodsGainHolo
    {
        const string __DllName = "autd3capi_gain_holo";



        [DllImport(__DllName, EntryPoint = "AUTDGainHoloConstraintNormalize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern EmissionConstraintWrap AUTDGainHoloConstraintNormalize();

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloConstraintUniform", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern EmissionConstraintWrap AUTDGainHoloConstraintUniform(byte intensity);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloConstraintMultiply", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern EmissionConstraintWrap AUTDGainHoloConstraintMultiply(float v);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloConstraintClamp", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern EmissionConstraintWrap AUTDGainHoloConstraintClamp(byte min_v, byte max_v);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloGreedySphere", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainHoloGreedySphere(Vector3* points, float* amps, uint size, byte div, EmissionConstraintWrap constraint);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloGreedyT4010A1", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainHoloGreedyT4010A1(Vector3* points, float* amps, uint size, byte div, EmissionConstraintWrap constraint);

        [DllImport(__DllName, EntryPoint = "AUTDGainGreedyIsDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDGainGreedyIsDefault(GainPtr greedy);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloGSSphere", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainHoloGSSphere(BackendPtr backend, Vector3* points, float* amps, uint size, uint repeat, EmissionConstraintWrap constraint);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloGST4010A1", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainHoloGST4010A1(BackendPtr backend, Vector3* points, float* amps, uint size, uint repeat, EmissionConstraintWrap constraint);

        [DllImport(__DllName, EntryPoint = "AUTDGainGSIsDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDGainGSIsDefault(GainPtr gs);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloGSPATSphere", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainHoloGSPATSphere(BackendPtr backend, Vector3* points, float* amps, uint size, uint repeat, EmissionConstraintWrap constraint);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloGSPATT4010A1", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainHoloGSPATT4010A1(BackendPtr backend, Vector3* points, float* amps, uint size, uint repeat, EmissionConstraintWrap constraint);

        [DllImport(__DllName, EntryPoint = "AUTDGainGSPATIsDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDGainGSPATIsDefault(GainPtr gs);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloSPLToPascal", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float AUTDGainHoloSPLToPascal(float value);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloPascalToSPL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern float AUTDGainHoloPascalToSPL(float value);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloLMSphere", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainHoloLMSphere(BackendPtr backend, Vector3* points, float* amps, uint size, float eps_1, float eps_2, float tau, uint k_max, float* initial_ptr, uint initial_len, EmissionConstraintWrap constraint);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloLMT4010A1", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainHoloLMT4010A1(BackendPtr backend, Vector3* points, float* amps, uint size, float eps_1, float eps_2, float tau, uint k_max, float* initial_ptr, uint initial_len, EmissionConstraintWrap constraint);

        [DllImport(__DllName, EntryPoint = "AUTDGainLMIsDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDGainLMIsDefault(GainPtr gs);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloNaiveSphere", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainHoloNaiveSphere(BackendPtr backend, Vector3* points, float* amps, uint size, EmissionConstraintWrap constraint);

        [DllImport(__DllName, EntryPoint = "AUTDGainHoloNaiveT4010A1", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainHoloNaiveT4010A1(BackendPtr backend, Vector3* points, float* amps, uint size, EmissionConstraintWrap constraint);

        [DllImport(__DllName, EntryPoint = "AUTDGainNaiveIsDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDGainNaiveIsDefault(GainPtr gs);

        [DllImport(__DllName, EntryPoint = "AUTDNalgebraBackendSphere", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern BackendPtr AUTDNalgebraBackendSphere();

        [DllImport(__DllName, EntryPoint = "AUTDNalgebraBackendT4010A1", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern BackendPtr AUTDNalgebraBackendT4010A1();

        [DllImport(__DllName, EntryPoint = "AUTDDeleteNalgebraBackendSphere", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDDeleteNalgebraBackendSphere(BackendPtr backend);

        [DllImport(__DllName, EntryPoint = "AUTDDeleteNalgebraBackendT4010A1", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDDeleteNalgebraBackendT4010A1(BackendPtr backend);


    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe partial struct EmissionConstraintValue
    {
        [FieldOffset(0)]
        public byte @null;
        [FieldOffset(0)]
        public byte uniform;
        [FieldOffset(0)]
        public float multiply;
        [FieldOffset(0)]
        public fixed byte clamp[2];
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct EmissionConstraintWrap
    {
        public EmissionConstraintTag tag;
        public EmissionConstraintValue value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct BackendPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultBackend
    {
        public BackendPtr result;
        public uint err_len;
        public ConstPtr err;
    }


    public enum EmissionConstraintTag : byte
    {
        Normalize = 1,
        Uniform = 2,
        Multiply = 3,
        Clamp = 4,
    }


}
    