namespace Samples

open AUTD3Sharp
open AUTD3Sharp.Gain
open AUTD3Sharp.Modulation.AudioFile
open AUTD3Sharp.Utils
open System.IO
open type AUTD3Sharp.Units

module WavTest =
    let Test<'T> (autd : Controller<'T>) = 
        (Silencer.Default()) |> autd.Send;
        
        let m = new Wav("sin150.wav");
        let g = new Focus(autd.Geometry.Center + Vector3d(0, 0, 150));
        (m, g) |> autd.Send;
       