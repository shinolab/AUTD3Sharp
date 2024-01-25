open AUTD3Sharp.Utils
open AUTD3Sharp
open AUTD3Sharp.Link
open Samples

let onLost (msg:string): unit = 
    printfn $"Unrecoverable error occurred: {msg}"
    System.Environment.Exit(-1)

let autd = (new ControllerBuilder())
            .AddDevice(new AUTD3(Vector3d.zero))
            .OpenWithAsync(SOEM.Builder().WithOnLost(new SOEM.OnErrCallbackDelegate(onLost))) |> Async.AwaitTask |> Async.RunSynchronously 

SampleRunner.Run autd
