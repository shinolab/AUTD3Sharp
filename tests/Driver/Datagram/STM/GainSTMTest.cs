namespace tests.Driver.Datagram.STM;

public class GainSTMTest
{
    [Fact]
    public async Task TestGainSTM()
    {
        var autd = await Controller.Builder([
                new AUTD3(Vector3.Zero),new AUTD3(Vector3.Zero)
            ])
            .OpenAsync(Audit.Builder());

        await autd.SendAsync(Silencer.Disable());

        const float radius = 30.0f;
        const int size = 2;
        var center = autd.Geometry.Center + new Vector3(0, 0, 150);
        var stm = new GainSTM(1.0f * Hz, Enumerable.Range(0, size).Select(i => 2 * MathF.PI * i / size).Select(theta =>
                new Focus(center + radius * new Vector3(MathF.Cos(theta), MathF.Sin(theta), 0))));
        Assert.Equal(1.0f * Hz, stm.Freq);
        Assert.Equal(TimeSpan.FromSeconds(1), stm.Period);
        Assert.Equal(20000u, stm.SamplingConfig.Division);
        await autd.SendAsync(stm);

        foreach (var dev in autd.Geometry)
        {
            Assert.True(autd.Link.IsStmGainMode(dev.Idx, Segment.S0));
            Assert.Equal(20000u, autd.Link.StmFreqDivision(dev.Idx, Segment.S0));
        }

        stm = GainSTM.Nearest(1.0f * Hz, [new Uniform(EmitIntensity.Max), new Uniform(new EmitIntensity(0x80))]);
        await autd.SendAsync(stm);
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(20000u, autd.Link.StmFreqDivision(dev.Idx, Segment.S0));
            Assert.Equal(LoopBehavior.Infinite, autd.Link.StmLoopBehavior(dev.Idx, Segment.S0));
        }

        stm = new GainSTM(TimeSpan.FromSeconds(1), [new Uniform(EmitIntensity.Max), new Uniform(new EmitIntensity(0x80))]);
        await autd.SendAsync(stm);
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(20000u, autd.Link.StmFreqDivision(dev.Idx, Segment.S0));
            Assert.Equal(LoopBehavior.Infinite, autd.Link.StmLoopBehavior(dev.Idx, Segment.S0));
        }

        stm = GainSTM.Nearest(TimeSpan.FromSeconds(1), [new Uniform(EmitIntensity.Max), new Uniform(new EmitIntensity(0x80))]);
        await autd.SendAsync(stm);
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(20000u, autd.Link.StmFreqDivision(dev.Idx, Segment.S0));
            Assert.Equal(LoopBehavior.Infinite, autd.Link.StmLoopBehavior(dev.Idx, Segment.S0));
        }

        stm = new GainSTM(new SamplingConfig(1), [new Uniform(EmitIntensity.Max), new Uniform(new EmitIntensity(0x80))]).WithLoopBehavior(LoopBehavior.Once);
        Assert.Equal(LoopBehavior.Once, stm.LoopBehavior);
        await autd.SendAsync(stm);
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(1u, autd.Link.StmFreqDivision(dev.Idx, Segment.S0));
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
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)])
         .OpenAsync(Audit.Builder());

        await autd.SendAsync(new ReadsFPGAState(_ => true));
        await autd.SendAsync(Silencer.Disable());

        var infos = await autd.FPGAStateAsync();
        Assert.Equal(Segment.S0, infos[0]?.CurrentGainSegment);
        Assert.Null(infos[0]?.CurrentSTMSegment);

        const float radius = 30.0f;
        const int size = 2;
        var center = autd.Geometry.Center + new Vector3(0, 0, 150);
        var stm = new GainSTM(1.0f * Hz, Enumerable.Range(0, size).Select(i => 2 * MathF.PI * i / size).Select(theta =>
                new Focus(center + radius * new Vector3(MathF.Cos(theta), MathF.Sin(theta), 0))));
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