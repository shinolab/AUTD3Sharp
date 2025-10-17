using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Firmware.FPGA;

public class TransitionModeTest
{
    [Fact]
    public void Constructor_SyncIdx()
    {
        var m = ((ITransitionMode)new AUTD3Sharp.TransitionMode.SyncIdx()).Inner;
        Assert.Equal(TransitionModeTag.SyncIdx, m.tag);
        Assert.Equal(0ul, m.value.@null);
    }

    [Fact]
    public void Constructor_SysTime()
    {
        var now = new DcSysTime(1000);
        var m = ((ITransitionMode)new AUTD3Sharp.TransitionMode.SysTime(now)).Inner;
        Assert.Equal(TransitionModeTag.SysTime, m.tag);
        Assert.Equal(now.SysTime, m.value.sys_time);
    }

    [Fact]
    public void Constructor_GPIO()
    {
        var m = ((ITransitionMode)new AUTD3Sharp.TransitionMode.GPIO(AUTD3Sharp.GPIOIn.I1)).Inner;
        Assert.Equal(TransitionModeTag.Gpio, m.tag);
        Assert.Equal((byte)AUTD3Sharp.GPIOIn.I1.ToNative(), m.value.gpio_in);
    }

    [Fact]
    public void Constructor_Ext()
    {
        var m = ((ITransitionMode)new AUTD3Sharp.TransitionMode.Ext()).Inner;
        Assert.Equal(TransitionModeTag.Ext, m.tag);
        Assert.Equal(0ul, m.value.@null);
    }

    [Fact]
    public void Constructor_Immediate()
    {
        var m = ((ITransitionMode)new AUTD3Sharp.TransitionMode.Immediate()).Inner;
        Assert.Equal(TransitionModeTag.Immediate, m.tag);
        Assert.Equal(0ul, m.value.@null);
    }
}
