using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Utils;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public sealed class FocusSTM : IDatagramST<FocusSTMPtr>, IDatagram
    {
        private readonly Freq<double>? _freq;
        private readonly Freq<double>? _freqNearest;
        private readonly SamplingConfigWrap? _config;

        private readonly List<double> _points;
        private readonly List<EmitIntensity> _intensities;
        public NativeMethods.LoopBehavior LoopBehavior { get; private set; }

        private FocusSTM(Freq<double>? freq, Freq<double>? freqNearest, SamplingConfigWrap? config)
        {
            _freq = freq;
            _freqNearest = freqNearest;
            _config = config;

            _points = new List<double>();
            _intensities = new List<EmitIntensity>();

            LoopBehavior = AUTD3Sharp.LoopBehavior.Infinite;
        }

        public static FocusSTM FromFreq(Freq<double> freq)
        {
            return new FocusSTM(freq, null, null);
        }

        public static FocusSTM FromFreqNearest(Freq<double> freq)
        {
            return new FocusSTM(null, freq, null);
        }

        public static FocusSTM FromSamplingConfig(SamplingConfigWrap config)
        {
            return new FocusSTM(null, null, config);
        }

        public FocusSTM AddFocus(Vector3d point, EmitIntensity? intensity = null)
        {
            _points.Add(point.X);
            _points.Add(point.Y);
            _points.Add(point.Z);
            _intensities.Add(intensity ?? EmitIntensity.Max);
            return this;
        }

        public FocusSTM AddFociFromIter(IEnumerable<Vector3d> iter)
        {
            return iter.Aggregate(this, (stm, point) => stm.AddFocus(point));
        }

        public FocusSTM AddFociFromIter(IEnumerable<(Vector3d, EmitIntensity)> iter)
        {
            return iter.Aggregate(this, (stm, point) => stm.AddFocus(point.Item1, point.Item2));
        }

        public DatagramPtr Ptr(Geometry geometry) => NativeMethodsBase.AUTDSTMFocusIntoDatagram(RawPtr(geometry));

        public FocusSTM WithLoopBehavior(NativeMethods.LoopBehavior loopBehavior)
        {
            LoopBehavior = loopBehavior;
            return this;
        }

        public DatagramPtr IntoSegmentTransition(FocusSTMPtr p, Segment segment, TransitionModeWrap? transitionMode) =>
            transitionMode.HasValue
                ? NativeMethodsBase.AUTDSTMFocusIntoDatagramWithSegmentTransition(p, segment, transitionMode.Value)
                : NativeMethodsBase.AUTDSTMFocusIntoDatagramWithSegment(p, segment);

        public FocusSTMPtr RawPtr(Geometry geometry)
        {
            var points = _points.ToArray();
            var intensities = _intensities.ToArray();
            unsafe
            {
                var ptr = (_freq, _freqNearest, _config) switch
                {
                    ({ } f, null, null) => NativeMethodsBase.AUTDSTMFocusFromFreq(f.Hz),
                    (null, { } f, null) => NativeMethodsBase.AUTDSTMFocusFromFreqNearest(f.Hz),
                    _ => NativeMethodsBase.AUTDSTMFocusFromSamplingConfig(_config!.Value),
                };
                ptr = NativeMethodsBase.AUTDSTMFocusWithLoopBehavior(ptr, LoopBehavior);
                fixed (double* pp = &points[0])
                fixed (EmitIntensity* ps = &intensities[0])
                {

                    return NativeMethodsBase.AUTDSTMFocusAddFoci(ptr, pp, (byte*)ps, (ulong)_intensities.Count);
                }
            }
        }

        public DatagramWithSegmentTransition<FocusSTM, FocusSTMPtr> WithSegment(Segment segment, TransitionModeWrap? transitionMode)
            => new DatagramWithSegmentTransition<FocusSTM, FocusSTMPtr>(this, segment, transitionMode);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
