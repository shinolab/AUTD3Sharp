using System;

namespace AUTD3Sharp
{
    public readonly struct Angle
    {
        public static Angle FromDegree(float degree) => new Angle(degree / 180 * MathF.PI);
        public static Angle FromRadian(float radian) => new Angle(radian);

        public float Radian { get; }

        public class UnitRadian
        {
            internal UnitRadian() { }
            public static Angle operator *(float a, UnitRadian _) => FromRadian(a);
        }
        public class UnitDegree
        {
            internal UnitDegree() { }
            public static Angle operator *(float a, UnitDegree _) => FromDegree(a);
        }

        private Angle(float value)
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
