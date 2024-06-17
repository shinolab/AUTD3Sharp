﻿using AUTD3Sharp;
using AUTD3Sharp.Link;
using AUTD3Sharp.Utils;
using Samples;

AUTD3Sharp.Debug.TracingInit(AUTD3Sharp.Debug.Level.Info);

using var autd = Controller.Builder([new AUTD3(Vector3.Zero)])
    .Open(SOEM.Builder()
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

SampleRunner.Run(autd);