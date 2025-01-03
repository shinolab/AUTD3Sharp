using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Common;

public class TransitionModeTest
{
    [Fact]
    public void TransitionModeSyncIdx()
    {
        var m = TransitionMode.SyncIdx;
        Assert.Equal(TransitionModeTag.SyncIdx, m.tag);
        Assert.Equal(0ul, m.value.@null);
    }

    [Fact]
    public void TransitionModeSysTime()
    {
        var now = DcSysTime.Now;
        var m = TransitionMode.SysTime(now);
        Assert.Equal(TransitionModeTag.SysTime, m.tag);
        Assert.Equal(now.SysTime, m.value.sys_time);
    }

    [Fact]
    public void TransitionModeGPIO()
    {
        var m = TransitionMode.GPIO(AUTD3Sharp.GPIOIn.I1);
        Assert.Equal(TransitionModeTag.Gpio, m.tag);
        Assert.Equal((byte)AUTD3Sharp.GPIOIn.I1, m.value.gpio_in);
    }

    [Fact]
    public void TransitionModeExt()
    {
        var m = TransitionMode.Ext;
        Assert.Equal(TransitionModeTag.Ext, m.tag);
        Assert.Equal(0ul, m.value.@null);
    }

    [Fact]
    public void TransitionModeImmediate()
    {
        var m = TransitionMode.Immediate;
        Assert.Equal(TransitionModeTag.Immediate, m.tag);
        Assert.Equal(0ul, m.value.@null);
    }
}
