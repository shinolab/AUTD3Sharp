using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    /// <summary>
    /// Modulation for modulating radiation pressure
    /// </summary>
    [Modulation(ConfigNoChange = true, NoTransform = true, NoRadiationPressure = true)]
    public sealed partial class RadiationPressure<TM> : Modulation
    where TM : Modulation
    {
        private readonly TM _m;

        public RadiationPressure(TM m)
        {
            _m = m;
        }

        internal override ModulationPtr ModulationPtr()
        {
            return NativeMethodsBase.AUTDModulationWithRadiationPressure(_m.ModulationPtr(), LoopBehavior.Internal);
        }
    }
}
