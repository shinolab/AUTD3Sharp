#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

namespace AUTD3Sharp.Driver.Datagram.STM
{
    public abstract class STM : IDatagram
    {
        private readonly float_t? _freq;
        private readonly TimeSpan? _period;
        private readonly SamplingConfiguration? _samplingConfig;
        protected int StartIdxV;
        protected int FinishIdxV;

        protected STM(float_t? freq, TimeSpan? period, SamplingConfiguration? config)
        {
            _freq = freq;
            _period = period;
            _samplingConfig = config;
            StartIdxV = -1;
            FinishIdxV = -1;
        }

        DatagramPtr IDatagram.Ptr(Geometry.Geometry geometry) => STMPtr(geometry);

        internal abstract DatagramPtr STMPtr(Geometry.Geometry geometry);

        public ushort? StartIdx => StartIdxV == -1 ? null : (ushort?)StartIdxV;

        public ushort? FinishIdx => FinishIdxV == -1 ? null : (ushort?)FinishIdxV;

        internal STMPropsPtr Props()
        {
            var ptr = new STMPropsPtr();
            if (_freq != null)
                ptr = NativeMethodsBase.AUTDSTMPropsFromFreq(_freq.Value);
            if (_period != null)
                ptr = NativeMethodsBase.AUTDSTMPropsFromPeriod((ulong)(_period.Value.TotalSeconds * 1000 * 1000 *
                                                                          1000));
            if (_samplingConfig != null)
                ptr = NativeMethodsBase.AUTDSTMPropsFromSamplingConfig(_samplingConfig.Value.Internal);
            ptr = NativeMethodsBase.AUTDSTMPropsWithStartIdx(ptr, StartIdxV);
            ptr = NativeMethodsBase.AUTDSTMPropsWithFinishIdx(ptr, FinishIdxV);
            return ptr;
        }

        protected float_t FreqFromSize(int size) => NativeMethodsBase.AUTDSTMPropsFrequency(Props(), (ulong)size);

        protected TimeSpan PeriodFromSize(int size) =>
            TimeSpan.FromSeconds(NativeMethodsBase.AUTDSTMPropsPeriod(Props(), (ulong)size) / 1000.0 / 1000.0 /
                                 1000.0);

        protected SamplingConfiguration SamplingConfigFromSize(int size) => new SamplingConfiguration(
            NativeMethodsBase.AUTDSTMPropsSamplingConfig(Props(), (ulong)size).Validate());
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
