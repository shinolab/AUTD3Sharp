using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace tests;

public class WithParallelThresholdTest
{
    [Fact]
    public async Task TestWithParallelThreshold()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)])
            .OpenAsync(Audit.Builder());

        await autd.SendAsync(new Null().WithParallelThreshold(4));
        await autd.SendAsync((new Static(), new Null()).WithParallelThreshold(null));
    }
}
