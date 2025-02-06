using AUTD3Sharp.Driver.Datagram;

namespace tests.Driver.Datagram.Modulation;

public class SegmentTest
{
    [Fact]
    public void TestChangeModulationSegment()
    {
        var autd = CreateController(1);

        autd.Send(new ReadsFPGAState(_ => true));

        var infos = autd.FPGAState();
        Assert.Equal(Segment.S0, infos[0]?.CurrentModSegment());

        var m = new Static();
        autd.Send(m);
        Assert.Equal(Segment.S0, autd.Link().CurrentModulationSegment(0));
        infos = autd.FPGAState();
        Assert.Equal(Segment.S0, infos[0]?.CurrentModSegment());
        Assert.Null(infos[0]?.CurrentSTMSegment());

        autd.Send(new WithSegment(m, Segment.S1, TransitionMode.Immediate));
        Assert.Equal(Segment.S1, autd.Link().CurrentModulationSegment(0));
        infos = autd.FPGAState();
        Assert.Equal(Segment.S1, infos[0]?.CurrentModSegment());
        Assert.Null(infos[0]?.CurrentSTMSegment());

        autd.Send(new WithSegment(m, Segment.S0, null));
        Assert.Equal(Segment.S1, autd.Link().CurrentModulationSegment(0));
        infos = autd.FPGAState();
        Assert.Equal(Segment.S1, infos[0]?.CurrentModSegment());
        Assert.Null(infos[0]?.CurrentSTMSegment());

        autd.Send(SwapSegment.Modulation(Segment.S0, TransitionMode.Immediate));
        Assert.Equal(Segment.S0, autd.Link().CurrentModulationSegment(0));
        infos = autd.FPGAState();
        Assert.Equal(Segment.S0, infos[0]?.CurrentModSegment());
        Assert.Null(infos[0]?.CurrentSTMSegment());
    }
}
