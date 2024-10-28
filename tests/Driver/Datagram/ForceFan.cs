using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Datagram;

public class ForceFanTest
{
    [Fact]
    public async Task TestConfigForceFan()
    {
        var autd = await CreateController();
        foreach (var dev in autd)
            Assert.False(autd.Link.IsForceFan(dev.Idx));

        await autd.SendAsync(new ForceFan(dev => dev.Idx == 0));
        Assert.True(autd.Link.IsForceFan(0));
        Assert.False(autd.Link.IsForceFan(1));

        await autd.SendAsync(new ForceFan(dev => dev.Idx == 1));
        Assert.False(autd.Link.IsForceFan(0));
        Assert.True(autd.Link.IsForceFan(1));
    }
}
