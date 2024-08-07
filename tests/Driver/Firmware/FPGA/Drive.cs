namespace tests.Driver.Common;

public class DriveTest
{
    [Fact]
    public void DriveFromEmitIntensity()
    {
        Drive d = new EmitIntensity(0x80);
        Assert.Equal(0x80, d.Intensity.Value);
        Assert.Equal(0x00, d.Phase.Value);
    }

    [Fact]
    public void DriveFromPhase()
    {
        Drive d = new Phase(0x80);
        Assert.Equal(0xFF, d.Intensity.Value);
        Assert.Equal(0x80, d.Phase.Value);
    }
}
