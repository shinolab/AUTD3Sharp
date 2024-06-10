using System.Net;
using Xunit.Abstractions;

namespace tests.Link;

public class SOEMTest()
{
    [Fact, Trait("require", "soem")]
    public async Task TestSOEM()
    {
        await Assert.ThrowsAsync<AUTDException>(async () => _ = await Controller.Builder([new AUTD3(Vector3.Zero)])
             .OpenAsync(SOEM.Builder()
                 .WithIfname("ifname")
                 .WithBufSize(32)
                 .WithSendCycle(2)
                 .WithSync0Cycle(2)
                 .WithErrHandler((slave, status, msg) => { })
                 .WithTimerStrategy(TimerStrategy.Sleep)
                 .WithSyncMode(SyncMode.FreeRun)
                 .WithStateCheckInterval(TimeSpan.FromMilliseconds(100))
                 .WithTimeout(TimeSpan.FromMilliseconds(200))));
    }


    [Fact, Trait("require", "soem")]
    public void TestRemoteSOEM()
    {
        _ = RemoteSOEM.Builder(new IPEndPoint(IPAddress.Parse("172.0.0.1"), 8080))
            .WithTimeout(TimeSpan.FromMilliseconds(200));
    }
}