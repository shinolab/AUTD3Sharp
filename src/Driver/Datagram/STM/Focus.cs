using System;
using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Driver.Datagram.STM;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Utils;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
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
    public sealed class FocusSTM : STM, IDatagramS<FocusSTMPtr>
    {
        private readonly List<double> _points;
        private readonly List<EmitIntensity> _intensities;

        private FocusSTM(double? freq, TimeSpan? period, SamplingConfiguration? config) : base(freq, period, config)
        {
            _points = new List<double>();
            _intensities = new List<EmitIntensity>();
        }

        public static FocusSTM FromFreq(double freq)
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
        public FocusSTM AddFocus(Vector3d point, EmitIntensity? intensity = null)
        {
            _points.Add(point.X);
            _points.Add(point.Y);
            _points.Add(point.Z);
            _intensities.Add(intensity ?? EmitIntensity.Max);
            return this;
        }

        /// <summary>
        /// Add foci
        /// </summary>
        /// <param name="iter">Enumerable of foci</param>
        public FocusSTM AddFociFromIter(IEnumerable<Vector3d> iter)
        {
            return iter.Aggregate(this, (stm, point) => stm.AddFocus(point));
        }

        /// <summary>
        /// Add foci
        /// </summary>
        /// <param name="iter">Enumerable of foci and duty shifts</param>
        public FocusSTM AddFociFromIter(IEnumerable<(Vector3d, EmitIntensity)> iter)
        {
            return iter.Aggregate(this, (stm, point) => stm.AddFocus(point.Item1, point.Item2));
        }

        public double Frequency => FreqFromSize(_intensities.Count);
        public TimeSpan Period => PeriodFromSize(_intensities.Count);
        public SamplingConfiguration SamplingConfiguration => SamplingConfigFromSize(_intensities.Count);

        internal override DatagramPtr STMPtr(Geometry geometry)
        {
            return NativeMethodsBase.AUTDSTMFocusIntoDatagram(((IDatagramS<FocusSTMPtr>)this).RawPtr(geometry));
        }

        public FocusSTM WithLoopBehavior(LoopBehavior loopBehavior)
        {
            LoopBehavior = loopBehavior;
            return this;
        }

        DatagramPtr IDatagramS<FocusSTMPtr>.IntoSegment(FocusSTMPtr p, Segment segment, bool updateSegment) => NativeMethodsBase.AUTDSTMFocusIntoDatagramWithSegment(p, segment, updateSegment);
        FocusSTMPtr IDatagramS<FocusSTMPtr>.RawPtr(Geometry geometry)
        {
            var points = _points.ToArray();
            var intensities = _intensities.ToArray();
            unsafe
            {
                fixed (double* pp = &points[0])
                fixed (EmitIntensity* ps = &intensities[0])
                {
                    return NativeMethodsBase.AUTDSTMFocus(Props(), pp, (byte*)ps, (ulong)_intensities.Count)
                        .Validate();
                }

            }
        }

        public DatagramWithSegment<FocusSTM, FocusSTMPtr> WithSegment(Segment segment, bool updateSegment)
        {
            return new DatagramWithSegment<FocusSTM, FocusSTMPtr>(this, segment, updateSegment);
        }
    }

    public sealed class ChangeFocusSTMSegment : IDatagram
    {
        private readonly Segment _segment;

        public ChangeFocusSTMSegment(Segment segment)
        {
            _segment = segment;
        }

        DatagramPtr IDatagram.Ptr(Geometry _) => NativeMethodsBase.AUTDDatagramChangeFocusSTMSegment(_segment);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
