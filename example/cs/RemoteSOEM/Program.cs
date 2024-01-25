using System.Net;
using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;

using var autd = await new ControllerBuilder()
    .AddDevice(new AUTD3(Vector3d.zero))
    .OpenWithAsync(RemoteSOEM.Builder(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080)));

await SampleRunner.Run(autd);
