open AUTD3Sharp.Utils
open AUTD3Sharp
open AUTD3Sharp.Link
open Samples
open System.Net

let addr = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
(new ControllerBuilder()).AddDevice(new AUTD3(Vector3d.Zero)).Open(RemoteSOEM.Builder addr) |> SampleRunner.Run
