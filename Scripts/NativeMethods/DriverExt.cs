using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("tests")]
namespace AUTD3Sharp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GainSTMOption
    {
        public GainSTMMode Mode;
    }

    public enum GainSTMMode : byte
    {
        PhaseIntensityFull = 0,
        PhaseFull = 1,
        PhaseHalf = 2
    }

    public enum Segment
    {
        S0,
        S1
    }

    public enum GPIOIn
    {
        I0,
        I1,
        I2,
        I3
    }

    public enum GPIOOut
    {
        O0,
        O1,
        O2,
        O3
    }

    public enum SpinStrategyTag
    {
        YieldThread,
        SpinLoopHint
    }

    public enum ParallelMode
    {
        Auto,
        On,
        Off
    }

    namespace NativeMethods
    {
        [ExcludeFromCodeCoverage]
        public static class EnumExtensions
        {
            internal static Segment ToNative(this AUTD3Sharp.Segment seg) => seg switch
            {
                AUTD3Sharp.Segment.S0 => Segment.S0,
                AUTD3Sharp.Segment.S1 => Segment.S1,
                _ => throw new ArgumentOutOfRangeException(nameof(seg), seg, null)
            };

            internal static AUTD3Sharp.Segment ToManaged(this Segment seg) => seg switch
            {
                Segment.S0 => AUTD3Sharp.Segment.S0,
                Segment.S1 => AUTD3Sharp.Segment.S1,
                _ => throw new ArgumentOutOfRangeException(nameof(seg), seg, null)
            };

            internal static GPIOIn ToNative(this AUTD3Sharp.GPIOIn gpio) => gpio switch
            {
                AUTD3Sharp.GPIOIn.I0 => GPIOIn.I0,
                AUTD3Sharp.GPIOIn.I1 => GPIOIn.I1,
                AUTD3Sharp.GPIOIn.I2 => GPIOIn.I2,
                AUTD3Sharp.GPIOIn.I3 => GPIOIn.I3,
                _ => throw new ArgumentOutOfRangeException(nameof(gpio), gpio, null)
            };

            internal static AUTD3Sharp.GPIOOut ToManaged(this GPIOOut gpio) => gpio switch
            {
                GPIOOut.O0 => AUTD3Sharp.GPIOOut.O0,
                GPIOOut.O1 => AUTD3Sharp.GPIOOut.O1,
                GPIOOut.O2 => AUTD3Sharp.GPIOOut.O2,
                GPIOOut.O3 => AUTD3Sharp.GPIOOut.O3,
                _ => throw new ArgumentOutOfRangeException(nameof(gpio), gpio, null)
            };

            internal static SpinStrategyTag ToNative(this AUTD3Sharp.SpinStrategyTag tag) => tag switch
            {
                AUTD3Sharp.SpinStrategyTag.YieldThread => SpinStrategyTag.YieldThread,
                AUTD3Sharp.SpinStrategyTag.SpinLoopHint => SpinStrategyTag.SpinLoopHint,
                _ => throw new ArgumentOutOfRangeException(nameof(tag), tag, null)
            };

            internal static ParallelMode ToNative(this AUTD3Sharp.ParallelMode mode) => mode switch
            {
                AUTD3Sharp.ParallelMode.Auto => ParallelMode.Auto,
                AUTD3Sharp.ParallelMode.On => ParallelMode.On,
                AUTD3Sharp.ParallelMode.Off => ParallelMode.Off,
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };
        }

        [ExcludeFromCodeCoverage]
        public static class ResultExtensions
        {
            public static AUTDStatus Validate(this ResultStatus res)
            {
                if (res.result != AUTDStatus.AUTDErr) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            internal static SamplingConfigWrap Validate(this ResultSamplingConfig res)
            {
                if (res.err_len == 0) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            internal static float Validate(this ResultF32 res)
            {
                if (res.err_len == 0) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            internal static ushort Validate(this ResultU16 res)
            {
                if (res.err_len == 0) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            internal static Duration Validate(this ResultDuration res)
            {
                if (res.err_len == 0) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            internal static ControllerPtr Validate(this ResultController res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            internal static GainPtr Validate(this ResultGain res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static LinkPtr Validate(this ResultLink res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            internal static ModulationPtr Validate(this ResultModulation res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            internal static FirmwareVersionListPtr Validate(this ResultFirmwareVersionList res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            internal static FPGAStateListPtr Validate(this ResultFPGAStateList res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct FixedCompletionSteps
        {
            internal ushort intensity;
            internal ushort phase;
            [MarshalAs(UnmanagedType.U1)]
            internal bool strict_mode;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct FixedUpdateRate
        {
            internal ushort intensity;
            internal ushort phase;
        }

        public static class Ffi
        {
            public static byte[] ToNullTerminatedUtf8(string str)
            {
                var len = System.Text.Encoding.UTF8.GetByteCount(str);
                var bytes = new byte[len + 1];
                System.Text.Encoding.UTF8.GetBytes(str, 0, str.Length, bytes, bytes.GetLowerBound(0));
                return bytes;
            }
        }
    }
}
