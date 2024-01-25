﻿
using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.Utils;

namespace Samples;
internal static class CustomTest
{

    private class Focus : Gain
    {
        private readonly Vector3d _point;

        public Focus(Vector3d point)
        {
            _point = point;
        }

        public override Dictionary<int, Drive[]> Calc(Geometry geometry)
        {
            return Transform(geometry, (dev, tr) =>
            {
                var tp = tr.Position;
                var dist = (tp - _point).L2Norm;
                var phase = Phase.FromRad(dist * tr.Wavenumber(dev.SoundSpeed));
                return new Drive { Phase = phase, Intensity = EmitIntensity.Max };
            });
        }
    }

    public static async Task Test<T>(Controller<T> autd)
    {
        var config = ConfigureSilencer.Default();
        await autd.SendAsync(config);

        var m = new Sine(150);
        var g = new Focus(autd.Geometry.Center + new Vector3d(0, 0, 150));

        await autd.SendAsync((m, g));
    }
}
