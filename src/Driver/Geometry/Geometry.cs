#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

#if UNITY_2018_3_OR_NEWER
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
#else
using Vector3 = AUTD3Sharp.Utils.Vector3d;
#endif

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Geometry
{
    public sealed class Geometry : IEnumerable<Device>
    {
        internal readonly GeometryPtr Ptr;
        private readonly List<Device> _devices;

        internal Geometry(GeometryPtr ptr)
        {
            Ptr = ptr;
            _devices = Enumerable.Range(0, (int)NativeMethodsBase.AUTDGeometryNumDevices(Ptr)).Select(x => new Device(x, NativeMethodsBase.AUTDDevice(Ptr, (uint)x))).ToList();
        }

        /// <summary>
        /// Number of devices
        /// </summary>
        public int NumDevices => _devices.Count;

        /// <summary>
        /// Number of transducers
        /// </summary>
        public int NumTransducers => _devices.Sum(d => d.NumTransducers);

        /// <summary>
        /// Get center position of all transducers
        /// </summary>
        public Vector3 Center
        {
            get
            {
                return _devices.Aggregate(Vector3.zero, (current, device) => current + device.Center) / _devices.Count;
            }
        }

        public Device this[int index] => _devices[index];
        public IEnumerator<Device> GetEnumerator() => _devices.GetEnumerator();
        [ExcludeFromCodeCoverage] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<Device> Devices() => _devices.Where(x => x.Enable);


        /// <summary>
        /// Set speed of sound of enabled devices
        /// </summary>
        /// <param name="c">Speed of sound</param>
        public void SetSoundSpeed(float_t c)
        {
            foreach (var dev in Devices()) dev.SoundSpeed = c;
        }

        /// <summary>
        /// Set speed of sound of enabled devices from temperature
        /// </summary>
        /// <param name="temp">Temperature in celsius</param>
        /// <param name="k">Ratio of specific heat</param>
        /// <param name="r">Gas constant</param>
        /// <param name="m">Molar mass</param>
        public void SetSoundSpeedFromTemp(float_t temp, float_t k = (float_t)1.4, float_t r = (float_t)8.31446261815324, float_t m = (float_t)28.9647e-3)
        {
            foreach (var dev in Devices()) dev.SetSoundSpeedFromTemp(temp, k, r, m);
        }
    }
}
