namespace Samples

open AUTD3Sharp
open AUTD3Sharp.Gain
open AUTD3Sharp.Modulation
open type AUTD3Sharp.Units

module TransTest =
    let Test<'T> (autd : Controller<'T>) = 
        (Silencer.Default()) |> autd.Send;

        let m = new Sine(150u * Hz);
        let g = new Custom(fun dev tr -> match (dev.Idx, tr.Idx) with
                                                    | (0, 0) -> new Drive(new Phase(byte(0)), EmitIntensity.Max)
                                                    | (0, 248) -> new Drive(new Phase(byte(0)), EmitIntensity.Max)
                                                    | _ ->  Drive.Null);
        (m, g) |> autd.Send;
