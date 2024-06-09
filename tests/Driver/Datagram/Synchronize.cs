using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace tests;

public class SynchronizeTest
{
    [Fact]
    public async Task TestSynchronize()
    {
        var autd = await new ControllerBuilder([new AUTD3(Vector3.Zero), new AUTD3(Vector3.Zero)])
            .OpenAsync(Audit.Builder());

        await autd.SendAsync(new Synchronize());
    }
}
