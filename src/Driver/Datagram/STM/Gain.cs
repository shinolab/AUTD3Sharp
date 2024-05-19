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
        private readonly Freq<double>? _freq;
        private readonly Freq<double>? _freqNearest;
        private readonly SamplingConfigWrap? _config;

        private readonly List<Driver.Datagram.Gain.IGain> _gains;
        private GainSTMMode _mode;

        public NativeMethods.LoopBehavior LoopBehavior { get; private set; }

        private GainSTM(Freq<double>? freq, Freq<double>? freqNearest, SamplingConfigWrap? config)
        {
            _freq = freq;
            _freqNearest = freqNearest;
            _config = config;

            _gains = new List<Driver.Datagram.Gain.IGain>();
            _mode = GainSTMMode.PhaseIntensityFull;

            LoopBehavior = AUTD3Sharp.LoopBehavior.Infinite;
        }

        public static GainSTM FromFreq(Freq<double> freq)
        {
            return new GainSTM(freq, null, null);
        }

        public static GainSTM FromPeriod(Freq<double> freq)
        {
            return new GainSTM(null, freq, null);
        }

        public static GainSTM FromSamplingConfig(SamplingConfigWrap config)
        {
            return new GainSTM(null, null, config);
        }

        public GainSTM AddGain(Driver.Datagram.Gain.IGain gain)
        {
            _gains.Add(gain);
            return this;
        }

        public GainSTM AddGainsFromIter(IEnumerable<Driver.Datagram.Gain.IGain> iter)
        {
            return iter.Aggregate(this, (stm, gain) => stm.AddGain(gain));
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

        public DatagramPtr Ptr(Geometry geometry) => NativeMethodsBase.AUTDSTMGainIntoDatagram(this.RawPtr(geometry));

        public DatagramPtr IntoSegmentTransition(GainSTMPtr p, Segment segment, TransitionModeWrap? transitionMode) =>
            transitionMode.HasValue
                ? NativeMethodsBase.AUTDSTMGainIntoDatagramWithSegmentTransition(p, segment, transitionMode.Value)
                : NativeMethodsBase.AUTDSTMGainIntoDatagramWithSegment(p, segment);

        public GainSTMPtr RawPtr(Geometry geometry)
        {
            var gains = _gains.Select(g => g.GainPtr(geometry)).ToArray();
            unsafe
            {
#pragma warning disable CS8509
                var ptr = (_freq, _freqNearest, _config) switch
                {
                    ({ } f, null, null) => NativeMethodsBase.AUTDSTMGainFromFreq(f.Hz),
                    (null, { } f, null) => NativeMethodsBase.AUTDSTMGainFromFreqNearest(f.Hz),
                    (null, null, { } c) => NativeMethodsBase.AUTDSTMGainFromSamplingConfig(c),
                };
#pragma warning restore CS8509
                ptr = NativeMethodsBase.AUTDSTMGainWithLoopBehavior(ptr, LoopBehavior);
                ptr = NativeMethodsBase.AUTDSTMGainWithMode(ptr, _mode);
                fixed (GainPtr* gp = &gains[0])
                {
                    return NativeMethodsBase.AUTDSTMGainAddGains(ptr, gp, (uint)gains.Length);
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
