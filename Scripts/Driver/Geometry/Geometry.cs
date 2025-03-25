using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public class Geometry : IEnumerable<Device>
    {
        internal readonly GeometryPtr GeometryPtr;
        private List<Device> _devices;

        internal Geometry(GeometryPtr geometryPtr)
        {
            GeometryPtr = geometryPtr;
            _devices = Enumerable.Range(0, (int)NativeMethodsBase.AUTDGeometryNumDevices(GeometryPtr)).Select(x => new Device((ushort)x, GeometryPtr)).ToList();
        }

        public int NumDevices() => (int)NativeMethodsBase.AUTDGeometryNumDevices(GeometryPtr);
        public int NumTransducers() => (int)NativeMethodsBase.AUTDGeometryNumTransducers(GeometryPtr);

        public Point3 Center() => NativeMethodsBase.AUTDGeometrCenter(GeometryPtr);

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

        public void Reconfigure(Func<Device, AUTD3> f)
        {
            var devices = _devices.Select(d => f(d)).ToArray();
            var pos = devices.Select(d => d.Pos).ToArray();
            var rot = devices.Select(d => d.Rot).ToArray();
            unsafe
            {
                fixed (Point3* pPos = &pos[0])
                fixed (Quaternion* pRot = &rot[0])
                {
                    NativeMethodsBase.AUTDGeometryReconfigure(GeometryPtr, pPos, pRot);
                    _devices = Enumerable.Range(0, (int)NativeMethodsBase.AUTDGeometryNumDevices(GeometryPtr)).Select(x => new Device((ushort)x, GeometryPtr)).ToList();
                }
            }
        }
    }
}
