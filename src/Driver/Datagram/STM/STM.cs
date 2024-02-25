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

        public LoopBehavior LoopBehavior { get; protected set; } = LoopBehavior.Infinite;

        protected STM(float_t? freq, TimeSpan? period, SamplingConfiguration? config)
        {
            _freq = freq;
            _period = period;
            _samplingConfig = config;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => STMPtr(geometry);

        internal abstract DatagramPtr STMPtr(Geometry geometry);

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
            ptr = NativeMethodsBase.AUTDSTMPropsWithLoopBehavior(ptr, LoopBehavior.Internal);
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
