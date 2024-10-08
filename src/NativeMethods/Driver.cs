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
    public static unsafe partial class NativeMethodsDriver
    {
        const string __DllName = "autd3capi_driver";

        public const uint NUM_TRANS_IN_UNIT = 249;
        public const uint NUM_TRANS_IN_X = 18;
        public const uint NUM_TRANS_IN_Y = 14;
        public const float TRANS_SPACING_MM = 10.16f;
        public const float DEVICE_HEIGHT_MM = 151.4f;
        public const float DEVICE_WIDTH_MM = 192f;
        public const int AUTD3_TRUE = 1;
        public const int AUTD3_FALSE = 0;



    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct DebugTypeWrap
    {
        public DebugTypeTag ty;
        public ulong value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct Drive
    {
        public byte phase;
        public byte intensity;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct LoopBehavior
    {
        public ushort rep;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct TransitionModeWrap
    {
        public TransitionModeTag tag;
        public ulong value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ConstPtr
    {
        public IntPtr Item1;
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
    public unsafe partial struct ControllerPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct DatagramPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultDatagram
    {
        public DatagramPtr result;
        public uint err_len;
        public ConstPtr err;
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
    public unsafe partial struct ResultModulation
    {
        public ModulationPtr result;
        public uint err_len;
        public ConstPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct RuntimePtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct HandlePtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct FociSTMPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct GainSTMPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultFociSTM
    {
        public FociSTMPtr result;
        public uint err_len;
        public ConstPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultGainSTM
    {
        public GainSTMPtr result;
        public uint err_len;
        public ConstPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct DynSincInterpolator
    {
        public DynWindow window;
        public uint window_size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultI32
    {
        public int result;
        public uint err_len;
        public ConstPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultSamplingConfig
    {
        public SamplingConfig result;
        public uint err_len;
        public ConstPtr err;
    }


    public enum SilencerTarget : byte
    {
        Intensity = 0,
        PulseWidth = 1,
    }

    public enum DebugTypeTag : byte
    {
        None = 0,
        BaseSignal = 1,
        Thermo = 2,
        ForceFan = 3,
        Sync = 4,
        ModSegment = 5,
        ModIdx = 6,
        StmSegment = 7,
        StmIdx = 8,
        IsStmMode = 9,
        PwmOut = 10,
        Direct = 11,
        SysTimeEq = 12,
    }

    public enum GPIOIn : byte
    {
        I0 = 0,
        I1 = 1,
        I2 = 2,
        I3 = 3,
    }

    public enum GPIOOut : byte
    {
        O0 = 0,
        O1 = 1,
        O2 = 2,
        O3 = 3,
    }

    public enum TransitionModeTag : byte
    {
        SyncIdx = 0,
        SysTime = 1,
        Gpio = 2,
        Ext = 3,
        Immediate = 4,
    }

    public enum DynWindow : uint
    {
        Rectangular = 0,
        Blackman = 1,
    }


}
    