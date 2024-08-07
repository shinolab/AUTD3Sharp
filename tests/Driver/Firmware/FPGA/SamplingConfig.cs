namespace tests.Driver.Common;

public class SamplingConfigTest
{
    [Fact]
    public void SamplingConfigFreq()
    {
        SamplingConfig c = 4000u * Hz;
        Assert.Equal(4000.0f * Hz, c.Freq);
        Assert.Equal(TimeSpan.FromMicroseconds(250), c.Period);
        Assert.Equal(10, c.Division);
    }

    [Fact]
    public void SamplingConfigFreqNearest()
    {
        var c = SamplingConfig.Nearest(4000.0f * Hz);
        Assert.Equal(4000.0f * Hz, c.Freq);
        Assert.Equal(TimeSpan.FromMicroseconds(250), c.Period);
        Assert.Equal(10, c.Division);
    }

    [Fact]
    public void SamplingConfigPeriod()
    {
        SamplingConfig c = TimeSpan.FromMicroseconds(250);
        Assert.Equal(4000.0f * Hz, c.Freq);
        Assert.Equal(TimeSpan.FromMicroseconds(250), c.Period);
        Assert.Equal(10, c.Division);
    }

    [Fact]
    public void SamplingConfigPeriodNearest()
    {
        var c = SamplingConfig.Nearest(TimeSpan.FromMicroseconds(250));
        Assert.Equal(4000.0f * Hz, c.Freq);
        Assert.Equal(TimeSpan.FromMicroseconds(250), c.Period);
        Assert.Equal(10, c.Division);
    }

    [Fact]
    public void SamplingConfigFreqDivision()
    {
        var c = new SamplingConfig(10);
        Assert.Equal(4000.0f * Hz, c.Freq);
        Assert.Equal(TimeSpan.FromMicroseconds(250), c.Period);
        Assert.Equal(10, c.Division);
    }
}
