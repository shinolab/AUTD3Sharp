using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    [Builder]
    public sealed partial class GS<TB> : Holo<GS<TB>>
            where TB : Backend
    {
        private readonly TB _backend;

        public GS(TB backend) : base(EmissionConstraint.DontCare)
        {
            _backend = backend;
            Repeat = 100;
        }

        [Property] public uint Repeat { get; private set; }

        private GainPtr GainPtr(Geometry _)
        {
            return _backend.Gs(Foci.ToArray(), Amps.ToArray(),
                (uint)Amps.Count, Repeat, Constraint);
        }
    }
}
