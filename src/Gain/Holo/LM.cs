using System;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Gain.Holo
{
    /// <summary>
    /// Gain to produce multiple foci with Levenberg-Marquardt algorithm
    /// </summary>
    /// <remarks>
    /// <para>K.Levenberg, “A method for the solution of certain non-linear problems in least squares,” Quarterly of applied mathematics, vol.2, no.2, pp.164–168, 1944.</para> 
    /// <para> D.W.Marquardt, “An algorithm for least-squares estimation of non-linear parameters,” Journal of the society for Industrial and AppliedMathematics, vol.11, no.2, pp.431–441, 1963.</para> 
    /// <para>K.Madsen, H.Nielsen, and O.Tingleff, “Methods for non-linear least squares problems (2nd ed.),” 2004.</para> 
    /// </remarks>
    /// <typeparam name="TB">Backend</typeparam>
    [Gain]
    [Builder]
    public sealed partial class LM<TB> : Holo<LM<TB>>
        where TB : Backend
    {
        private readonly TB _backend;
        private double[] _initial;

        public LM(TB backend) : base(EmissionConstraint.DontCare())
        {
            _backend = backend;
            Eps1 = 1e-8;
            Eps2 = 1e-8;
            Tau = 1e-3;
            KMax = 5;
            _initial = Array.Empty<double>();
        }

        public LM<TB> WithInitial(double[] value)
        {
            _initial = value;
            return this;
        }

        [Property]
        public double Eps1 { get; private set; }

        [Property]
        public double Eps2 { get; private set; }

        [Property]
        public double Tau { get; private set; }

        [Property]
        public uint KMax { get; private set; }

        public ReadOnlySpan<double> Initial => new ReadOnlySpan<double>(_initial);

        private GainPtr GainPtr(Geometry _) =>
            _backend.Lm(Foci.ToArray(), Amps.ToArray(),
                (ulong)Amps.Count, Eps1, Eps2, Tau, KMax, _initial, Constraint.Ptr);
    }
}
