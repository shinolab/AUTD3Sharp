using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#endif

namespace AUTD3Sharp.Gain
{
    public class PlaneOption
    {
        public EmitIntensity Intensity { get; init; } = EmitIntensity.Max;
        public Phase PhaseOffset { get; init; } = Phase.Zero;

        internal NativeMethods.PlaneOption ToNative() => new()
        {
            intensity = Intensity.Inner,
            phase_offset = PhaseOffset.Inner
        };
    }

    public sealed class Plane : IGain
    {
        public Vector3 Dir;
        public PlaneOption Option;

        public Plane(Vector3 dir, PlaneOption option)
        {
            Dir = dir;
            Option = option;
        }

        GainPtr IGain.GainPtr(Geometry _) => NativeMethodsBase.AUTDGainPlane(Dir, Option.ToNative());
    }
}
