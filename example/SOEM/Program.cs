using AUTD3Sharp;
using AUTD3Sharp.Link;
using AUTD3Sharp.Utils;
using Samples;

System.Environment.SetEnvironmentVariable("RUST_LOG", "autd3=INFO");
Tracing.Init();

using var autd = Controller.Builder([new AUTD3(Vector3.Zero)])
    .Open(SOEM.Builder()
        .WithErrHandler((slave, status) =>
        {
            Console.Error.WriteLine($"slave [{slave}]: {status}");
            if (status == Status.Lost)
                // You can also wait for the link to recover, without exiting the process
                Environment.Exit(-1);
        }));

SampleRunner.Run(autd);
