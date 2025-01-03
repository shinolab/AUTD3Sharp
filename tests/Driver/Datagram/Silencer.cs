using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;
using SamplingConfig = AUTD3Sharp.SamplingConfig;

namespace tests.Driver.Datagram;

public class SilencerTest
{
    [Fact]
    public async Task TestSilencerFromUpdateRate()
    {
        using var autd = await CreateController();

        foreach (var dev in autd)
        {
            Assert.Equal(250ul, autd.Link.SilencerCompletionStepsIntensity(dev.Idx).AsMicros());
            Assert.Equal(1000ul, autd.Link.SilencerCompletionStepsPhase(dev.Idx).AsMicros());
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.Equal(AUTD3Sharp.SilencerTarget.Intensity, autd.Link.SilencerTarget(dev.Idx));
        }

        await autd.SendAsync(new Silencer(new FixedUpdateRate { Intensity = 1, Phase = 2 }).WithTarget(AUTD3Sharp.SilencerTarget.PulseWidth));
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
    public async Task TestSilencerFromCompletionTime()
    {
        using var autd = await CreateController();

        var s = new Silencer();
        Assert.True(NativeMethodsBase.AUTDDatagramSilencerFixedCompletionTimeIsDefault(
            ((FixedCompletionTime)s.Inner).Intensity,
            ((FixedCompletionTime)s.Inner).Phase
            , s.StrictMode,
                s.Target));

        await autd.SendAsync(new Silencer(new FixedCompletionTime { Intensity = Duration.FromMicros(25), Phase = Duration.FromMicros(50) }).WithTarget(AUTD3Sharp.SilencerTarget.PulseWidth));
        foreach (var dev in autd)
        {
            Assert.Equal(25ul, autd.Link.SilencerCompletionStepsIntensity(dev.Idx).AsMicros());
            Assert.Equal(50ul, autd.Link.SilencerCompletionStepsPhase(dev.Idx).AsMicros());
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.True(autd.Link.SilencerStrictMode(dev.Idx));
            Assert.Equal(AUTD3Sharp.SilencerTarget.PulseWidth, autd.Link.SilencerTarget(dev.Idx));
        }
    }

    [Fact]
    public async Task TestSilencerLargeSteps()
    {
        using var autd = await CreateController();

        await autd.SendAsync(Silencer.Disable());
        foreach (var dev in autd)
        {
            Assert.Equal(25ul, autd.Link.SilencerCompletionStepsIntensity(dev.Idx).AsMicros());
            Assert.Equal(25ul, autd.Link.SilencerCompletionStepsPhase(dev.Idx).AsMicros());
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.True(autd.Link.SilencerStrictMode(dev.Idx));
        }

        Assert.True(Silencer.Disable().IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1)));

        Assert.False(new Silencer(new FixedCompletionTime { Intensity = Duration.FromMicros(250), Phase = Duration.FromMicros(1000) }).IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Silencer(new FixedCompletionTime { Intensity = Duration.FromMicros(250), Phase = Duration.FromMicros(1000) })));
    }

    [Fact]
    public async Task TestSilencerSmallFreqDivMod()
    {
        using var autd = await CreateController();

        foreach (var dev in autd)
        {
            Assert.Equal(250ul, autd.Link.SilencerCompletionStepsIntensity(dev.Idx).AsMicros());
            Assert.Equal(1000ul, autd.Link.SilencerCompletionStepsPhase(dev.Idx).AsMicros());
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        Assert.False(new Silencer().IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));

        Assert.True(new Silencer().WithStrictMode(false).IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        await autd.SendAsync(new Silencer().WithStrictMode(false));
        foreach (var dev in autd)
        {
            Assert.Equal(250ul, autd.Link.SilencerCompletionStepsIntensity(dev.Idx).AsMicros());
            Assert.Equal(1000ul, autd.Link.SilencerCompletionStepsPhase(dev.Idx).AsMicros());
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.False(autd.Link.SilencerStrictMode(dev.Idx));
        }

        await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1)));
    }

    [Fact]
    public async Task TestSilencerSmallFreqDivGainSTM()
    {
        using var autd = await CreateController();

        foreach (var dev in autd)
        {
            Assert.Equal(250ul, autd.Link.SilencerCompletionStepsIntensity(dev.Idx).AsMicros());
            Assert.Equal(1000ul, autd.Link.SilencerCompletionStepsPhase(dev.Idx).AsMicros());
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        Assert.False(new Silencer().IsValid(new GainSTM(new SamplingConfig(1), [new Null(), new Null()])));
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new GainSTM(new SamplingConfig(1), [new Null(), new Null()])));

        await autd.SendAsync(new Silencer(new FixedCompletionTime { Intensity = Duration.FromMicros(250), Phase = Duration.FromMicros(1000) }).WithStrictMode(false));
        foreach (var dev in autd)
        {
            Assert.Equal(250ul, autd.Link.SilencerCompletionStepsIntensity(dev.Idx).AsMicros());
            Assert.Equal(1000ul, autd.Link.SilencerCompletionStepsPhase(dev.Idx).AsMicros());
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.False(autd.Link.SilencerStrictMode(dev.Idx));
        }
        await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1)));

        await autd.SendAsync(new GainSTM(new SamplingConfig(1), [new Null(), new Null()]));
    }



    [Fact]
    public async Task TestSilencerSmallFreqDivFociSTM()
    {
        using var autd = await CreateController();

        foreach (var dev in autd)
        {
            Assert.Equal(250ul, autd.Link.SilencerCompletionStepsIntensity(dev.Idx).AsMicros());
            Assert.Equal(1000ul, autd.Link.SilencerCompletionStepsPhase(dev.Idx).AsMicros());
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        Assert.False(new Silencer().IsValid(new FociSTM(new SamplingConfig(1), [Point3.Origin, Point3.Origin])));
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new FociSTM(new SamplingConfig(1), [Point3.Origin, Point3.Origin])));

        await autd.SendAsync(new Silencer(new FixedCompletionTime { Intensity = Duration.FromMicros(250), Phase = Duration.FromMicros(1000) }).WithStrictMode(false));
        foreach (var dev in autd)
        {
            Assert.Equal(250ul, autd.Link.SilencerCompletionStepsIntensity(dev.Idx).AsMicros());
            Assert.Equal(1000ul, autd.Link.SilencerCompletionStepsPhase(dev.Idx).AsMicros());
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }
        await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1)));

        await autd.SendAsync(new FociSTM(new SamplingConfig(1), [Point3.Origin, Point3.Origin]));
    }
}
