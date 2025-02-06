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
}
