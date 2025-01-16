using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;
using SamplingConfig = AUTD3Sharp.SamplingConfig;

namespace tests.Driver.Datagram;

public class SilencerTest
{
    [Fact]
    public void TestSilencerFromUpdateRate()
    {
        using var autd = CreateController();

        foreach (var dev in autd)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.Equal(AUTD3Sharp.SilencerTarget.Intensity, autd.Link.SilencerTarget(dev.Idx));
        }

        autd.Send(new Silencer(new FixedUpdateRate { Intensity = 1, Phase = 2 }).WithTarget(AUTD3Sharp.SilencerTarget.PulseWidth));
        foreach (var dev in autd)
        {
            Assert.Equal(1, autd.Link.SilencerUpdateRateIntensity(dev.Idx));
            Assert.Equal(2, autd.Link.SilencerUpdateRatePhase(dev.Idx));
            Assert.False(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.Equal(AUTD3Sharp.SilencerTarget.PulseWidth, autd.Link.SilencerTarget(dev.Idx));
        }

        Assert.Throws<NotImplementedException>(() => new Silencer(new FixedUpdateRate { Intensity = 1, Phase = 2 }).IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        Assert.Throws<NotImplementedException>(() => new Silencer(new FixedUpdateRate { Intensity = 1, Phase = 2 }).IsValid(new FociSTM(new SamplingConfig(1), [Point3.Origin, Point3.Origin])));
    }

    [Fact]
    public void TestSilencerFromCompletionTime()
    {
        using var autd = CreateController();

        var s = new Silencer();
        Assert.True(NativeMethodsBase.AUTDDatagramSilencerFixedCompletionStepsIsDefault(
            ((FixedCompletionSteps)s.Inner).Intensity,
            ((FixedCompletionSteps)s.Inner).Phase
            , s.StrictMode,
                s.Target));

        autd.Send(new Silencer(new FixedCompletionTime { Intensity = Duration.FromMicros(25), Phase = Duration.FromMicros(50) }).WithTarget(AUTD3Sharp.SilencerTarget.PulseWidth));
        foreach (var dev in autd)
        {
            Assert.Equal(1, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(2, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.True(autd.Link.SilencerStrictMode(dev.Idx));
            Assert.Equal(AUTD3Sharp.SilencerTarget.PulseWidth, autd.Link.SilencerTarget(dev.Idx));
        }

        Assert.False(new Silencer(new FixedCompletionTime { Intensity = Duration.FromMicros(50), Phase = Duration.FromMicros(25) }).IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        Assert.False(new Silencer(new FixedCompletionTime { Intensity = Duration.FromMicros(25), Phase = Duration.FromMicros(50) }).IsValid(new FociSTM(new SamplingConfig(1), [Point3.Origin, Point3.Origin])));
    }

    [Fact]
    public void TestSilencerLargeSteps()
    {
        using var autd = CreateController();

        autd.Send(Silencer.Disable());
        foreach (var dev in autd)
        {
            Assert.Equal(1, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(1, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.True(autd.Link.SilencerStrictMode(dev.Idx));
        }

        Assert.True(Silencer.Disable().IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        autd.Send(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1)));

        Assert.False(new Silencer(new FixedCompletionTime { Intensity = Duration.FromMicros(250), Phase = Duration.FromMicros(1000) }).IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        Assert.Throws<AUTDException>(() => autd.Send(new Silencer(new FixedCompletionTime { Intensity = Duration.FromMicros(250), Phase = Duration.FromMicros(1000) })));
    }

    [Fact]
    public void TestSilencerSmallFreqDivMod()
    {
        using var autd = CreateController();

        foreach (var dev in autd)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        Assert.False(new Silencer().IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        Assert.Throws<AUTDException>(() => autd.Send(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));

        Assert.True(new Silencer().WithStrictMode(false).IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        autd.Send(new Silencer().WithStrictMode(false));
        foreach (var dev in autd)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.False(autd.Link.SilencerStrictMode(dev.Idx));
        }

        autd.Send(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1)));
    }

    [Fact]
    public void TestSilencerSmallFreqDivGainSTM()
    {
        using var autd = CreateController();

        foreach (var dev in autd)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        Assert.False(new Silencer().IsValid(new GainSTM(new SamplingConfig(1), [new Null(), new Null()])));
        Assert.Throws<AUTDException>(() => autd.Send(new GainSTM(new SamplingConfig(1), [new Null(), new Null()])));

        autd.Send(new Silencer(new FixedCompletionTime { Intensity = Duration.FromMicros(250), Phase = Duration.FromMicros(1000) }).WithStrictMode(false));
        foreach (var dev in autd)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.False(autd.Link.SilencerStrictMode(dev.Idx));
        }
        autd.Send(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1)));

        autd.Send(new GainSTM(new SamplingConfig(1), [new Null(), new Null()]));
    }



    [Fact]
    public void TestSilencerSmallFreqDivFociSTM()
    {
        using var autd = CreateController();

        foreach (var dev in autd)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        Assert.False(new Silencer().IsValid(new FociSTM(new SamplingConfig(1), [Point3.Origin, Point3.Origin])));
        Assert.Throws<AUTDException>(() => autd.Send(new FociSTM(new SamplingConfig(1), [Point3.Origin, Point3.Origin])));

        autd.Send(new Silencer(new FixedCompletionTime { Intensity = Duration.FromMicros(250), Phase = Duration.FromMicros(1000) }).WithStrictMode(false));
        foreach (var dev in autd)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }
        autd.Send(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1)));

        autd.Send(new FociSTM(new SamplingConfig(1), [Point3.Origin, Point3.Origin]));
    }
}
