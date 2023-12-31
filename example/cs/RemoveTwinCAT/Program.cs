﻿/*
 * File: Program.cs
 * Project: RemoveTwinCAT
 * Created Date: 13/10/2022
 * Author: Shun Suzuki
 * -----
 * Last Modified: 26/11/2023
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2022-2023 Shun Suzuki. All rights reserved.
 * 
 */

using AUTD3Sharp.Utils;
using AUTD3Sharp;
using AUTD3Sharp.Link;
using Samples;

const string serverAmsNetId = "your TwinCATAUTDServer AMS net id (e.g. 172.16.99.2.1.1)";

using var autd = await new ControllerBuilder()
    .AddDevice(new AUTD3(Vector3d.zero))
    .OpenWithAsync(RemoteTwinCAT.Builder(serverAmsNetId));


await SampleRunner.Run(autd);
