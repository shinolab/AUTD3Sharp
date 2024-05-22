﻿using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class BesselBeamTest
{
    public static void Test<T>(Controller<T> autd)
    {
        var config = Silencer.Default();
        autd.Send(config);

        var m = new Sine(150 * Hz); // AM sin 150 Hz

        var start = autd.Geometry.Center;
        var dir = Vector3d.UnitZ;
        var g = new Bessel(start, dir, 13.0 / 180.0 * Math.PI * rad); // BesselBeam from (x, y, 0), theta = 13 deg

        autd.Send((m, g));
    }
}
