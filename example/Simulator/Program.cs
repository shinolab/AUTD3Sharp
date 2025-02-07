using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;
using System.Net;

using var autd = Controller.Open([
        new AUTD3(pos: Point3.Origin, rot: Quaternion.Identity),
        new AUTD3(pos: new Point3(AUTD3.DeviceWidth, 0, 0), rot: Quaternion.Identity)
    ], new Simulator(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080)));

SampleRunner.Run(autd);
