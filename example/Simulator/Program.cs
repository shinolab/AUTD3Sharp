using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;


using var autd = Controller.Builder([
        new AUTD3(Vector3.Zero),
        new AUTD3(new Vector3(AUTD3.DeviceWidth, 0, 0))
    ])
    .Open(Simulator.Builder(8080));

SampleRunner.Run(autd);
