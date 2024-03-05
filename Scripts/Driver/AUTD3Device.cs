#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System;
using AUTD3Sharp.NativeMethods;

#if UNITY_2018_3_OR_NEWER
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using Math = UnityEngine.Mathf;
#else
using Vector3 = AUTD3Sharp.Utils.Vector3d;
using Quaternion = AUTD3Sharp.Utils.Quaterniond;
#endif

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

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
        public const float_t Meter = 1;
#else
        public const float_t Meter = 1000;
#endif

        /// <summary>
        /// Millimeter
        /// </summary>
        public const float_t Millimeter = Meter / 1000;

        /// <summary>
        /// Mathematical constant pi
        /// </summary>
        public const float_t Pi = Math.PI;

        /// <summary>
        /// Number of transducer in an AUTD3 device
        /// </summary>
        public const int NumTransInUnit = (int)NativeMethodsDef.NUM_TRANS_IN_UNIT;

        /// <summary>
        /// Spacing between transducers in mm
        /// </summary>
        public const float_t TransSpacingMm = NativeMethodsDef.TRANS_SPACING_MM;

        /// <summary>
        /// Spacing between transducers in m
        /// </summary>
        public const float_t TransSpacing = NativeMethodsDef.TRANS_SPACING_MM * Millimeter;

        /// <summary>
        /// Number of transducer in x-axis of AUTD3 device
        /// </summary>
        public const int NumTransInX = (int)NativeMethodsDef.NUM_TRANS_IN_X;

        /// <summary>
        /// Number of transducer in y-axis of AUTD3 device
        /// </summary>
        public const int NumTransInY = (int)NativeMethodsDef.NUM_TRANS_IN_Y;

        /// <summary>
        /// FPGA clock frequency
        /// </summary>
        public const uint FPGAClkFreq = NativeMethodsDef.FPGA_CLK_FREQ;

        /// <summary>
        /// Device height including substrate
        /// </summary>
        public const float_t DeviceHeight = NativeMethodsDef.DEVICE_HEIGHT_MM * Millimeter;

        /// <summary>
        /// Device width including substrate
        /// </summary>
        public const float_t DeviceWidth = NativeMethodsDef.DEVICE_WIDTH_MM * Millimeter;

        #endregion

        internal Vector3 Pos;
        internal Quaternion? Rot;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pos">Global position</param>
        public AUTD3(Vector3 pos)
        {
            Pos = pos;
            Rot = null;
        }

        public AUTD3 WithRotation(Quaternion rot)
        {
            Rot = rot;
            return this;
        }
    }
}
