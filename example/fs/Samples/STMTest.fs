namespace Samples

open System
open AUTD3Sharp
open AUTD3Sharp.Gain
open AUTD3Sharp.Modulation
open AUTD3Sharp.Utils
open System.Threading.Tasks
open type AUTD3Sharp.Units

module STMTest =
    let GainSTMTest<'T> (autd : Controller<'T>) = 
        (Silencer.Disable()) |> autd.Send;

        (new Static()) |> autd.Send;
        
        let center = autd.Geometry.Center + Vector3d(0, 0, 150);
        let stm = 
            [0..199]
            |> List.map (fun i -> (2.0 * Math.PI * (float)i / 200.0))
            |> List.map (fun theta -> (center + 30.0 * Vector3d(cos(theta), sin(theta), 0.0)))
            |> List.map (fun p -> (new Focus(p)))
            |> List.fold (fun (acc: GainSTM) v -> acc.AddGain v) (GainSTM.FromFreq(1.0 * Hz))

        autd.Send stm;

    let FocusSTMTest<'T> (autd : Controller<'T>) = 
        (Silencer.Disable()) |> autd.Send;

        (new Static()) |> autd.Send;
        
        let center = autd.Geometry.Center + Vector3d(0, 0, 150);
        let stm = 
            [0..199]
            |> List.map (fun i -> (2.0 * Math.PI * (float)i / 200.0))
            |> List.map (fun theta -> (center + 30.0 * Vector3d(cos(theta), sin(theta), 0.0)))
            |> List.fold (fun (acc: FocusSTM) v -> acc.AddFocus v) (FocusSTM.FromFreq(1.0 * Hz))

        autd.Send stm;
