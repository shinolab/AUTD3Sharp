namespace tests;

public class AUTDTest
{
    [Fact]
    public void TestTracingInit()
    {
        Tracing.Init();
    }

    [Fact]
    public void TestControllerIsDefault()
    {
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDSenderOptionIsDefault(new SenderOption().ToNative()));
    }

    [Fact]
    public void TestFPGAState()
    {
        using var autd = CreateController();

        autd.Send(new ReadsFPGAState(_ => true));

        {
            autd.Link<Audit>().AssertThermalSensor(0);

            var infos = autd.FPGAState();
            Assert.True(infos[0]!.IsThermalAssert());
            Assert.False(infos[1]!.IsThermalAssert());
            Assert.Equal("Thermal assert = True", infos[0]!.ToString());
            Assert.Equal("Thermal assert = False", infos[1]!.ToString());
        }

        {
            autd.Link<Audit>().DeassertThermalSensor(0);
            autd.Link<Audit>().AssertThermalSensor(1);

            var infos = autd.FPGAState();
            Assert.False(infos[0]!.IsThermalAssert());
            Assert.True(infos[1]!.IsThermalAssert());
        }

        autd.Send(new ReadsFPGAState(dev => dev.Idx() == 1));
        {
            var infos = autd.FPGAState();
            Assert.Null(infos[0]);
            Assert.NotNull(infos[1]);
        }

        {
            autd.Link<Audit>().BreakDown();
            Assert.Throws<AUTDException>(() => _ = autd.FPGAState());
            autd.Link<Audit>().Repair();
        }
    }


    [Fact]
    public void TestFirmwareVersion()
    {
        using var autd = CreateController();

        Assert.Equal("v11.0.0", AUTD3Sharp.Driver.FirmwareVersion.LatestVersion);

        {
            foreach (var (info, i) in autd.FirmwareVersion().Select((info, i) => (info, i)))
            {
                Assert.Equal(info.Info, $"{i}: CPU = v11.0.0, FPGA = v11.0.0 [Emulator]");
                Assert.Equal($"{info}", $"{i}: CPU = v11.0.0, FPGA = v11.0.0 [Emulator]");
            }
        }

        {
            autd.Link<Audit>().BreakDown();
            Assert.Throws<AUTDException>(() => _ = autd.FirmwareVersion().Last());
            autd.Link<Audit>().Repair();
        }
    }


    [Fact]
    public void TestClose()
    {
        {
            var autd = CreateController();
            Assert.True(autd.Link<Audit>().IsOpen());

            autd.Close();
            autd.Close();
        }

        {
            var autd = CreateController();

            autd.Link<Audit>().BreakDown();
            Assert.Throws<AUTDException>(() => autd.Close());
        }
    }

    [Fact]
    public void TestSendSingle()
    {
        var autd = CreateController();

        foreach (var dev in autd)
        {
            var m = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
        }
        autd.Send(new Static());

        foreach (var dev in autd)
        {
            var m = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
        }

        autd.Link<Audit>().BreakDown();
        Assert.Throws<AUTDException>(() => autd.Send(new Static()));
        autd.Link<Audit>().Repair();
    }

    [Fact]
    public void TestSendDouble()
    {
        var autd = CreateController();

        foreach (var dev in autd)
        {
            var m = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        autd.Send((new Static(), new Uniform(intensity: EmitIntensity.Max, phase: Phase.Zero)));
        foreach (var dev in autd)
        {
            var m = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }

        autd.Link<Audit>().BreakDown();
        Assert.Throws<AUTDException>(() => autd.Send((new Static(), new Uniform(intensity: EmitIntensity.Max, phase: Phase.Zero))));
        autd.Link<Audit>().Repair();
    }

    [Fact]
    public void TestGroup()
    {
        var autd = CreateController();

        autd.GroupSend(dev => dev.Idx(), new GroupDictionary
        {
            { 0, (new Static(), new Null()) },
            { 1, (new Sine(freq: 150 * Hz, option: new SineOption()), new Uniform(intensity: EmitIntensity.Max, phase: Phase.Zero)) }
        });

        {
            var m = autd.Link<Audit>().Modulation(0, Segment.S0);
            Assert.Equal(2, m.Length);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link<Audit>().Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var m = autd.Link<Audit>().Modulation(1, Segment.S0);
            Assert.Equal(80, m.Length);
            var (intensities, phases) = autd.Link<Audit>().Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }

        autd.GroupSend(dev => dev.Idx(), new GroupDictionary()
            {
                { 1, new Null() }, { 0, new Uniform(intensity: EmitIntensity.Max, phase: Phase.Zero) }
            });

        {
            var (intensities, phases) = autd.Link<Audit>().Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, _) = autd.Link<Audit>().Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
        }

        autd.GroupSend(dev => dev.Idx() switch
        {
            0 => 0,
            _ => null
        }, new GroupDictionary()
        {
            {0, new Uniform(intensity: EmitIntensity.Max, phase: Phase.Zero)}
        });
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, _) = autd.Link<Audit>().Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
        }
    }

    [Fact]
    public void TestGroupCheckOnlyForEnabled()
    {
        using var autd = CreateController();
        var check = new bool[autd.NumDevices()];

        autd[0].Enable = false;

        autd.GroupSend(dev =>
        {
            check[dev.Idx()] = true;
            return 0;
        }, new GroupDictionary()
        {
            {0, (new Sine(freq: 150 * Hz, option: new SineOption()), new Uniform(intensity:new EmitIntensity(0x80), phase: new Phase(0x90)))}
        });

        Assert.False(check[0]);
        Assert.True(check[1]);

        {
            var (intensities, phases) = autd.Link<Audit>().Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }
    }
}
