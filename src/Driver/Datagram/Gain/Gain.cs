using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Gain
{
    [ComVisible(false)]
    public interface IGain
    {
        public GainPtr GainPtr(Geometry geometry);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Device GetDevice(ushort devIdx, GeometryPtr ptr) => new(devIdx, ptr);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Transducer GetTransducer(byte trIdx, DevicePtr ptr) => new(trIdx, ptr);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GeometryPtr GetGeometryPtr(Geometry geometry) => geometry.Ptr;
    }
}
