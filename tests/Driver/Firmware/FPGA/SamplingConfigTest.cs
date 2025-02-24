namespace tests.Driver.Firmware.FPGA;

public class SamplingConfigTest
{
    [Fact]
    public void SamplingConfigFreq()
    {
        SamplingConfig c = 4000.0f * Hz;
        Assert.Equal(4000.0f * Hz, c.Freq());
        Assert.Equal(Duration.FromMicros(250), c.Period());
        Assert.Equal(10, c.Division);
    }

    [Fact]
    public void SamplingConfigFreqNearest()
    {
        var c = new SamplingConfig(4000.0f * Hz).IntoNearest();
        Assert.Equal(4000.0f * Hz, c.Freq());
        Assert.Equal(Duration.FromMicros(250), c.Period());
        Assert.Equal(10, c.Division);
    }

    [Fact]
    public void SamplingConfigPeriod()
    {
        SamplingConfig c = Duration.FromMicros(250);
        Assert.Equal(4000.0f * Hz, c.Freq());
        Assert.Equal(Duration.FromMicros(250), c.Period());
        Assert.Equal(10, c.Division);
    }

    [Fact]
    public void SamplingConfigPeriodNearest()
    {
        var c = new SamplingConfig(Duration.FromMicros(250)).IntoNearest();
        Assert.Equal(4000.0f * Hz, c.Freq());
        Assert.Equal(Duration.FromMicros(250), c.Period());
        Assert.Equal(10, c.Division);
    }

    [Fact]
    public void SamplingConfigFreqDivision()
    {
        var c = new SamplingConfig(10);
        Assert.Equal(4000.0f * Hz, c.Freq());
        Assert.Equal(Duration.FromMicros(250), c.Period());
        Assert.Equal(10, c.Division);
    }

    [Fact]
    public void Equals_SamplingConfig()
    {
        var m1 = new SamplingConfig(1);
        var m2 = new SamplingConfig(1);
        var m3 = new SamplingConfig(2);

        Assert.True(m1 == m2);
        Assert.True(m1 != m3);
        Assert.True(!m1.Equals(null));
        Assert.True(m1.Equals((object?)m2));
        Assert.True(!m1.Equals((object?)null));
    }
}
