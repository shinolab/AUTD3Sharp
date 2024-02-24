using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;


using var autd = await new ControllerBuilder()
    .AddDevice(new AUTD3(Vector3d.zero))
    .AddDevice(new AUTD3(new Vector3d(AUTD3.DeviceWidth, 0, 0)))
    .OpenAsync(Simulator.Builder(8080));

await SampleRunner.Run(autd);
