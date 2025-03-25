namespace tests.Driver.Datagram;

public class SilencerTest
{
    [Fact]
    public void TestSilencerFromUpdateRate()
    {
        using var autd = CreateController();

        foreach (var dev in autd)
        {
            Assert.Equal(10, autd.Link<Audit>().SilencerCompletionStepsIntensity(dev.Idx()));
            Assert.Equal(40, autd.Link<Audit>().SilencerCompletionStepsPhase(dev.Idx()));
            Assert.True(autd.Link<Audit>().SilencerFixedCompletionStepsMode(dev.Idx()));
        }

        autd.Send(new Silencer(config: new FixedUpdateRate { Intensity = 1, Phase = 2 }));
        foreach (var dev in autd)
        {
            Assert.Equal(1, autd.Link<Audit>().SilencerUpdateRateIntensity(dev.Idx()));
            Assert.Equal(2, autd.Link<Audit>().SilencerUpdateRatePhase(dev.Idx()));
            Assert.False(autd.Link<Audit>().SilencerFixedCompletionStepsMode(dev.Idx()));
        }
    }

    [Fact]
    public void TestSilencerFromCompletionSteps()
    {
        using var autd = CreateController();

        var s = new Silencer();
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDDatagramSilencerFixedCompletionStepsIsDefault(((FixedCompletionSteps)s.Config).ToNative()));

        autd.Send(new Silencer(config: new FixedCompletionSteps() { Intensity = 1, Phase = 2 }));
        foreach (var dev in autd)
        {
            Assert.Equal(1, autd.Link<Audit>().SilencerCompletionStepsIntensity(dev.Idx()));
            Assert.Equal(2, autd.Link<Audit>().SilencerCompletionStepsPhase(dev.Idx()));
            Assert.True(autd.Link<Audit>().SilencerFixedCompletionStepsMode(dev.Idx()));
            Assert.True(autd.Link<Audit>().SilencerStrictMode(dev.Idx()));
        }
    }

    [Fact]
    public void TestSilencerFromCompletionTime()
    {
        using var autd = CreateController();

        var s = new Silencer();
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDDatagramSilencerFixedCompletionStepsIsDefault(((FixedCompletionSteps)s.Config).ToNative()));

        autd.Send(new Silencer(config: new FixedCompletionTime { Intensity = Duration.FromMicros(25), Phase = Duration.FromMicros(50) }));
        foreach (var dev in autd)
        {
            Assert.Equal(1, autd.Link<Audit>().SilencerCompletionStepsIntensity(dev.Idx()));
            Assert.Equal(2, autd.Link<Audit>().SilencerCompletionStepsPhase(dev.Idx()));
            Assert.True(autd.Link<Audit>().SilencerFixedCompletionStepsMode(dev.Idx()));
            Assert.True(autd.Link<Audit>().SilencerStrictMode(dev.Idx()));
        }
    }

    [Fact]
    public void TestSilencerLargeSteps()
    {
        using var autd = CreateController();

        autd.Send(Silencer.Disable());
        foreach (var dev in autd)
        {
            Assert.Equal(1, autd.Link<Audit>().SilencerCompletionStepsIntensity(dev.Idx()));
            Assert.Equal(1, autd.Link<Audit>().SilencerCompletionStepsPhase(dev.Idx()));
            Assert.True(autd.Link<Audit>().SilencerFixedCompletionStepsMode(dev.Idx()));
            Assert.True(autd.Link<Audit>().SilencerStrictMode(dev.Idx()));
        }

        autd.Send(new Sine(freq: 150 * Hz, option: new SineOption()
        {
            SamplingConfig = new SamplingConfig(1)
        }));

        Assert.Throws<AUTDException>(() => autd.Send(new Silencer(config: new FixedCompletionTime { Intensity = Duration.FromMicros(250), Phase = Duration.FromMicros(1000) })));
    }

    [Fact]
    public void TestSilencerSmallFreqDivMod()
    {
        using var autd = CreateController();

        foreach (var dev in autd)
        {
            Assert.Equal(10, autd.Link<Audit>().SilencerCompletionStepsIntensity(dev.Idx()));
            Assert.Equal(40, autd.Link<Audit>().SilencerCompletionStepsPhase(dev.Idx()));
            Assert.True(autd.Link<Audit>().SilencerFixedCompletionStepsMode(dev.Idx()));
        }

        Assert.Throws<AUTDException>(() => autd.Send(new Sine(freq: 150 * Hz, option: new SineOption()
        {
            SamplingConfig = new SamplingConfig(1)
        })));

        autd.Send(new Silencer(new FixedCompletionSteps()
        {
            StrictMode = false
        }));
        foreach (var dev in autd)
        {
            Assert.Equal(10, autd.Link<Audit>().SilencerCompletionStepsIntensity(dev.Idx()));
            Assert.Equal(40, autd.Link<Audit>().SilencerCompletionStepsPhase(dev.Idx()));
            Assert.True(autd.Link<Audit>().SilencerFixedCompletionStepsMode(dev.Idx()));
            Assert.False(autd.Link<Audit>().SilencerStrictMode(dev.Idx()));
        }
    }

    [Fact]
    public void TestSilencerSmallFreqDivGainSTM()
    {
        using var autd = CreateController();

        foreach (var dev in autd)
        {
            Assert.Equal(10, autd.Link<Audit>().SilencerCompletionStepsIntensity(dev.Idx()));
            Assert.Equal(40, autd.Link<Audit>().SilencerCompletionStepsPhase(dev.Idx()));
            Assert.True(autd.Link<Audit>().SilencerFixedCompletionStepsMode(dev.Idx()));
        }

        Assert.Throws<AUTDException>(() => autd.Send(new GainSTM(gains: [new Null(), new Null()], config: new SamplingConfig(1), option: new GainSTMOption())));
        autd.Send(new Silencer(config: new FixedCompletionSteps() { StrictMode = false }));
        foreach (var dev in autd)
        {
            Assert.Equal(10, autd.Link<Audit>().SilencerCompletionStepsIntensity(dev.Idx()));
            Assert.Equal(40, autd.Link<Audit>().SilencerCompletionStepsPhase(dev.Idx()));
            Assert.True(autd.Link<Audit>().SilencerFixedCompletionStepsMode(dev.Idx()));
            Assert.False(autd.Link<Audit>().SilencerStrictMode(dev.Idx()));
        }
    }

    [Fact]
    public void TestSilencerSmallFreqDivFociSTM()
    {
        using var autd = CreateController();

        foreach (var dev in autd)
        {
            Assert.Equal(10, autd.Link<Audit>().SilencerCompletionStepsIntensity(dev.Idx()));
            Assert.Equal(40, autd.Link<Audit>().SilencerCompletionStepsPhase(dev.Idx()));
            Assert.True(autd.Link<Audit>().SilencerFixedCompletionStepsMode(dev.Idx()));
        }

        Assert.Throws<AUTDException>(() => autd.Send(new FociSTM(foci: [Point3.Origin, Point3.Origin], config: new SamplingConfig(1))));

        autd.Send(new Silencer(config: new FixedCompletionSteps() { StrictMode = false }));
        foreach (var dev in autd)
        {
            Assert.Equal(10, autd.Link<Audit>().SilencerCompletionStepsIntensity(dev.Idx()));
            Assert.Equal(40, autd.Link<Audit>().SilencerCompletionStepsPhase(dev.Idx()));
            Assert.True(autd.Link<Audit>().SilencerFixedCompletionStepsMode(dev.Idx()));
        }
    }
}
