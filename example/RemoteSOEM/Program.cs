﻿using System.Net;
using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;

using var autd = Controller.Builder([new AUTD3(Vector3.Zero)])
    .Open(RemoteSOEM.Builder(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080)));

SampleRunner.Run(autd);
