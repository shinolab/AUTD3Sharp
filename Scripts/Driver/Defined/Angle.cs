using System;

namespace AUTD3Sharp
{
    public readonly struct Angle
    {
        public static Angle FromDegree(double degree) => new Angle(degree / 180 * Math.PI);
        public static Angle FromRadian(double radian) => new Angle(radian);

        public double Radian { get; }

        public class UnitRadian
        {
            internal UnitRadian() { }
            public static Angle operator *(double a, UnitRadian _) => FromRadian(a);
        }
        public class UnitDegree
        {
            internal UnitDegree() { }
            public static Angle operator *(double a, UnitDegree _) => FromDegree(a);
        }

        private Angle(double value)
        {
            Radian = value;
        }
    }

    public static partial class Units
    {
#pragma warning disable IDE1006
        public static Angle.UnitRadian rad { get; } = new Angle.UnitRadian();
        public static Angle.UnitDegree deg { get; } = new Angle.UnitDegree();
#pragma warning restore IDE1006
    }
}
