using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#endif

namespace AUTD3Sharp.Gain
{
    public class BesselOption
    {
        public Intensity Intensity { get; init; } = Intensity.Max;
        public Phase PhaseOffset { get; init; } = Phase.Zero;

        internal NativeMethods.BesselOption ToNative() => new()
        {
            intensity = Intensity.Inner,
            phase_offset = PhaseOffset.Inner
        };
    }

    public sealed class Bessel : IGain
    {
        public Bessel(Point3 apex, Vector3 dir, Angle theta, BesselOption option)
        {
            Apex = apex;
            Dir = dir;
            Theta = theta;
            Option = option;
        }

        public Point3 Apex { get; }

        public Vector3 Dir { get; }

        public Angle Theta { get; }

        public BesselOption Option { get; set; }

        GainPtr IGain.GainPtr(Geometry _) => NativeMethodsBase.AUTDGainBessel(Apex, Dir, Theta.ToNative(), Option.ToNative());
    }
}
