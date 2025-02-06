using AUTD3Sharp.Utils;
using System;
using System.Runtime.InteropServices;

namespace AUTD3Sharp
{
    public interface IControlPoints
    {
        public ControlPoint[] Points { get; set; }
        public EmitIntensity Intensity { get; set; }
        internal byte N { get; }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ControlPoint : IEquatable<ControlPoint>
    {
        public Point3 Point = Point3.Origin;
        private NativeMethods.Phase _phaseOffset = new();

        public Phase PhaseOffset
        {
            readonly get => new(_phaseOffset.Item1);
            set => _phaseOffset = value.Inner;
        }

        public ControlPoint() { }
        public ControlPoint(Point3 point)
        {
            Point = point;
            PhaseOffset = new Phase(0);
        }

        public static bool operator ==(ControlPoint left, ControlPoint right) => left.Equals(right);
        public static bool operator !=(ControlPoint left, ControlPoint right) => !left.Equals(right);
        public readonly bool Equals(ControlPoint other) => Point.Equals(other.Point) && _phaseOffset.Equals(other._phaseOffset);
        public readonly override bool Equals(object? obj) => obj is Drive other && Equals(other);
        public readonly override int GetHashCode() => HashCode.Combine(Point, _phaseOffset);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ControlPoints1 : IControlPoints, IEquatable<ControlPoints1>
    {
        public ControlPoint _points;
        private NativeMethods.EmitIntensity _intensity;

        public ControlPoint[] Points
        {
            readonly get => new[] { _points };
            set => _points = value[0];
        }

        public EmitIntensity Intensity
        {
            readonly get => new(_intensity.Item1);
            set => _intensity = value.Inner;
        }

        readonly byte IControlPoints.N => 1;

        public ControlPoints1()
        {
            _points = new ControlPoint();
            _intensity = EmitIntensity.Max.Inner;
        }
        public ControlPoints1(ControlPoint points, EmitIntensity intensity)
        {
            _points = points;
            _intensity = intensity.Inner;
        }
        public ControlPoints1(ControlPoint points) : this(points, EmitIntensity.Max) { }
        public ControlPoints1(Point3 v) : this(new ControlPoint(v)) { }

        public static bool operator ==(ControlPoints1 left, ControlPoints1 right) => left.Equals(right);
        public static bool operator !=(ControlPoints1 left, ControlPoints1 right) => !left.Equals(right);
        public readonly bool Equals(ControlPoints1 other) => Points.Equals(other.Points) && _intensity.Equals(other._intensity);
        public readonly override bool Equals(object? obj) => obj is Drive other && Equals(other);
        public readonly override int GetHashCode() => HashCode.Combine(Points, _intensity);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ControlPoints2 : IControlPoints, IEquatable<ControlPoints2>
    {
        public (ControlPoint, ControlPoint) _points;
        private NativeMethods.EmitIntensity _intensity = EmitIntensity.Max.Inner;

        public ControlPoint[] Points
        {
            readonly get => new[] { _points.Item1, _points.Item2 };
            set => _points = (value[0], value[1]);
        }

        public EmitIntensity Intensity
        {
            readonly get => new(_intensity.Item1);
            set => _intensity = value.Inner;
        }

        readonly byte IControlPoints.N => 2;

        public ControlPoints2()
        {
            _points = (new ControlPoint(), new ControlPoint());
            _intensity = EmitIntensity.Max.Inner;
        }
        public ControlPoints2((ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _points = points;
            _intensity = intensity.Inner;
        }
        public ControlPoints2((ControlPoint, ControlPoint) points) : this(points, EmitIntensity.Max) { }
        public ControlPoints2((Point3, Point3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2))) { }

        public static bool operator ==(ControlPoints2 left, ControlPoints2 right) => left.Equals(right);
        public static bool operator !=(ControlPoints2 left, ControlPoints2 right) => !left.Equals(right);
        public readonly bool Equals(ControlPoints2 other) => Points.Equals(other.Points) && _intensity.Equals(other._intensity);
        public readonly override bool Equals(object? obj) => obj is Drive other && Equals(other);
        public readonly override int GetHashCode() => HashCode.Combine(Points, _intensity);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ControlPoints3 : IControlPoints, IEquatable<ControlPoints3>
    {
        public (ControlPoint, ControlPoint, ControlPoint) _points;
        private NativeMethods.EmitIntensity _intensity = EmitIntensity.Max.Inner;

        public ControlPoint[] Points
        {
            readonly get => new[] { _points.Item1, _points.Item2, _points.Item3 };
            set => _points = (value[0], value[1], value[2]);
        }

        public EmitIntensity Intensity
        {
            readonly get => new(_intensity.Item1);
            set => _intensity = value.Inner;
        }

        readonly byte IControlPoints.N => 3;

        public ControlPoints3()
        {
            _points = (new ControlPoint(), new ControlPoint(), new ControlPoint());
            _intensity = EmitIntensity.Max.Inner;
        }
        public ControlPoints3((ControlPoint, ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _points = points;
            _intensity = intensity.Inner;
        }
        public ControlPoints3((ControlPoint, ControlPoint, ControlPoint) points) : this(points, EmitIntensity.Max) { }
        public ControlPoints3((Point3, Point3, Point3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3))) { }

        public static bool operator ==(ControlPoints3 left, ControlPoints3 right) => left.Equals(right);
        public static bool operator !=(ControlPoints3 left, ControlPoints3 right) => !left.Equals(right);
        public readonly bool Equals(ControlPoints3 other) => Points.Equals(other.Points) && _intensity.Equals(other._intensity);
        public readonly override bool Equals(object? obj) => obj is Drive other && Equals(other);
        public readonly override int GetHashCode() => HashCode.Combine(Points, _intensity);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ControlPoints4 : IControlPoints, IEquatable<ControlPoints4>
    {
        public (ControlPoint, ControlPoint, ControlPoint, ControlPoint) _points;
        private NativeMethods.EmitIntensity _intensity = EmitIntensity.Max.Inner;

        public ControlPoint[] Points
        {
            readonly get => new[] { _points.Item1, _points.Item2, _points.Item3, _points.Item4 };
            set => _points = (value[0], value[1], value[2], value[3]);
        }

        public EmitIntensity Intensity
        {
            readonly get => new(_intensity.Item1);
            set => _intensity = value.Inner;
        }

        readonly byte IControlPoints.N => 4;

        public ControlPoints4()
        {
            _points = (new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint());
            _intensity = EmitIntensity.Max.Inner;
        }
        public ControlPoints4((ControlPoint, ControlPoint, ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _points = points;
            _intensity = intensity.Inner;
        }
        public ControlPoints4((ControlPoint, ControlPoint, ControlPoint, ControlPoint) points) : this(points, EmitIntensity.Max) { }

        public ControlPoints4((Point3, Point3, Point3, Point3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4))) { }

        public static bool operator ==(ControlPoints4 left, ControlPoints4 right) => left.Equals(right);
        public static bool operator !=(ControlPoints4 left, ControlPoints4 right) => !left.Equals(right);
        public readonly bool Equals(ControlPoints4 other) => Points.Equals(other.Points) && _intensity.Equals(other._intensity);
        public readonly override bool Equals(object? obj) => obj is Drive other && Equals(other);
        public readonly override int GetHashCode() => HashCode.Combine(Points, _intensity);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ControlPoints5 : IControlPoints, IEquatable<ControlPoints5>
    {
        public (ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) _points;
        private NativeMethods.EmitIntensity _intensity = EmitIntensity.Max.Inner;
        public ControlPoint[] Points
        {
            readonly get => new[] { _points.Item1, _points.Item2, _points.Item3, _points.Item4, _points.Item5 };
            set => _points = (value[0], value[1], value[2], value[3], value[4]);
        }
        public EmitIntensity Intensity
        {
            readonly get => new(_intensity.Item1);
            set => _intensity = value.Inner;
        }
        readonly byte IControlPoints.N => 5;

        public ControlPoints5()
        {
            _points = (new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint());
            _intensity = EmitIntensity.Max.Inner;
        }
        public ControlPoints5((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points,
            EmitIntensity intensity)
        {
            _points = points;
            _intensity = intensity.Inner;
        }
        public ControlPoints5((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points) : this(points, EmitIntensity.Max) { }
        public ControlPoints5((Point3, Point3, Point3, Point3, Point3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5))) { }

        public static bool operator ==(ControlPoints5 left, ControlPoints5 right) => left.Equals(right);
        public static bool operator !=(ControlPoints5 left, ControlPoints5 right) => !left.Equals(right);
        public readonly bool Equals(ControlPoints5 other) => Points.Equals(other.Points) && _intensity.Equals(other._intensity);
        public readonly override bool Equals(object? obj) => obj is Drive other && Equals(other);
        public readonly override int GetHashCode() => HashCode.Combine(Points, _intensity);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ControlPoints6 : IControlPoints, IEquatable<ControlPoints6>
    {
        public (ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) _points;
        private NativeMethods.EmitIntensity _intensity = EmitIntensity.Max.Inner;
        public ControlPoint[] Points
        {
            readonly get => new[] { _points.Item1, _points.Item2, _points.Item3, _points.Item4, _points.Item5, _points.Item6 };
            set => _points = (value[0], value[1], value[2], value[3], value[4], value[5]);
        }
        public EmitIntensity Intensity
        {
            readonly get => new(_intensity.Item1);
            set => _intensity = value.Inner;
        }

        readonly byte IControlPoints.N => 6;

        public ControlPoints6()
        {
            _points = (new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint());
            _intensity = EmitIntensity.Max.Inner;
        }
        public ControlPoints6((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points,
            EmitIntensity intensity)
        {
            _points = points;
            _intensity = intensity.Inner;
        }
        public ControlPoints6((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points) : this(points, EmitIntensity.Max) { }
        public ControlPoints6((Point3, Point3, Point3, Point3, Point3, Point3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6))) { }

        public static bool operator ==(ControlPoints6 left, ControlPoints6 right) => left.Equals(right);
        public static bool operator !=(ControlPoints6 left, ControlPoints6 right) => !left.Equals(right);
        public readonly bool Equals(ControlPoints6 other) => Points.Equals(other.Points) && _intensity.Equals(other._intensity);
        public readonly override bool Equals(object? obj) => obj is Drive other && Equals(other);
        public readonly override int GetHashCode() => HashCode.Combine(Points, _intensity);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ControlPoints7 : IControlPoints, IEquatable<ControlPoints7>
    {
        public (ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) _points;
        private NativeMethods.EmitIntensity _intensity = EmitIntensity.Max.Inner;
        public ControlPoint[] Points
        {
            readonly get => new[] { _points.Item1, _points.Item2, _points.Item3, _points.Item4, _points.Item5, _points.Item6, _points.Item7 };
            set => _points = (value[0], value[1], value[2], value[3], value[4], value[5], value[6]);
        }
        public EmitIntensity Intensity
        {
            readonly get => new(_intensity.Item1);
            set => _intensity = value.Inner;
        }

        readonly byte IControlPoints.N => 7;

        public ControlPoints7()
        {
            _points = (new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint());
            _intensity = EmitIntensity.Max.Inner;
        }
        public ControlPoints7((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points, EmitIntensity intensity)
        {
            _points = points;
            _intensity = intensity.Inner;
        }
        public ControlPoints7((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points) : this(points, EmitIntensity.Max) { }
        public ControlPoints7((Point3, Point3, Point3, Point3, Point3, Point3, Point3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6), new ControlPoint(v.Item7))) { }

        public static bool operator ==(ControlPoints7 left, ControlPoints7 right) => left.Equals(right);
        public static bool operator !=(ControlPoints7 left, ControlPoints7 right) => !left.Equals(right);
        public readonly bool Equals(ControlPoints7 other) => Points.Equals(other.Points) && _intensity.Equals(other._intensity);
        public readonly override bool Equals(object? obj) => obj is Drive other && Equals(other);
        public readonly override int GetHashCode() => HashCode.Combine(Points, _intensity);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ControlPoints8 : IControlPoints, IEquatable<ControlPoints8>
    {
        public (ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) _points;
        private NativeMethods.EmitIntensity _intensity = EmitIntensity.Max.Inner;
        public ControlPoint[] Points
        {
            readonly get => new[] { _points.Item1, _points.Item2, _points.Item3, _points.Item4, _points.Item5, _points.Item6, _points.Item7, _points.Item8 };
            set => _points = (value[0], value[1], value[2], value[3], value[4], value[5], value[6], value[7]);
        }
        public EmitIntensity Intensity
        {
            readonly get => new(_intensity.Item1);
            set => _intensity = value.Inner;
        }

        readonly byte IControlPoints.N => 8;

        public ControlPoints8()
        {
            _points = (new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint());
            _intensity = EmitIntensity.Max.Inner;
        }
        public ControlPoints8((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points,
            EmitIntensity intensity)
        {
            _points = points;
            _intensity = intensity.Inner;
        }
        public ControlPoints8((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points) : this(points, EmitIntensity.Max) { }
        public ControlPoints8((Point3, Point3, Point3, Point3, Point3, Point3, Point3, Point3) v) : this((new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6), new ControlPoint(v.Item7), new ControlPoint(v.Item8))) { }

        public static bool operator ==(ControlPoints8 left, ControlPoints8 right) => left.Equals(right);
        public static bool operator !=(ControlPoints8 left, ControlPoints8 right) => !left.Equals(right);
        public readonly bool Equals(ControlPoints8 other) => Points.Equals(other.Points) && _intensity.Equals(other._intensity);
        public readonly override bool Equals(object? obj) => obj is Drive other && Equals(other);
        public readonly override int GetHashCode() => HashCode.Combine(Points, _intensity);
    }
}
