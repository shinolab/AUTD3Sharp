namespace tests.Driver.Common;

using static AUTD3Sharp.Units;

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
}
