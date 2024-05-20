namespace Samples

open AUTD3Sharp
open AUTD3Sharp.Gain
open AUTD3Sharp.Modulation
open AUTD3Sharp.Utils
open type AUTD3Sharp.Units

module FocusTest =
    let Test<'T> (autd : Controller<'T>) = 
        (Silencer.Default()) |> autd.Send;

        let m = new Sine (150u * Hz);
        let g = new Focus(autd.Geometry.Center + Vector3d(0, 0, 150));
        (m, g) |> autd.Send;
