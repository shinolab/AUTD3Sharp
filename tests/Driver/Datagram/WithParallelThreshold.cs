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

        await autd.SendAsync(new Null().WithParallelThreshold(null));
        Assert.Null(autd.Link.LastParallelThreshold());

        await autd.SendAsync(new Null().WithParallelThreshold(10));
        Assert.Equal(10, autd.Link.LastParallelThreshold());

        await autd.SendAsync((new Static(), new Null()).WithParallelThreshold(20));
        Assert.Equal(20, autd.Link.LastParallelThreshold());
    }
}
