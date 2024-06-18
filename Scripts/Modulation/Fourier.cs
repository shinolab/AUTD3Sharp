using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class Fourier
    {
        private readonly Sine[] _components;

        public Fourier(IEnumerable<Sine> iter)
        {
            _components = iter as Sine[] ?? iter.ToArray();
        }

        private ModulationPtr ModulationPtr(Geometry geometry)
        {
            var components = _components.Select(m => ((Driver.Datagram.Modulation.IModulation)m).ModulationPtr(geometry)).ToArray();
            unsafe
            {
                fixed (ModulationPtr* p = &components[0])
                    return _components[0].Mode.FourierPtr(p, (uint)components.Length, LoopBehavior);
            }
        }
    }
}
