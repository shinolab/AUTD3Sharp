using System;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

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

        public static class Units
        {
            public static UnitRadian Rad { get; } = new UnitRadian();
            public static UnitDegree Deg { get; } = new UnitDegree();
        }

        private Angle(double value)
        {
            Radian = value;
        }
    }

    public static class EulerAngles
    {
        public static Quaterniond FromZyz(Angle z1, Angle y, Angle z2)
        {
            unsafe
            {
                var rot = stackalloc double[4];
                NativeMethodsBase.AUTDRotationFromEulerZYZ(z1.Radian, y.Radian, z2.Radian, rot);
                return new Quaterniond(rot[0], rot[1], rot[2], rot[3]);
            }
        }
    }
}
