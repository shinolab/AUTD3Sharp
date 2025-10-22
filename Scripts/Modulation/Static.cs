using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    public sealed class Static : Driver.Datagram.Modulation
    {
        public byte Intensity;

        public Static(byte intensity = 0xFF) => Intensity = intensity;

        internal override ModulationPtr ModulationPtr() => NativeMethodsBase.AUTDModulationStatic(Intensity);
    }
}
