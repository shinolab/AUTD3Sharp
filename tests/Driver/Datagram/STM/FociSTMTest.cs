using AUTD3Sharp.Driver.Datagram;
using System.Runtime.InteropServices;
using Point3 = AUTD3Sharp.Utils.Point3;

namespace tests.Driver.Datagram.STM;

public class FociSTMTest
{

    private static IEnumerable<Point3> CreateFoci(int size) =>
        Enumerable.Range(0, size).Select(i => 2 * MathF.PI * i / size).Select(theta =>
            new Point3(0, 0, 150) + 30.0f * new Vector3(MathF.Cos(theta), MathF.Sin(theta), 0));

    [Fact]
    public void TestFociSTMFreq()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        var stm = new FociSTM(foci: CreateFoci(2), config: 1.0f * Hz);
        Assert.Equal(20000u, stm.SamplingConfig().Division);
        autd.Send(stm);
        foreach (var dev in autd)
        {
            Assert.False(autd.Link<Audit>().IsStmGainMode(dev.Idx(), Segment.S0));
            Assert.Equal((uint)(dev.SoundSpeed / 1000.0f * 64.0f), autd.Link<Audit>().StmSoundSpeed(dev.Idx(), Segment.S0));
            Assert.Equal(2, autd.Link<Audit>().StmCycle(dev.Idx(), Segment.S0));
            Assert.Equal(20000u, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S0));
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
        }
    }

    [Fact]
    public void TestFociSTMFreqNearest()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        var stm = new FociSTM(foci: CreateFoci(2), config: 1.0f * Hz).IntoNearest();
        autd.Send(stm);
        foreach (var dev in autd)
        {
            Assert.False(autd.Link<Audit>().IsStmGainMode(dev.Idx(), Segment.S0));
            Assert.Equal((uint)(dev.SoundSpeed / 1000.0f * 64.0f), autd.Link<Audit>().StmSoundSpeed(dev.Idx(), Segment.S0));
            Assert.Equal(20000u, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S0));
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
        }
    }

    [Fact]
    public void TestFociSTMPeriod()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        var stm = new FociSTM(foci: CreateFoci(2), config: Duration.FromSecs(1));
        autd.Send(stm);
        foreach (var dev in autd)
        {
            Assert.False(autd.Link<Audit>().IsStmGainMode(dev.Idx(), Segment.S0));
            Assert.Equal((uint)(dev.SoundSpeed / 1000.0f * 64.0f), autd.Link<Audit>().StmSoundSpeed(dev.Idx(), Segment.S0));
            Assert.Equal(20000u, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S0));
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
        }
    }

    [Fact]
    public void TestFociSTMPeriodNearest()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        var stm = new FociSTM(foci: CreateFoci(2), config: Duration.FromSecs(1)).IntoNearest();
        autd.Send(stm);
        foreach (var dev in autd)
        {
            Assert.False(autd.Link<Audit>().IsStmGainMode(dev.Idx(), Segment.S0));
            Assert.Equal((uint)(dev.SoundSpeed / 1000.0f * 64.0f), autd.Link<Audit>().StmSoundSpeed(dev.Idx(), Segment.S0));
            Assert.Equal(20000u, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S0));
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
        }
    }

    [Fact]
    public void TestFociSTMSamplingConfig()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        var stm = new FociSTM(foci: CreateFoci(2), config: new SamplingConfig(1));
        autd.Send(stm);
        foreach (var dev in autd)
        {
            Assert.False(autd.Link<Audit>().IsStmGainMode(dev.Idx(), Segment.S0));
            Assert.Equal((uint)(dev.SoundSpeed / 1000.0f * 64.0f), autd.Link<Audit>().StmSoundSpeed(dev.Idx(), Segment.S0));
            Assert.Equal(1, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S0));
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
        }
    }

    [Fact]
    public void TestFociSTMWithLoopBehavior()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        var stm = new FociSTM(foci: CreateFoci(2), config: new SamplingConfig(1));
        autd.Send(new WithLoopBehavior(stm, LoopBehavior.Once, Segment.S1, TransitionMode.SyncIdx));
        foreach (var dev in autd)
        {
            Assert.Equal(1u, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S1));
            Assert.Equal(LoopBehavior.Once, autd.Link<Audit>().StmLoopBehavior(dev.Idx(), Segment.S1));
            Assert.False(autd.Link<Audit>().IsStmGainMode(dev.Idx(), Segment.S1));
            Assert.Equal((uint)(dev.SoundSpeed / 1000.0f * 64.0f), autd.Link<Audit>().StmSoundSpeed(dev.Idx(), Segment.S1));
            Assert.Equal(1, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S1));
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S1, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S1, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
        }

        autd.Send(new WithLoopBehavior(stm, LoopBehavior.Finite(10), Segment.S0, null));
        foreach (var dev in autd)
        {
            Assert.Equal(1u, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S0));
            Assert.Equal(LoopBehavior.Finite(10), autd.Link<Audit>().StmLoopBehavior(dev.Idx(), Segment.S0));
            Assert.False(autd.Link<Audit>().IsStmGainMode(dev.Idx(), Segment.S0));
            Assert.Equal((uint)(dev.SoundSpeed / 1000.0f * 64.0f), autd.Link<Audit>().StmSoundSpeed(dev.Idx(), Segment.S0));
            Assert.Equal(1, autd.Link<Audit>().StmFreqDivision(dev.Idx(), Segment.S0));
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
        }
    }

    [Fact]
    public void TestChangeFociSTMSegment()
    {
        var autd = CreateController(1);

        autd.Send(new ReadsFPGAState(_ => true));
        autd.Send(Silencer.Disable());

        var infos = autd.FPGAState();
        Assert.Equal(Segment.S0, infos[0]?.CurrentGainSegment());
        Assert.Null(infos[0]?.CurrentSTMSegment());

        var stm = new FociSTM(CreateFoci(2), 1.0f * Hz);

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

        autd.Send(SwapSegment.FociSTM(Segment.S0, TransitionMode.Immediate));
        Assert.Equal(Segment.S0, autd.Link<Audit>().CurrentStmSegment(0));
        infos = autd.FPGAState();
        Assert.Null(infos[0]?.CurrentGainSegment());
        Assert.Equal(Segment.S0, infos[0]?.CurrentSTMSegment());
    }

    [Fact]
    public void TestFociSTM1()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        const int size = 2;
        var center = autd.Center() + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(Enumerable.Range(0, size).Select(_ => center), 1.0f * Hz).IntoNearest());
        foreach (var dev in autd)
            for (var i = 0; i < size; i++)
                Assert.All(autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, i).Item1, d => Assert.Equal(0xFF, d));
    }

    [Fact]
    public void TestFociSTM2()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        const int size = 2;
        var center = autd.Center() + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(Enumerable.Range(0, size).Select(i => new ControlPoints([center, center])), 1.0f * Hz).IntoNearest());
        foreach (var dev in autd)
            for (var i = 0; i < size; i++)
                Assert.All(autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, i).Item1, d => Assert.Equal(0xFF, d));
    }

    [Fact]
    public void TestFociSTM3()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        const int size = 2;
        var center = autd.Center() + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(Enumerable.Range(0, size).Select(i => new ControlPoints([center, center, center])), 1.0f * Hz).IntoNearest());
        foreach (var dev in autd)
            for (var i = 0; i < size; i++)
                Assert.All(autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, i).Item1, d => Assert.Equal(0xFF, d));
    }

    [Fact]
    public void TestFociSTM4()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        const int size = 2;
        var center = autd.Center() + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(Enumerable.Range(0, size).Select(i => new ControlPoints([center, center, center, center])), 1.0f * Hz).IntoNearest());
        foreach (var dev in autd)
            for (var i = 0; i < size; i++)
                Assert.All(autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, i).Item1, d => Assert.Equal(0xFF, d));
    }

    [Fact]
    public void TestFociSTM5()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        const int size = 2;
        var center = autd.Center() + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(Enumerable.Range(0, size).Select(i => new ControlPoints([center, center, center, center, center])), 1.0f * Hz).IntoNearest());
        foreach (var dev in autd)
            for (var i = 0; i < size; i++)
                Assert.All(autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, i).Item1, d => Assert.Equal(0xFF, d));
    }

    [Fact]
    public void TestFociSTM6()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        const int size = 2;
        var center = autd.Center() + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(Enumerable.Range(0, size).Select(i => new ControlPoints([center, center, center, center, center, center])), 1.0f * Hz).IntoNearest());
        foreach (var dev in autd)
            for (var i = 0; i < size; i++)
                Assert.All(autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, i).Item1, d => Assert.Equal(0xFF, d));
    }

    [Fact]
    public void TestFociSTM7()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        const int size = 2;
        var center = autd.Center() + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(Enumerable.Range(0, size).Select(i => new ControlPoints([center, center, center, center, center, center, center])), 1.0f * Hz).IntoNearest());
        foreach (var dev in autd)
            for (var i = 0; i < size; i++)
                Assert.All(autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, i).Item1, d => Assert.Equal(0xFF, d));
    }

    [Fact]
    public void TestFociSTM8()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        const int size = 2;
        var center = autd.Center() + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(Enumerable.Range(0, size).Select(i => new ControlPoints([center, center, center, center, center, center, center, center])), 1.0f * Hz).IntoNearest());
        foreach (var dev in autd)
            for (var i = 0; i < size; i++)
                Assert.All(autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, i).Item1, d => Assert.Equal(0xFF, d));
    }

    [Fact]
    public void TestFociSTM0()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        Assert.Throws<ArgumentOutOfRangeException>(() => autd.Send(new FociSTM(Enumerable.Empty<ControlPoints>(), 1.0f * Hz).IntoNearest()));
    }

    [Fact]
    public void TestFociSTM9()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        Assert.Throws<ArgumentOutOfRangeException>(() => autd.Send(new FociSTM(Enumerable.Range(0, 2).Select(i => new ControlPoints([Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin])), 1.0f * Hz).IntoNearest()));
    }

    [Fact]
    public void TestFociSTMMixed()
    {
        var autd = CreateController();
        autd.Send(Silencer.Disable());
        Assert.Throws<AUTDException>(() => autd.Send(new FociSTM([new ControlPoints([Point3.Origin]), new ControlPoints([Point3.Origin, Point3.Origin])], 1.0f * Hz).IntoNearest()));
    }
}
