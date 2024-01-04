/*
 * File: Static.cs
 * Project: Modulation
 * Created Date: 13/09/2023
 * Author: Shun Suzuki
 * -----
 * Last Modified: 04/01/2024
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

namespace AUTD3Sharp.Modulation
{
    /// <summary>
    /// Without modulation
    /// </summary>
    public sealed class Static : Internal.Modulation
    {
        private EmitIntensity? _intensity;

        public Static()
        {
            _intensity = null;
        }

        private Static(EmitIntensity intensity)
        {
            _intensity = intensity;
        }

        /// <summary>
        /// Set amplitude
        /// </summary>
        /// <param name="intensity">Emission intensity</param>
        /// <returns></returns>
        public static Static WithIntensity(byte intensity)
        {
            return new Static(new EmitIntensity(intensity));
        }

        /// <summary>
        /// Set amplitude
        /// </summary>
        /// <param name="intensity">Emission intensity</param>
        /// <returns></returns>
        public static Static WithIntensity(EmitIntensity intensity)
        {
            return new Static(intensity);
        }

        internal override ModulationPtr ModulationPtr()
        {
            if (_intensity != null)
                return NativeMethodsBase.AUTDModulationStaticWithIntensity(_intensity.Value.Value);
            else return NativeMethodsBase.AUTDModulationStatic();
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
