#if UNITY_2018_3_OR_NEWER
#define DIMENSION_M
#endif

using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public class AUTD3
    {
        #region const
#if DIMENSION_M
        public const float Meter = 1;
#else
        public const float Meter = 1000;
#endif
        public const float Millimeter = Meter / 1000;
        public const int NumTransInUnit = (int)NativeMethodsDriver.NUM_TRANS_IN_UNIT;
        public const float TransSpacingMm = NativeMethodsDriver.TRANS_SPACING_MM;
        public const float TransSpacing = NativeMethodsDriver.TRANS_SPACING_MM * Millimeter;
        public const int NumTransX = (int)NativeMethodsDriver.NUM_TRANS_IN_X;
        public const int NumTransY = (int)NativeMethodsDriver.NUM_TRANS_IN_Y;
        public const float DeviceHeight = NativeMethodsDriver.DEVICE_HEIGHT_MM * Millimeter;
        public const float DeviceWidth = NativeMethodsDriver.DEVICE_WIDTH_MM * Millimeter;
        #endregion

        public Point3 Pos = Point3.Origin;
        public Quaternion Rot = Quaternion.Identity;

        public AUTD3() { }
        public AUTD3(Point3 pos, Quaternion rot)
        {
            Pos = pos;
            Rot = rot;
        }
    }
}
