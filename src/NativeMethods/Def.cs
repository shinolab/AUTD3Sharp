// <auto-generated>
// This code is generated by csbindgen.
// DON'T CHANGE THIS DIRECTLY.
// </auto-generated>
#pragma warning disable CS8500
#pragma warning disable CS8981
using System;
using System.Runtime.InteropServices;


namespace AUTD3Sharp.NativeMethods
{
    public static unsafe partial class NativeMethodsDef
    {
        const string __DllName = "autd3capi_def";

        public const double DEFAULT_CORRECTED_ALPHA = 0.803;
        public const uint NUM_TRANS_IN_UNIT = 249;
        public const uint NUM_TRANS_IN_X = 18;
        public const uint NUM_TRANS_IN_Y = 14;
        public const double TRANS_SPACING_MM = 10.16;
        public const double DEVICE_HEIGHT_MM = 151.4;
        public const double DEVICE_WIDTH_MM = 192;
        public const uint FPGA_CLK_FREQ = 20480000;
        public const double ULTRASOUND_FREQUENCY = 40000;
        public const int AUTD3_TRUE = 1;
        public const int AUTD3_FALSE = 0;


        [DllImport(__DllName, EntryPoint = "AUTDEmitIntensityWithCorrectionAlpha", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte AUTDEmitIntensityWithCorrectionAlpha(byte value, double alpha);

        [DllImport(__DllName, EntryPoint = "AUTDPhaseFromRad", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte AUTDPhaseFromRad(double value);

        [DllImport(__DllName, EntryPoint = "AUTDPhaseToRad", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double AUTDPhaseToRad(byte value);

        [DllImport(__DllName, EntryPoint = "AUTDLoopBehaviorInfinite", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LoopBehaviorRaw AUTDLoopBehaviorInfinite();

        [DllImport(__DllName, EntryPoint = "AUTDLoopBehaviorFinite", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LoopBehaviorRaw AUTDLoopBehaviorFinite(uint v);

        [DllImport(__DllName, EntryPoint = "AUTDLoopBehaviorOnce", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LoopBehaviorRaw AUTDLoopBehaviorOnce();

        [DllImport(__DllName, EntryPoint = "AUTDGetErr", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDGetErr(IntPtr src, byte* dst);

        [DllImport(__DllName, EntryPoint = "AUTDSamplingConfigFromFrequencyDivision", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultSamplingConfig AUTDSamplingConfigFromFrequencyDivision(uint div);

        [DllImport(__DllName, EntryPoint = "AUTDSamplingConfigFromFrequency", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultSamplingConfig AUTDSamplingConfigFromFrequency(double f);

        [DllImport(__DllName, EntryPoint = "AUTDSamplingConfigFromPeriod", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultSamplingConfig AUTDSamplingConfigFromPeriod(ulong p);

        [DllImport(__DllName, EntryPoint = "AUTDSamplingConfigFrequencyDivision", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint AUTDSamplingConfigFrequencyDivision(SamplingConfigurationRaw config);

        [DllImport(__DllName, EntryPoint = "AUTDSamplingConfigFrequency", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double AUTDSamplingConfigFrequency(SamplingConfigurationRaw config);

        [DllImport(__DllName, EntryPoint = "AUTDSamplingConfigPeriod", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ulong AUTDSamplingConfigPeriod(SamplingConfigurationRaw config);


    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct DriveRaw
    {
        public byte phase;
        public byte intensity;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct LoopBehaviorRaw
    {
        public uint rep;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultI32
    {
        public int result;
        public uint err_len;
        public IntPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultModulation
    {
        public ModulationPtr result;
        public uint err_len;
        public IntPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultDatagram
    {
        public DatagramPtr result;
        public uint err_len;
        public IntPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultFocusSTM
    {
        public FocusSTMPtr result;
        public uint err_len;
        public IntPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultGainSTM
    {
        public GainSTMPtr result;
        public uint err_len;
        public IntPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct SamplingConfigurationRaw
    {
        public uint div;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultSamplingConfig
    {
        public SamplingConfigurationRaw result;
        public uint err_len;
        public IntPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ControllerPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultController
    {
        public ControllerPtr result;
        public uint err_len;
        public IntPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct DebugSettings
    {
        public fixed byte ty[4];
        public fixed ushort value[4];
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct LinkBuilderPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct LinkPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct DatagramPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct GainPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct GeometryPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct DevicePtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct TransducerPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ModulationPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct FocusSTMPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct GainSTMPtr
    {
        public IntPtr Item1;
    }



}
    