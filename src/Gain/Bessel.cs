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
        public EmitIntensity Intensity { get; init; } = EmitIntensity.Max;
        public Phase PhaseOffset { get; init; } = Phase.Zero;

        internal NativeMethods.BesselOption ToNative() => new()
        {
            intensity = Intensity.Inner,
            phase_offset = PhaseOffset.Inner
        };
    }

    public sealed class Bessel : IGain
    {
        public Bessel(Point3 pos, Vector3 dir, Angle theta, BesselOption option)
        {
            Pos = pos;
            Dir = dir;
            Theta = theta;
            Option = option;
        }

        public Point3 Pos { get; }

        public Vector3 Dir { get; }

        public Angle Theta { get; }

        public BesselOption Option { get; set; }

        GainPtr IGain.GainPtr(Geometry _) => NativeMethodsBase.AUTDGainBessel(Pos, Dir, Theta.ToNative(), Option.ToNative());
    }
}
