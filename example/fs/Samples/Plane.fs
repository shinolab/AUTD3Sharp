namespace Samples

open AUTD3Sharp
open AUTD3Sharp.Gain
open AUTD3Sharp.Modulation
open AUTD3Sharp.Utils

module PlaneTest =
    let Test<'T> (autd : Controller<'T>) = 
        (Silencer.Default()) |> autd.SendAsync |> Async.AwaitTask |> Async.RunSynchronously |> ignore;

        let m = new Sine(150 * Hz);
        let g = new Plane(new Vector3d(0,0,1));
        (m, g) |> autd.SendAsync |> Async.AwaitTask |> Async.RunSynchronously |> ignore;
