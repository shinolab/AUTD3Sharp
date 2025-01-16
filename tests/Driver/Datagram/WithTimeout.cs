using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace tests;

public class WithTimeoutTest
{
    [Fact]
    public void TestWithTimeout()
    {
        var autd = Controller.Builder([new AUTD3(Point3.Origin)])
            .Open(Audit.Builder());

        autd.Send(new Null().WithTimeout(null));
        Assert.Null(autd.Link.LastTimeout());

        autd.Send(new Null().WithTimeout(Duration.FromMillis(100)));
        Assert.Equal(Duration.FromMillis(100), autd.Link.LastTimeout());

        autd.Send((new Static(), new Null()).WithTimeout(Duration.FromMillis(200)));
        Assert.Equal(Duration.FromMillis(200), autd.Link.LastTimeout());
    }
}
