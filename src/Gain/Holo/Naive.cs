using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;
using System.Collections.Generic;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    public sealed partial class Naive : Holo<Naive>
    {
        private readonly Backend _backend;

        public Naive(Backend backend, IEnumerable<(Vector3, Amplitude)> iter) : base(EmissionConstraint.Clamp(0x00, 0xFF), iter)
        {
            _backend = backend;
        }

        private GainPtr GainPtr(Geometry _) =>
            _backend.Naive(Foci, Amps, (uint)Amps.Length, Constraint);
    }
}
