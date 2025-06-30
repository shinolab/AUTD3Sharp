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

        public Quaternion Rotation() => NativeMethodsBase.AUTDDeviceRotation(Ptr);
        public Vector3 XDirection() => NativeMethodsBase.AUTDDeviceDirectionX(Ptr);
        public Vector3 YDirection() => NativeMethodsBase.AUTDDeviceDirectionY(Ptr);
        public Vector3 AxialDirection() => NativeMethodsBase.AUTDDeviceDirectionAxial(Ptr);

        public Point3 Center() => NativeMethodsBase.AUTDDeviceCenter(Ptr);


        public Transducer this[int index] => _transducers[index];

        public IEnumerator<Transducer> GetEnumerator() => _transducers.GetEnumerator();
        [ExcludeFromCodeCoverage] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
