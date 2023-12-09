/*
 * File: LM.cs
 * Project: Holo
 * Created Date: 13/09/2023
 * Author: Shun Suzuki
 * -----
 * Last Modified: 29/11/2023
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2023 Shun Suzuki. All rights reserved.
 * 
 */

#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using AUTD3Sharp.NativeMethods;

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

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
    public sealed class LM<TB> : Holo<LM<TB>>
        where TB : Backend
    {
        private readonly TB _backend;
        private float_t? _eps1;
        private float_t? _eps2;
        private float_t? _tau;
        private uint? _kMax;
        private float_t[]? _initial;

        private IAmplitudeConstraint? _constraint;

        public LM(TB backend)
        {
            _backend = backend;
        }

        /// <summary>
        /// Parameter. See the paper for details.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public LM<TB> WithEps1(float_t value)
        {
            _eps1 = value;
            return this;
        }

        /// <summary>
        /// Parameter. See the paper for details.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public LM<TB> WithEps2(float_t value)
        {
            _eps2 = value;
            return this;
        }

        /// <summary>
        /// Parameter. See the paper for details.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public LM<TB> WithTau(float_t value)
        {
            _tau = value;
            return this;
        }

        /// <summary>
        /// Parameter. See the paper for details.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public LM<TB> WithKMax(uint value)
        {
            _kMax = value;
            return this;
        }

        /// <summary>
        /// Set amplitude constraint
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public LM<TB> WithConstraint(IAmplitudeConstraint constraint)
        {
            _constraint = constraint;
            return this;
        }

        /// <summary>
        /// Parameter. See the paper for details.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public LM<TB> WithInitial(float_t[] value)
        {
            _initial = value;
            return this;
        }

        internal override GainPtr GainPtr(Geometry geometry)
        {
            var ptr = _backend.Lm(Foci.ToArray(), Amps.ToArray(),
                (ulong)Amps.Count);
            if (_eps1.HasValue) ptr = _backend.LmWithEps1(ptr, _eps1.Value);
            if (_eps2.HasValue) ptr = _backend.LmWithEps2(ptr, _eps2.Value);
            if (_tau.HasValue) ptr = _backend.LmWithTau(ptr, _tau.Value);
            if (_kMax.HasValue) ptr = _backend.LmWithKMax(ptr, _kMax.Value);
            if (_initial != null)
                ptr = _backend.LmWithInitial(ptr, _initial, (ulong)_initial.Length);
            if (_constraint != null) ptr = _backend.LmWithConstraint(ptr, _constraint.Ptr());
            return ptr;
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
