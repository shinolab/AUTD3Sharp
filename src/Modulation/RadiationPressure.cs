using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    public sealed class RadiationPressure : IModulation
    {
        public IModulation Target;

        public RadiationPressure(IModulation target) => Target = target;

        ModulationPtr IModulation.ModulationPtr() => NativeMethodsBase.AUTDModulationWithRadiationPressure(Target.ModulationPtr());
    }
}
