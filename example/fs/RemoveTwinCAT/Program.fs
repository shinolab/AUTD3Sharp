open AUTD3Sharp.Utils
open AUTD3Sharp
open AUTD3Sharp.Link
open Samples

let serverAmsNetId = "your TwinCATAUTDServer AMS net id (e.g. 172.16.99.2.1.1)"
 
(new ControllerBuilder()).AddDevice(new AUTD3(Vector3d.zero)).OpenWithAsync(RemoteTwinCAT.Builder serverAmsNetId) |> Async.AwaitTask |> Async.RunSynchronously |> SampleRunner.Run
