using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    /// <summary>
    /// Modulation for modulating radiation pressure
    /// </summary>
    public sealed class RadiationPressure<TM> : Driver.Datagram.Modulation
    where TM : Driver.Datagram.Modulation
    {
        private readonly TM _m;

        public RadiationPressure(TM m)
        {
            _m = m;
        }

        internal override ModulationPtr ModulationPtr()
        {
            return NativeMethodsBase.AUTDModulationWithRadiationPressure(_m.ModulationPtr());
        }
    }

    public static class RadiationPressureModulationExtensions
    {
        public static RadiationPressure<TM> WithRadiationPressure<TM>(this TM s)
        where TM : Driver.Datagram.Modulation
        {
            return new RadiationPressure<TM>(s);
        }
    }
}
