﻿/*
 * File: BesselBeam.cs
 * Project: Test
 * Created Date: 30/04/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 14/11/2023
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2022-2023 Shun Suzuki. All rights reserved.
 * 
 */


using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;

namespace Samples;

internal static class BesselBeamTest
{
    public static async Task Test<T>(Controller<T> autd)
    {
        var config = new Silencer();
        await autd.SendAsync(config);

        var m = new Sine(150); // AM sin 150 Hz

        var start = autd.Geometry.Center;
        var dir = Vector3d.UnitZ;
        var g = new Bessel(start, dir, 13.0 / 180.0 * AUTD3.Pi); // BesselBeam from (x, y, 0), theta = 13 deg

        await autd.SendAsync((m, g));
    }
}
