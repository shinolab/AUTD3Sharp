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
        }

        await autd.SendAsync(Silencer.FromUpdateRate(1, 2));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(1, autd.Link.SilencerUpdateRateIntensity(dev.Idx));
            Assert.Equal(2, autd.Link.SilencerUpdateRatePhase(dev.Idx));
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

        await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1)));

        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(Silencer.FromCompletionTime(TimeSpan.FromMicroseconds(250), TimeSpan.FromMicroseconds(1000))));
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

        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(512))));

        await autd.SendAsync(Silencer.FromCompletionTime(TimeSpan.FromMicroseconds(250), TimeSpan.FromMicroseconds(1000)).WithStrictMode(false));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }

        await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1)));
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

        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new GainSTM(new SamplingConfig(1), [new Null(), new Null()])));

        await autd.SendAsync(Silencer.FromCompletionTime(TimeSpan.FromMicroseconds(250), TimeSpan.FromMicroseconds(1000)).WithStrictMode(false));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(40, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
        }
        await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1)));

        await autd.SendAsync(new GainSTM(new SamplingConfig(1), [new Null(), new Null()]));
    }
}
