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
    internal interface IControlPointsArray
    {
        internal FociSTMPtr Ptr(SamplingConfig config, LoopBehavior loopBehavior);
        internal byte N();
        internal int Length();
    }

    internal class ControlPointsArray1 : IControlPointsArray
    {
        private readonly ControlPoints1[] _points;
        public ControlPointsArray1(ControlPoints1[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config, LoopBehavior loopBehavior)
        {
            unsafe
            {
                fixed (ControlPoints1* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 1, loopBehavior).Validate();
            }
        }
        byte IControlPointsArray.N() => 1;
        int IControlPointsArray.Length() => _points.Length;
    }

    internal class ControlPointsArray2 : IControlPointsArray
    {
        private readonly ControlPoints2[] _points;
        public ControlPointsArray2(ControlPoints2[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config, LoopBehavior loopBehavior)
        {
            unsafe
            {
                fixed (ControlPoints2* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 2, loopBehavior).Validate();
            }
        }
        byte IControlPointsArray.N() => 2;
        int IControlPointsArray.Length() => _points.Length;
    }

    internal class ControlPointsArray3 : IControlPointsArray
    {
        private readonly ControlPoints3[] _points;
        public ControlPointsArray3(ControlPoints3[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config, LoopBehavior loopBehavior)
        {
            unsafe
            {
                fixed (ControlPoints3* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 3, loopBehavior).Validate();
            }
        }
        byte IControlPointsArray.N() => 3;
        int IControlPointsArray.Length() => _points.Length;
    }

    internal class ControlPointsArray4 : IControlPointsArray
    {
        private readonly ControlPoints4[] _points;
        public ControlPointsArray4(ControlPoints4[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config, LoopBehavior loopBehavior)
        {
            unsafe
            {
                fixed (ControlPoints4* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 4, loopBehavior).Validate();
            }
        }
        byte IControlPointsArray.N() => 4;
        int IControlPointsArray.Length() => _points.Length;
    }

    internal class ControlPointsArray5 : IControlPointsArray
    {
        private readonly ControlPoints5[] _points;
        public ControlPointsArray5(ControlPoints5[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config, LoopBehavior loopBehavior)
        {
            unsafe
            {
                fixed (ControlPoints5* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 5, loopBehavior).Validate();
            }
        }
        byte IControlPointsArray.N() => 5;
        int IControlPointsArray.Length() => _points.Length;
    }

    internal class ControlPointsArray6 : IControlPointsArray
    {
        private readonly ControlPoints6[] _points;
        public ControlPointsArray6(ControlPoints6[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config, LoopBehavior loopBehavior)
        {
            unsafe
            {
                fixed (ControlPoints6* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 6, loopBehavior).Validate();
            }
        }
        byte IControlPointsArray.N() => 6;
        int IControlPointsArray.Length() => _points.Length;
    }

    internal class ControlPointsArray7 : IControlPointsArray
    {
        private readonly ControlPoints7[] _points;
        public ControlPointsArray7(ControlPoints7[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config, LoopBehavior loopBehavior)
        {
            unsafe
            {
                fixed (ControlPoints7* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 7, loopBehavior).Validate();
            }
        }
        byte IControlPointsArray.N() => 7;
        int IControlPointsArray.Length() => _points.Length;
    }

    internal class ControlPointsArray8 : IControlPointsArray
    {
        private readonly ControlPoints8[] _points;
        public ControlPointsArray8(ControlPoints8[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config, LoopBehavior loopBehavior)
        {
            unsafe
            {
                fixed (ControlPoints8* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 8, loopBehavior).Validate();
            }
        }
        byte IControlPointsArray.N() => 8;
        int IControlPointsArray.Length() => _points.Length;
    }

    public sealed class FociSTM : IDatagramS<FociSTMPtr>, IDatagram, IWithSampling
    {
        private readonly STMSamplingConfig _config;

        private readonly IControlPointsArray _points;

        public LoopBehavior LoopBehavior { get; private set; } = AUTD3Sharp.LoopBehavior.Infinite;

        public FociSTM(STMSamplingConfig config, IEnumerable<ControlPoints1> points)
        {
            var pointsArray = points as ControlPoints1[] ?? points.ToArray();
            _config = config;
            _points = new ControlPointsArray1(pointsArray);
        }
        public FociSTM(STMSamplingConfig config, IEnumerable<ControlPoint> points) : this(config, points.Select(p => new ControlPoints1(p))) { }
        public FociSTM(STMSamplingConfig config, IEnumerable<Point3> points) : this(config, points.Select(p => new ControlPoint(p))) { }

        public FociSTM(STMSamplingConfig config, IEnumerable<ControlPoints2> points)
        {
            var pointsArray = points as ControlPoints2[] ?? points.ToArray();
            _config = config;
            _points = new ControlPointsArray2(pointsArray);
        }

        public FociSTM(STMSamplingConfig config, IEnumerable<ControlPoints3> points)
        {
            var pointsArray = points as ControlPoints3[] ?? points.ToArray();
            _config = config;
            _points = new ControlPointsArray3(pointsArray);
        }

        public FociSTM(STMSamplingConfig config, IEnumerable<ControlPoints4> points)
        {
            var pointsArray = points as ControlPoints4[] ?? points.ToArray();
            _config = config;
            _points = new ControlPointsArray4(pointsArray);
        }

        public FociSTM(STMSamplingConfig config, IEnumerable<ControlPoints5> points)
        {
            var pointsArray = points as ControlPoints5[] ?? points.ToArray();
            _config = config;
            _points = new ControlPointsArray5(pointsArray);
        }

        public FociSTM(STMSamplingConfig config, IEnumerable<ControlPoints6> points)
        {
            var pointsArray = points as ControlPoints6[] ?? points.ToArray();
            _config = config;
            _points = new ControlPointsArray6(pointsArray);
        }

        public FociSTM(STMSamplingConfig config, IEnumerable<ControlPoints7> points)
        {
            var pointsArray = points as ControlPoints7[] ?? points.ToArray();
            _config = config;
            _points = new ControlPointsArray7(pointsArray);
        }

        public FociSTM(STMSamplingConfig config, IEnumerable<ControlPoints8> points)
        {
            var pointsArray = points as ControlPoints8[] ?? points.ToArray();
            _config = config;
            _points = new ControlPointsArray8(pointsArray);
        }

        public static FociSTM Nearest(STMSamplingConfigNearest config, IEnumerable<ControlPoints1> points)
        {
            var pointsArray = points as ControlPoints1[] ?? points.ToArray();
            return new FociSTM(config.STMSamplingConfig(pointsArray.Length), pointsArray);
        }

        public static FociSTM Nearest(STMSamplingConfigNearest config, IEnumerable<ControlPoint> points)
        {
            var pointsArray = points.Select(p => new ControlPoints1(p)).ToArray();
            return new FociSTM(config.STMSamplingConfig(pointsArray.Length), pointsArray);
        }

        public static FociSTM Nearest(STMSamplingConfigNearest config, IEnumerable<Point3> points)
        {
            var pointsArray = points.Select(p => new ControlPoints1(p)).ToArray();
            return new FociSTM(config.STMSamplingConfig(pointsArray.Length), pointsArray);
        }

        public static FociSTM Nearest(STMSamplingConfigNearest config, IEnumerable<ControlPoints2> points)
        {
            var pointsArray = points as ControlPoints2[] ?? points.ToArray();
            return new FociSTM(config.STMSamplingConfig(pointsArray.Length), pointsArray);
        }

        public static FociSTM Nearest(STMSamplingConfigNearest config, IEnumerable<ControlPoints3> points)
        {
            var pointsArray = points as ControlPoints3[] ?? points.ToArray();
            return new FociSTM(config.STMSamplingConfig(pointsArray.Length), pointsArray);
        }

        public static FociSTM Nearest(STMSamplingConfigNearest config, IEnumerable<ControlPoints4> points)
        {
            var pointsArray = points as ControlPoints4[] ?? points.ToArray();
            return new FociSTM(config.STMSamplingConfig(pointsArray.Length), pointsArray);
        }

        public static FociSTM Nearest(STMSamplingConfigNearest config, IEnumerable<ControlPoints5> points)
        {
            var pointsArray = points as ControlPoints5[] ?? points.ToArray();
            return new FociSTM(config.STMSamplingConfig(pointsArray.Length), pointsArray);
        }

        public static FociSTM Nearest(STMSamplingConfigNearest config, IEnumerable<ControlPoints6> points)
        {
            var pointsArray = points as ControlPoints6[] ?? points.ToArray();
            return new FociSTM(config.STMSamplingConfig(pointsArray.Length), pointsArray);
        }

        public static FociSTM Nearest(STMSamplingConfigNearest config, IEnumerable<ControlPoints7> points)
        {
            var pointsArray = points as ControlPoints7[] ?? points.ToArray();
            return new FociSTM(config.STMSamplingConfig(pointsArray.Length), pointsArray);
        }

        public static FociSTM Nearest(STMSamplingConfigNearest config, IEnumerable<ControlPoints8> points)
        {
            var pointsArray = points as ControlPoints8[] ?? points.ToArray();
            return new FociSTM(config.STMSamplingConfig(pointsArray.Length), pointsArray);
        }

        public DatagramPtr Ptr(Geometry geometry) => NativeMethodsBase.AUTDSTMFociIntoDatagram(RawPtr(geometry), _points.N());

        public FociSTM WithLoopBehavior(LoopBehavior loopBehavior)
        {
            LoopBehavior = loopBehavior;
            return this;
        }

        public DatagramPtr IntoSegmentTransition(FociSTMPtr p, Segment segment, TransitionModeWrap? transitionMode) =>
            NativeMethodsBase.AUTDSTMFociIntoDatagramWithSegment(p, _points.N(), segment, transitionMode ?? TransitionMode.None);

        public FociSTMPtr RawPtr(Geometry geometry) => _points.Ptr(_config.SamplingConfig(_points.Length()), LoopBehavior);

        public DatagramWithSegment<FociSTM, FociSTMPtr> WithSegment(Segment segment, TransitionModeWrap? transitionMode) => new(this, segment, transitionMode);

        public Freq<float> Freq => _config.Freq(_points.Length());
        public Duration Period => _config.Period(_points.Length());
        public SamplingConfig SamplingConfig => new(_config.SamplingConfig(_points.Length()));

        SamplingConfig IWithSampling.SamplingConfigIntensity() => SamplingConfig;
        SamplingConfig? IWithSampling.SamplingConfigPhase() => SamplingConfig;
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
