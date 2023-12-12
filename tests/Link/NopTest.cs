/*
 * File: NopTest.cs
 * Project: Link
 * Created Date: 12/12/2023
 * Author: Shun Suzuki
 * -----
 * Last Modified: 12/12/2023
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2023 Shun Suzuki. All rights reserved.
 * 
 */


using System.Net;

namespace tests.Link;

public class NopTest
{
    [Fact]
    public async Task TestNop()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero)).OpenWithAsync(Nop.Builder());

        await autd.CloseAsync();
    }
}
