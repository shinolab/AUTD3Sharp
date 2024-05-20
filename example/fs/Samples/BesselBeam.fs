namespace Samples

open System
open AUTD3Sharp
open AUTD3Sharp.Gain
open AUTD3Sharp.Modulation
open AUTD3Sharp.Utils
open type AUTD3Sharp.Units

module BesselBeamTest =
    let Test<'T> (autd : Controller<'T>) = 
        (Silencer.Default()) |> autd.Send;

        let m = new Sine (150u * Hz);

        let start = autd.Geometry.Center;
        let dir = Vector3d.UnitZ;

        let g = new Bessel(start, dir, 13.0 / 180.0 * Math.PI);
        (m, g) |> autd.Send;
