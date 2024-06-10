using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;
using System.Runtime.InteropServices;

namespace AUTD3Sharp
{
    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoint
    {
        public Vector3 Point { get; }
        [Property]
        public Phase Offset { get; private set; }

        public ControlPoint(Vector3 point)
        {
            Point = point;
            Offset = new Phase(0);
        }
    }

    public interface IFociTuple
    {
        ControlPoint[] Points { get; }
        byte Value { get; }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct N1 : IFociTuple
    {
        private ControlPoint _point;

        public ControlPoint[] Points => new[] { _point };

        public readonly byte Value => 1;

        public static implicit operator N1(ControlPoint v) => new N1 { _point = v };
        public static implicit operator N1(Vector3 v) => new N1 { _point = new ControlPoint(v) };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct N2 : IFociTuple
    {
        private (ControlPoint, ControlPoint) _points;
        public ControlPoint[] Points => new[] { _points.Item1, _points.Item2 };

        public readonly byte Value => 2;

        public static implicit operator N2((ControlPoint, ControlPoint) v) => new N2 { _points = v };
        public static implicit operator N2((Vector3, Vector3) v) => new N2 { _points = (new ControlPoint(v.Item1), new ControlPoint(v.Item2)) };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct N3 : IFociTuple
    {
        private (ControlPoint, ControlPoint, ControlPoint) _points;
        public ControlPoint[] Points => new[] { _points.Item1, _points.Item2, _points.Item3 };

        public readonly byte Value => 3;

        public static implicit operator N3((ControlPoint, ControlPoint, ControlPoint) v) => new N3 { _points = v };
        public static implicit operator N3((Vector3, Vector3, Vector3) v) => new N3 { _points = (new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3)) };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct N4 : IFociTuple
    {
        private (ControlPoint, ControlPoint, ControlPoint, ControlPoint) _points;
        public ControlPoint[] Points => new[] { _points.Item1, _points.Item2, _points.Item3, _points.Item4 };

        public readonly byte Value => 4;

        public static implicit operator N4((ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) => new N4 { _points = v };
        public static implicit operator N4((Vector3, Vector3, Vector3, Vector3) v) => new N4 { _points = (new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4)) };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct N5 : IFociTuple
    {
        private (ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) _points;
        public ControlPoint[] Points => new[] { _points.Item1, _points.Item2, _points.Item3, _points.Item4, _points.Item5 };

        public readonly byte Value => 5;

        public static implicit operator N5((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) => new N5 { _points = v };
        public static implicit operator N5((Vector3, Vector3, Vector3, Vector3, Vector3) v) => new N5 { _points = (new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5)) };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct N6 : IFociTuple
    {
        private (ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) _points;
        public ControlPoint[] Points => new[] { _points.Item1, _points.Item2, _points.Item3, _points.Item4, _points.Item5, _points.Item6 };

        public readonly byte Value => 6;

        public static implicit operator N6((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) => new N6 { _points = v };
        public static implicit operator N6((Vector3, Vector3, Vector3, Vector3, Vector3, Vector3) v) => new N6 { _points = (new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6)) };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct N7 : IFociTuple
    {
        private (ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) _points;
        public ControlPoint[] Points => new[] { _points.Item1, _points.Item2, _points.Item3, _points.Item4, _points.Item5, _points.Item6, _points.Item7 };

        public readonly byte Value => 7;

        public static implicit operator N7((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) => new N7 { _points = v };
        public static implicit operator N7((Vector3, Vector3, Vector3, Vector3, Vector3, Vector3, Vector3) v) => new N7 { _points = (new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6), new ControlPoint(v.Item7)) };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct N8 : IFociTuple
    {
        private (ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) _points;
        public ControlPoint[] Points => new[] { _points.Item1, _points.Item2, _points.Item3, _points.Item4, _points.Item5, _points.Item6, _points.Item7, _points.Item8 };

        public readonly byte Value => 8;

        public static implicit operator N8((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) => new N8 { _points = v };
        public static implicit operator N8((Vector3, Vector3, Vector3, Vector3, Vector3, Vector3, Vector3, Vector3) v) => new N8 { _points = (new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6), new ControlPoint(v.Item7), new ControlPoint(v.Item8)) };
    }

    [Builder]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ControlPoints<T>
        where T : IFociTuple
    {
        private readonly T _points;
        public readonly ControlPoint[] Points => _points.Points;

        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        private ControlPoints(T points, EmitIntensity intensity)
        {
            _points = points;
            Intensity = intensity;
        }

        public ControlPoints(T points) : this(points, EmitIntensity.Max)
        {
        }
    }
}