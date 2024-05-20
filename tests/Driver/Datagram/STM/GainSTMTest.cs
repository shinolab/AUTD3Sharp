namespace tests.Driver.Datagram.STM;

public class GainSTMTest
{
    [Fact]
    public async Task TestGainSTM()
    {
        var autd = await new ControllerBuilder()
            .AddDevice(new AUTD3(Vector3d.Zero))
            .AddDevice(new AUTD3(Vector3d.Zero))
            .OpenAsync(Audit.Builder());

        await autd.SendAsync(Silencer.Disable());

        const double radius = 30.0;
        const int size = 2;
        var center = autd.Geometry.Center + new Vector3d(0, 0, 150);
        var stm = GainSTM.FromFreq(1.0 * Hz)
            .AddGainsFromIter(Enumerable.Range(0, size).Select(i => 2 * Math.PI * i / size).Select(theta =>
                new Focus(center + radius * new Vector3d(Math.Cos(theta), Math.Sin(theta), 0))));
        await autd.SendAsync(stm);

        foreach (var dev in autd.Geometry)
        {
            Assert.True(autd.Link.IsStmGainMode(dev.Idx, Segment.S0));
        }

        foreach (var dev in autd.Geometry) Assert.Equal(10240000u, autd.Link.StmFreqDivision(dev.Idx, Segment.S0));

        stm = GainSTM.FromSamplingConfig(SamplingConfig.Division(512)).AddGain(new Uniform(EmitIntensity.Max)).AddGain(new Uniform(new EmitIntensity(0x80))).WithLoopBehavior(LoopBehavior.Once);
        Assert.Equal(LoopBehavior.Once, stm.LoopBehavior);
        await autd.SendAsync(stm);
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
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0, p));
            }
            {
                var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0x80, d));
                Assert.All(phases, p => Assert.Equal(0, p));
            }
        }

        stm = stm.WithMode(GainSTMMode.PhaseFull);
        await autd.SendAsync(stm);
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(2u, autd.Link.StmCycle(dev.Idx, Segment.S0));
            {
                var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0, p));
            }
            {
                var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0, p));
            }
        }

        stm = stm.WithMode(GainSTMMode.PhaseHalf);
        await autd.SendAsync(stm);
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(2u, autd.Link.StmCycle(dev.Idx, Segment.S0));
            {
                var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0, p));
            }
            {
                var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0, p));
            }
        }
    }

    [Fact]
    public async Task TestChangeGainSTMSegment()
    {
        var autd = await new ControllerBuilder()
         .AddDevice(new AUTD3(Vector3d.Zero))
         .OpenAsync(Audit.Builder());

        await autd.SendAsync(new ReadsFPGAState(_ => true));
        await autd.SendAsync(Silencer.Disable());

        var infos = await autd.FPGAStateAsync();
        Assert.Equal(Segment.S0, infos[0]?.CurrentGainSegment);
        Assert.Null(infos[0]?.CurrentSTMSegment);

        const double radius = 30.0;
        const int size = 2;
        var center = autd.Geometry.Center + new Vector3d(0, 0, 150);
        var stm = GainSTM.FromFreq(1.0 * Hz)
            .AddGainsFromIter(Enumerable.Range(0, size).Select(i => 2 * Math.PI * i / size).Select(theta =>
                new Focus(center + radius * new Vector3d(Math.Cos(theta), Math.Sin(theta), 0))));
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

        await autd.SendAsync(SwapSegment.GainSTM(Segment.S0, TransitionMode.Immediate));
        Assert.Equal(Segment.S0, autd.Link.CurrentStmSegment(0));
        infos = await autd.FPGAStateAsync();
        Assert.Null(infos[0]?.CurrentGainSegment);
        Assert.Equal(Segment.S0, infos[0]?.CurrentSTMSegment);
    }
}