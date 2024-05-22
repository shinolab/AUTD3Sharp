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
        public const double Meter = 1;
#else
        public const double Meter = 1000;
#endif
        public const double Millimeter = Meter / 1000;
        public const int NumTransInUnit = (int)NativeMethodsDriver.NUM_TRANS_IN_UNIT;
        public const double TransSpacingMm = NativeMethodsDriver.TRANS_SPACING_MM;
        public const double TransSpacing = NativeMethodsDriver.TRANS_SPACING_MM * Millimeter;
        public const int NumTransInX = (int)NativeMethodsDriver.NUM_TRANS_IN_X;
        public const int NumTransInY = (int)NativeMethodsDriver.NUM_TRANS_IN_Y;
        public const double DeviceHeight = NativeMethodsDriver.DEVICE_HEIGHT_MM * Millimeter;
        public const double DeviceWidth = NativeMethodsDriver.DEVICE_WIDTH_MM * Millimeter;
        #endregion

        internal Vector3d Pos;
        internal Quaterniond? Rot;

        public AUTD3(Vector3d pos)
        {
            Pos = pos;
            Rot = null;
        }

        public AUTD3 WithRotation(Quaterniond rot)
        {
            Rot = rot;
            return this;
        }
    }
}
