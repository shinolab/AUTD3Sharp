using AUTD3Sharp.Driver.Datagram;

namespace tests.Driver.Datagram.Modulation;

public class LoopBehaviorTest
{
    [Fact]
    public void TestChangeModulationLoopBehavior()
    {
        var autd = CreateController(1);

        autd.Send(new ReadsFPGAState(_ => true));

        Assert.Equal(LoopBehavior.Infinite, autd.Link<Audit>().ModulationLoopBehavior(0, Segment.S0));
        Assert.Equal(LoopBehavior.Infinite, autd.Link<Audit>().ModulationLoopBehavior(0, Segment.S1));

        var m = new Static();
        autd.Send(m);
        Assert.Equal(Segment.S0, autd.Link<Audit>().CurrentModulationSegment(0));

        autd.Send(new WithLoopBehavior(m, LoopBehavior.Once, Segment.S1, TransitionMode.SyncIdx));
        Assert.Equal(LoopBehavior.Once, autd.Link<Audit>().ModulationLoopBehavior(0, Segment.S1));

        autd.Send(new WithLoopBehavior(m, LoopBehavior.Once, Segment.S0, null));
        Assert.Equal(LoopBehavior.Once, autd.Link<Audit>().ModulationLoopBehavior(0, Segment.S0));
    }
}
