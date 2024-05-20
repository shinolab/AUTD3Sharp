using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
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
