namespace tests;

public class AUTDTest
{
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

        Assert.Equal("v12.1.0", AUTD3Sharp.Driver.FirmwareVersion.LatestVersion);

        {
            foreach (var (info, i) in autd.FirmwareVersion().Select((info, i) => (info, i)))
            {
                Assert.Equal(info.Info, $"{i}: CPU = v12.1.0, FPGA = v12.1.0 [Emulator]");
                Assert.Equal($"{info}", $"{i}: CPU = v12.1.0, FPGA = v12.1.0 [Emulator]");
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
    public void TestSender()
    {
        var autd = CreateController();

        foreach (var dev in autd)
        {
            var m = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
        }

        autd.Sender(new SenderOption()).Send(new Static()
        {
            Intensity = 0x80,
        });
        foreach (var dev in autd)
        {
            var m = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            Assert.All(m, d => Assert.Equal(0x80, d));
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
        autd.Send((new Static(), new Uniform(intensity: Intensity.Max, phase: Phase.Zero)));
        foreach (var dev in autd)
        {
            var m = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }

        autd.Link<Audit>().BreakDown();
        Assert.Throws<AUTDException>(() => autd.Send((new Static(), new Uniform(intensity: Intensity.Max, phase: Phase.Zero))));
        autd.Link<Audit>().Repair();
    }

    [Fact]
    public void TestDefaultSenderOption()
    {
        var autd = CreateController();
        var option = new SenderOption
        {
            SendInterval = Duration.FromMillis(2),
            ReceiveInterval = Duration.FromMillis(3),
            Timeout = null,
        };
        autd.DefaultSenderOption = option;
        Assert.Equal(option, autd.DefaultSenderOption);
    }

}
