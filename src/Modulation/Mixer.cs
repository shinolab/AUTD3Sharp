using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class Mixer
    {
        private readonly Sine[] _components;

        public Mixer(IEnumerable<Sine> iter)
        {
            _components = iter as Sine[] ?? iter.ToArray();
        }

        private ModulationPtr ModulationPtr()
        {
            var components = _components.Select(m => ((Driver.Datagram.Modulation.IModulation)m).ModulationPtr()).ToArray();
            unsafe
            {
                fixed (ModulationPtr* p = &components[0])
                    return _components[0].Mode.MixerPtr(p, (uint)components.Length, LoopBehavior);
            }
        }
    }
}
