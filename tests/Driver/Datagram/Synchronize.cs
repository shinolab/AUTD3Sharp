using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace tests;

public class SynchronizeTest
{
    [Fact]
    public async Task TestSynchronize()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero))
            .OpenAsync(Audit.Builder());

        Assert.True(await autd.SendAsync(new Synchronize()));
    }
}
