using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public sealed class Device : IEnumerable<Transducer>
    {
        internal readonly DevicePtr Ptr;
        private readonly List<Transducer> _transducers;

        internal Device(ushort idx, DevicePtr ptr)
        {
            Idx = idx;
            Ptr = ptr;
            _transducers = Enumerable.Range(0, (int)NativeMethodsBase.AUTDDeviceNumTransducers(Ptr)).Select(i => new Transducer((byte)i, Ptr)).ToList();
        }

        public int Idx { get; }

        public int NumTransducers => _transducers.Count;

        public float SoundSpeed
        {
            get => NativeMethodsBase.AUTDDeviceGetSoundSpeed(Ptr);
            set => NativeMethodsBase.AUTDDeviceSetSoundSpeed(Ptr, value);
        }

        public bool Enable
        {
            get => NativeMethodsBase.AUTDDeviceEnableGet(Ptr);
            set => NativeMethodsBase.AUTDDeviceEnableSet(Ptr, value);
        }

        public Quaternion Rotation
        {
            get
            {
                unsafe
                {
                    var rot = stackalloc float[4];
                    NativeMethodsBase.AUTDDeviceRotation(Ptr, rot);
                    return new Quaternion(rot[0], rot[1], rot[2], rot[3]);
                }
            }
        }

        public Vector3 XDirection
        {
            get
            {
                unsafe
                {
                    var dir = stackalloc float[3];
                    NativeMethodsBase.AUTDDeviceDirectionX(Ptr, dir);
                    return new Vector3(dir[0], dir[1], dir[2]);
                }
            }
        }

        public Vector3 YDirection
        {
            get
            {
                unsafe
                {
                    var dir = stackalloc float[3];
                    NativeMethodsBase.AUTDDeviceDirectionY(Ptr, dir);
                    return new Vector3(dir[0], dir[1], dir[2]);
                }
            }
        }

        public Vector3 AxialDirection
        {
            get
            {
                unsafe
                {
                    var dir = stackalloc float[3];
                    NativeMethodsBase.AUTDDeviceDirectionAxial(Ptr, dir);
                    return new Vector3(dir[0], dir[1], dir[2]);
                }
            }
        }
        public Vector3 Center
        {
            get
            {
                unsafe
                {
                    var center = stackalloc float[3];
                    NativeMethodsBase.AUTDDeviceCenter(Ptr, center);
                    return new Vector3(center[0], center[1], center[2]);
                }
            }
        }

        public void Translate(Vector3 t)
        {
            NativeMethodsBase.AUTDDeviceTranslate(Ptr, t.X, t.Y, t.Z);
        }

        public void Rotate(Quaternion r)
        {
            NativeMethodsBase.AUTDDeviceRotate(Ptr, r.W, r.X, r.Y, r.Z);
        }

        public void Affine(Vector3 t, Quaternion r)
        {
            NativeMethodsBase.AUTDDeviceAffine(Ptr, t.X, t.Y, t.Z, r.W, r.X, r.Y, r.Z);
        }

        public void SetSoundSpeedFromTemp(float temp, float k = 1.4f, float r = 8.31446261815324f, float m = 28.9647e-3f)
        {
            NativeMethodsBase.AUTDDeviceSetSoundSpeedFromTemp(Ptr, temp, k, r, m);
        }

        public float Wavelength => NativeMethodsBase.AUTDDeviceWavelength(Ptr);
        public float Wavenumber => NativeMethodsBase.AUTDDeviceWavenumber(Ptr);

        public Transducer this[int index] => _transducers[index];

        public IEnumerator<Transducer> GetEnumerator() => _transducers.GetEnumerator();
        [ExcludeFromCodeCoverage] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
