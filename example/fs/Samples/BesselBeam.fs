namespace Samples

open AUTD3Sharp
open AUTD3Sharp.Gain
open AUTD3Sharp.Modulation
open AUTD3Sharp.Utils

module BesselBeamTest =
    let Test<'T> (autd : Controller<'T>) = 
        (ConfigureSilencer.Default()) |> autd.SendAsync |> Async.AwaitTask |> Async.RunSynchronously |> ignore;

        let m = new Sine 150;

        let start = autd.Geometry.Center;
        let dir = Vector3d.UnitZ;

        let g = new Bessel(start, dir, 13.0 / 180.0 * AUTD3.Pi);
        (m, g) |> autd.SendAsync |> Async.AwaitTask |> Async.RunSynchronously |> ignore;
