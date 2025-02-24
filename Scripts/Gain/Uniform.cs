using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#endif

namespace AUTD3Sharp.Gain
{
    public class Uniform : IGain
    {
        public EmitIntensity Intensity { get; init; }
        public Phase Phase { get; init; }

        public Uniform(EmitIntensity intensity, Phase phase)
        {
            Intensity = intensity;
            Phase = phase;
        }

        GainPtr IGain.GainPtr(Geometry _) => NativeMethodsBase.AUTDGainUniform(Intensity.Inner, Phase.Inner);
    }
}
