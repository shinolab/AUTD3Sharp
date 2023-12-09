/*
 * File: RadiationPressure.cs
 * Project: Modulation
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
using Math = UnityEngine.Mathf;
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
    /// Modulation for modulating radiation pressure
    /// </summary>
    public sealed class RadiationPressure : Internal.Modulation
    {
        private readonly Internal.Modulation _m;

        public RadiationPressure(Internal.Modulation m)
        {
            _m = m;
        }

        internal override ModulationPtr ModulationPtr()
        {
            return NativeMethodsBase.AUTDModulationWithRadiationPressure(_m.ModulationPtr());
        }
    }

    public static class RadiationPressureModulationExtensions
    {
        public static RadiationPressure WithRadiationPressure(this Internal.Modulation s)
        {
            return new RadiationPressure(s);
        }
    }
}
