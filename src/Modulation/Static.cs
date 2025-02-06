using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    public sealed class Static : IModulation
    {
        public byte Intensity;

        public Static(byte intensity = 0xFF) => Intensity = intensity;

        ModulationPtr IModulation.ModulationPtr() => NativeMethodsBase.AUTDModulationStatic(Intensity);
    }
}
