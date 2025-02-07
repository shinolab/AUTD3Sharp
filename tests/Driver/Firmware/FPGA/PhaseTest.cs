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

    [Fact]
    public void Equals_Phase()
    {
        var m1 = new Phase(1);
        var m2 = new Phase(1);
        var m3 = new Phase(2);

        Assert.True(m1 == m2);
        Assert.True(m1 != m3);
        Assert.True(!m1.Equals(null));
        Assert.True(m1.Equals((object?)m2));
        Assert.True(!m1.Equals((object?)null));
    }
}
