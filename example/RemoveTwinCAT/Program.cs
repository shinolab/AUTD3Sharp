using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;

const string serverAmsNetId = "your TwinCATAUTDServer AMS net id (e.g. 172.16.99.2.1.1)";

using var autd = Controller.Open([new AUTD3(pos: Point3.Origin, rot: Quaternion.Identity)], new RemoteTwinCAT(serverAmsNetId, new RemoteTwinCATOption()));

SampleRunner.Run(autd);
