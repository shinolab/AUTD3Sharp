﻿// File: SampleRunner.fs
// Project: Samples
// Created Date: 03/02/2023
// Author: Shun Suzuki
// -----
// Last Modified: 02/12/2023
// Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
// -----
// Copyright (c) 2023 Shun Suzuki. All rights reserved.
// 

namespace Samples

open AUTD3Sharp

module SampleRunner =
    let Run<'T> (autd : Controller<'T>) = 
        let examples = [
                (FocusTest.Test, "Single focus test");
                (BesselBeamTest.Test, "Bessel beam test");
                (PlaneTest.Test, "Plane wave test");
                (WavTest.Test, "Wav modulation test");
                (STMTest.FocusSTMTest, "FocusSTM test");
                (STMTest.GainSTMTest, "GainSTM test");
                (GainHoloTest.Test, "Multiple foci test");
                (CustomTest.Test, "Custom Gain & Modulation test");
                (FlagTest.Test, "Flag test");
                (TransTest.Test, "TransducerTest test");
                (GroupByTransducerTest.Test, "Group (by Transducer) test")];

        let examples = 
            if autd.Geometry.NumDevices >= 2 then examples @ [(GroupByDeviceTest.Test, "Group (by Device) test")] else examples;


        printfn "======== AUTD3 firmware information ========"
        autd.FirmwareInfoListAsync() |> Async.AwaitTask |> Async.RunSynchronously |> Seq.iter (fun firm -> printfn $"{firm}")
        printfn "============================================"

        let rec run_example () =
            let print_example i =
                let _, name = examples[i] in
                printfn $"[{i}]: {name}"
            [0..examples.Length-1] |> List.iter print_example
            printfn "[Others]: finish"
            
            printf "Choose number: "
            let input = stdin.ReadLine()
            match System.Int32.TryParse input with
                | true,i -> 
                    let f, _ = examples[i] in
                    f(autd)

                    printfn "press any key to finish..."
                    System.Console.ReadKey true |> ignore;

                    printfn "finish."

                    (new Stop()) |> autd.SendAsync  |> Async.AwaitTask|> ignore;

                    run_example()
                | _ -> ()

        run_example()

        autd.CloseAsync() |> Async.AwaitTask |> Async.RunSynchronously |> ignore;
        autd.Dispose() |> ignore;
