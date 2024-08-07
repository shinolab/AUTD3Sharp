using AUTD3Sharp.Driver;
using System.Reflection;

namespace tests;

public class AUTDTest
{
    public static async Task<Controller<Audit>> CreateController()
    {
        return await Controller.Builder([new AUTD3(Vector3.Zero), new AUTD3(Vector3.Zero)]).WithSendInterval(TimeSpan.FromMilliseconds(1)).WithTimerResolution(1).OpenAsync(Audit.Builder());
    }

    public static Controller<Audit> CreateControllerSync()
    {
        return Controller.Builder([new AUTD3(Vector3.Zero), new AUTD3(Vector3.Zero)]).WithSendInterval(TimeSpan.FromMilliseconds(1)).WithTimerResolution(1).Open(Audit.Builder());
    }

    [Fact]
    public async Task TestOoenWithTimeout()
    {
        _ = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder(), TimeSpan.FromMilliseconds(200));
    }

    [Fact]
    public void TestOoenWithTimeoutSync()
    {
        _ = Controller.Builder([new AUTD3(Vector3.Zero)]).Open(Audit.Builder(), TimeSpan.FromMilliseconds(200));
    }

    [Fact]
    public async Task TestFPGAState()
    {
        using var autd = await CreateController();

        await autd.SendAsync(new ReadsFPGAState(_ => true));

        {
            autd.Link.AssertThermalSensor(0);

            var infos = await autd.FPGAStateAsync();
            Assert.True(infos[0]!.IsThermalAssert);
            Assert.False(infos[1]!.IsThermalAssert);
            Assert.Equal("Thermal assert = True", infos[0]!.ToString());
            Assert.Equal("Thermal assert = False", infos[1]!.ToString());
        }

        {
            autd.Link.DeassertThermalSensor(0);
            autd.Link.AssertThermalSensor(1);

            var infos = await autd.FPGAStateAsync();
            Assert.False(infos[0]!.IsThermalAssert);
            Assert.True(infos[1]!.IsThermalAssert);
        }

        await autd.SendAsync(new ReadsFPGAState(dev => dev.Idx == 1));
        {
            var infos = await autd.FPGAStateAsync();
            Assert.Null(infos[0]);
            Assert.NotNull(infos[1]);
        }

        {
            autd.Link.BreakDown();
            await Assert.ThrowsAsync<AUTDException>(async () => _ = await autd.FPGAStateAsync());
            autd.Link.Repair();
        }
    }


    [Fact]
    public void TestFPGAStateSync()
    {
        var autd = CreateControllerSync();

        autd.Send(new ReadsFPGAState(_ => true));

        {
            autd.Link.AssertThermalSensor(0);

            var infos = autd.FPGAState();
            Assert.True(infos[0]!.IsThermalAssert);
            Assert.False(infos[1]!.IsThermalAssert);
        }

        {
            autd.Link.DeassertThermalSensor(0);
            autd.Link.AssertThermalSensor(1);

            var infos = autd.FPGAState();
            Assert.False(infos[0]!.IsThermalAssert);
            Assert.True(infos[1]!.IsThermalAssert);
        }

        autd.Send(new ReadsFPGAState(dev => dev.Idx == 1));
        {
            var infos = autd.FPGAState();
            Assert.Null(infos[0]);
            Assert.NotNull(infos[1]);
        }

        {
            autd.Link.BreakDown();
            Assert.Throws<AUTDException>(() => _ = autd.FPGAState());
            autd.Link.Repair();
        }
    }

    [Fact]
    public async Task TestFirmwareVersion()
    {
        using var autd = await CreateController();

        Assert.Equal("v9.0.0", FirmwareVersion.LatestVersion);

        {
            foreach (var (info, i) in (await autd.FirmwareVersionAsync()).Select((info, i) => (info, i)))
            {
                Assert.Equal(info.Info, $"{i}: CPU = v9.0.0, FPGA = v9.0.0 [Emulator]");
                Assert.Equal($"{info}", $"{i}: CPU = v9.0.0, FPGA = v9.0.0 [Emulator]");
            }
        }

        {
            autd.Link.BreakDown();
            await Assert.ThrowsAsync<AUTDException>(async () => _ = (await autd.FirmwareVersionAsync()).Last());
            autd.Link.Repair();
        }
    }

    [Fact]
    public void TestFirmwareVersionListSync()
    {
        var autd = CreateControllerSync();

        foreach (var (info, i) in autd.FirmwareVersion().Select((info, i) => (info, i)))
            Assert.Equal(info.Info, $"{i}: CPU = v9.0.0, FPGA = v9.0.0 [Emulator]");

        autd.Link.BreakDown();
        Assert.Throws<AUTDException>(() => _ = autd.FirmwareVersion().Last());
        autd.Link.Repair();
    }


    [Fact]
    public async Task TestClose()
    {
        {
            using var autd = await CreateController();
            Assert.True(autd.Link.IsOpen());

            await autd.CloseAsync();
            Assert.False(autd.Link.IsOpen());

            await autd.CloseAsync();
            Assert.False(autd.Link.IsOpen());
        }

        {
            using var autd = await CreateController();

            autd.Link.BreakDown();
            await Assert.ThrowsAsync<AUTDException>(async () => await autd.CloseAsync());
            autd.Link.Repair();
        }
    }

    [Fact]
    public void TestCloseSync()
    {
        {
            var autd = CreateControllerSync();
            Assert.True(autd.Link.IsOpen());

            autd.Close();
            Assert.False(autd.Link.IsOpen());

            autd.Close();
            Assert.False(autd.Link.IsOpen());
        }

        {
            var autd = CreateControllerSync();

            autd.Link.BreakDown();
            Assert.Throws<AUTDException>(() => autd.Close());
            autd.Link.Repair();
        }
    }

    [Fact]
    public void TestSendTimeout()
    {
        var autd = Controller.Builder([new AUTD3(Vector3.Zero), new AUTD3(Vector3.Zero)])
            .Open(Audit.Builder().WithTimeout(TimeSpan.FromMicroseconds(0)));
        Assert.Equal(TimeSpan.FromMicroseconds(0), autd.Link.Timeout());
        Assert.Equal(TimeSpan.FromMilliseconds(200), autd.Link.LastTimeout());

        autd.Send(new Null());
        Assert.Equal(TimeSpan.FromMilliseconds(0), autd.Link.LastTimeout());

        autd.Send(new Null().WithTimeout(TimeSpan.FromMicroseconds(1)));
        Assert.Equal(TimeSpan.FromMicroseconds(1), autd.Link.LastTimeout());

        autd.Send((new Null(), new Null()).WithTimeout(TimeSpan.FromMicroseconds(2)));
        Assert.Equal(TimeSpan.FromMicroseconds(2), autd.Link.LastTimeout());
    }

    [Fact]
    public void TestParallelThreshold()
    {
        var autd = Controller.Builder([new AUTD3(Vector3.Zero), new AUTD3(Vector3.Zero)])
            .WithParallelThreshold(0)
            .Open(Audit.Builder().WithTimeout(TimeSpan.FromMicroseconds(0)));
        Assert.Equal(0xFFFF, AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDControllerLastParallelThreshold(
            (AUTD3Sharp.NativeMethods.ControllerPtr)typeof(Controller<Audit>).GetField("Ptr", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(autd)!
        ));

        autd.Send(new Null());
        Assert.Equal(0, AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDControllerLastParallelThreshold(
            (AUTD3Sharp.NativeMethods.ControllerPtr)typeof(Controller<Audit>).GetField("Ptr", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(autd)!
        ));

        autd.Send(new Static());
        Assert.Equal(0xFFFF, AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDControllerLastParallelThreshold(
            (AUTD3Sharp.NativeMethods.ControllerPtr)typeof(Controller<Audit>).GetField("Ptr", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(autd)!
        ));

        autd.Send(new Static().WithParallelThreshold(10));
        Assert.Equal(10, AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDControllerLastParallelThreshold(
            (AUTD3Sharp.NativeMethods.ControllerPtr)typeof(Controller<Audit>).GetField("Ptr", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(autd)!
        ));

        autd.Send((new Static(), new Static()).WithParallelThreshold(5));
        Assert.Equal(5, AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDControllerLastParallelThreshold(
            (AUTD3Sharp.NativeMethods.ControllerPtr)typeof(Controller<Audit>).GetField("Ptr", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(autd)!
        ));
    }

    [Fact]
    public async Task TestSendSingle()
    {
        using var autd = await CreateController();

        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
        }
        await autd.SendAsync(new Static());

        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
        }

        autd.Link.Down();
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Static()));
        autd.Link.Up();

        autd.Link.BreakDown();
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Static()));
        autd.Link.Repair();
    }

    [Fact]
    public void TestSendSingleSync()
    {
        var autd = CreateControllerSync();

        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
        }
        autd.Send(new Static());

        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
        }

        autd.Link.Down();
        Assert.Throws<AUTDException>(() => autd.Send(new Static()));
        autd.Link.Up();

        autd.Link.BreakDown();
        Assert.Throws<AUTDException>(() => autd.Send(new Static()));
        autd.Link.Repair();
    }

    [Fact]
    public async Task TestSendDouble()
    {
        using var autd = await CreateController();

        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        await autd.SendAsync((new Static(), new Uniform(EmitIntensity.Max)));
        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }

        autd.Link.Down();
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync((new Static(), new Uniform(EmitIntensity.Max))));
        autd.Link.Up();

        autd.Link.BreakDown();
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync((new Static(), new Uniform(EmitIntensity.Max))));
        autd.Link.Repair();
    }

    [Fact]
    public void TestSendDoubleSync()
    {
        var autd = CreateControllerSync();

        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        autd.Send((new Static(), new Uniform(EmitIntensity.Max)));
        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }

        autd.Link.Down();
        Assert.Throws<AUTDException>(() => autd.Send((new Static(), new Uniform(EmitIntensity.Max))));
        autd.Link.Up();

        autd.Link.BreakDown();
        Assert.Throws<AUTDException>(() => autd.Send((new Static(), new Uniform(EmitIntensity.Max))));
        autd.Link.Repair();
    }

    [Fact]
    public async Task TestGroup()
    {
        using var autd = await CreateController();

        await autd.Group(dev => dev.Idx.ToString())
             .Set("0", (new Static(), new Null()))
             .Set("1", (new Sine(150 * Hz), new Uniform(EmitIntensity.Max)))
             .SendAsync();
        {
            var m = autd.Link.Modulation(0, Segment.S0);
            Assert.Equal(2, m.Length);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var m = autd.Link.Modulation(1, Segment.S0);
            Assert.Equal(80, m.Length);
            var (intensities, phases) = autd.Link.Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }

        await autd.Group(dev => dev.Idx.ToString())
             .Set("1", new Null())
             .Set("0", new Uniform(EmitIntensity.Max))
             .SendAsync();
        {
            var (intensities, phases) = autd.Link.Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, _) = autd.Link.Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
        }

        await autd.Group(dev => dev.Idx switch
        {
            0 => "0",
            _ => null
        }).Set("0", new Uniform(EmitIntensity.Max)).SendAsync();
        {
            var (intensities, phases) = autd.Link.Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, _) = autd.Link.Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
        }

        Assert.Throws<AUTDException>(() => autd.Group(dev => dev.Idx.ToString())
             .Set("0", new Null())
             .Set("0", new Uniform(EmitIntensity.Max)));
        Assert.Throws<AUTDException>(() => autd.Group(dev => dev.Idx.ToString())
             .Set("0", new Uniform(EmitIntensity.Max))
             .Set("0", new Null()));
    }


    [Fact]
    public void TestGroupSync()
    {
        var autd = CreateControllerSync();

        autd.Group(dev => dev.Idx.ToString())
             .Set("0", (new Static(), new Null()))
             .Set("1", (new Sine(150 * Hz), new Uniform(EmitIntensity.Max)))
             .Send();
        {
            var m = autd.Link.Modulation(0, Segment.S0);
            Assert.Equal(2, m.Length);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var m = autd.Link.Modulation(1, Segment.S0);
            Assert.Equal(80, m.Length);
            var (intensities, phases) = autd.Link.Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }

        autd.Group(dev => dev.Idx.ToString())
            .Set("1", new Null())
            .Set("0", new Uniform(EmitIntensity.Max))
            .Send();
        {
            var (intensities, phases) = autd.Link.Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, _) = autd.Link.Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
        }

        autd.Group(dev => dev.Idx switch
       {
           0 => "0",
           _ => null
       }).Set("0", new Uniform(EmitIntensity.Max)).Send();
        {
            var (intensities, phases) = autd.Link.Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, _) = autd.Link.Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
        }
    }

    [Fact]
    public async Task TestGroupWithTimeout()
    {
        using var autd = await CreateController();

        await autd.Group(dev => dev.Idx.ToString())
             .Set("0", (new Static(), new Null()))
             .Set("1", new Null())
             .SendAsync();
    }

    [Fact]
    public async Task TestGroupCheckOnlyForEnabled()
    {
        using var autd = await CreateController();
        autd.Geometry[0].Enable = false;

        var check = new bool[autd.Geometry.NumDevices];
        await autd.Group(dev =>
        {
            check[dev.Idx] = true;
            return "0";
        })
                 .Set("0", (new Sine(150 * Hz), new Uniform((new EmitIntensity(0x80), new Phase(0x90)))))
                 .SendAsync();

        Assert.False(check[0]);
        Assert.True(check[1]);

        {
            var (intensities, phases) = autd.Link.Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, phases) = autd.Link.Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }
    }
}
