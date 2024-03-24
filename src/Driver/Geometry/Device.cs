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

        internal Device(int idx, DevicePtr ptr)
        {
            Idx = idx;
            Ptr = ptr;
            _transducers = Enumerable.Range(0, (int)NativeMethodsBase.AUTDDeviceNumTransducers(Ptr)).Select(i => new Transducer(i, Ptr)).ToList();
        }

        /// <summary>
        /// Index of the transducer
        /// </summary>
        public int Idx { get; }

        /// <summary>
        /// Number of transducers
        /// </summary>
        public int NumTransducers => _transducers.Count;

        /// <summary>
        /// Speed of sound
        /// </summary>
        public double SoundSpeed
        {
            get => NativeMethodsBase.AUTDDeviceGetSoundSpeed(Ptr);
            set => NativeMethodsBase.AUTDDeviceSetSoundSpeed(Ptr, value);
        }

        /// <summary>
        /// Attenuation coefficient
        /// </summary>
        public double Attenuation
        {
            get => NativeMethodsBase.AUTDDeviceGetAttenuation(Ptr);
            set => NativeMethodsBase.AUTDDeviceSetAttenuation(Ptr, value);
        }

        public bool Enable
        {
            get => NativeMethodsBase.AUTDDeviceEnableGet(Ptr);
            set => NativeMethodsBase.AUTDDeviceEnableSet(Ptr, value);
        }

        /// <summary>
        /// Get center position of all transducers
        /// </summary>
        public Vector3d Center
        {
            get
            {
                unsafe
                {
                    var center = stackalloc double[3];
                    NativeMethodsBase.AUTDDeviceCenter(Ptr, center);
                    return new Vector3d(center[0], center[1], center[2]);
                }
            }
        }

        public void Translate(Vector3d t)
        {
            NativeMethodsBase.AUTDDeviceTranslate(Ptr, t.X, t.Y, t.Z);
        }

        public void Rotate(Quaterniond r)
        {
            NativeMethodsBase.AUTDDeviceRotate(Ptr, r.W, r.X, r.Y, r.Z);
        }

        public void Affine(Vector3d t, Quaterniond r)
        {
            NativeMethodsBase.AUTDDeviceAffine(Ptr, t.X, t.Y, t.Z, r.W, r.X, r.Y, r.Z);
        }

        /// <summary>
        /// Set the sound speed from temperature
        /// </summary>
        /// <param name="temp">Temperature in celsius</param>
        /// <param name="k">Ratio of specific heat</param>
        /// <param name="r">Gas constant</param>
        /// <param name="m">Molar mass</param>
        public void SetSoundSpeedFromTemp(double temp, double k = 1.4, double r = 8.31446261815324, double m = 28.9647e-3)
        {
            NativeMethodsBase.AUTDDeviceSetSoundSpeedFromTemp(Ptr, temp, k, r, m);
        }

        public Transducer this[int index] => _transducers[index];

        public IEnumerator<Transducer> GetEnumerator() => _transducers.GetEnumerator();
        [ExcludeFromCodeCoverage] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
