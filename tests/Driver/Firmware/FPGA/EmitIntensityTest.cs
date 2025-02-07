namespace tests.Driver.Firmware.FPGA;

public class EmitIntensityTest
{
    [Fact]
    public void EmitIntensityNew()
    {
        for (var i = 0; i <= 0xFF; i++)
        {
            var intensity = new EmitIntensity((byte)i);
            Assert.Equal(i, intensity.Item1);
        }
    }


    [Fact]
    public void EmitIntensityDiv()
    {
        var intensity = new EmitIntensity(0x80);
        var div = intensity / 2;
        Assert.Equal(0x40, div.Item1);
    }

    [Fact]
    public void Equals_EmitIntensity()
    {
        var m1 = new EmitIntensity(1);
        var m2 = new EmitIntensity(1);
        var m3 = new EmitIntensity(2);

        Assert.True(m1 == m2);
        Assert.True(m1 != m3);
        Assert.True(!m1.Equals(null));
        Assert.True(m1.Equals((object?)m2));
        Assert.True(!m1.Equals((object?)null));
    }
}
