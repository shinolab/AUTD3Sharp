namespace tests.Driver.Firmware.FPGA;

public class LoopBehaviorTest
{
    [Fact]
    public void TestLoopBehavior()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = LoopBehavior.Finite(0));

        Assert.Equal(LoopBehavior.Once, LoopBehavior.Finite(1));
        Assert.NotEqual(LoopBehavior.Once, LoopBehavior.Infinite);
    }

    [Fact]
    public void Equals_LoopBehavior()
    {
        var m1 = LoopBehavior.Once;
        var m2 = LoopBehavior.Finite(1);
        var m3 = LoopBehavior.Infinite;

        Assert.True(m1 == m2);
        Assert.True(m1 != m3);
        Assert.True(!m1.Equals(null));
        Assert.True(m1.Equals((object?)m2));
        Assert.True(!m1.Equals((object?)null));
    }
}
