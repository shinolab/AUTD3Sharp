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
            Assert.Equal(AUTD3Sharp.SilencerTarget.Intensity.Into(), autd.Link.SilencerTarget(dev.Idx));
        }

        await autd.SendAsync(Silencer.FromUpdateRate(1, 2).WithTarget(AUTD3Sharp.SilencerTarget.PulseWidth));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(1, autd.Link.SilencerUpdateRateIntensity(dev.Idx));
            Assert.Equal(2, autd.Link.SilencerUpdateRatePhase(dev.Idx));
            Assert.False(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.Equal(AUTD3Sharp.SilencerTarget.PulseWidth.Into(), autd.Link.SilencerTarget(dev.Idx));
        }

        Assert.True(Silencer.FromUpdateRate(1, 2).IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
    }

    [Fact]
    public async Task TestSilencerFromCompletionTime()
    {
        using var autd = await CreateController();

#pragma warning disable CS8602, CS8605
        var s = Silencer.Default();
        Assert.True(NativeMethodsBase.AUTDDatagramSilencerFixedCompletionTimeIsDefault((AUTD3Sharp.NativeMethods.DatagramPtr)typeof(SilencerFixedCompletionTime).GetMethod("RawPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(s, [])));
#pragma warning restore CS8602, CS8605

        await autd.SendAsync(Silencer.FromCompletionTime(TimeSpan.FromMicroseconds(25), TimeSpan.FromMicroseconds(50)).WithTarget(AUTD3Sharp.SilencerTarget.PulseWidth));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(1, autd.Link.SilencerCompletionStepsIntensity(dev.Idx));
            Assert.Equal(2, autd.Link.SilencerCompletionStepsPhase(dev.Idx));
            Assert.True(autd.Link.SilencerFixedCompletionStepsMode(dev.Idx));
            Assert.True(autd.Link.SilencerStrictMode(dev.Idx));
            Assert.Equal(AUTD3Sharp.SilencerTarget.PulseWidth.Into(), autd.Link.SilencerTarget(dev.Idx));
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

        Assert.False(Silencer.FromCompletionTime(TimeSpan.FromMicroseconds(250), TimeSpan.FromMicroseconds(1000)).IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
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

        Assert.False(Silencer.Default().IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));

        Assert.True(Silencer.Default().WithStrictMode(false).IsValid(new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(1))));
        await autd.SendAsync(Silencer.Default().WithStrictMode(false));
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

        Assert.False(Silencer.Default().IsValid(new GainSTM(new SamplingConfig(1), [new Null(), new Null()])));
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new GainSTM(new SamplingConfig(1), [new Null(), new Null()])));

        await autd.SendAsync(Silencer.FromCompletionTime(TimeSpan.FromMicroseconds(250), TimeSpan.FromMicroseconds(1000)).WithStrictMode(false));
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

        Assert.False(Silencer.Default().IsValid(new FociSTM(new SamplingConfig(1), [Vector3.Zero, Vector3.Zero])));
        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new FociSTM(new SamplingConfig(1), [Vector3.Zero, Vector3.Zero])));

        await autd.SendAsync(Silencer.FromCompletionTime(TimeSpan.FromMicroseconds(250), TimeSpan.FromMicroseconds(1000)).WithStrictMode(false));
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
