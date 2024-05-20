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
}
