using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    [Builder]
    public sealed partial class GSPAT<TB> : Holo<GSPAT<TB>>
            where TB : Backend
    {
        private readonly TB _backend;

        public GSPAT(TB backend) : base(EmissionConstraint.DontCare)
        {
            _backend = backend;
            Repeat = 100;
        }

        [Property]

        public uint Repeat { get; private set; }

        private GainPtr GainPtr(Geometry _)
        {
            return _backend.Gspat(Foci.ToArray(), Amps.ToArray(), (ulong)Amps.Count, Repeat, Constraint);
        }
    }
}
