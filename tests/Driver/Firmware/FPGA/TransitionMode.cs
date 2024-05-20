using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Common;

public class TransitionModeTest
{
    [Fact]
    public void TransitionModeSyncIdx()
    {
        var m = TransitionMode.SyncIdx;
        Assert.Equal(0, m.ty);
        Assert.Equal(0ul, m.value);
    }

    [Fact]
    public void TransitionModeSysTime()
    {
        var now = DcSysTime.Now;
        var m = TransitionMode.SysTime(now);
        Assert.Equal(1, m.ty);
        Assert.Equal(now.SysTime, m.value);
    }

    [Fact]
    public void TransitionModeGPIO()
    {
        var m = TransitionMode.GPIO(GPIOIn.I1);
        Assert.Equal(2, m.ty);
        Assert.Equal(1ul, m.value);
    }

    [Fact]
    public void TransitionModeExt()
    {
        var m = TransitionMode.Ext;
        Assert.Equal(0xF0, m.ty);
        Assert.Equal(0ul, m.value);
    }

    [Fact]
    public void TransitionModeImmediate()
    {
        var m = TransitionMode.Immediate;
        Assert.Equal(0xFF, m.ty);
        Assert.Equal(0ul, m.value);
    }
}
