using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;
using System.Net;


using var autd = Controller.Builder([
        new AUTD3(Point3.Origin),
        new AUTD3(new Point3(AUTD3.DeviceWidth, 0, 0))
    ])
    .Open(Simulator.Builder(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080)));

SampleRunner.Run(autd);
