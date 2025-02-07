using AUTD3Sharp.Driver.FPGA.Defined;

namespace tests.Driver.Firmware.FPGA;

public class FPGAStateTest
{
    [Fact]
    public void Equals_FPGAState()
    {
        var m1 = new FPGAState(1);
        var m2 = new FPGAState(1);
        var m3 = new FPGAState(2);

        Assert.True(m1 == m2);
        Assert.True(m1 != m3);
        Assert.True(!m1.Equals(null));
        Assert.True(m1.Equals((object?)m2));
        Assert.True(!m1.Equals((object?)null));
    }
}