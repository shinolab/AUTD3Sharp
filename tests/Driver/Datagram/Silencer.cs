using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Datagram;

public class SilencerTest
{
    [Fact]
    public async Task TestSilencerFixedCompletionSteps()
    {
        using var autd = await CreateController();

        Assert.True(NativeMethodsBase.AUTDDatagramSilencerFixedCompletionStepsIsDefault(((IDatagram)Silencer.Default()).Ptr(autd.Geometry)));

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        await autd.SendAsync(Silencer.FixedCompletionSteps(2, 3));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(2, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(3, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        await autd.SendAsync(Silencer.Disable());
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(1, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(1, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        await autd.SendAsync(Silencer.Default());
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

        await autd.SendAsync(Silencer.FixedUpdateRate(256, 257));
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

        await autd.SendAsync(Silencer.Disable());
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(1, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(1, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(SamplingConfig.Division(512)));

        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(Silencer.FixedCompletionSteps(10, 40)));
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

        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(SamplingConfig.Division(512))));

        await autd.SendAsync(Silencer.FixedCompletionSteps(10, 40).WithStrictMode(false));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(SamplingConfig.Division(512)));
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

        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(GainSTM.FromSamplingConfig(SamplingConfig.Division(512)).AddGain(new Null()).AddGain(new Null())));

        await autd.SendAsync(Silencer.FixedCompletionSteps(10, 40).WithStrictMode(false));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }
        await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(SamplingConfig.Division(512)));

        await autd.SendAsync(GainSTM.FromSamplingConfig(SamplingConfig.Division(512)).AddGain(new Null()).AddGain(new Null()));
    }
}
