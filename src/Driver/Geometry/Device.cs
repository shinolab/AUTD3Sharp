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

        public Quaternion Rotation => NativeMethodsBase.AUTDDeviceRotation(Ptr);

        public Vector3 XDirection => NativeMethodsBase.AUTDDeviceDirectionX(Ptr);
        public Vector3 YDirection => NativeMethodsBase.AUTDDeviceDirectionY(Ptr);
        public Vector3 AxialDirection => NativeMethodsBase.AUTDDeviceDirectionAxial(Ptr);

        public Vector3 Center => NativeMethodsBase.AUTDDeviceCenter(Ptr);

        public void Translate(Vector3 t)
        {
            NativeMethodsBase.AUTDDeviceTranslate(Ptr, t);
        }

        public void Rotate(Quaternion r)
        {
            NativeMethodsBase.AUTDDeviceRotate(Ptr, r);
        }

        public void Affine(Vector3 t, Quaternion r)
        {
            NativeMethodsBase.AUTDDeviceAffine(Ptr, t, r);
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
