using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace tests;

public class WithTimeoutTest
{
    [Fact]
    public async Task TestWithTimeout()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)])
            .OpenAsync(Audit.Builder());

        await autd.SendAsync(new Null().WithTimeout(TimeSpan.FromMilliseconds(100)));
        await autd.SendAsync((new Static(), new Null()).WithTimeout(null));
    }
}
