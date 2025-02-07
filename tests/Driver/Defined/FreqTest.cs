namespace tests.Driver.Defined;

public class FreqTest
{
    [Fact]
    public void FreqkHzIntTest()
    {
        var f = 1 * kHz;
        Assert.Equal(1000u, f.Hz);
        Assert.Equal("1000Hz", f.ToString());
    }

    [Fact]
    public void FreqkHzFloatTest()
    {
        var f = 1.0f * kHz;
        Assert.Equal(1000.0f, f.Hz);
        Assert.Equal("1000Hz", f.ToString());
    }

    [Fact]
    public void Equals_Freq()
    {
        var m1 = 1000 * Hz;
        var m2 = 1 * kHz;
        var m3 = 1 * Hz;

        Assert.True(m1 == m2);
        Assert.True(m1 != m3);
        Assert.True(!m1.Equals(null));
        Assert.True(m1.Equals((object?)m2));
        Assert.True(!m1.Equals((object?)null));
    }
}
