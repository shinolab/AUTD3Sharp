using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class RadiationPressure<TM>
    where TM : IModulation
    {
        private readonly TM _m;

        public RadiationPressure(TM m)
        {
            _m = m;
        }

        private ModulationPtr ModulationPtr()
        {
            return NativeMethodsBase.AUTDModulationWithRadiationPressure(_m.ModulationPtr(), LoopBehavior);
        }
    }
}
