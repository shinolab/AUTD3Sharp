using System;
using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public sealed class FociSTM<T> : IDatagramST<FociSTMPtr>, IDatagram
    where T : struct, IFociTuple
    {
        private readonly Freq<float>? _freq;
        private readonly Freq<float>? _freqNearest;
        private readonly SamplingConfigWrap? _config;

        private readonly ControlPoints<T>[] _points;

        public NativeMethods.LoopBehavior LoopBehavior { get; private set; }

        private FociSTM(Freq<float>? freq, Freq<float>? freqNearest, SamplingConfigWrap? config, IEnumerable<ControlPoints<T>> points)
        {
            _freq = freq;
            _freqNearest = freqNearest;
            _config = config;

            _points = points.ToArray();

            LoopBehavior = AUTD3Sharp.LoopBehavior.Infinite;
        }

        public static FociSTM<T> FromFreq(Freq<float> freq, IEnumerable<ControlPoints<T>> points)
        {
            return new FociSTM<T>(freq, null, null, points);
        }

        public static FociSTM<T> FromFreqNearest(Freq<float> freq, IEnumerable<ControlPoints<T>> points)
        {
            return new FociSTM<T>(null, freq, null, points);
        }

        public static FociSTM<T> FromSamplingConfig(SamplingConfigWrap config, IEnumerable<ControlPoints<T>> points)
        {
            return new FociSTM<T>(null, null, config, points);
        }

        public DatagramPtr Ptr(Geometry geometry) => NativeMethodsBase.AUTDSTMFociIntoDatagram(RawPtr(geometry), default(T).Value);

        public FociSTM<T> WithLoopBehavior(NativeMethods.LoopBehavior loopBehavior)
        {
            LoopBehavior = loopBehavior;
            return this;
        }

        public DatagramPtr IntoSegmentTransition(FociSTMPtr p, Segment segment, TransitionModeWrap? transitionMode) =>
        transitionMode.HasValue
            ? NativeMethodsBase.AUTDSTMFociIntoDatagramWithSegmentTransition(p, default(T).Value, segment, transitionMode.Value)
            : NativeMethodsBase.AUTDSTMFociIntoDatagramWithSegment(p, default(T).Value, segment);

        public FociSTMPtr RawPtr(Geometry geometry)
        {
            var intensities = _points.Select(p => p.Intensity).ToArray();
            var offsets = _points.SelectMany(p => p.Points.Select(c => c.Offset)).ToArray();
            var points = _points.SelectMany(p => p.Points.SelectMany(c => c.Point)).ToArray();
            unsafe
            {
                fixed (float* pp = &points[0])
                fixed (EmitIntensity* ip = &intensities[0])
                fixed (Phase* op = &offsets[0])
                {
                    var ptr = (_freq, _freqNearest, _config) switch
                    {
                        ({ } f, null, null) => NativeMethodsBase.AUTDSTMFociFromFreq(f.Hz, pp, (byte*)op, (byte*)ip, (ushort)intensities.Length, default(T).Value).Validate(),
                        (null, { } f, null) => NativeMethodsBase.AUTDSTMFociFromFreqNearest(f.Hz, pp, (byte*)op, (byte*)ip, (ushort)intensities.Length, default(T).Value).Validate(),
                        _ => NativeMethodsBase.AUTDSTMFociFromSamplingConfig(_config!.Value, pp, (byte*)op, (byte*)ip, (ushort)intensities.Length, default(T).Value)
                    };
                    return NativeMethodsBase.AUTDSTMFociWithLoopBehavior(ptr, default(T).Value, LoopBehavior);
                }
            }
        }

        public DatagramWithSegmentTransition<FociSTM<T>, FociSTMPtr> WithSegment(Segment segment, TransitionModeWrap? transitionMode)
            => new DatagramWithSegmentTransition<FociSTM<T>, FociSTMPtr>(this, segment, transitionMode);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
