namespace tests.Driver.Datagram.Modulation;

public class SegmentTest
{
    [Fact]
    public async Task TestChangeModulationSegment()
    {
        var autd = await new ControllerBuilder()
         .AddDevice(new AUTD3(Vector3d.Zero))
         .OpenAsync(Audit.Builder());

        await autd.SendAsync(new ReadsFPGAState(_ => true));

        var infos = await autd.FPGAStateAsync();
        Assert.Equal(Segment.S0, infos[0]?.CurrentModSegment);

        var m = new Static();
        await autd.SendAsync(m);
        Assert.Equal(Segment.S0, autd.Link.CurrentModulationSegment(0));
        infos = await autd.FPGAStateAsync();
        Assert.Equal(Segment.S0, infos[0]?.CurrentModSegment);
        Assert.Null(infos[0]?.CurrentSTMSegment);

        await autd.SendAsync(m.WithSegment(Segment.S1, TransitionMode.Immediate));
        Assert.Equal(Segment.S1, autd.Link.CurrentModulationSegment(0));
        infos = await autd.FPGAStateAsync();
        Assert.Equal(Segment.S1, infos[0]?.CurrentModSegment);
        Assert.Null(infos[0]?.CurrentSTMSegment);

        await autd.SendAsync(m.WithSegment(Segment.S0, null));
        Assert.Equal(Segment.S1, autd.Link.CurrentModulationSegment(0));
        infos = await autd.FPGAStateAsync();
        Assert.Equal(Segment.S1, infos[0]?.CurrentModSegment);
        Assert.Null(infos[0]?.CurrentSTMSegment);

        await autd.SendAsync(SwapSegment.Modulation(Segment.S0, TransitionMode.Immediate));
        Assert.Equal(Segment.S0, autd.Link.CurrentModulationSegment(0));
        infos = await autd.FPGAStateAsync();
        Assert.Equal(Segment.S0, infos[0]?.CurrentModSegment);
        Assert.Null(infos[0]?.CurrentSTMSegment);
    }
}