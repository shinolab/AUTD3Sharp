
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Gain.Holo
{
    /// <summary>
    /// Gain to produce multiple foci with GS algorithm
    /// </summary>
    /// <remarks>Asier Marzo and Bruce W Drinkwater. Holographic acoustic tweezers.Proceedings of theNational Academy of Sciences, 116(1):84–89, 2019.</remarks>
    /// <typeparam name="TB">Backend</typeparam>
    [Gain]
    [Builder]
    public sealed partial class GS<TB> : Holo<GS<TB>>
            where TB : Backend
    {
        private readonly TB _backend;

        public GS(TB backend) : base(EmissionConstraint.DontCare())
        {
            _backend = backend;
            Repeat = 100;
        }

        [Property] public uint Repeat { get; private set; }

        private GainPtr GainPtr(Geometry _)
        {
            return _backend.Gs(Foci.ToArray(), Amps.ToArray(),
                (ulong)Amps.Count, Repeat, Constraint.Ptr);
        }
    }
}
