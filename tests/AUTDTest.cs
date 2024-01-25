using AUTD3Sharp.Driver;

namespace tests;

public class AUTDTest
{
    public static async Task<Controller<Audit>> CreateController()
    {
        return await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero)).OpenWithAsync(Audit.Builder());
    }

    public static Controller<Audit> CreateControllerSync()
    {
        return new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero)).OpenWith(Audit.Builder());
    }

    [Fact]
    public async Task TestSilencerFixedCompletionSteps()
    {
        using var autd = await CreateController();

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        Assert.True(await autd.SendAsync(ConfigureSilencer.FixedCompletionSteps(2, 3)));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(2, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(3, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        Assert.True(await autd.SendAsync(ConfigureSilencer.Disable()));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(1, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(1, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        Assert.True(await autd.SendAsync(ConfigureSilencer.Default()));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }
    }

    [Fact]
    public async Task TestSilencerFixedUpdateRate()
    {
        using var autd = await CreateController();

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        Assert.True(await autd.SendAsync(ConfigureSilencer.FixedUpdateRate(256, 257)));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(256, autd.Link.SilencerUpdateRateIntensity(dev.Idx));
            Assert.Equal(257, autd.Link.SilencerUpdateRatePhase(dev.Idx));
            Assert.False(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }
    }

    [Fact]
    public async Task TestSilencerLargeSteps()
    {
        using var autd = await CreateController();

        Assert.True(await autd.SendAsync(ConfigureSilencer.Disable()));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(1, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(1, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        Assert.True(await autd.SendAsync(new Sine(150).WithSamplingConfig(SamplingConfiguration.FromFrequencyDivision(512))));

        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(ConfigureSilencer.FixedCompletionSteps(10, 40)));
    }

    [Fact]
    public async Task TestSilencerSmallFreqDivMod()
    {
        using var autd = await CreateController();

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Sine(150).WithSamplingConfig(SamplingConfiguration.FromFrequencyDivision(512))));

        Assert.True(await autd.SendAsync(ConfigureSilencer.FixedCompletionSteps(10, 40).WithStrictMode(false)));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        Assert.True(await autd.SendAsync(new Sine(150).WithSamplingConfig(SamplingConfiguration.FromFrequencyDivision(512))));
    }

    [Fact]
    public async Task TestSilencerSmallFreqDivSTM()
    {
        using var autd = await CreateController();

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(GainSTM.FromSamplingConfig(SamplingConfiguration.FromFrequencyDivision(512)).AddGain(new Null()).AddGain(new Null())));

        Assert.True(await autd.SendAsync(ConfigureSilencer.FixedCompletionSteps(10, 40).WithStrictMode(false)));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }
        Assert.True(await autd.SendAsync(new Sine(150).WithSamplingConfig(SamplingConfiguration.FromFrequencyDivision(512))));

        Assert.True(await autd.SendAsync(GainSTM.FromSamplingConfig(SamplingConfiguration.FromFrequencyDivision(512)).AddGain(new Null()).AddGain(new Null())));
    }


    [Fact]
    public async Task TestDebugOutputIdx()
    {
        using var autd = await CreateController();

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(0xFF, autd.Link.DebugOutputIdx(dev.Idx));
        }

        Assert.True(await autd.SendAsync(new ConfigureDebugOutputIdx(device => device[0])));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(0, autd.Link.DebugOutputIdx(dev.Idx));
        }

        Assert.True(await autd.SendAsync(new ConfigureDebugOutputIdx(device => device.Idx == 0 ? device[10] : null)));
        Assert.Equal(10, autd.Link.DebugOutputIdx(0));
        Assert.Equal(0xFF, autd.Link.DebugOutputIdx(1));
    }

    [Fact]
    public async Task TestFPGAState()
    {
        using var autd = await CreateController();

        Assert.True(await autd.SendAsync(new ConfigureReadsFPGAState(_ => true)));

        {
            var infos = await autd.FPGAStateAsync();
            foreach (var info in infos)
            {
                Assert.Null(info);
            }
        }

        {
            autd.Link.AssertThermalSensor(0);
            autd.Link.Update(0);
            autd.Link.Update(1);

            var infos = await autd.FPGAStateAsync();
            Assert.True(infos[0]!.IsThermalAssert);
            Assert.False(infos[1]!.IsThermalAssert);
            Assert.Equal("Thermal assert = True", infos[0]!.ToString());
            Assert.Equal("Thermal assert = False", infos[1]!.ToString());
        }

        {
            autd.Link.DeassertThermalSensor(0);
            autd.Link.AssertThermalSensor(1);
            autd.Link.Update(0);
            autd.Link.Update(1);

            var infos = await autd.FPGAStateAsync();
            Assert.False(infos[0]!.IsThermalAssert);
            Assert.True(infos[1]!.IsThermalAssert);
        }

        {
            autd.Link.BreakDown();
            await Assert.ThrowsAsync<AUTDException>(async () => _ = await autd.FPGAStateAsync());
        }
    }


    [Fact]
    public void TestFPGAStateSync()
    {
        var autd = CreateControllerSync();

        Assert.True(autd.Send(new ConfigureReadsFPGAState(_ => true)));

        {
            var infos = autd.FPGAState();
            foreach (var info in infos)
                Assert.Null(info);
        }

        {
            autd.Link.AssertThermalSensor(0);
            autd.Link.Update(0);
            autd.Link.Update(1);

            var infos = autd.FPGAState();
            Assert.True(infos[0]!.IsThermalAssert);
            Assert.False(infos[1]!.IsThermalAssert);
        }

        {
            autd.Link.DeassertThermalSensor(0);
            autd.Link.AssertThermalSensor(1);
            autd.Link.Update(0);
            autd.Link.Update(1);

            var infos = autd.FPGAState();
            Assert.False(infos[0]!.IsThermalAssert);
            Assert.True(infos[1]!.IsThermalAssert);
        }

        {
            autd.Link.BreakDown();
            Assert.Throws<AUTDException>(() => _ = autd.FPGAState());
        }
    }

    [Fact]
    public async Task TestFirmwareInfoList()
    {
        using var autd = await CreateController();

        Assert.Equal("v5.1.1", FirmwareInfo.LatestVersion);

        {
            foreach (var (info, i) in (await autd.FirmwareInfoListAsync()).Select((info, i) => (info, i)))
            {
                Assert.Equal(info.Info, $"{i}: CPU = v5.1.1, FPGA = v5.1.1 [Emulator]");
                Assert.Equal($"{info}", $"{i}: CPU = v5.1.1, FPGA = v5.1.1 [Emulator]");
            }
        }

        {
            autd.Link.BreakDown();
            await Assert.ThrowsAsync<AUTDException>(async () => _ = (await autd.FirmwareInfoListAsync()).Last());
        }
    }

    [Fact]
    public void TestFirmwareInfoListSync()
    {
        var autd = CreateControllerSync();

        foreach (var (info, i) in autd.FirmwareInfoList().Select((info, i) => (info, i)))
            Assert.Equal(info.Info, $"{i}: CPU = v5.1.1, FPGA = v5.1.1 [Emulator]");

        autd.Link.BreakDown();
        Assert.Throws<AUTDException>(() => _ = autd.FirmwareInfoList().Last());
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
        }
    }

    [Fact]
    public async Task TestSendTimeout()
    {
        {
            var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero))
                .OpenWithAsync(Audit.Builder().WithTimeout(TimeSpan.FromMicroseconds(0)));

            await autd.SendAsync(new Null());

            await autd.SendAsync(new Null(), TimeSpan.FromMicroseconds(1));

            await autd.SendAsync((new Null(), new Null()), TimeSpan.FromMicroseconds(2));
        }

        {
            var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero))
                .OpenWithAsync(Audit.Builder().WithTimeout(TimeSpan.FromMicroseconds(10)));

            await autd.SendAsync(new Null());

            await autd.SendAsync(new Null(), TimeSpan.FromMicroseconds(1));

            await autd.SendAsync((new Null(), new Null()), TimeSpan.FromMicroseconds(2));
        }
    }

    [Fact]
    public void TestSendTimeoutSync()
    {
        {
            var autd = new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero))
                .OpenWith(Audit.Builder().WithTimeout(TimeSpan.FromMicroseconds(0)));

            autd.Send(new Null());

            autd.Send(new Null(), TimeSpan.FromMicroseconds(1));

            autd.Send((new Null(), new Null()), TimeSpan.FromMicroseconds(2));
        }

        {
            var autd = new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero))
                .OpenWith(Audit.Builder().WithTimeout(TimeSpan.FromMicroseconds(10)));

            autd.Send(new Null());

            autd.Send(new Null(), TimeSpan.FromMicroseconds(1));

            autd.Send((new Null(), new Null()), TimeSpan.FromMicroseconds(2));
        }
    }

    [Fact]
    public async Task TestSendSingle()
    {
        using var autd = await CreateController();

        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx);
            Assert.All(m, d => Assert.Equal(0xFF, d));
        }
        Assert.True(await autd.SendAsync(new Static()));

        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx);
            Assert.All(m, d => Assert.Equal(0xFF, d));
        }

        autd.Link.Down();
        Assert.False(await autd.SendAsync(new Static()));

        autd.Link.BreakDown();
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Static()));
    }

    [Fact]
    public void TestSendSingleSync()
    {
        var autd = CreateControllerSync();

        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx);
            Assert.All(m, d => Assert.Equal(0xFF, d));
        }
        Assert.True(autd.Send(new Static()));

        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx);
            Assert.All(m, d => Assert.Equal(0xFF, d));
        }

        autd.Link.Down();
        Assert.False(autd.Send(new Static()));

        autd.Link.BreakDown();
        Assert.Throws<AUTDException>(() => autd.Send(new Static()));
    }

    [Fact]
    public async Task TestSendDouble()
    {
        using var autd = await CreateController();

        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(dev.Idx, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        Assert.True(await autd.SendAsync(new Static(), new Uniform(EmitIntensity.Max)));
        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(dev.Idx, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }

        autd.Link.Down();
        Assert.False(await autd.SendAsync((new Static(), new Uniform(EmitIntensity.Max))));

        autd.Link.BreakDown();
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Static(), new Uniform(EmitIntensity.Max)));
    }

    [Fact]
    public void TestSendDoubleSync()
    {
        var autd = CreateControllerSync();

        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(dev.Idx, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        Assert.True(autd.Send(new Static(), new Uniform(EmitIntensity.Max)));
        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(dev.Idx, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }

        autd.Link.Down();
        Assert.False(autd.Send((new Static(), new Uniform(EmitIntensity.Max))));

        autd.Link.BreakDown();
        Assert.Throws<AUTDException>(() => autd.Send(new Static(), new Uniform(EmitIntensity.Max)));
    }

    [Fact]
    public async Task TestGroup()
    {
        using var autd = await CreateController();

        await autd.Group(dev => dev.Idx.ToString())
             .Set("0", (new Static(), new Null()))
             .Set("1", new Sine(150), new Uniform(EmitIntensity.Max))
             .SendAsync();
        {
            var m = autd.Link.Modulation(0);
            Assert.Equal(2, m.Length);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var m = autd.Link.Modulation(1);
            Assert.Equal(80, m.Length);
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(1, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }

        await autd.Group(dev => dev.Idx.ToString())
             .Set("1", new Null())
             .Set("0", new Uniform(EmitIntensity.Max))
             .SendAsync();
        {
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, _) = autd.Link.IntensitiesAndPhases(1, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
        }

        await autd.Group(dev => dev.Idx switch
        {
            0 => "0",
            _ => null
        }).Set("0", new Uniform(EmitIntensity.Max)).SendAsync();
        {
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, _) = autd.Link.IntensitiesAndPhases(1, 0);
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
             .Set("1", new Sine(150), new Uniform(EmitIntensity.Max))
             .Send();
        {
            var m = autd.Link.Modulation(0);
            Assert.Equal(2, m.Length);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var m = autd.Link.Modulation(1);
            Assert.Equal(80, m.Length);
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(1, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }

        autd.Group(dev => dev.Idx.ToString())
            .Set("1", new Null())
            .Set("0", new Uniform(EmitIntensity.Max))
            .Send();
        {
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, _) = autd.Link.IntensitiesAndPhases(1, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
        }

        autd.Group(dev => dev.Idx switch
       {
           0 => "0",
           _ => null
       }).Set("0", new Uniform(EmitIntensity.Max)).Send();
        {
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, _) = autd.Link.IntensitiesAndPhases(1, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
        }
    }

    [Fact]
    public async Task TestGroupWithTimeout()
    {
        using var autd = await CreateController();

        await autd.Group(dev => dev.Idx.ToString())
             .Set("0", (new Static(), new Null()), TimeSpan.FromSeconds(10))
             .Set("1", new Null(), TimeSpan.FromSeconds(5))
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
                 .Set("0", new Sine(150), new Uniform(new EmitIntensity(0x80)).WithPhase(new Phase(0x90)))
                 .SendAsync();

        Assert.False(check[0]);
        Assert.True(check[1]);

        {
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(1, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }
    }

    [Fact]
    public async Task TestClear()
    {
        using var autd = await CreateController();
        Assert.True(await autd.SendAsync(new Uniform(EmitIntensity.Max).WithPhase(new Phase(0x90))));
        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(dev.Idx, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }

        Assert.True(await autd.SendAsync(new Clear()));
        foreach (var dev in autd.Geometry)
        {
            var m = autd.Link.Modulation(dev.Idx);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(dev.Idx, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
    }

    [Fact]
    public async Task TestSynchronize()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero))
            .OpenWithAsync(Audit.Builder());

        Assert.True(await autd.SendAsync(new Synchronize()));
    }

    [Fact]
    public async Task TestConfigureModDelay()
    {
        using var autd = await CreateController();

        foreach (var dev in autd.Geometry)
        {
            Assert.All(autd.Link.ModDelays(dev.Idx), d => Assert.Equal(0, d));
        }

        Assert.True(await autd.SendAsync(new ConfigureModDelay((_, _) => 1)));
        foreach (var dev in autd.Geometry)
        {
            Assert.All(autd.Link.ModDelays(dev.Idx), d => Assert.Equal(1, d));
        }
    }

    [Fact]
    public async Task TestConfigForceFan()
    {
        var autd = await CreateController();
        foreach (var dev in autd.Geometry)
            Assert.False(autd.Link.IsForceFan(dev.Idx));

        await autd.SendAsync(new ConfigureForceFan(dev => dev.Idx == 0));
        Assert.True(autd.Link.IsForceFan(0));
        Assert.False(autd.Link.IsForceFan(1));

        await autd.SendAsync(new ConfigureForceFan(dev => dev.Idx == 1));
        Assert.False(autd.Link.IsForceFan(0));
        Assert.True(autd.Link.IsForceFan(1));
    }
}
