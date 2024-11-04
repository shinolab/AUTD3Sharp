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
        SpinSleep = 0,
        StdSleep = 1,
        SpinWait = 2
    }

    public enum SyncMode : byte
    {
        DC = 0,
        FreeRun = 1
    }

    namespace Link
    {
        public enum ProcessPriority : byte
        {
            Idle = 0,
            BelowNormal = 1,
            Normal = 2,
            AboveNormal = 3,
            High = 4,
            Realtime = 5,
        }
    }

    public enum Segment : byte
    {
        S0 = 0,
        S1 = 1
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

#pragma warning restore CS0169, IDE0051

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

            public static LinkBuilderPtr Validate(this ResultSyncLinkBuilder res)
            {
                if (res.result.Item1 != IntPtr.Zero) return new LinkBuilderPtr { Item1 = res.result.Item1 };
                var err = new byte[res.err_len];
                unsafe
                {
                    fixed (byte* p = &err[0]) NativeMethodsBase.AUTDGetErr(res.err, p);
                }
                throw new AUTDException(err);
            }

            public static LinkBuilderPtr Validate(this ResultLinkBuilder res)
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

        public static class Ffi
        {
            public static byte[] toNullTerminatedUtf8(string str)
            {
                var len = System.Text.Encoding.UTF8.GetByteCount(str);
                var bytes = new byte[len + 1];
                System.Text.Encoding.UTF8.GetBytes(str, 0, str.Length, bytes, bytes.GetLowerBound(0));
                return bytes;
            }
        }
    }
}
