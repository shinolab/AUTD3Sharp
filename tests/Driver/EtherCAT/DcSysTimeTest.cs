namespace tests.Driver.EtherCAT;

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
        Assert.Equal(now.SysTime + 1ul * 1000 * 1000, (now + Duration.FromMillis(1)).SysTime);
    }

    [Fact]
    public void DcSysTimeSub()
    {
        var now = DcSysTime.Now;
        Assert.Equal(now.SysTime - 1ul * 1000 * 1000, (now - Duration.FromMillis(1)).SysTime);
    }

    [Fact]
    public void Equals_DcSysTime()
    {
        var m1 = DcSysTime.Now;
        var m2 = m1;
        var m3 = m1 + Duration.FromNanos(1);

        Assert.True(m1 == m2);
        Assert.True(m1 != m3);
        Assert.True(!m1.Equals(null));
        Assert.True(m1.Equals((object?)m2));
        Assert.True(!m1.Equals((object?)null));
    }
}
