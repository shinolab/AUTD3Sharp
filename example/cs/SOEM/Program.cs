using AUTD3Sharp;
using AUTD3Sharp.Link;
using AUTD3Sharp.Utils;
using AUTD3Sharp.NativeMethods;
using Samples;

using var autd = await new ControllerBuilder().
    AddDevice(new AUTD3(Vector3d.zero))
    .OpenWithAsync(SOEM.Builder()
        .WithErrHandler((slave, status, msg) =>
        {
            switch (status)
            {
                case Status.Error:
                    Console.Error.WriteLine($"Error [{slave}]: {msg}");
                    break;
                case Status.Lost:
                    Console.Error.WriteLine($"Lost [{slave}]: {msg}");
                    // You can also wait for the link to recover, without exiting the process
                    Environment.Exit(-1);
                    break;
                case Status.StateChanged:
                    Console.Error.WriteLine($"StateChanged [{slave}]: {msg}");
                    break;
            };
        }));

await SampleRunner.Run(autd);
