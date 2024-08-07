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
        internal FociSTMPtr Ptr(SamplingConfig config);
        internal byte N();
    }

    internal class ControlPointsArray1 : IControlPointsArray
    {
        private readonly ControlPoints1[] _points;
        public ControlPointsArray1(ControlPoints1[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config)
        {
            unsafe
            {
                fixed (ControlPoints1* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config.Inner, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 1).Validate();
            }
        }
        byte IControlPointsArray.N() => 1;
    }

    internal class ControlPointsArray2 : IControlPointsArray
    {
        private readonly ControlPoints2[] _points;
        public ControlPointsArray2(ControlPoints2[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config)
        {
            unsafe
            {
                fixed (ControlPoints2* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config.Inner, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 2).Validate();
            }
        }
        byte IControlPointsArray.N() => 2;
    }

    internal class ControlPointsArray3 : IControlPointsArray
    {
        private readonly ControlPoints3[] _points;
        public ControlPointsArray3(ControlPoints3[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config)
        {
            unsafe
            {
                fixed (ControlPoints3* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config.Inner, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 3).Validate();
            }
        }
        byte IControlPointsArray.N() => 3;
    }

    internal class ControlPointsArray4 : IControlPointsArray
    {
        private readonly ControlPoints4[] _points;
        public ControlPointsArray4(ControlPoints4[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config)
        {
            unsafe
            {
                fixed (ControlPoints4* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config.Inner, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 4).Validate();
            }
        }
        byte IControlPointsArray.N() => 4;
    }

    internal class ControlPointsArray5 : IControlPointsArray
    {
        private readonly ControlPoints5[] _points;
        public ControlPointsArray5(ControlPoints5[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config)
        {
            unsafe
            {
                fixed (ControlPoints5* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config.Inner, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 5).Validate();
            }
        }
        byte IControlPointsArray.N() => 5;
    }

    internal class ControlPointsArray6 : IControlPointsArray
    {
        private readonly ControlPoints6[] _points;
        public ControlPointsArray6(ControlPoints6[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config)
        {
            unsafe
            {
                fixed (ControlPoints6* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config.Inner, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 6).Validate();
            }
        }
        byte IControlPointsArray.N() => 6;
    }

    internal class ControlPointsArray7 : IControlPointsArray
    {
        private readonly ControlPoints7[] _points;
        public ControlPointsArray7(ControlPoints7[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config)
        {
            unsafe
            {
                fixed (ControlPoints7* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config.Inner, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 7).Validate();
            }
        }
        byte IControlPointsArray.N() => 7;
    }

    internal class ControlPointsArray8 : IControlPointsArray
    {
        private readonly ControlPoints8[] _points;
        public ControlPointsArray8(ControlPoints8[] points)
        {
            _points = points;
        }
        FociSTMPtr IControlPointsArray.Ptr(SamplingConfig config)
        {
            unsafe
            {
                fixed (ControlPoints8* pp = &_points[0]) return NativeMethodsBase.AUTDSTMFoci(config.Inner, new ConstPtr { Item1 = (IntPtr)pp }, (ushort)_points.Length, 8).Validate();
            }
        }
        byte IControlPointsArray.N() => 8;
    }

    public sealed class FociSTM : IDatagramST<FociSTMPtr>, IDatagram
    {
        private readonly STMSamplingConfig _config;

        private readonly IControlPointsArray _points;

        public NativeMethods.LoopBehavior LoopBehavior { get; private set; } = AUTD3Sharp.LoopBehavior.Infinite;

        public FociSTM(Freq<float> freq, IEnumerable<ControlPoints1> points)
        {
            var pointsArray = points as ControlPoints1[] ?? points.ToArray();
            _config = new STMSamplingConfig(freq, pointsArray.Length);
            _points = new ControlPointsArray1(pointsArray);
        }

        public FociSTM(Freq<float> freq, IEnumerable<ControlPoint> points)
        {
            var pointsArray = points.Select(p => new ControlPoints1(p)).ToArray();
            _config = new STMSamplingConfig(freq, pointsArray.Length);
            _points = new ControlPointsArray1(pointsArray);
        }

        public FociSTM(Freq<float> freq, IEnumerable<Vector3> points)
        {
            var pointsArray = points.Select(p => new ControlPoints1(p)).ToArray();
            _config = new STMSamplingConfig(freq, pointsArray.Length);
            _points = new ControlPointsArray1(pointsArray);
        }

        public FociSTM(Freq<float> freq, IEnumerable<ControlPoints2> points)
        {
            var pointsArray = points as ControlPoints2[] ?? points.ToArray();
            _config = new STMSamplingConfig(freq, pointsArray.Length);
            _points = new ControlPointsArray2(pointsArray);
        }

        public FociSTM(Freq<float> freq, IEnumerable<ControlPoints3> points)
        {
            var pointsArray = points as ControlPoints3[] ?? points.ToArray();
            _config = new STMSamplingConfig(freq, pointsArray.Length);
            _points = new ControlPointsArray3(pointsArray);
        }

        public FociSTM(Freq<float> freq, IEnumerable<ControlPoints4> points)
        {
            var pointsArray = points as ControlPoints4[] ?? points.ToArray();
            _config = new STMSamplingConfig(freq, pointsArray.Length);
            _points = new ControlPointsArray4(pointsArray);
        }

        public FociSTM(Freq<float> freq, IEnumerable<ControlPoints5> points)
        {
            var pointsArray = points as ControlPoints5[] ?? points.ToArray();
            _config = new STMSamplingConfig(freq, pointsArray.Length);
            _points = new ControlPointsArray5(pointsArray);
        }

        public FociSTM(Freq<float> freq, IEnumerable<ControlPoints6> points)
        {
            var pointsArray = points as ControlPoints6[] ?? points.ToArray();
            _config = new STMSamplingConfig(freq, pointsArray.Length);
            _points = new ControlPointsArray6(pointsArray);
        }

        public FociSTM(Freq<float> freq, IEnumerable<ControlPoints7> points)
        {
            var pointsArray = points as ControlPoints7[] ?? points.ToArray();
            _config = new STMSamplingConfig(freq, pointsArray.Length);
            _points = new ControlPointsArray7(pointsArray);
        }

        public FociSTM(Freq<float> freq, IEnumerable<ControlPoints8> points)
        {
            var pointsArray = points as ControlPoints8[] ?? points.ToArray();
            _config = new STMSamplingConfig(freq, pointsArray.Length);
            _points = new ControlPointsArray8(pointsArray);
        }

        public DatagramPtr Ptr(Geometry geometry) => NativeMethodsBase.AUTDSTMFociIntoDatagram(RawPtr(geometry), _points.N());

        public FociSTM WithLoopBehavior(NativeMethods.LoopBehavior loopBehavior)
        {
            LoopBehavior = loopBehavior;
            return this;
        }

        public DatagramPtr IntoSegmentTransition(FociSTMPtr p, Segment segment, TransitionModeWrap? transitionMode) =>
        transitionMode.HasValue
            ? NativeMethodsBase.AUTDSTMFociIntoDatagramWithSegmentTransition(p, _points.N(), segment, transitionMode.Value)
            : NativeMethodsBase.AUTDSTMFociIntoDatagramWithSegment(p, _points.N(), segment);

        public FociSTMPtr RawPtr(Geometry geometry)
        {
            var ptr = _points.Ptr(SamplingConfig);
            return NativeMethodsBase.AUTDSTMFociWithLoopBehavior(ptr, _points.N(), LoopBehavior);
        }

        public DatagramWithSegmentTransition<FociSTM, FociSTMPtr> WithSegment(Segment segment, TransitionModeWrap? transitionMode) => new(this, segment, transitionMode);

        public Freq<float> Freq => _config.Freq;
        public TimeSpan Period => _config.Period;
        public SamplingConfig SamplingConfig => _config.SamplingConfig;
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
