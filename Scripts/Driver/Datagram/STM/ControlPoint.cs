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
        public Point3 Point { get; }

        [Property(Phase = true)]
        public Phase PhaseOffset { get; private set; }

        public ControlPoint(Point3 point)
        {
            Point = point;
            PhaseOffset = new Phase(0);
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints1
    {
        private ControlPoint _point;

        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [ExcludeFromCodeCoverage]
        public readonly ControlPoint[] Points => new[] { _point };

        private ControlPoints1(ControlPoint point, EmitIntensity intensity)
        {
            _point = point;
            Intensity = intensity;
        }

        public ControlPoints1(ControlPoint v) : this(v, EmitIntensity.Max)
        {
        }

        public ControlPoints1(Point3 v) : this(new ControlPoint(v))
        {
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints2
    {
        private ControlPoint _point1;
        private ControlPoint _point2;
        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [ExcludeFromCodeCoverage]
        public readonly ControlPoint[] Points => new[] { _point1, _point2 };
        private ControlPoints2((ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _point1 = points.Item1;
            _point2 = points.Item2;
            Intensity = intensity;
        }

        public ControlPoints2((Point3, Point3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2)))
        {
        }

        public ControlPoints2((ControlPoint, ControlPoint) v) : this(v, EmitIntensity.Max)
        {
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints3
    {
        private ControlPoint _point1;
        private ControlPoint _point2;
        private ControlPoint _point3;
        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [ExcludeFromCodeCoverage]
        public readonly ControlPoint[] Points => new[] { _point1, _point2, _point3 };

        private ControlPoints3((ControlPoint, ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _point1 = points.Item1;
            _point2 = points.Item2;
            _point3 = points.Item3;
            Intensity = intensity;
        }

        public ControlPoints3((Point3, Point3, Point3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3)))
        {
        }

        public ControlPoints3((ControlPoint, ControlPoint, ControlPoint) v) : this(v, EmitIntensity.Max)
        {
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints4
    {
        private ControlPoint _point1;
        private ControlPoint _point2;
        private ControlPoint _point3;
        private ControlPoint _point4;
        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [ExcludeFromCodeCoverage]
        public readonly ControlPoint[] Points => new[] { _point1, _point2, _point3, _point4 };

        private ControlPoints4((ControlPoint, ControlPoint, ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _point1 = points.Item1;
            _point2 = points.Item2;
            _point3 = points.Item3;
            _point4 = points.Item4;
            Intensity = intensity;
        }

        public ControlPoints4((Point3, Point3, Point3, Point3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4)))
        {
        }

        public ControlPoints4((ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) : this(v, EmitIntensity.Max)
        {
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints5
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

        private ControlPoints5((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _point1 = points.Item1;
            _point2 = points.Item2;
            _point3 = points.Item3;
            _point4 = points.Item4;
            _point5 = points.Item5;
            Intensity = intensity;
        }

        public ControlPoints5((Point3, Point3, Point3, Point3, Point3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5)))
        {
        }

        public ControlPoints5((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) : this(v, EmitIntensity.Max)
        {
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints6
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

        public ControlPoints6((Point3, Point3, Point3, Point3, Point3, Point3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6)))
        {
        }

        public ControlPoints6((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) : this(v, EmitIntensity.Max)
        {
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints7
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

        public ControlPoints7((Point3, Point3, Point3, Point3, Point3, Point3, Point3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6), new ControlPoint(v.Item7)))
        {
        }

        public ControlPoints7((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) : this(v, EmitIntensity.Max)
        {
        }
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints8
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

        public ControlPoints8((Point3, Point3, Point3, Point3, Point3, Point3, Point3, Point3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6), new ControlPoint(v.Item7), new ControlPoint(v.Item8)))
        {
        }

        public ControlPoints8((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) : this(v, EmitIntensity.Max)
        {
        }
    }
}