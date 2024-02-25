open AUTD3Sharp.Utils
open AUTD3Sharp
open AUTD3Sharp.Link
open Samples

let autd = (new ControllerBuilder())
            .AddDevice(new AUTD3(Vector3d.zero))
            .OpenAsync(SOEM.Builder().WithErrHandler(
                fun (slave:int) (status:Status) (msg:string) -> 
                    match status with
                        | Status.Error -> eprintfn $"Error [{slave}]: {msg}"
                        | Status.Lost -> 
                            eprintfn $"Lost [{slave}]: {msg}"
                            // You can also wait for the link to recover, without exiting the process
                            System.Environment.Exit(-1)
                        | Status.StateChanged -> eprintfn $"StateChanged [{slave}]: {msg}"
            )) |> Async.AwaitTask |> Async.RunSynchronously 

SampleRunner.Run autd
