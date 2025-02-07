namespace tests.Driver.Datagram.STM;

public class STMSamplingConfigTest
{
    [Fact]
    public void IntoNearest_Freq()
    {
        var c = (STMSamplingConfig)(1.0f * Hz);
        Assert.Equal(STMSamplingConfigTag.Freq, c._tag);

        var cn = c.IntoNearest();
        Assert.Equal(STMSamplingConfigTag.FreqNearest, cn._tag);

        var cn2 = cn.IntoNearest();
        Assert.Equal(STMSamplingConfigTag.FreqNearest, cn2._tag);
    }

    [Fact]
    public void IntoNearest_Period()
    {
        var c = (STMSamplingConfig)(Duration.FromMillis(1));
        Assert.Equal(STMSamplingConfigTag.Period, c._tag);

        var cn = c.IntoNearest();
        Assert.Equal(STMSamplingConfigTag.PeriodNearest, cn._tag);

        var cn2 = cn.IntoNearest();
        Assert.Equal(STMSamplingConfigTag.PeriodNearest, cn2._tag);
    }

    [Fact]
    public void IntoNearest_Config()
    {
        var c = (STMSamplingConfig)(new SamplingConfig(1));
        Assert.Equal(STMSamplingConfigTag.Config, c._tag);

        var cn = c.IntoNearest();
        Assert.Equal(STMSamplingConfigTag.Config, cn._tag);
    }
}
