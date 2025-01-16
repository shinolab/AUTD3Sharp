using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Datagram;

public class ForceFanTest
{
    [Fact]
    public void TestConfigForceFan()
    {
        var autd = CreateController();
        foreach (var dev in autd)
            Assert.False(autd.Link.IsForceFan(dev.Idx));

        autd.Send(new ForceFan(dev => dev.Idx == 0));
        Assert.True(autd.Link.IsForceFan(0));
        Assert.False(autd.Link.IsForceFan(1));

        autd.Send(new ForceFan(dev => dev.Idx == 1));
        Assert.False(autd.Link.IsForceFan(0));
        Assert.True(autd.Link.IsForceFan(1));
    }
}
