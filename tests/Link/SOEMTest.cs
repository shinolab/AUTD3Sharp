using System.Net;
using Xunit.Abstractions;

namespace tests.Link;

public class SOEMTest()
{
    [Fact, Trait("require", "soem")]
    public void TestThreadPriority()
    {
        _ = AUTD3Sharp.Link.ThreadPriority.Max;
        _ = AUTD3Sharp.Link.ThreadPriority.Min;
        _ = AUTD3Sharp.Link.ThreadPriority.Crossplatform(0);
        _ = AUTD3Sharp.Link.ThreadPriority.Crossplatform(99);
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = AUTD3Sharp.Link.ThreadPriority.Crossplatform(100));
    }

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
                 .WithSyncTolerance(TimeSpan.FromMilliseconds(1e-3))
                 .WithSyncTimeout(TimeSpan.FromSeconds(10))
                 .WithStateCheckInterval(TimeSpan.FromMilliseconds(100))
                 .WithThreadPriority(AUTD3Sharp.Link.ThreadPriority.Max)
                 .WithProcessPriority(ProcessPriority.High)
                 .WithTimeout(TimeSpan.FromMilliseconds(200))));
    }

    [Fact, Trait("require", "soem")]
    public void TestRemoteSOEM()
    {
        _ = RemoteSOEM.Builder(new IPEndPoint(IPAddress.Parse("172.0.0.1"), 8080))
            .WithTimeout(TimeSpan.FromMilliseconds(200));
    }
}