namespace tests.Driver.Firmware.FPGA;

public class IntensityTest
{
    [Fact]
    public void IntensityNew()
    {
        for (var i = 0; i <= 0xFF; i++)
        {
            var intensity = new Intensity((byte)i);
            Assert.Equal(i, intensity.Item1);
        }
    }


    [Fact]
    public void IntensityDiv()
    {
        var intensity = new Intensity(0x80);
        var div = intensity / 2;
        Assert.Equal(0x40, div.Item1);
    }

    [Fact]
    public void Equals_Intensity()
    {
        var m1 = new Intensity(1);
        var m2 = new Intensity(1);
        var m3 = new Intensity(2);

        Assert.True(m1 == m2);
        Assert.True(m1 != m3);
        Assert.True(!m1.Equals(null));
        Assert.True(m1.Equals((object?)m2));
        Assert.True(!m1.Equals((object?)null));
    }
}
