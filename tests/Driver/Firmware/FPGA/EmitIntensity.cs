namespace tests.Driver.Common;

public class EmitIntensityTest
{
    [Fact]
    public void EmitIntensityNew()
    {
        for (var i = 0; i <= 0xFF; i++)
        {
            var intensity = new EmitIntensity((byte)i);
            Assert.Equal(i, intensity.Value);
        }
    }


    [Fact]
    public void EmitIntensityDiv()
    {
        var intensity = new EmitIntensity(0x80);
        var div = intensity / 2;
        Assert.Equal(0x40, div.Value);
    }
}
