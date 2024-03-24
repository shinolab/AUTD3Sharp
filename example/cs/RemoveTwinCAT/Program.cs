using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;

const string serverAmsNetId = "your TwinCATAUTDServer AMS net id (e.g. 172.16.99.2.1.1)";

using var autd = await new ControllerBuilder()
    .AddDevice(new AUTD3(Vector3d.Zero))
    .OpenAsync(RemoteTwinCAT.Builder(serverAmsNetId));


await SampleRunner.Run(autd);
