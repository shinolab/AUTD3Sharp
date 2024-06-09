#if UNITY_2018_3_OR_NEWER
#define DIMENSION_M
#endif

using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    [Builder]
    public partial class AUTD3
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
        public const int NumTransInX = (int)NativeMethodsDriver.NUM_TRANS_IN_X;
        public const int NumTransInY = (int)NativeMethodsDriver.NUM_TRANS_IN_Y;
        public const float DeviceHeight = NativeMethodsDriver.DEVICE_HEIGHT_MM * Millimeter;
        public const float DeviceWidth = NativeMethodsDriver.DEVICE_WIDTH_MM * Millimeter;
        #endregion

        public Vector3 Pos { get; }
        [Property]
        public Quaternion Rot { get; private set; }

        public AUTD3(Vector3 pos)
        {
            Pos = pos;
            Rot = Quaternion.Identity;
        }
    }
}
