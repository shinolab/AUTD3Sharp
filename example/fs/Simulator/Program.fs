open AUTD3Sharp.Utils
open AUTD3Sharp
open AUTD3Sharp.Link
open Samples


let autd = (new ControllerBuilder())
                .AddDevice(new AUTD3(Vector3d.zero))
                .AddDevice(new AUTD3(Vector3d(AUTD3.DeviceWidth, 0, 0)))
                .OpenWithAsync(Simulator.Builder 8080us) |> Async.AwaitTask |> Async.RunSynchronously 

SampleRunner.Run autd
