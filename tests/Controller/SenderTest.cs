using AUTD3Sharp.NativeMethods;
using SpinStrategyTag = AUTD3Sharp.SpinStrategyTag;

namespace tests.Controller;

public class SenderTest
{
    [Fact]
    public void ToNative_StdSleeper()
    {
        var s = new StdSleeper();
        var sn = ((ISleeper)s).ToNative();
        Assert.Equal(SleeperTag.Std, sn.tag);
    }

    [Fact]
    public void ToNative_SpinSleeper()
    {
        var s = new SpinSleeper()
        {
            NativeAccuracyNs = 700_000,
            SpinStrategy = SpinStrategyTag.SpinLoopHint
        };
        var sn = ((ISleeper)s).ToNative();
        Assert.Equal(SleeperTag.Spin, sn.tag);
        Assert.Equal(700_000u, sn.value);
        Assert.Equal(AUTD3Sharp.NativeMethods.SpinStrategyTag.SpinLoopHint, sn.spin_strategy);
    }

    [Fact]
    public void ToNative_SpinWaitSleeper()
    {
        var s = new SpinWaitSleeper();
        var sn = ((ISleeper)s).ToNative();
        Assert.Equal(SleeperTag.SpinWait, sn.tag);
    }

}
