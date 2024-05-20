namespace tests.Driver.Common;

public class DcSysTimeTest
{
    [Fact]
    public void DcSysTimeNow()
    {
        var now = DcSysTime.Now;
        Assert.True(0 < now.SysTime);
    }

    [Fact]
    public void DcSysTimeAdd()
    {
        var now = DcSysTime.Now;
        Assert.Equal(now.SysTime + 1ul * 1000 * 1000, (now + TimeSpan.FromMilliseconds(1)).SysTime);
    }

    [Fact]
    public void DcSysTimeSub()
    {
        var now = DcSysTime.Now;
        Assert.Equal(now.SysTime - 1ul * 1000 * 1000, (now - TimeSpan.FromMilliseconds(1)).SysTime);
    }
}
