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
    public void TestStatus()
    {
        var lost = Status.Lost;
        var stateChanged = Status.StateChanged;
        var error = Status.Error;

        Assert.Equal(lost, Status.Lost);
        Assert.NotEqual(lost, stateChanged);
        Assert.NotEqual(lost, error);
        Assert.Equal(stateChanged, Status.StateChanged);
        Assert.NotEqual(stateChanged, lost);
        Assert.NotEqual(stateChanged, error);
        Assert.Equal(error, Status.Error);
        Assert.NotEqual(error, lost);
        Assert.NotEqual(error, stateChanged);

        Assert.Equal("", lost.ToString());
    }

    [Fact, Trait("require", "soem")]
    public void TestSOEM()
    {
        var builder = SOEM.Builder();
        Assert.True(
            AUTD3Sharp.NativeMethods.NativeMethodsLinkSOEM.AUTDLinkSOEMIsDefault(
                builder.BufSize,
                (ulong)builder.SendCycle.TotalNanoseconds,
                (ulong)builder.Sync0Cycle.TotalNanoseconds,
                builder.SyncMode,
                builder.ProcessPriority,
                builder.ThreadPriority,
                (ulong)builder.StateCheckInterval.TotalNanoseconds,
                builder.TimerStrategy,
                (ulong)builder.SyncTolerance.TotalNanoseconds,
                (ulong)builder.SyncTimeout.TotalNanoseconds
            )
        );
    }

    [Fact, Trait("require", "soem")]
    public void TestRemoteSOEM()
    {
        _ = RemoteSOEM.Builder(new IPEndPoint(IPAddress.Parse("172.0.0.1"), 8080));
    }
}