using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public sealed class Geometry : IEnumerable<Device>
    {
        internal readonly GeometryPtr Ptr;
        private readonly List<Device> _devices;

        internal Geometry(GeometryPtr ptr)
        {
            Ptr = ptr;
            _devices = Enumerable.Range(0, (int)NativeMethodsBase.AUTDGeometryNumDevices(Ptr)).Select(x => new Device((ushort)x, Ptr)).ToList();
        }

        public int NumDevices => _devices.Count;
        public int NumTransducers => _devices.Sum(d => d.NumTransducers);

        public Vector3 Center
        {
            get
            {
                return _devices.Aggregate(Vector3.Zero, (current, device) => current + device.Center) / _devices.Count;
            }
        }

        public Device this[int index] => _devices[index];
        public IEnumerator<Device> GetEnumerator() => _devices.GetEnumerator();
        [ExcludeFromCodeCoverage] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<Device> Devices() => _devices.Where(x => x.Enable);

        public void SetSoundSpeed(float c)
        {
            foreach (var dev in Devices()) dev.SoundSpeed = c;
        }

        public void SetSoundSpeedFromTemp(float temp, float k = 1.4f, float r = 8.31446261815324f, float m = 28.9647e-3f)
        {
            foreach (var dev in Devices()) dev.SetSoundSpeedFromTemp(temp, k, r, m);
        }
    }
}
