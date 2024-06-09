using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;

const string serverAmsNetId = "your TwinCATAUTDServer AMS net id (e.g. 172.16.99.2.1.1)";

using var autd = new ControllerBuilder([new AUTD3(Vector3.Zero)])
    .Open(RemoteTwinCAT.Builder(serverAmsNetId));


SampleRunner.Run(autd);
