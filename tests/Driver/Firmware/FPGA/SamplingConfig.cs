using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Common;

public class SamplingConfigTest
{
    [Fact]
    public void SamplingConfigFreq()
    {
        SamplingConfig c = 4000u * Hz;
        Assert.Equal(4000.0f * Hz, c.Freq);
        Assert.Equal(TimeSpan.FromMilliseconds(250e-3), c.Period);
        Assert.Equal(5120u, c.Div);

        var m = (SamplingConfigWrap)c;
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
    public void SamplingConfigPeriod()
    {
        var m = (SamplingConfigWrap)(SamplingConfig)(TimeSpan.FromMilliseconds(250e-3));
        Assert.Equal(SamplingConfigTag.Period, m.tag);
        Assert.Equal(250000ul, m.value.period_ns);
    }

    [Fact]
    public void SamplingConfigPeriodNearest()
    {
        var m = (SamplingConfigWrap)SamplingConfig.PeriodNearest(TimeSpan.FromMilliseconds(25e-3));
        Assert.Equal(SamplingConfigTag.PeriodNearest, m.tag);
        Assert.Equal(25000ul, m.value.period_ns);
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
