namespace Samples

open AUTD3Sharp
open AUTD3Sharp.Gain
open AUTD3Sharp.Modulation.AudioFile
open AUTD3Sharp.Utils
open System.IO

module WavTest =
    let Test<'T> (autd : Controller<'T>) = 
        (ConfigureSilencer.Default()) |> autd.SendAsync  |> Async.AwaitTask |> Async.RunSynchronously |> ignore;
        
        let m = new Wav("sin150.wav");
        let g = new Focus(autd.Geometry.Center + Vector3d(0, 0, 150));
        (m, g) |> autd.SendAsync  |> Async.AwaitTask |> Async.RunSynchronously |> ignore;
       