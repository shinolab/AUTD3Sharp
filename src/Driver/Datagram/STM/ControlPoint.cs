using AUTD3Sharp.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    namespace NativeMethods
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct ControlPoint
        {
            internal Point3 point;
            internal Phase phase_offset;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ControlPoints1
        {
            internal ControlPoint points;
            internal EmitIntensity intensity;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ControlPoints2
        {
            internal (ControlPoint, ControlPoint) points;
            internal EmitIntensity intensity;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ControlPoints3
        {
            internal (ControlPoint, ControlPoint, ControlPoint) points;
            internal EmitIntensity intensity;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ControlPoints4
        {
            internal (ControlPoint, ControlPoint, ControlPoint, ControlPoint) points;
            internal EmitIntensity intensity;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ControlPoints5
        {
            internal (ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points;
            internal EmitIntensity intensity;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ControlPoints6
        {
            internal (ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points;
            internal EmitIntensity intensity;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ControlPoints7
        {
            internal (ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points;
            internal EmitIntensity intensity;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ControlPoints8
        {
            internal (ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) points;
            internal EmitIntensity intensity;
        }
    }

    public class ControlPoint : IEquatable<ControlPoint>
    {
        public Point3 Point { get; init; }
        public Phase PhaseOffset { get; init; }

        public ControlPoint()
        {
            Point = Point3.Origin;
            PhaseOffset = Phase.Zero;
        }

        public ControlPoint(Point3 point)
        {
            Point = point;
            PhaseOffset = new Phase(0);
        }

        public static bool operator ==(ControlPoint left, ControlPoint right) => left.Equals(right);
        public static bool operator !=(ControlPoint left, ControlPoint right) => !left.Equals(right);
        public bool Equals(ControlPoint? other) => other is not null && Point.Equals(other.Point) && PhaseOffset.Equals(other.PhaseOffset);
        public override bool Equals(object? obj) => obj is ControlPoint other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => HashCode.Combine(Point, PhaseOffset);

        internal NativeMethods.ControlPoint ToNative() => new()
        {
            point = Point,
            phase_offset = PhaseOffset.Inner
        };
    }

    public class ControlPoints : IEquatable<ControlPoints>
    {
        public ControlPoint[] Points { get; init; }
        public EmitIntensity Intensity { get; init; }

        public ControlPoints(IEnumerable<ControlPoint> points, EmitIntensity intensity)
        {
            Points = points as ControlPoint[] ?? points.ToArray();
            Intensity = intensity;
        }
        public ControlPoints(IEnumerable<ControlPoint> points) : this(points, EmitIntensity.Max) { }
        public ControlPoints(IEnumerable<Point3> points) : this(points.Select(v => new ControlPoint(v))) { }

        public static bool operator ==(ControlPoints left, ControlPoints right) => left.Equals(right);
        public static bool operator !=(ControlPoints left, ControlPoints right) => !left.Equals(right);
        public bool Equals(ControlPoints? other) => other is not null && Points.SequenceEqual(other.Points) && Intensity.Equals(other.Intensity);
        public override bool Equals(object? obj) => obj is ControlPoints other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => HashCode.Combine(Points, Intensity);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
