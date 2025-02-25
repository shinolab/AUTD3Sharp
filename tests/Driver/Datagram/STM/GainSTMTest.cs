using AUTD3Sharp.Driver.Datagram;

namespace tests.Driver.Datagram.STM;

public class GainSTMTest
{

    private static IEnumerable<Uniform> CreateGains(int size) =>
        Enumerable.Range(0, size).Select(_ => new Uniform(intensity: EmitIntensity.Max, phase: new Phase(0xFF)));

    [Fact]
    public void TestGainSTMFreq()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        var stm = new GainSTM(CreateGains(2), 1.0f * Hz, new GainSTMOption());
        Assert.Equal(20000u, stm.SamplingConfig().Division);
        autd.Send(stm);
        foreach (var dev in autd)
        {
            Assert.True(autd.Link<Audit>().IsStmGainMode(dev.Idx(), Segment.S0));
            Assert.Equal(2, autd.Link<Audit>().StmCycle(dev.Idx(), Segment.S0));
            Assert.Equal(20000u, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S0));
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0xFF, p));
            }
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0xFF, p));
            }
        }
    }

    [Fact]
    public void TestGainSTMFreqNearest()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        var stm = new GainSTM(CreateGains(2), 1.0f * Hz, new GainSTMOption()).IntoNearest();
        autd.Send(stm);
        foreach (var dev in autd)
        {
            Assert.True(autd.Link<Audit>().IsStmGainMode(dev.Idx(), Segment.S0));
            Assert.Equal(20000u, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S0));
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0xFF, p));
            }
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0xFF, p));
            }
        }
    }

    [Fact]
    public void TestGainSTMPeriod()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        var stm = new GainSTM(CreateGains(2), Duration.FromSecs(1), new GainSTMOption());
        autd.Send(stm);
        foreach (var dev in autd)
        {
            Assert.True(autd.Link<Audit>().IsStmGainMode(dev.Idx(), Segment.S0));
            Assert.Equal(20000u, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S0));
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0xFF, p));
            }
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0xFF, p));
            }
        }
    }

    [Fact]
    public void TestGainSTMPeriodNearest()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        var stm = new GainSTM(CreateGains(2), Duration.FromSecs(1), new GainSTMOption()).IntoNearest();
        autd.Send(stm);
        foreach (var dev in autd)
        {
            Assert.True(autd.Link<Audit>().IsStmGainMode(dev.Idx(), Segment.S0));
            Assert.Equal(20000u, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S0));
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0xFF, p));
            }
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0xFF, p));
            }
        }
    }

    [Fact]
    public void TestGainSTMSamplingConfig()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        var stm = new GainSTM(CreateGains(2), new SamplingConfig(1), new GainSTMOption());
        autd.Send(stm);
        foreach (var dev in autd)
        {
            Assert.True(autd.Link<Audit>().IsStmGainMode(dev.Idx(), Segment.S0));
            Assert.Equal(1, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S0));
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0xFF, p));
            }
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0xFF, p));
            }
        }
    }

    [Fact]
    public void TestGainSTMWithLoopBehavior()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        var stm = new GainSTM(CreateGains(2), new SamplingConfig(1), new GainSTMOption());
        autd.Send(new WithLoopBehavior(stm, LoopBehavior.Once, Segment.S1, TransitionMode.SyncIdx));
        foreach (var dev in autd)
        {
            Assert.Equal(1u, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S1));
            Assert.Equal(LoopBehavior.Once, autd.Link<Audit>().StmLoopBehavior(dev.Idx(), Segment.S1));
            Assert.True(autd.Link<Audit>().IsStmGainMode(dev.Idx(), Segment.S1));
            Assert.Equal(1, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S1));
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S1, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0xFF, p));
            }
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S1, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0xFF, p));
            }
        }

        autd.Send(new WithLoopBehavior(stm, LoopBehavior.Finite(10), Segment.S0, null));
        foreach (var dev in autd)
        {
            Assert.Equal(1u, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S0));
            Assert.Equal(LoopBehavior.Finite(10), autd.Link<Audit>().StmLoopBehavior(dev.Idx(), Segment.S0));
            Assert.True(autd.Link<Audit>().IsStmGainMode(dev.Idx(), Segment.S0));
            Assert.Equal(1, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S0));
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0xFF, p));
            }
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.All(phases, p => Assert.Equal(0xFF, p));
            }
        }
    }

    [Fact]
    public void TestChangeGainSTMSegment()
    {
        var autd = CreateController(1);

        autd.Send(new ReadsFPGAState(_ => true));
        autd.Send(Silencer.Disable());

        var infos = autd.FPGAState();
        Assert.Equal(Segment.S0, infos[0]?.CurrentGainSegment());
        Assert.Null(infos[0]?.CurrentSTMSegment());

        var stm = new GainSTM(CreateGains(2), 1.0f * Hz, new GainSTMOption());

        autd.Send(stm);
        Assert.Equal(Segment.S0, autd.Link<Audit>().CurrentStmSegment(0));
        infos = autd.FPGAState();
        Assert.Null(infos[0]?.CurrentGainSegment());
        Assert.Equal(Segment.S0, infos[0]?.CurrentSTMSegment());

        autd.Send(new WithSegment(stm, Segment.S1, TransitionMode.Immediate));
        Assert.Equal(Segment.S1, autd.Link<Audit>().CurrentStmSegment(0));
        infos = autd.FPGAState();
        Assert.Null(infos[0]?.CurrentGainSegment());
        Assert.Equal(Segment.S1, infos[0]?.CurrentSTMSegment());

        autd.Send(new WithSegment(stm, Segment.S0, null));
        Assert.Equal(Segment.S1, autd.Link<Audit>().CurrentStmSegment(0));
        infos = autd.FPGAState();
        Assert.Null(infos[0]?.CurrentGainSegment());
        Assert.Equal(Segment.S1, infos[0]?.CurrentSTMSegment());

        autd.Send(SwapSegment.GainSTM(Segment.S0, TransitionMode.Immediate));
        Assert.Equal(Segment.S0, autd.Link<Audit>().CurrentStmSegment(0));
        infos = autd.FPGAState();
        Assert.Null(infos[0]?.CurrentGainSegment());
        Assert.Equal(Segment.S0, infos[0]?.CurrentSTMSegment());
    }
}
