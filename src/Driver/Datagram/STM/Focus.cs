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

#if UNITY_2018_3_OR_NEWER
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
#else
using Vector3 = AUTD3Sharp.Utils.Vector3d;
#endif


#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

namespace AUTD3Sharp
{
    /// <summary>
    /// FocusSTM is an STM for moving a focal point
    /// </summary>
    /// <remarks>
    /// <para>The sampling timing is determined by hardware, thus the sampling time is precise.</para>
    /// <para>FocusSTM has the following restrictions:</para>
    /// <list>
    /// <item>The maximum number of sampling points is 65536.</item>
    /// <item>The sampling frequency is <see cref="AUTD3.FPGAClkFreq">AUTD3.FPGAClkFreq</see>/N, where `N` is a 32-bit unsigned integer and must be at 4096.</item>
    /// </list></remarks>
    public sealed class FocusSTM : STM
    {
        private readonly List<float_t> _points;
        private readonly List<EmitIntensity> _intensities;

        private FocusSTM(float_t? freq, TimeSpan? period, SamplingConfiguration? config) : base(freq, period, config)
        {
            _points = new List<float_t>();
            _intensities = new List<EmitIntensity>();
        }

        public static FocusSTM FromFreq(float_t freq)
        {
            return new FocusSTM(freq, null, null);
        }

        public static FocusSTM FromPeriod(TimeSpan period)
        {
            return new FocusSTM(null, period, null);
        }

        public static FocusSTM FromSamplingConfig(SamplingConfiguration config)
        {
            return new FocusSTM(null, null, config);
        }

        /// <summary>
        /// Add focus point
        /// </summary>
        /// <param name="point">Focus point</param>
        /// <param name="intensity">Emission intensity</param>
        /// <returns></returns>
        public FocusSTM AddFocus(Vector3 point, EmitIntensity? intensity = null)
        {
            _points.Add(point.x);
            _points.Add(point.y);
            _points.Add(point.z);
            _intensities.Add(intensity ?? EmitIntensity.Max);
            return this;
        }

        /// <summary>
        /// Add foci
        /// </summary>
        /// <param name="iter">Enumerable of foci</param>
        public FocusSTM AddFociFromIter(IEnumerable<Vector3> iter)
        {
            return iter.Aggregate(this, (stm, point) => stm.AddFocus(point));
        }

        /// <summary>
        /// Add foci
        /// </summary>
        /// <param name="iter">Enumerable of foci and duty shifts</param>
        public FocusSTM AddFociFromIter(IEnumerable<(Vector3, EmitIntensity)> iter)
        {
            return iter.Aggregate(this, (stm, point) => stm.AddFocus(point.Item1, point.Item2));
        }

        public FocusSTM WithStartIdx(ushort? startIdx)
        {
            StartIdxV = startIdx ?? -1;
            return this;
        }

        public FocusSTM WithFinishIdx(ushort? finishIdx)
        {
            FinishIdxV = finishIdx ?? -1;
            return this;
        }

        public float_t Frequency => FreqFromSize(_intensities.Count);
        public TimeSpan Period => PeriodFromSize(_intensities.Count);
        public SamplingConfiguration SamplingConfiguration => SamplingConfigFromSize(_intensities.Count);

        internal override DatagramPtr STMPtr(Geometry geometry)
        {
            var points = _points.ToArray();
            var intensities = _intensities.ToArray();
            unsafe
            {
                fixed (float_t* pp = &points[0])
                fixed (EmitIntensity* ps = &intensities[0])
                    return NativeMethodsBase.AUTDSTMFocus(Props(), pp, (byte*)ps, (ulong)_intensities.Count)
                        .Validate();
            }
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
