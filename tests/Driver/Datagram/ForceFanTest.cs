namespace tests.Driver.Datagram;

public class ForceFanTest
{
    [Fact]
    public void TestConfigForceFan()
    {
        var autd = CreateController();
        foreach (var dev in autd)
            Assert.False(autd.Link<Audit>().IsForceFan(dev.Idx()));

        autd.Send(new ForceFan(dev => dev.Idx() == 0));
        Assert.True(autd.Link<Audit>().IsForceFan(0));
        Assert.False(autd.Link<Audit>().IsForceFan(1));

        autd.Send(new ForceFan(dev => dev.Idx() == 1));
        Assert.False(autd.Link<Audit>().IsForceFan(0));
        Assert.True(autd.Link<Audit>().IsForceFan(1));
    }
}
