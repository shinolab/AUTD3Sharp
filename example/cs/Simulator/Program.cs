using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;


using var autd = new ControllerBuilder()
    .AddDevice(new AUTD3(Vector3d.Zero))
    .AddDevice(new AUTD3(new Vector3d(AUTD3.DeviceWidth, 0, 0)))
    .Open(Simulator.Builder(8080));

SampleRunner.Run(autd);
