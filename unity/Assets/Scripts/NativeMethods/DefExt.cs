
#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

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

    public enum SamplingMode : byte
    {
        ExactFrequency = 0,
        SizeOptimized = 1
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

    namespace NativeMethods
    {
        public static class SyncModeExt
        {
            public static SyncMode Into(this AUTD3Sharp.SyncMode mode)
            {
                return mode switch
                {
                    AUTD3Sharp.SyncMode.FreeRun => SyncMode.FreeRun,
                    AUTD3Sharp.SyncMode.DC => SyncMode.DC,
                    _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
                };
            }
        }

        public static partial class NativeMethodsDef
        {
            public const int AUTD3_ERR = -1;
        }

        public static class ResultExtensions
        {
            public static int Validate(this ResultI32 res)
            {
                if (res.result != NativeMethodsDef.AUTD3_ERR) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static ControllerPtr Validate(this ResultController res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static LinkSimulatorBuilderPtr Validate(this ResultLinkSimulatorBuilder res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static LinkRemoteSOEMBuilderPtr Validate(this ResultLinkRemoteSOEMBuilder res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static LinkRemoteTwinCATBuilderPtr Validate(this ResultLinkRemoteTwinCATBuilder res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static ModulationPtr Validate(this ResultModulation res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static GainCalcDrivesMapPtr Validate(this ResultGainCalcDrivesMap res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static FirmwareInfoListPtr Validate(this ResultFirmwareInfoList res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static GroupKVMapPtr Validate(this ResultGroupKVMap res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static DatagramPtr Validate(this ResultDatagram res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static SamplingConfigurationRaw Validate(this ResultSamplingConfig res)
            {
                if (res.result.div != 0) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static BackendPtr Validate(this ResultBackend res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }
            public static PlotConfigPtr Validate(this ResultPlotConfig res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }
            public static PyPlotConfigPtr Validate(this ResultPyPlotConfig res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static ModulationCalcPtr Validate(this ResultModulationCalc res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static FocusSTMPtr Validate(this ResultFocusSTM res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static GainSTMPtr Validate(this ResultGainSTM res)
            {
                if (res.result.Item1 != IntPtr.Zero) return res.result;
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsDef.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }
        }
    }
}
