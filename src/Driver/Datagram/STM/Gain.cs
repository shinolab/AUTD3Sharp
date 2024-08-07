using System.Collections.Generic;
using System;
using System.Linq;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Driver.Datagram.Gain;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public sealed class GainSTM : IDatagramST<GainSTMPtr>, IDatagram
    {
        private readonly STMSamplingConfig _config;

        private readonly IGain[] _gains;
        private GainSTMMode _mode = GainSTMMode.PhaseIntensityFull;

        public NativeMethods.LoopBehavior LoopBehavior { get; private set; } = AUTD3Sharp.LoopBehavior.Infinite;

        private GainSTM(STMSamplingConfig config, IGain[] gains)
        {
            _config = config;
            _gains = gains;
        }

        public GainSTM(Freq<float> freq, IEnumerable<IGain> iter)
        {
            _gains = iter as IGain[] ?? iter.ToArray();
            _config = new STMSamplingConfig(freq, _gains.Length);
        }

        public GainSTM(TimeSpan period, IEnumerable<IGain> iter)
        {
            _gains = iter as IGain[] ?? iter.ToArray();
            _config = new STMSamplingConfig(period, _gains.Length);
        }

        public GainSTM(SamplingConfig config, IEnumerable<IGain> iter)
        {
            _gains = iter as IGain[] ?? iter.ToArray();
            _config = new STMSamplingConfig(config, _gains.Length);
        }

        public static GainSTM Nearest(Freq<float> freq, IEnumerable<IGain> iter)
        {
            var gains = iter as IGain[] ?? iter.ToArray();
            var config = STMSamplingConfig.Nearest(freq, gains.Length);
            return new GainSTM(config, gains);
        }
        public static GainSTM Nearest(TimeSpan period, IEnumerable<IGain> iter)
        {
            var gains = iter as IGain[] ?? iter.ToArray();
            var config = STMSamplingConfig.Nearest(period, gains.Length);
            return new GainSTM(config, gains);
        }

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

        public DatagramWithSegmentTransition<GainSTM, GainSTMPtr> WithSegment(Segment segment, TransitionModeWrap? transitionMode) => new(this, segment, transitionMode);

        public Freq<float> Freq => _config.Freq;
        public TimeSpan Period => _config.Period;
        public SamplingConfig SamplingConfig => _config.SamplingConfig;
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
