using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Common;

public class SamplingConfigTest
{
    [Fact]
    public void SamplingConfigFreq()
    {
        var m = (SamplingConfigWrap)(SamplingConfig)(4000u * Hz);
        Assert.Equal(SamplingConfigTag.Freq, m.tag);
        Assert.Equal(4000u, m.value.freq);
    }

    [Fact]
    public void SamplingConfigFreqNearest()
    {
        var m = (SamplingConfigWrap)SamplingConfig.FreqNearest(4000.0f * Hz);
        Assert.Equal(SamplingConfigTag.FreqNearest, m.tag);
        Assert.Equal(4000.0f, m.value.freq_nearest);
    }

    [Fact]
    public void SamplingConfigFreqDivision()
    {
        var m = (SamplingConfigWrap)SamplingConfig.Division(5120u);
        Assert.Equal(SamplingConfigTag.Division, m.tag);
        Assert.Equal(5120u, m.value.div);
    }

    [Fact]
    public void SamplingConfigFreqDivisionRaw()
    {
        var m = (SamplingConfigWrap)SamplingConfig.DivisionRaw(5120u);
        Assert.Equal(SamplingConfigTag.DivisionRaw, m.tag);
        Assert.Equal(5120u, m.value.div);
    }
}
