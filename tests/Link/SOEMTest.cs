using System.Net;
using Xunit.Abstractions;

namespace tests.Link;

public class SOEMTest(ITestOutputHelper testOutputHelper)
{
    [Fact, Trait("require", "soem")]
    public async Task TestSOEM()
    {
        var onLost = new SOEM.OnErrCallbackDelegate(msg =>
        {
            testOutputHelper.WriteLine(msg);
            Environment.Exit(-1);
        });
        var onErr = new SOEM.OnErrCallbackDelegate(testOutputHelper.WriteLine);

        await Assert.ThrowsAsync<AUTDException>(async () => _ = await new ControllerBuilder()
             .AddDevice(new AUTD3(Vector3d.zero))
             .OpenWithAsync(SOEM.Builder()
                 .WithIfname("ifname")
                 .WithBufSize(32)
                 .WithSendCycle(2)
                 .WithSync0Cycle(2)
                 .WithOnLost(onLost)
                 .WithOnErr(onErr)
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