﻿open AUTD3Sharp.Utils
open AUTD3Sharp
open AUTD3Sharp.Link
open Samples

(new ControllerBuilder()).AddDevice(new AUTD3(Vector3d.zero)).OpenWithAsync(TwinCAT.Builder()) |> Async.AwaitTask |> Async.RunSynchronously |> SampleRunner.Run
