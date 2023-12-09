﻿/*
 * File: Program.cs
 * Project: RemoteSOEM
 * Created Date: 13/09/2023
 * Author: Shun Suzuki
 * -----
 * Last Modified: 26/11/2023
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2023 Shun Suzuki. All rights reserved.
 * 
 */

using System.Net;
using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;

using var autd = await new ControllerBuilder()
    .AddDevice(new AUTD3(Vector3d.zero))
    .OpenWithAsync(RemoteSOEM.Builder(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080)));

await SampleRunner.Run(autd);
