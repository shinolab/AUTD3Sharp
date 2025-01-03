using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace tests;

public class SynchronizeTest
{
    [Fact]
    public async Task TestSynchronize()
    {
        var autd = await Controller.Builder([new AUTD3(Point3.Origin), new AUTD3(Point3.Origin)])
            .OpenAsync(Audit.Builder());

        await autd.SendAsync(new Synchronize());
    }
}
