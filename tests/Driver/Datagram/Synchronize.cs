using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace tests;

public class SynchronizeTest
{
    [Fact]
    public void TestSynchronize()
    {
        var autd = CreateController();
        autd.Send(new Synchronize());
    }
}
