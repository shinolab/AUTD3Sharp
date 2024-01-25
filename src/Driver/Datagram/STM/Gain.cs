#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Driver.Datagram.STM;
using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif


#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

namespace AUTD3Sharp
{
    /// <summary>
    /// FocusSTM is an STM for moving Gains
    /// </summary>
    /// <remarks>
    /// <para>The sampling timing is determined by hardware, thus the sampling time is precise.</para>
    /// <para>FocusSTM has the following restrictions:</para>
    /// <list>
    /// <item>The maximum number of sampling Gain is 2048.</item>
    /// <item>The sampling frequency is <see cref="AUTD3.FPGAClkFreq">AUTD3.FPGAClkFreq</see>/N, where `N` is a 32-bit unsigned integer and must be at 4096.</item>
    /// </list></remarks>
    public sealed class GainSTM : STM
    {
        private readonly List<Driver.Datagram.Gain> _gains;
        private GainSTMMode _mode;

        private GainSTM(float_t? freq, TimeSpan? period, SamplingConfiguration? config) : base(
            freq, period, config)
        {
            _gains = new List<Driver.Datagram.Gain>();
            _mode = GainSTMMode.PhaseIntensityFull;
        }

        public static GainSTM FromFreq(float_t freq)
        {
            return new GainSTM(freq, null, null);
        }

        public static GainSTM FromPeriod(TimeSpan period)
        {
            return new GainSTM(null, period, null);
        }

        public static GainSTM FromSamplingConfig(SamplingConfiguration config)
        {
            return new GainSTM(null, null, config);
        }

        /// <summary>
        /// Add Gain
        /// </summary>
        /// <param name="gain">Gain</param>
        /// <returns></returns>
        public GainSTM AddGain(Driver.Datagram.Gain gain)
        {
            _gains.Add(gain);
            return this;
        }

        /// <summary>
        /// Add Gains
        /// </summary>
        /// <param name="iter">Enumerable of Gains</param>
        public GainSTM AddGainsFromIter(IEnumerable<Driver.Datagram.Gain> iter)
        {
            return iter.Aggregate(this, (stm, gain) => stm.AddGain(gain));
        }

        public GainSTM WithStartIdx(ushort? startIdx)
        {
            StartIdxV = startIdx ?? -1;
            return this;
        }

        public GainSTM WithFinishIdx(ushort? finishIdx)
        {
            FinishIdxV = finishIdx ?? -1;
            return this;
        }

        public GainSTM WithMode(GainSTMMode mode)
        {
            _mode = mode;
            return this;
        }

        public float_t Frequency => FreqFromSize(_gains.Count);
        public TimeSpan Period => PeriodFromSize(_gains.Count);
        public SamplingConfiguration SamplingConfiguration => SamplingConfigFromSize(_gains.Count);


        internal override DatagramPtr STMPtr(Geometry geometry)
        {
            var gains = _gains.Select(g => g.GainPtr(geometry)).ToArray();
            unsafe
            {
                fixed (GainPtr* gp = &gains[0])
                    return NativeMethodsBase.AUTDSTMGain(Props(), gp, (uint)gains.Length, _mode).Validate();
            }
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
