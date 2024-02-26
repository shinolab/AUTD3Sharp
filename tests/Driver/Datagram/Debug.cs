using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Datagram;

public class DebugTest
{
    [Fact]
    public async Task TestDebugOutputIdx()
    {
        using var autd = await CreateController();

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(0xFF, autd.Link.DebugOutputIdx(dev.Idx));
        }

        Assert.True(await autd.SendAsync(new ConfigureDebugOutputIdx(device => device[0])));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(0, autd.Link.DebugOutputIdx(dev.Idx));
        }

        Assert.True(await autd.SendAsync(new ConfigureDebugOutputIdx(device => device.Idx == 0 ? device[10] : null)));
        Assert.Equal(10, autd.Link.DebugOutputIdx(0));
        Assert.Equal(0xFF, autd.Link.DebugOutputIdx(1));
    }
}
