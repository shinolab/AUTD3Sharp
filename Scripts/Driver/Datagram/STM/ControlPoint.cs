using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;

namespace AUTD3Sharp
{
    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoint
    {
        [ExcludeFromCodeCoverage]
        public Vector3 Point { get; }

        [Property(Phase = true)]
        public Phase Offset { get; private set; }

        public ControlPoint(Vector3 point)
        {
            Point = point;
            Offset = new Phase(0);
        }
    }

    public interface IControlPoints
    {
        byte Value { get; }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints1 : IControlPoints
    {
        private ControlPoint _point;

        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [ExcludeFromCodeCoverage]
        public readonly ControlPoint[] Points => new[] { _point };
        public readonly byte Value => 1;

        private ControlPoints1(ControlPoint point, EmitIntensity intensity)
        {
            _point = point;
            Intensity = intensity;
        }

        public ControlPoints1(Vector3 v) : this(new ControlPoint(v), EmitIntensity.Max)
        {
        }

        [ExcludeFromCodeCoverage]
        public ControlPoints1(ControlPoint v) : this(v, EmitIntensity.Max)
        {
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints2 : IControlPoints
    {
        private ControlPoint _point1;
        private ControlPoint _point2;
        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [ExcludeFromCodeCoverage]
        public readonly ControlPoint[] Points => new[] { _point1, _point2 };
        public readonly byte Value => 2;

        private ControlPoints2((ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _point1 = points.Item1;
            _point2 = points.Item2;
            Intensity = intensity;
        }

        public ControlPoints2((Vector3, Vector3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2)))
        {
        }

        public ControlPoints2((ControlPoint, ControlPoint) v) : this(v, EmitIntensity.Max)
        {
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints3 : IControlPoints
    {
        private ControlPoint _point1;
        private ControlPoint _point2;
        private ControlPoint _point3;
        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [ExcludeFromCodeCoverage]
        public readonly ControlPoint[] Points => new[] { _point1, _point2, _point3 };
        public readonly byte Value => 3;

        private ControlPoints3((ControlPoint, ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _point1 = points.Item1;
            _point2 = points.Item2;
            _point3 = points.Item3;
            Intensity = intensity;
        }

        public ControlPoints3((Vector3, Vector3, Vector3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3)))
        {
        }

        public ControlPoints3((ControlPoint, ControlPoint, ControlPoint) v) : this(v, EmitIntensity.Max)
        {
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints4 : IControlPoints
    {
        private ControlPoint _point1;
        private ControlPoint _point2;
        private ControlPoint _point3;
        private ControlPoint _point4;
        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [ExcludeFromCodeCoverage]
        public readonly ControlPoint[] Points => new[] { _point1, _point2, _point3, _point4 };
        public readonly byte Value => 4;

        private ControlPoints4((ControlPoint, ControlPoint, ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _point1 = points.Item1;
            _point2 = points.Item2;
            _point3 = points.Item3;
            _point4 = points.Item4;
            Intensity = intensity;
        }

        public ControlPoints4((Vector3, Vector3, Vector3, Vector3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4)))
        {
        }

        public ControlPoints4((ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) : this(v, EmitIntensity.Max)
        {
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints5 : IControlPoints
    {
        private ControlPoint _point1;
        private ControlPoint _point2;
        private ControlPoint _point3;
        private ControlPoint _point4;
        private ControlPoint _point5;
        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [ExcludeFromCodeCoverage]
        public readonly ControlPoint[] Points => new[] { _point1, _point2, _point3, _point4, _point5 };
        public readonly byte Value => 5;

        private ControlPoints5((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _point1 = points.Item1;
            _point2 = points.Item2;
            _point3 = points.Item3;
            _point4 = points.Item4;
            _point5 = points.Item5;
            Intensity = intensity;
        }

        public ControlPoints5((Vector3, Vector3, Vector3, Vector3, Vector3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5)))
        {
        }

        public ControlPoints5((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) : this(v, EmitIntensity.Max)
        {
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints6 : IControlPoints
    {
        private ControlPoint _point1;
        private ControlPoint _point2;
        private ControlPoint _point3;
        private ControlPoint _point4;
        private ControlPoint _point5;
        private ControlPoint _point6;
        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [ExcludeFromCodeCoverage]
        public readonly ControlPoint[] Points => new[] { _point1, _point2, _point3, _point4, _point5, _point6 };
        public readonly byte Value => 6;

        private ControlPoints6((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _point1 = points.Item1;
            _point2 = points.Item2;
            _point3 = points.Item3;
            _point4 = points.Item4;
            _point5 = points.Item5;
            _point6 = points.Item6;
            Intensity = intensity;
        }

        public ControlPoints6((Vector3, Vector3, Vector3, Vector3, Vector3, Vector3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6)))
        {
        }

        public ControlPoints6((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) : this(v, EmitIntensity.Max)
        {
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints7 : IControlPoints
    {
        private ControlPoint _point1;
        private ControlPoint _point2;
        private ControlPoint _point3;
        private ControlPoint _point4;
        private ControlPoint _point5;
        private ControlPoint _point6;
        private ControlPoint _point7;
        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [ExcludeFromCodeCoverage]
        public readonly ControlPoint[] Points => new[] { _point1, _point2, _point3, _point4, _point5, _point6, _point7 };
        public readonly byte Value => 7;

        private ControlPoints7((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _point1 = points.Item1;
            _point2 = points.Item2;
            _point3 = points.Item3;
            _point4 = points.Item4;
            _point5 = points.Item5;
            _point6 = points.Item6;
            _point7 = points.Item7;
            Intensity = intensity;
        }

        public ControlPoints7((Vector3, Vector3, Vector3, Vector3, Vector3, Vector3, Vector3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6), new ControlPoint(v.Item7)))
        {
        }

        public ControlPoints7((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) : this(v, EmitIntensity.Max)
        {
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints8 : IControlPoints
    {
        private ControlPoint _point1;
        private ControlPoint _point2;
        private ControlPoint _point3;
        private ControlPoint _point4;
        private ControlPoint _point5;
        private ControlPoint _point6;
        private ControlPoint _point7;
        private ControlPoint _point8;
        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [ExcludeFromCodeCoverage]
        public readonly ControlPoint[] Points => new[] { _point1, _point2, _point3, _point4, _point5, _point6, _point7, _point8 };
        public readonly byte Value => 8;

        private ControlPoints8((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _point1 = points.Item1;
            _point2 = points.Item2;
            _point3 = points.Item3;
            _point4 = points.Item4;
            _point5 = points.Item5;
            _point6 = points.Item6;
            _point7 = points.Item7;
            _point8 = points.Item8;
            Intensity = intensity;
        }

        public ControlPoints8((Vector3, Vector3, Vector3, Vector3, Vector3, Vector3, Vector3, Vector3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6), new ControlPoint(v.Item7), new ControlPoint(v.Item8)))
        {
        }

        public ControlPoints8((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) : this(v, EmitIntensity.Max)
        {
        }
    }
}