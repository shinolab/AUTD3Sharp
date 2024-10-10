using System;

namespace AUTD3Sharp
{
    public enum GainSTMMode : byte
    {
        PhaseIntensityFull = 0,
        PhaseFull = 1,
        PhaseHalf = 2
    }

    public enum TimerStrategy : byte
    {
        Sleep = 0,
        BusyWait = 1,
        NativeTimer = 2
    }

    public enum SyncMode : byte
    {
        FreeRun = 0,
        DC = 1
    }

    public enum Segment : byte
    {
        S0 = 0,
        S1 = 1
    }

    public enum Status : byte
    {
        Error = 0,
        StateChanged = 1,
        Lost = 2
    }

    public enum GPIOIn : byte
    {
        I0 = 0,
        I1 = 1,
        I2 = 2,
        I3 = 3
    }

    public enum GPIOOut : byte
    {
        O0 = 0,
        O1 = 1,
        O2 = 2,
        O3 = 3
    }

    public enum SilencerTarget : byte
    {
        Intensity = 0,
        PulseWidth = 1
    }

    namespace NativeMethods
    {
#pragma warning disable CS0169, IDE0051
        public struct FfiFuture
        {
            private unsafe fixed byte _data[24];
        }

        public struct LocalFfiFuture
        {
            private unsafe fixed byte _data[24];
        }

        public readonly struct SamplingConfig
        {
            private readonly ushort _div;
        }
#pragma warning restore CS0169, IDE0051

        public static class StatusExt
        {
            public static AUTD3Sharp.Status Into(this Status mode)
            {
                return mode switch
                {
                    Status.Error => AUTD3Sharp.Status.Error,
                    Status.StateChanged => AUTD3Sharp.Status.StateChanged,
                    Status.Lost => AUTD3Sharp.Status.Lost,
                    _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
                };
            }
        }

        public static class GPIOInExt
        {
            public static GPIOIn Into(this AUTD3Sharp.GPIOIn mode)
            {
                return mode switch
                {
                    AUTD3Sharp.GPIOIn.I0 => GPIOIn.I0,
                    AUTD3Sharp.GPIOIn.I1 => GPIOIn.I1,
                    AUTD3Sharp.GPIOIn.I2 => GPIOIn.I2,
                    AUTD3Sharp.GPIOIn.I3 => GPIOIn.I3,
                    _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
                };
            }
        }

        public static class TimerStrategyExt
        {
            public static TimerStrategy Into(this AUTD3Sharp.TimerStrategy strategy)
            {
                return strategy switch
                {
                    AUTD3Sharp.TimerStrategy.Sleep => TimerStrategy.Sleep,
                    AUTD3Sharp.TimerStrategy.BusyWait => TimerStrategy.BusyWait,
                    _ => throw new ArgumentOutOfRangeException(nameof(strategy), strategy, null)
                };
            }
        }

        public static class SilencerTargetExt
        {
            public static SilencerTarget Into(this AUTD3Sharp.SilencerTarget target)
            {
                return target switch
                {
                    AUTD3Sharp.SilencerTarget.Intensity => SilencerTarget.Intensity,
                    AUTD3Sharp.SilencerTarget.PulseWidth => SilencerTarget.PulseWidth,
                    _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
                };
            }
        }

        public static partial class NativeMethodsBase
        {
            public const int AUTD3_ERR = -1;
        }

        public static class ResultExtensions
        {
            public static int Validate(this ResultI32 res)
            {
                if (res.result != NativeMethodsBase.AUTD3_ERR) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static SamplingConfig Validate(this ResultSamplingConfig res)
            {
                if (res.err_len == 0) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static ControllerPtr Validate(this ResultController res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static LinkSimulatorBuilderPtr Validate(this ResultLinkSimulatorBuilder res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static LinkRemoteSOEMBuilderPtr Validate(this ResultLinkRemoteSOEMBuilder res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static LinkRemoteTwinCATBuilderPtr Validate(this ResultLinkRemoteTwinCATBuilder res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static ModulationPtr Validate(this ResultModulation res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static GainCalcPtr Validate(this ResultGainCalcDrivesMap res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static FirmwareVersionListPtr Validate(this ResultFirmwareVersionList res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static FPGAStateListPtr Validate(this ResultFPGAStateList res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static DatagramPtr Validate(this ResultDatagram res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static BackendPtr Validate(this ResultBackend res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static ModulationCalcPtr Validate(this ResultModulationCalc res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static GainSTMPtr Validate(this ResultGainSTM res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static FociSTMPtr Validate(this ResultFociSTM res)
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
    }
}
