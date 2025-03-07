using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public sealed class Device : IEnumerable<Transducer>
    {
        internal readonly GeometryPtr GeoPtr;
        internal readonly DevicePtr Ptr;
        private readonly List<Transducer> _transducers;
        private readonly ushort _idx;

        internal Device(ushort idx, GeometryPtr ptr)
        {
            _idx = idx;
            GeoPtr = ptr;
            Ptr = NativeMethodsBase.AUTDDevice(ptr, idx);
            _transducers = Enumerable.Range(0, (int)NativeMethodsBase.AUTDDeviceNumTransducers(Ptr)).Select(i => new Transducer((byte)i, idx, Ptr)).ToList();
        }

        public int Idx() => _idx;

        public int NumTransducers() => _transducers.Count;

        public float SoundSpeed
        {
            get => NativeMethodsBase.AUTDDeviceGetSoundSpeed(Ptr);
            set => NativeMethodsBase.AUTDDeviceSetSoundSpeed(GeoPtr, _idx, value);
        }

        public bool Enable
        {
            get => NativeMethodsBase.AUTDDeviceEnableGet(Ptr);
            set => NativeMethodsBase.AUTDDeviceEnableSet(GeoPtr, _idx, value);
        }

        public Quaternion Rotation() => NativeMethodsBase.AUTDDeviceRotation(Ptr);
        public Vector3 XDirection() => NativeMethodsBase.AUTDDeviceDirectionX(Ptr);
        public Vector3 YDirection() => NativeMethodsBase.AUTDDeviceDirectionY(Ptr);
        public Vector3 AxialDirection() => NativeMethodsBase.AUTDDeviceDirectionAxial(Ptr);

        public Point3 Center() => NativeMethodsBase.AUTDDeviceCenter(Ptr);

        public void SetSoundSpeedFromTemp(float temp, float k = 1.4f, float r = 8.31446261815324f, float m = 28.9647e-3f) => NativeMethodsBase.AUTDDeviceSetSoundSpeedFromTemp(GeoPtr, _idx, temp, k, r, m);

        public float Wavelength() => NativeMethodsBase.AUTDDeviceWavelength(Ptr);
        public float Wavenumber() => NativeMethodsBase.AUTDDeviceWavenumber(Ptr);

        public Transducer this[int index] => _transducers[index];

        public IEnumerator<Transducer> GetEnumerator() => _transducers.GetEnumerator();
        [ExcludeFromCodeCoverage] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
