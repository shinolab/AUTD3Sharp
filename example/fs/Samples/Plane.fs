namespace Samples

open AUTD3Sharp
open AUTD3Sharp.Gain
open AUTD3Sharp.Modulation
open AUTD3Sharp.Utils
open type AUTD3Sharp.Units

module PlaneTest =
    let Test<'T> (autd : Controller<'T>) = 
        (Silencer.Default()) |> autd.Send;

        let m = new Sine(150u * Hz);
        let g = new Plane(new Vector3d(0,0,1));
        (m, g) |> autd.Send;
