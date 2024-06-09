namespace tests.Driver.Datagram.STM;

public class FociSTMTest
{
    [Fact]
    public async Task TestFociSTM()
    {
        var autd = await AUTDTest.CreateController();

        await autd.SendAsync(Silencer.Disable());

        const float radius = 30.0;
        const int size = 2;
        var center = autd.Geometry.Center + new Vector3(0, 0, 150);
        var stm = FociSTM.FromFreq(1.0 * Hz)
            .AddFociFromIter(Enumerable.Range(0, size).Select(i => 2 * Math.PI * i / size).Select(theta =>
                (center + radius * new Vector3(Math.Cos(theta), Math.Sin(theta), 0), EmitIntensity.Max)));
        await autd.SendAsync(stm);

        foreach (var dev in autd.Geometry) Assert.False(autd.Link.IsStmGainMode(dev.Idx, Segment.S0));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal((uint)(dev.SoundSpeed / 1000.0 * 1024.0), autd.Link.StmSoundSpeed(dev.Idx, Segment.S0));
            Assert.Equal(10240000u, autd.Link.StmFreqDivision(dev.Idx, Segment.S0));
        }

        stm = FociSTM.FromFreqNearest(1.0 * Hz)
            .AddFociFromIter(Enumerable.Range(0, size).Select(i => 2 * Math.PI * i / size).Select(theta =>
                center + radius * new Vector3(Math.Cos(theta), Math.Sin(theta), 0)));
        await autd.SendAsync(stm);
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10240000u, autd.Link.StmFreqDivision(dev.Idx, Segment.S0));
            Assert.Equal(LoopBehavior.Infinite, autd.Link.StmLoopBehavior(dev.Idx, Segment.S0));
        }

        stm = FociSTM.FromSamplingConfig(SamplingConfig.Division(512)).AddFocus(center).AddFocus(center).WithLoopBehavior(LoopBehavior.Once);
        await autd.SendAsync(stm);
        Assert.Equal(LoopBehavior.Once, stm.LoopBehavior);
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(512u, autd.Link.StmFreqDivision(dev.Idx, Segment.S0));
            Assert.Equal(LoopBehavior.Once, autd.Link.StmLoopBehavior(dev.Idx, Segment.S0));
        }

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(2u, autd.Link.StmCycle(dev.Idx, Segment.S0));
            {
                var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
                Assert.Contains(intensities, d => d != 0);
                Assert.Contains(phases, p => p != 0);
            }
            {
                var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 1);
                Assert.Contains(intensities, d => d != 0);
                Assert.Contains(phases, p => p != 0);
            }
        }
    }

    [Fact]
    public async Task TestChangeFociSTMSegment()
    {
        var autd = await new ControllerBuilder()
         .AddDevice(new AUTD3(Vector3.Zero))
         .OpenAsync(Audit.Builder());

        await autd.SendAsync(new ReadsFPGAState(_ => true));
        await autd.SendAsync(Silencer.Disable());

        var infos = await autd.FPGAStateAsync();
        Assert.Equal(Segment.S0, infos[0]?.CurrentGainSegment);
        Assert.Null(infos[0]?.CurrentSTMSegment);

        const float radius = 30.0;
        const int size = 2;
        var center = autd.Geometry.Center + new Vector3(0, 0, 150);
        var stm = FociSTM.FromFreq(1.0 * Hz)
            .AddFociFromIter(Enumerable.Range(0, size).Select(i => 2 * Math.PI * i / size).Select(theta =>
                (center + radius * new Vector3(Math.Cos(theta), Math.Sin(theta), 0), EmitIntensity.Max)));

        await autd.SendAsync(stm);
        Assert.Equal(Segment.S0, autd.Link.CurrentStmSegment(0));
        infos = await autd.FPGAStateAsync();
        Assert.Null(infos[0]?.CurrentGainSegment);
        Assert.Equal(Segment.S0, infos[0]?.CurrentSTMSegment);

        await autd.SendAsync(stm.WithSegment(Segment.S1, TransitionMode.Immediate));
        Assert.Equal(Segment.S1, autd.Link.CurrentStmSegment(0));
        infos = await autd.FPGAStateAsync();
        Assert.Null(infos[0]?.CurrentGainSegment);
        Assert.Equal(Segment.S1, infos[0]?.CurrentSTMSegment);

        await autd.SendAsync(stm.WithSegment(Segment.S0, null));
        Assert.Equal(Segment.S1, autd.Link.CurrentStmSegment(0));
        infos = await autd.FPGAStateAsync();
        Assert.Null(infos[0]?.CurrentGainSegment);
        Assert.Equal(Segment.S1, infos[0]?.CurrentSTMSegment);

        await autd.SendAsync(SwapSegment.FociSTM(Segment.S0, TransitionMode.Immediate));
        Assert.Equal(Segment.S0, autd.Link.CurrentStmSegment(0));
        infos = await autd.FPGAStateAsync();
        Assert.Null(infos[0]?.CurrentGainSegment);
        Assert.Equal(Segment.S0, infos[0]?.CurrentSTMSegment);
    }
}