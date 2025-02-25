using AUTD3Sharp.Driver.Datagram;

namespace tests.Driver.Datagram.Gain;

public class SegmentTest
{
    [Fact]
    public void TestChangeGainSegment()
    {
        var autd = CreateController(1);

        autd.Send(new ReadsFPGAState(_ => true));

        var infos = autd.FPGAState();
        Assert.Equal(Segment.S0, infos[0]?.CurrentGainSegment());
        Assert.Null(infos[0]?.CurrentSTMSegment());

        var g = new Null();
        autd.Send(g);
        Assert.Equal(Segment.S0, autd.Link<Audit>().CurrentStmSegment(0));
        infos = autd.FPGAState();
        Assert.Equal(Segment.S0, infos[0]?.CurrentGainSegment());
        Assert.Null(infos[0]?.CurrentSTMSegment());

        autd.Send(new WithSegment(g, Segment.S1, TransitionMode.Immediate));
        Assert.Equal(Segment.S1, autd.Link<Audit>().CurrentStmSegment(0));
        infos = autd.FPGAState();
        Assert.Equal(Segment.S1, infos[0]?.CurrentGainSegment());
        Assert.Null(infos[0]?.CurrentSTMSegment());

        autd.Send(new WithSegment(g, Segment.S0, null));
        Assert.Equal(Segment.S1, autd.Link<Audit>().CurrentStmSegment(0));
        infos = autd.FPGAState();
        Assert.Equal(Segment.S1, infos[0]?.CurrentGainSegment());
        Assert.Null(infos[0]?.CurrentSTMSegment());

        autd.Send(SwapSegment.Gain(Segment.S0, TransitionMode.Immediate));
        Assert.Equal(Segment.S0, autd.Link<Audit>().CurrentStmSegment(0));
        infos = autd.FPGAState();
        Assert.Equal(Segment.S0, infos[0]?.CurrentGainSegment());
        Assert.Null(infos[0]?.CurrentSTMSegment());
    }
}
