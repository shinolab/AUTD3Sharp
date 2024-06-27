using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public sealed class GainSTM : IDatagramST<GainSTMPtr>, IDatagram
    {
        private readonly STMSamplingConfig _config;

        private readonly Driver.Datagram.Gain.IGain[] _gains;
        private GainSTMMode _mode;

        public NativeMethods.LoopBehavior LoopBehavior { get; private set; }

        private GainSTM(STMSamplingConfig config, IEnumerable<Driver.Datagram.Gain.IGain> iter)
        {
            _config = config;

            _gains = iter as Driver.Datagram.Gain.IGain[] ?? iter.ToArray();
            _mode = GainSTMMode.PhaseIntensityFull;

            LoopBehavior = AUTD3Sharp.LoopBehavior.Infinite;
        }

        public static GainSTM FromFreq(Freq<float> freq, IEnumerable<Driver.Datagram.Gain.IGain> iter) => new (STMSamplingConfig.Freq(freq), iter);

        public static GainSTM FromFreqNearest(Freq<float> freq, IEnumerable<Driver.Datagram.Gain.IGain> iter) => new (STMSamplingConfig.FreqNearest(freq), iter);

        public static GainSTM FromSamplingConfig(SamplingConfig config, IEnumerable<Driver.Datagram.Gain.IGain> iter) => new (STMSamplingConfig.SamplingConfig(config), iter);

        public GainSTM WithMode(GainSTMMode mode)
        {
            _mode = mode;
            return this;
        }

        public GainSTM WithLoopBehavior(NativeMethods.LoopBehavior loopBehavior)
        {
            LoopBehavior = loopBehavior;
            return this;
        }

        public DatagramPtr Ptr(Geometry geometry) => NativeMethodsBase.AUTDSTMGainIntoDatagram(RawPtr(geometry));

        public DatagramPtr IntoSegmentTransition(GainSTMPtr p, Segment segment, TransitionModeWrap? transitionMode) =>
            transitionMode.HasValue
                ? NativeMethodsBase.AUTDSTMGainIntoDatagramWithSegmentTransition(p, segment, transitionMode.Value)
                : NativeMethodsBase.AUTDSTMGainIntoDatagramWithSegment(p, segment);

        public GainSTMPtr RawPtr(Geometry geometry)
        {
            var gains = _gains.Select(g => g.GainPtr(geometry)).ToArray();
            unsafe
            {
                fixed (GainPtr* gp = &gains[0])
                {
                    var ptr = NativeMethodsBase.AUTDSTMGain(_config.Inner, gp, (ushort)gains.Length).Validate();
                    ptr = NativeMethodsBase.AUTDSTMGainWithLoopBehavior(ptr, LoopBehavior);
                    ptr = NativeMethodsBase.AUTDSTMGainWithMode(ptr, _mode);
                    return ptr;
                }
            }
        }

        public DatagramWithSegmentTransition<GainSTM, GainSTMPtr> WithSegment(Segment segment, TransitionModeWrap? transitionMode)
          => new DatagramWithSegmentTransition<GainSTM, GainSTMPtr>(this, segment, transitionMode);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
