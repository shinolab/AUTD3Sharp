// File: Transtest.fs
// Project: Samples
// Created Date: 14/09/2023
// Author: Shun Suzuki
// -----
// Last Modified: 04/01/2024
// Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
// -----
// Copyright (c) 2023 Shun Suzuki. All rights reserved.
// 

namespace Samples

open AUTD3Sharp
open AUTD3Sharp.Gain
open AUTD3Sharp.Modulation

module TransTest =
    let Test<'T> (autd : Controller<'T>) = 
        (ConfigureSilencer.Default()) |> autd.SendAsync |> Async.AwaitTask |> Async.RunSynchronously |> ignore;

        let m = new Sine(150);
        let g = new TransducerTest(fun dev tr -> match (dev.Idx, tr.Idx) with
                                                    | (0, 0) -> System.Nullable(new Drive(new Phase(byte(0)), EmitIntensity.Max))
                                                    | (0, 248) -> System.Nullable(new Drive(new Phase(byte(0)), EmitIntensity.Max))
                                                    | _ ->  System.Nullable());
        (m, g) |> autd.SendAsync |> Async.AwaitTask |> Async.RunSynchronously |> ignore;
