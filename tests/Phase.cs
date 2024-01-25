
namespace tests;

public class PhaseTest
{
    [Fact]
    public void PhaseNew()
    {
        for (var i = 0; i <= 0xFF; i++)
        {
            var phase = new Phase((byte)i);
            Assert.Equal(i, phase.Value);
        }
    }

    [Fact]
    public void PhaseFromRad()
    {
        var phase = Phase.FromRad(0.0);
        Assert.Equal(0, phase.Value);
        Assert.Equal(0.0, phase.Radian);

        phase = Phase.FromRad(Math.PI);
        Assert.Equal(128, phase.Value);
        Assert.Equal(Math.PI, phase.Radian);

        phase = Phase.FromRad(2 * Math.PI);
        Assert.Equal(0, phase.Value);
        Assert.Equal(0, phase.Radian);
    }
}