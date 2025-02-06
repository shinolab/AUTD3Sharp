using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#endif

namespace AUTD3Sharp.Gain
{
    public readonly struct FocusOption
    {
        public EmitIntensity Intensity { get; init; } = EmitIntensity.Max;
        public Phase PhaseOffset { get; init; } = Phase.Zero;

        public FocusOption() { }

        internal NativeMethods.FocusOption ToNative() => new()
        {
            intensity = Intensity.Inner,
            phase_offset = PhaseOffset.Inner
        };
    }

    public sealed class Focus : IGain
    {
        public Focus(Point3 pos, FocusOption option)
        {
            Pos = pos;
            Option = option;
        }

        public Point3 Pos;
        public FocusOption Option;

        GainPtr IGain.GainPtr(Geometry _) => NativeMethodsBase.AUTDGainFocus(Pos, Option.ToNative());
    }
}
