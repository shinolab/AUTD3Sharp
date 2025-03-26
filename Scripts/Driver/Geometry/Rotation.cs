using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public static class EulerAngles
    {
        public static Quaternion Xyz(Angle x, Angle y, Angle z) => NativeMethodsBase.AUTDRotationFromEulerXYZ(x.Radian, y.Radian, z.Radian);
        public static Quaternion Zyz(Angle z1, Angle y, Angle z2) => NativeMethodsBase.AUTDRotationFromEulerZYZ(z1.Radian, y.Radian, z2.Radian);
    }
}
