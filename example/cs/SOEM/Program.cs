using AUTD3Sharp;
using AUTD3Sharp.Link;
using AUTD3Sharp.Utils;
using Samples;

var onLost = new SOEM.OnErrCallbackDelegate(msg =>
{
    Console.WriteLine($"Unrecoverable error occurred: {msg}");
    Environment.Exit(-1);
});

using var autd = await new ControllerBuilder().
    AddDevice(new AUTD3(Vector3d.zero))
    .OpenWithAsync(SOEM.Builder()
        .WithOnLost(onLost));

await SampleRunner.Run(autd);
