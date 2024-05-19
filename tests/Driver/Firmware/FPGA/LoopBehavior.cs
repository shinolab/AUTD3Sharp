namespace tests.Driver.Common;

public class LoopBehaviorTest
{
    [Fact]
    public void TestLoopBehavior()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = LoopBehavior.Finite(0));

        Assert.Equal(LoopBehavior.Once, LoopBehavior.Finite(1));
        Assert.NotEqual(LoopBehavior.Once, LoopBehavior.Infinite);
    }
}