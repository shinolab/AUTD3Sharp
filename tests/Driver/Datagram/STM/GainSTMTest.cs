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

        Assert.True(await autd.SendAsync(ConfigureSilencer.Disable()));

        const double radius = 30.0;
        const int size = 2;
        var center = autd.Geometry.Center + new Vector3d(0, 0, 150);
        var stm = GainSTM.FromFreq(1)
            .AddGainsFromIter(Enumerable.Range(0, size).Select(i => 2 * Math.PI * i / size).Select(theta =>
                new Focus(center + radius * new Vector3d(Math.Cos(theta), Math.Sin(theta), 0))));
        Assert.True(await autd.SendAsync(stm));

        foreach (var dev in autd.Geometry)
        {
            Assert.True(autd.Link.IsStmGainMode(dev.Idx, Segment.S0));
        }

        Assert.Equal(1, stm.Frequency);
        Assert.Equal(TimeSpan.FromMicroseconds(1000000), stm.Period);
        Assert.Equal(2, stm.SamplingConfiguration.Frequency);
        Assert.Equal(10240000u, stm.SamplingConfiguration.FrequencyDivision);
        Assert.Equal(TimeSpan.FromMicroseconds(500000), stm.SamplingConfiguration.Period);
        foreach (var dev in autd.Geometry) Assert.Equal(10240000u, autd.Link.StmFrequencyDivision(dev.Idx, Segment.S0));

        stm = GainSTM.FromPeriod(TimeSpan.FromMicroseconds(1000000))
           .AddGainsFromIter(Enumerable.Range(0, size).Select(i => 2 * Math.PI * i / size).Select(theta =>
               new Focus(center + radius * new Vector3d(Math.Cos(theta), Math.Sin(theta), 0))));
        Assert.Equal(LoopBehavior.Infinite, stm.LoopBehavior);
        Assert.True(await autd.SendAsync(stm));
        Assert.Equal(1, stm.Frequency);
        Assert.Equal(TimeSpan.FromMicroseconds(1000000), stm.Period);
        Assert.Equal(2, stm.SamplingConfiguration.Frequency);
        Assert.Equal(10240000u, stm.SamplingConfiguration.FrequencyDivision);
        Assert.Equal(TimeSpan.FromMicroseconds(500000), stm.SamplingConfiguration.Period);
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10240000u, autd.Link.StmFrequencyDivision(dev.Idx, Segment.S0));
            Assert.Equal(LoopBehavior.Infinite, autd.Link.StmLoopBehavior(dev.Idx, Segment.S0));
        }

        stm = GainSTM.FromSamplingConfig(SamplingConfiguration.FromFrequencyDivision(512)).AddGain(new Uniform(EmitIntensity.Max)).AddGain(new Uniform(new EmitIntensity(0x80))).WithLoopBehavior(LoopBehavior.Once);
        Assert.Equal(LoopBehavior.Once, stm.LoopBehavior);
        Assert.True(await autd.SendAsync(stm));
        Assert.Equal(20000.0, stm.Frequency);
        Assert.Equal(2 * 20000.0, stm.SamplingConfiguration.Frequency);
        Assert.Equal(512u, stm.SamplingConfiguration.FrequencyDivision);
        Assert.Equal(TimeSpan.FromMicroseconds(25), stm.SamplingConfiguration.Period);
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(512u, autd.Link.StmFrequencyDivision(dev.Idx, Segment.S0));
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
        Assert.True(await autd.SendAsync(stm));
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
        Assert.True(await autd.SendAsync(stm));
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

        Assert.True(await autd.SendAsync(new ConfigureReadsFPGAState(_ => true)));
        Assert.True(await autd.SendAsync(ConfigureSilencer.Disable()));

        var infos = await autd.FPGAStateAsync();
        Assert.Equal(Segment.S0, infos[0]?.CurrentGainSegment);
        Assert.Null(infos[0]?.CurrentSTMSegment);

        const double radius = 30.0;
        const int size = 2;
        var center = autd.Geometry.Center + new Vector3d(0, 0, 150);
        var stm = GainSTM.FromFreq(1)
            .AddGainsFromIter(Enumerable.Range(0, size).Select(i => 2 * Math.PI * i / size).Select(theta =>
                new Focus(center + radius * new Vector3d(Math.Cos(theta), Math.Sin(theta), 0))));
        Assert.True(await autd.SendAsync(stm));
        Assert.Equal(Segment.S0, autd.Link.CurrentStmSegment(0));
        infos = await autd.FPGAStateAsync();
        Assert.Null(infos[0]?.CurrentGainSegment);
        Assert.Equal(Segment.S0, infos[0]?.CurrentSTMSegment);

        Assert.True(await autd.SendAsync(stm.WithSegment(Segment.S1, true)));
        Assert.Equal(Segment.S1, autd.Link.CurrentStmSegment(0));
        infos = await autd.FPGAStateAsync();
        Assert.Null(infos[0]?.CurrentGainSegment);
        Assert.Equal(Segment.S1, infos[0]?.CurrentSTMSegment);

        Assert.True(await autd.SendAsync(stm.WithSegment(Segment.S0, false)));
        Assert.Equal(Segment.S1, autd.Link.CurrentStmSegment(0));
        infos = await autd.FPGAStateAsync();
        Assert.Null(infos[0]?.CurrentGainSegment);
        Assert.Equal(Segment.S1, infos[0]?.CurrentSTMSegment);

        Assert.True(await autd.SendAsync(new ChangeGainSTMSegment(Segment.S0)));
        Assert.Equal(Segment.S0, autd.Link.CurrentStmSegment(0));
        infos = await autd.FPGAStateAsync();
        Assert.Null(infos[0]?.CurrentGainSegment);
        Assert.Equal(Segment.S0, infos[0]?.CurrentSTMSegment);
    }
}