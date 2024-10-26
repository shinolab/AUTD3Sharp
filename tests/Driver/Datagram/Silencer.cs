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

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.Equal(AUTD3Sharp.SilencerTarget.Intensity, autd.Link.SilencerTarget(dev.Idx));
        }

        await autd.SendAsync(new Silencer(new FixedUpdateRate { Intensity = 1, Phase = 2 }).WithTarget(AUTD3Sharp.SilencerTarget.PulseWidth));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(1, autd.Link.SilencerUpdateRateIntensity(dev.Idx));
            Assert.Equal(2, autd.Link.SilencerUpdateRatePhase(dev.Idx));
            Assert.False(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.Equal(AUTD3Sharp.SilencerTarget.PulseWidth, autd.Link.SilencerTarget(dev.Idx));
        }

        Assert.True(new Silencer(new FixedUpdateRate { Intensity = 1, Phase = 2 }).IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        Assert.True(new Silencer(new FixedUpdateRate { Intensity = 1, Phase = 2 }).IsValid(new FociSTM(new SamplingConfig(1), [Vector3.Zero, Vector3.Zero])));
    }

    [Fact]
    public async Task TestSilencerFromCompletionTime()
    {
        using var autd = await CreateController();

        var s = new Silencer();
        Assert.True(NativeMethodsBase.AUTDDatagramSilencerFixedCompletionTimeIsDefault(
            (ulong)((FixedCompletionTime)s.Inner).Intensity.TotalNanoseconds,
            (ulong)((FixedCompletionTime)s.Inner).Phase.TotalNanoseconds
            , s.StrictMode,
                s.Target));

        await autd.SendAsync(new Silencer(new FixedCompletionTime { Intensity = TimeSpan.FromMicroseconds(25), Phase = TimeSpan.FromMicroseconds(50) }).WithTarget(AUTD3Sharp.SilencerTarget.PulseWidth));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(1, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(2, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
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
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(1, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(1, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.True(autd.Link.SilencerStrictMode(dev.Idx));
        }

        Assert.True(Silencer.Disable().IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1)));

        Assert.False(new Silencer(new FixedCompletionTime { Intensity = TimeSpan.FromMicroseconds(250), Phase = TimeSpan.FromMicroseconds(1000) }).IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Silencer(new FixedCompletionTime { Intensity = TimeSpan.FromMicroseconds(250), Phase = TimeSpan.FromMicroseconds(1000) })));
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

        Assert.False(new Silencer().IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));

        Assert.True(new Silencer().WithStrictMode(false).IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        await autd.SendAsync(new Silencer().WithStrictMode(false));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.False(autd.Link.SilencerStrictMode(dev.Idx));
        }

        await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1)));
    }

    [Fact]
    public async Task TestSilencerSmallFreqDivGainSTM()
    {
        using var autd = await CreateController();

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        Assert.False(new Silencer().IsValid(new GainSTM(new SamplingConfig(1), [new Null(), new Null()])));
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new GainSTM(new SamplingConfig(1), [new Null(), new Null()])));

        await autd.SendAsync(new Silencer(new FixedCompletionTime { Intensity = TimeSpan.FromMicroseconds(250), Phase = TimeSpan.FromMicroseconds(1000) }).WithStrictMode(false));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
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

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        Assert.False(new Silencer().IsValid(new FociSTM(new SamplingConfig(1), [Vector3.Zero, Vector3.Zero])));
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new FociSTM(new SamplingConfig(1), [Vector3.Zero, Vector3.Zero])));

        await autd.SendAsync(new Silencer(new FixedCompletionTime { Intensity = TimeSpan.FromMicroseconds(250), Phase = TimeSpan.FromMicroseconds(1000) }).WithStrictMode(false));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }
        await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1)));

        await autd.SendAsync(new FociSTM(new SamplingConfig(1), [Vector3.Zero, Vector3.Zero]));
    }
}
