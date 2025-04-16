using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    public sealed class RadiationPressure : Driver.Datagram.Modulation
    {
        public Driver.Datagram.Modulation Target;

        public RadiationPressure(Driver.Datagram.Modulation target) => Target = target;

        internal override ModulationPtr ModulationPtr() => NativeMethodsBase.AUTDModulationWithRadiationPressure(Target.ModulationPtr());
    }
}
