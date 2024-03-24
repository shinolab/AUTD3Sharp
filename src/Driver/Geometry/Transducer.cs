using System;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public sealed class Transducer
    {
        internal readonly TransducerPtr Ptr;

        internal Transducer(int trIdx, DevicePtr ptr)
        {
            Idx = trIdx;
            Ptr = NativeMethodsBase.AUTDTransducer(ptr, (uint)trIdx);
        }

        /// <summary>
        /// Index of the transducer
        /// </summary>
        public int Idx { get; }

        /// <summary>
        /// Position of the transducer
        /// </summary>
        public Vector3d Position
        {
            get
            {
                unsafe
                {
                    var pos = stackalloc double[3];
                    NativeMethodsBase.AUTDTransducerPosition(Ptr, pos);
                    return new Vector3d(pos[0], pos[1], pos[2]);
                }
            }
        }

        /// <summary>
        /// Rotation of the transducer
        /// </summary>
        public Quaterniond Rotation
        {
            get
            {
                unsafe
                {
                    var rot = stackalloc double[4];
                    NativeMethodsBase.AUTDTransducerRotation(Ptr, rot);
                    return new Quaterniond(rot[0], rot[1], rot[2], rot[3]);
                }
            }
        }

        /// <summary>
        /// X-direction of the transducer
        /// </summary>
        public Vector3d XDirection
        {
            get
            {
                unsafe
                {
                    var dir = stackalloc double[3];
                    NativeMethodsBase.AUTDTransducerDirectionX(Ptr, dir);
                    return new Vector3d(dir[0], dir[1], dir[2]);
                }
            }
        }

        /// <summary>
        /// Y-direction of the transducer
        /// </summary>
        public Vector3d YDirection
        {
            get
            {
                unsafe
                {
                    var dir = stackalloc double[3];
                    NativeMethodsBase.AUTDTransducerDirectionY(Ptr, dir);
                    return new Vector3d(dir[0], dir[1], dir[2]);
                }
            }
        }

        /// <summary>
        /// Z-direction of the transducer
        /// </summary>
        public Vector3d ZDirection
        {
            get
            {
                unsafe
                {
                    var dir = stackalloc double[3];
                    NativeMethodsBase.AUTDTransducerDirectionZ(Ptr, dir);
                    return new Vector3d(dir[0], dir[1], dir[2]);
                }
            }
        }

        /// <summary>
        /// Wavelength of the transducer
        /// </summary>
        /// <param name="soundSpeed">Speed of sound</param>
        /// <returns></returns>
        public double Wavelength(double soundSpeed) => NativeMethodsBase.AUTDTransducerWavelength(Ptr, soundSpeed);

        /// <summary>
        /// Wavenumber of the transducer
        /// </summary>
        /// <param name="soundSpeed">Speed of sound</param>
        /// <returns></returns>
        public double Wavenumber(double soundSpeed) => 2 * Math.PI / Wavelength(soundSpeed);
    }
}
