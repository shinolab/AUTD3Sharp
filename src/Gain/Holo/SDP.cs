using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    [Builder]
    public sealed partial class SDP<TB> : Holo<SDP<TB>>
           where TB : Backend
    {
        private readonly TB _backend;

        public SDP(TB backend) : base(EmissionConstraint.DontCare)
        {
            _backend = backend;
            Alpha = 1e-3f;
            Lambda = 0.9f;
            Repeat = 100;
        }

        [Property]
        public float Alpha { get; private set; }

        [Property]
        public float Lambda { get; private set; }

        [Property]
        public uint Repeat { get; private set; }

        private GainPtr GainPtr(Geometry _) =>
            _backend.Sdp(Foci.ToArray(), Amps.ToArray(),
                (uint)Amps.Count, Alpha, Repeat, Lambda, Constraint);
    }
}
