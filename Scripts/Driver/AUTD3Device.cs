#if UNITY_2018_3_OR_NEWER
#define DIMENSION_M
#endif

using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    /// <summary>
    /// AUTD3 device
    /// </summary>
    public class AUTD3
    {
        #region const

        /// <summary>
        /// Meter
        /// </summary>
#if DIMENSION_M
        public const double Meter = 1;
#else
        public const double Meter = 1000;
#endif

        /// <summary>
        /// Millimeter
        /// </summary>
        public const double Millimeter = Meter / 1000;

        /// <summary>
        /// Number of transducer in an AUTD3 device
        /// </summary>
        public const int NumTransInUnit = (int)NativeMethodsDef.NUM_TRANS_IN_UNIT;

        /// <summary>
        /// Spacing between transducers in mm
        /// </summary>
        public const double TransSpacingMm = NativeMethodsDef.TRANS_SPACING_MM;

        /// <summary>
        /// Spacing between transducers in m
        /// </summary>
        public const double TransSpacing = NativeMethodsDef.TRANS_SPACING_MM * Millimeter;

        /// <summary>
        /// Number of transducer in X-axis of AUTD3 device
        /// </summary>
        public const int NumTransInX = (int)NativeMethodsDef.NUM_TRANS_IN_X;

        /// <summary>
        /// Number of transducer in Y-axis of AUTD3 device
        /// </summary>
        public const int NumTransInY = (int)NativeMethodsDef.NUM_TRANS_IN_Y;

        /// <summary>
        /// FPGA clock frequency
        /// </summary>
        public const uint FPGAClkFreq = NativeMethodsDef.FPGA_CLK_FREQ;

        /// <summary>
        /// Device height including substrate
        /// </summary>
        public const double DeviceHeight = NativeMethodsDef.DEVICE_HEIGHT_MM * Millimeter;

        /// <summary>
        /// Device width including substrate
        /// </summary>
        public const double DeviceWidth = NativeMethodsDef.DEVICE_WIDTH_MM * Millimeter;

        #endregion

        internal Vector3d Pos;
        internal Quaterniond? Rot;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pos">Global position</param>
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
