using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public sealed class GainSTM : IDatagramS, IDatagram, IDatagramL
    {
        public IGain[] Gains;
        public STMSamplingConfig Config;
        public GainSTMOption Option;

        public GainSTM(IEnumerable<IGain> gains, STMSamplingConfig config, GainSTMOption option)
        {
            Gains = gains as IGain[] ?? gains.ToArray();
            Config = config;
            Option = option;
        }

        public GainSTM IntoNearest() => new(Gains, Config.IntoNearest(), Option);

        public SamplingConfig SamplingConfig() => Config.SamplingConfig(Gains.Length);

        private GainSTMPtr RawPtr(Geometry geometry)
        {
            var gains = Gains.Select(g => g.GainPtr(geometry)).ToArray();
            unsafe
            {
                fixed (GainPtr* pGains = &gains[0])
                    return NativeMethodsBase.AUTDSTMGain(SamplingConfig().Inner, pGains, (ushort)Gains.Length, Option);
            }
        }

        DatagramPtr IDatagramS.WithSegmentTransition(Geometry geometry, Segment segment, IInfiniteTransitionMode transitionMode) => NativeMethodsBase.AUTDSTMGainIntoDatagramWithSegment(RawPtr(geometry), segment.ToNative(), transitionMode.Inner);
        DatagramPtr IDatagramL.WithFiniteLoop(Geometry geometry, Segment segment, IFiniteTransitionMode transitionMode, ushort loopCount) => NativeMethodsBase.AUTDSTMGainIntoDatagramWithFiniteLoop(RawPtr(geometry), segment.ToNative(), transitionMode.Inner, loopCount);
        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDSTMGainIntoDatagram(RawPtr(geometry));
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
