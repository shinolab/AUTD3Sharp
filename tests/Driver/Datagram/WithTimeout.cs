using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace tests;

public class WithTimeoutTest
{
    [Fact]
    public async Task TestWithTimeout()
    {
        var autd = await Controller.Builder([new AUTD3(Point3.Origin)])
            .OpenAsync(Audit.Builder());

        await autd.SendAsync(new Null().WithTimeout(null));
        Assert.Null(autd.Link.LastTimeout());

        await autd.SendAsync(new Null().WithTimeout(Duration.FromMillis(100)));
        Assert.Equal(Duration.FromMillis(100), autd.Link.LastTimeout());

        await autd.SendAsync((new Static(), new Null()).WithTimeout(Duration.FromMillis(200)));
        Assert.Equal(Duration.FromMillis(200), autd.Link.LastTimeout());
    }
}
