using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;
using AUTD3Sharp.Derive;
using System.Collections.Generic;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    [Builder]
    public sealed partial class GS : Holo<GS>
    {
        private readonly Backend _backend;

        public GS(Backend backend, IEnumerable<(Vector3, Amplitude)> iter) : base(EmissionConstraint.Clamp(0x00, 0xFF), iter)
        {
            _backend = backend;
            Repeat = 100;
        }

        [Property] public uint Repeat { get; private set; }

        private GainPtr GainPtr(Geometry _)
        {
            return _backend.Gs(Foci, Amps, (uint)Amps.Length, Repeat, Constraint);
        }
    }
}
