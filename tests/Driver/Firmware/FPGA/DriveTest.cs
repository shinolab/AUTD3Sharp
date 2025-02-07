namespace tests.Driver.Firmware.FPGA;

public class DriveTest
{
    [Fact]
    public void Equals_EmitIntensity()
    {
        var m1 = new Drive
        {
            Intensity = new EmitIntensity(1),
            Phase = new Phase(1),
        };
        var m2 = new Drive
        {
            Intensity = new EmitIntensity(1),
            Phase = new Phase(1),
        }; ;
        var m3 = new Drive
        {
            Intensity = new EmitIntensity(2),
            Phase = new Phase(1),
        };
        var m4 = new Drive
        {
            Intensity = new EmitIntensity(1),
            Phase = new Phase(2),
        };

        Assert.True(m1 == m2);
        Assert.True(m1 != m3);
        Assert.True(m1 != m4);
        Assert.True(!m1.Equals(null));
        Assert.True(m1.Equals((object?)m2));
    }
}
