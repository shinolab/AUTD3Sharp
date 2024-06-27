using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    where T : unmanaged, IControlPoints
    {
        private readonly STMSamplingConfig _config;

        private readonly T[] _points;

        public NativeMethods.LoopBehavior LoopBehavior { get; private set; }

        internal FociSTM(STMSamplingConfig config, IEnumerable<T> points)
        {
            _config = config;

            _points = points as T[] ?? points.ToArray();

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
                fixed (T* pp = &_points[0])
                {

                    var ptr = NativeMethodsBase.AUTDSTMFoci(_config.Inner, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, default(T).Value).Validate();
                    return NativeMethodsBase.AUTDSTMFociWithLoopBehavior(ptr, default(T).Value, LoopBehavior);
                }
            }
        }

        public DatagramWithSegmentTransition<FociSTM<T>, FociSTMPtr> WithSegment(Segment segment, TransitionModeWrap? transitionMode)
            => new DatagramWithSegmentTransition<FociSTM<T>, FociSTMPtr>(this, segment, transitionMode);

        public Freq<float> Freq => _config.Freq(_points.Length);
        public TimeSpan Period => _config.Period(_points.Length);
        public SamplingConfig SamplingConfig => _config.SamplingConfig(_points.Length);
    }

    public static class FociSTM
    {
        [ExcludeFromCodeCoverage]
        public static FociSTM<T> FromFreq<T>(Freq<float> freq, IEnumerable<T> points) where T : unmanaged, IControlPoints => new FociSTM<T>(STMSamplingConfig.FromFreq(freq), points);
        [ExcludeFromCodeCoverage]
        public static FociSTM<T> FromFreqNearest<T>(Freq<float> freq, IEnumerable<T> points) where T : unmanaged, IControlPoints => new FociSTM<T>(STMSamplingConfig.FromFreqNearest(freq), points);
        [ExcludeFromCodeCoverage]
        public static FociSTM<T> FromSamplingConfig<T>(SamplingConfig config, IEnumerable<T> points) where T : unmanaged, IControlPoints => new FociSTM<T>(STMSamplingConfig.FromSamplingConfig(config), points);

        [ExcludeFromCodeCoverage]
        public static FociSTM<ControlPoints1> FromFreq(Freq<float> freq, IEnumerable<Vector3> points) => new FociSTM<ControlPoints1>(STMSamplingConfig.FromFreq(freq), points.Select(p => new ControlPoints1(p)));
        [ExcludeFromCodeCoverage]
        public static FociSTM<ControlPoints1> FromFreqNearest(Freq<float> freq, IEnumerable<Vector3> points) => new FociSTM<ControlPoints1>(STMSamplingConfig.FromFreqNearest(freq), points.Select(p => new ControlPoints1(p)));
        [ExcludeFromCodeCoverage]
        public static FociSTM<ControlPoints1> FromSamplingConfig(SamplingConfig config, IEnumerable<Vector3> points) => new FociSTM<ControlPoints1>(STMSamplingConfig.FromSamplingConfig(config), points.Select(p => new ControlPoints1(p)));

        [ExcludeFromCodeCoverage]
        public static FociSTM<ControlPoints1> FromFreq(Freq<float> freq, IEnumerable<ControlPoint> points) => new FociSTM<ControlPoints1>(STMSamplingConfig.FromFreq(freq), points.Select(p => new ControlPoints1(p)));
        [ExcludeFromCodeCoverage]
        public static FociSTM<ControlPoints1> FromFreqNearest(Freq<float> freq, IEnumerable<ControlPoint> points) => new FociSTM<ControlPoints1>(STMSamplingConfig.FromFreqNearest(freq), points.Select(p => new ControlPoints1(p)));
        [ExcludeFromCodeCoverage]
        public static FociSTM<ControlPoints1> FromSamplingConfig(SamplingConfig config, IEnumerable<ControlPoint> points) => new FociSTM<ControlPoints1>(STMSamplingConfig.FromSamplingConfig(config), points.Select(p => new ControlPoints1(p)));
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
