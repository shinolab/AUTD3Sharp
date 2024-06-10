using System;
using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

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

        internal FociSTM(Freq<float>? freq, Freq<float>? freqNearest, SamplingConfigWrap? config, IEnumerable<ControlPoints<T>> points)
        {
            _freq = freq;
            _freqNearest = freqNearest;
            _config = config;

            _points = points.ToArray();

            LoopBehavior = AUTD3Sharp.LoopBehavior.Infinite;
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
            unsafe
            {
#pragma warning disable CS8500
                fixed (ControlPoints<T>* pp = &_points[0])
                {
                    var ptr = (_freq, _freqNearest, _config) switch
                    {
                        ({ } f, null, null) => NativeMethodsBase.AUTDSTMFociFromFreq(f.Hz, (IntPtr)pp, (ushort)_points.Length, default(T).Value).Validate(),
                        (null, { } f, null) => NativeMethodsBase.AUTDSTMFociFromFreqNearest(f.Hz, (IntPtr)pp, (ushort)_points.Length, default(T).Value).Validate(),
                        _ => NativeMethodsBase.AUTDSTMFociFromSamplingConfig(_config!.Value, (IntPtr)pp, (ushort)_points.Length, default(T).Value)
                    };
                    return NativeMethodsBase.AUTDSTMFociWithLoopBehavior(ptr, default(T).Value, LoopBehavior);
                }
#pragma warning restore CS8500
            }
        }

        public DatagramWithSegmentTransition<FociSTM<T>, FociSTMPtr> WithSegment(Segment segment, TransitionModeWrap? transitionMode)
            => new DatagramWithSegmentTransition<FociSTM<T>, FociSTMPtr>(this, segment, transitionMode);
    }

    public class FociSTM
    {
        public static FociSTM<T> FromFreq<T>(Freq<float> freq, IEnumerable<ControlPoints<T>> points) where T : struct, IFociTuple => new FociSTM<T>(freq, null, null, points);
        public static FociSTM<T> FromFreqNearest<T>(Freq<float> freq, IEnumerable<ControlPoints<T>> points) where T : struct, IFociTuple => new FociSTM<T>(null, freq, null, points);
        public static FociSTM<T> FromSamplingConfig<T>(SamplingConfigWrap config, IEnumerable<ControlPoints<T>> points) where T : struct, IFociTuple => new FociSTM<T>(null, null, config, points);

        public static FociSTM<N1> FromFreq(Freq<float> freq, IEnumerable<Vector3> points) => new FociSTM<N1>(freq, null, null, points.Select(p => new ControlPoints<N1>(p)));
        public static FociSTM<N1> FromFreqNearest(Freq<float> freq, IEnumerable<Vector3> points) => new FociSTM<N1>(null, freq, null, points.Select(p => new ControlPoints<N1>(p)));
        public static FociSTM<N1> FromSamplingConfig(SamplingConfigWrap config, IEnumerable<Vector3> points) => new FociSTM<N1>(null, null, config, points.Select(p => new ControlPoints<N1>(p)));

        public static FociSTM<N1> FromFreq(Freq<float> freq, IEnumerable<ControlPoint> points) => new FociSTM<N1>(freq, null, null, points.Select(p => new ControlPoints<N1>(p)));
        public static FociSTM<N1> FromFreqNearest(Freq<float> freq, IEnumerable<ControlPoint> points) => new FociSTM<N1>(null, freq, null, points.Select(p => new ControlPoints<N1>(p)));
        public static FociSTM<N1> FromSamplingConfig(SamplingConfigWrap config, IEnumerable<ControlPoint> points) => new FociSTM<N1>(null, null, config, points.Select(p => new ControlPoints<N1>(p)));
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
