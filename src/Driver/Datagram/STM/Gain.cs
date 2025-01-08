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
    public sealed class GainSTM : IDatagramS<GainSTMPtr>, IDatagram, IWithSampling
    {
        private readonly STMSamplingConfig _config;

        private readonly IGain[] _gains;
        private GainSTMMode _mode = GainSTMMode.PhaseIntensityFull;

        public LoopBehavior LoopBehavior { get; private set; } = AUTD3Sharp.LoopBehavior.Infinite;

        public GainSTM(STMSamplingConfig config, IEnumerable<IGain> iter)
        {
            _gains = iter as IGain[] ?? iter.ToArray();
            _config = config;
        }

        public static GainSTM Nearest(STMSamplingConfigNearest config, IEnumerable<IGain> iter)
        {
            var gains = iter as IGain[] ?? iter.ToArray();
            return new GainSTM(config.STMSamplingConfig(gains.Length), gains);
        }

        public GainSTM WithMode(GainSTMMode mode)
        {
            _mode = mode;
            return this;
        }

        public GainSTM WithLoopBehavior(LoopBehavior loopBehavior)
        {
            LoopBehavior = loopBehavior;
            return this;
        }

        public DatagramPtr Ptr(Geometry geometry) => NativeMethodsBase.AUTDSTMGainIntoDatagram(RawPtr(geometry));

        public DatagramPtr IntoSegmentTransition(GainSTMPtr p, Segment segment, TransitionModeWrap? transitionMode) =>
                NativeMethodsBase.AUTDSTMGainIntoDatagramWithSegment(p, segment, transitionMode ?? TransitionMode.None);

        public GainSTMPtr RawPtr(Geometry geometry)
        {
            var gains = _gains.Select(g => g.GainPtr(geometry)).ToArray();
            unsafe
            {
                fixed (GainPtr* gp = &gains[0])
                {
                    return NativeMethodsBase.AUTDSTMGain(_config.SamplingConfig(_gains.Length), gp, (ushort)gains.Length, _mode, LoopBehavior).Validate();
                }
            }
        }

        public DatagramWithSegment<GainSTM, GainSTMPtr> WithSegment(Segment segment, TransitionModeWrap? transitionMode) => new(this, segment, transitionMode);

        public Freq<float> Freq => _config.Freq(_gains.Length);
#if !DYNAMIC_FREQ
        public Duration Period => _config.Period(_gains.Length);
#endif
        public SamplingConfig SamplingConfig => new(_config.SamplingConfig(_gains.Length));

        SamplingConfig IWithSampling.SamplingConfigIntensity() => SamplingConfig;
        SamplingConfig? IWithSampling.SamplingConfigPhase() => SamplingConfig;
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
