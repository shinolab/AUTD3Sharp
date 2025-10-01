using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;

Environment.SetEnvironmentVariable("RUST_LOG", "autd3=INFO");
EtherCrab.Tracing.Init();

using var autd = Controller.Open([new AUTD3(pos: Point3.Origin, rot: Quaternion.Identity)], new EtherCrab(
    (idx, status) =>
        {
            Console.Error.WriteLine($"Device[{idx}]: {status}");
        }, option: new EtherCrabOption()
));

SampleRunner.Run(autd);
