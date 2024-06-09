using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    [Builder]
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

    public struct N1 : IFociTuple
    {
        public ControlPoint[] Points { get; private set; }

        public readonly byte Value => 1;

        public static implicit operator N1(ControlPoint v) => new N1 { Points = new[] { v } };
        public static implicit operator N1(Vector3 v) => new N1 { Points = new[] { new ControlPoint(v) } };
    }

    public struct N2 : IFociTuple
    {
        public ControlPoint[] Points { get; private set; }

        public readonly byte Value => 2;

        public static implicit operator N2((ControlPoint, ControlPoint) v) => new N2 { Points = new[] { v.Item1, v.Item2 } };
        public static implicit operator N2((Vector3, Vector3) v) => new N2 { Points = new[] { new ControlPoint(v.Item1), new ControlPoint(v.Item2) } };
    }

    public struct N3 : IFociTuple
    {
        public ControlPoint[] Points { get; private set; }

        public readonly byte Value => 3;

        public static implicit operator N3((ControlPoint, ControlPoint, ControlPoint) v) => new N3 { Points = new[] { v.Item1, v.Item2, v.Item3 } };
        public static implicit operator N3((Vector3, Vector3, Vector3) v) => new N3 { Points = new[] { new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3) } };
    }

    public struct N4 : IFociTuple
    {
        public ControlPoint[] Points { get; private set; }

        public readonly byte Value => 4;

        public static implicit operator N4((ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) => new N4 { Points = new[] { v.Item1, v.Item2, v.Item3, v.Item4 } };
        public static implicit operator N4((Vector3, Vector3, Vector3, Vector3) v) => new N4 { Points = new[] { new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4) } };
    }

    public struct N5 : IFociTuple
    {
        public ControlPoint[] Points
        {
            get;
            private set;
        }

        public readonly byte Value => 5;

        public static implicit operator N5((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) => new N5 { Points = new[] { v.Item1, v.Item2, v.Item3, v.Item4, v.Item5 } };
        public static implicit operator N5((Vector3, Vector3, Vector3, Vector3, Vector3) v) => new N5 { Points = new[] { new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5) } };
    }

    public struct N6 : IFociTuple
    {
        public ControlPoint[] Points
        {
            get;
            private set;
        }

        public readonly byte Value => 6;

        public static implicit operator N6((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) => new N6 { Points = new[] { v.Item1, v.Item2, v.Item3, v.Item4, v.Item5, v.Item6 } };
        public static implicit operator N6((Vector3, Vector3, Vector3, Vector3, Vector3, Vector3) v) => new N6 { Points = new[] { new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6) } };
    }

    public struct N7 : IFociTuple
    {
        public ControlPoint[] Points
        {
            get;
            private set;
        }
        public readonly byte Value => 7;

        public static implicit operator N7((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) => new N7 { Points = new[] { v.Item1, v.Item2, v.Item3, v.Item4, v.Item5, v.Item6, v.Item7 } };
        public static implicit operator N7((Vector3, Vector3, Vector3, Vector3, Vector3, Vector3, Vector3) v) => new N7 { Points = new[] { new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6), new ControlPoint(v.Item7) } };

    }

    public struct N8 : IFociTuple
    {
        public ControlPoint[] Points
        {
            get;
            private set;
        }
        public readonly byte Value => 8;

        public static implicit operator N8((ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint, ControlPoint) v) => new N8 { Points = new[] { v.Item1, v.Item2, v.Item3, v.Item4, v.Item5, v.Item6, v.Item7, v.Item8 } };
        public static implicit operator N8((Vector3, Vector3, Vector3, Vector3, Vector3, Vector3, Vector3, Vector3) v) => new N8 { Points = new[] { new ControlPoint(v.Item1), new ControlPoint(v.Item2), new ControlPoint(v.Item3), new ControlPoint(v.Item4), new ControlPoint(v.Item5), new ControlPoint(v.Item6), new ControlPoint(v.Item7), new ControlPoint(v.Item8) } };
    }

    [Builder]
    public partial struct ControlPoints<T>
        where T : IFociTuple
    {
        private readonly T _points;
        public readonly ControlPoint[] Points => _points.Points;

        [Property]
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