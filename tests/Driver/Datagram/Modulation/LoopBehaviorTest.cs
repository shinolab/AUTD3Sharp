namespace tests.Driver.Datagram.Modulation;

public class LoopBehaviorTest
{
    [Fact]
    public void TestChangeModulationLoopCount()
    {
        var autd = CreateController(1);

        autd.Send(new ReadsFPGAState(_ => true));

        Assert.Equal(0xFFFF, autd.Link<Audit>().ModulationLoopCount(0, Segment.S0));
        Assert.Equal(0xFFFF, autd.Link<Audit>().ModulationLoopCount(0, Segment.S1));

        var m = new Static();
        autd.Send(m);
        Assert.Equal(Segment.S0, autd.Link<Audit>().CurrentModulationSegment(0));

        autd.Send(new WithFiniteLoop(m, 1, Segment.S1, new AUTD3Sharp.TransitionMode.SyncIdx()));
        Assert.Equal(0, autd.Link<Audit>().ModulationLoopCount(0, Segment.S1));

        autd.Send(new WithFiniteLoop(m, 1, Segment.S0, new AUTD3Sharp.TransitionMode.Later()));
        Assert.Equal(0, autd.Link<Audit>().ModulationLoopCount(0, Segment.S0));
    }
}
