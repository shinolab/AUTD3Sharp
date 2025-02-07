using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Firmware.FPGA;

public class TransitionModeTest
{
    [Fact]
    public void Constructor_SyncIdx()
    {
        var m = TransitionMode.SyncIdx.Inner;
        Assert.Equal(TransitionModeTag.SyncIdx, m.tag);
        Assert.Equal(0ul, m.value.@null);
    }

    [Fact]
    public void Constructor_SysTime()
    {
        var now = DcSysTime.Now;
        var m = TransitionMode.SysTime(now).Inner;
        Assert.Equal(TransitionModeTag.SysTime, m.tag);
        Assert.Equal(now.SysTime, m.value.sys_time);
    }

    [Fact]
    public void Constructor_GPIO()
    {
        var m = TransitionMode.GPIO(AUTD3Sharp.GPIOIn.I1).Inner;
        Assert.Equal(TransitionModeTag.Gpio, m.tag);
        Assert.Equal((byte)AUTD3Sharp.GPIOIn.I1.ToNative(), m.value.gpio_in);
    }

    [Fact]
    public void Constructor_Ext()
    {
        var m = TransitionMode.Ext.Inner;
        Assert.Equal(TransitionModeTag.Ext, m.tag);
        Assert.Equal(0ul, m.value.@null);
    }

    [Fact]
    public void Constructor_Immediate()
    {
        var m = TransitionMode.Immediate.Inner;
        Assert.Equal(TransitionModeTag.Immediate, m.tag);
        Assert.Equal(0ul, m.value.@null);
    }

    [Fact]
    public void Equals_TransitionMode()
    {
        var m1 = TransitionMode.SyncIdx;
        var m2 = TransitionMode.SyncIdx;
        var m3 = TransitionMode.SysTime(DcSysTime.Now);
        var m4 = TransitionMode.GPIO(AUTD3Sharp.GPIOIn.I1);
        var m5 = TransitionMode.Ext;
        var m6 = TransitionMode.Immediate;

        Assert.True(m1 == m2);
        Assert.True(m1 != m3);
        Assert.True(m1 != m4);
        Assert.True(m1 != m5);
        Assert.True(m1 != m6);
        Assert.True(!m1.Equals(null));
        Assert.True(m1.Equals((object?)m2));
        Assert.True(!m1.Equals((object?)null));
    }
}
