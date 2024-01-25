#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using AUTD3Sharp.NativeMethods;

#if UNITY_2018_3_OR_NEWER
using Quaternion = UnityEngine.Quaternion;
#else
using Quaternion = AUTD3Sharp.Utils.Quaterniond;
#endif

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

namespace AUTD3Sharp
{
    public readonly struct Angle
    {
        public static Angle FromDegree(float_t degree) => new Angle(degree / 180 * AUTD3.Pi);
        public static Angle FromRadian(float_t radian) => new Angle(radian);

        public float_t Radian { get; }

        public class UnitRadian
        {
            internal UnitRadian() { }
            public static Angle operator *(float_t a, UnitRadian _) => FromRadian(a);
        }
        public class UnitDegree
        {
            internal UnitDegree() { }
            public static Angle operator *(float_t a, UnitDegree _) => FromDegree(a);
        }

        public static class Units
        {
            public static UnitRadian Rad { get; } = new UnitRadian();
            public static UnitDegree Deg { get; } = new UnitDegree();
        }

        private Angle(float_t value)
        {
            Radian = value;
        }
    }

    public static class EulerAngles
    {
        public static Quaternion FromZyz(Angle z1, Angle y, Angle z2)
        {
            unsafe
            {
                var rot = stackalloc float_t[4];
                NativeMethodsBase.AUTDRotationFromEulerZYZ(z1.Radian, y.Radian, z2.Radian, rot);
                return new Quaternion(rot[1], rot[2], rot[3], rot[0]);
            }
        }
    }
}
