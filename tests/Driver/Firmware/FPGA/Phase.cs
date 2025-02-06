namespace tests.Driver.Firmware.FPGA;

public class PhaseTest
{
    [Fact]
    public void PhaseNew()
    {
        for (var i = 0; i <= 0xFF; i++)
        {
            var phase = new Phase((byte)i);
            Assert.Equal(i, phase.Item1);
        }
    }

    [Fact]
    public void PhaseFromRad()
    {
        var phase = new Phase(0.0f * rad);
        Assert.Equal(0, phase.Item1);
        Assert.Equal(0.0f, phase.Radian());

        phase = new Phase(MathF.PI * rad);
        Assert.Equal(128, phase.Item1);
        Assert.Equal(MathF.PI, phase.Radian());

        phase = new Phase(2 * MathF.PI * rad);
        Assert.Equal(0, phase.Item1);
        Assert.Equal(0, phase.Radian());
    }
}
