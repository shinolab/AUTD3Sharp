namespace tests.Driver.Datagram.Modulation;

public class SegmentTest
{
    [Fact]
    public void TestChangeModulationSegment()
    {
        var autd = Controller.Builder([new AUTD3(Point3.Origin)])
         .Open(Audit.Builder());

        autd.Send(new ReadsFPGAState(_ => true));

        var infos = autd.FPGAState();
        Assert.Equal(Segment.S0, infos[0]?.CurrentModSegment);

        var m = new Static();
        autd.Send(m);
        Assert.Equal(Segment.S0, autd.Link.CurrentModulationSegment(0));
        infos = autd.FPGAState();
        Assert.Equal(Segment.S0, infos[0]?.CurrentModSegment);
        Assert.Null(infos[0]?.CurrentSTMSegment);

        autd.Send(m.WithSegment(Segment.S1, TransitionMode.Immediate));
        Assert.Equal(Segment.S1, autd.Link.CurrentModulationSegment(0));
        infos = autd.FPGAState();
        Assert.Equal(Segment.S1, infos[0]?.CurrentModSegment);
        Assert.Null(infos[0]?.CurrentSTMSegment);

        autd.Send(m.WithSegment(Segment.S0, null));
        Assert.Equal(Segment.S1, autd.Link.CurrentModulationSegment(0));
        infos = autd.FPGAState();
        Assert.Equal(Segment.S1, infos[0]?.CurrentModSegment);
        Assert.Null(infos[0]?.CurrentSTMSegment);

        autd.Send(SwapSegment.Modulation(Segment.S0, TransitionMode.Immediate));
        Assert.Equal(Segment.S0, autd.Link.CurrentModulationSegment(0));
        infos = autd.FPGAState();
        Assert.Equal(Segment.S0, infos[0]?.CurrentModSegment);
        Assert.Null(infos[0]?.CurrentSTMSegment);
    }
}