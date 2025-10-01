using System.Net;

namespace tests;

public class EtherCrabTest
{
    [Fact, Trait("require", "ethercrab")]
    public void TestCoreId()
    {
        _ = new AUTD3Sharp.Link.CoreId { Id = 0 };
    }

    [Fact, Trait("require", "ethercrab")]
    public void TestThreadPriority()
    {
        _ = AUTD3Sharp.Link.ThreadPriority.Max;
        _ = AUTD3Sharp.Link.ThreadPriority.Min;
        _ = AUTD3Sharp.Link.ThreadPriority.Crossplatform(0);
        _ = AUTD3Sharp.Link.ThreadPriority.Crossplatform(99);
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = AUTD3Sharp.Link.ThreadPriority.Crossplatform(100));
    }

    [Fact, Trait("require", "ethercrab")]
    public void TestStatus()
    {
        var lost = Status.Lost;
        var stateChanged = Status.StateChanged;
        var error = Status.Error;
        var resumed = Status.Resumed;

        Assert.True(Status.Lost == lost);
        Assert.True(Status.StateChanged != lost);
        Assert.True(Status.Error != lost);
        Assert.True(Status.Resumed != lost);
        Assert.False(lost.Equals(null));
        Assert.True(lost.Equals((object?)lost));
        Assert.False(lost.Equals((object?)null));

        Assert.True(Status.Error == error);
        Assert.True(Status.Lost != error);
        Assert.True(Status.StateChanged != error);
        Assert.True(Status.Resumed != error);

        Assert.True(Status.Error != stateChanged);
        Assert.True(Status.Lost != stateChanged);
        Assert.True(Status.StateChanged == stateChanged);
        Assert.True(Status.Resumed != stateChanged);

        Assert.Equal("", lost.ToString());
    }

    [Fact, Trait("require", "ethercrab")]
    public void TestEtherCrabOption()
    {
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsLinkEthercrab.AUTDLinkEtherCrabIsDefault(new EtherCrabOption().ToNative()));
    }

    [Fact, Trait("require", "ethercrab")]
    public void TestEtherCrab()
    {
        _ = new EtherCrab((_, _) => { }, new EtherCrabOption());
    }
}