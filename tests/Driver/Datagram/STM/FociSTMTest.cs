using System.Runtime.InteropServices;

namespace tests.Driver.Datagram.STM;

public class FociSTMTest
{
    [Fact]
    public async Task TestFociSTM()
    {
        var autd = await AUTDTest.CreateController();

        await autd.SendAsync(Silencer.Disable());

        const float radius = 30.0f;
        const int size = 2;
        var center = autd.Geometry.Center + new Vector3(0, 0, 150);
        var stm = new FociSTM(1.0f * Hz, Enumerable.Range(0, size).Select(i => 2 * MathF.PI * i / size).Select(theta =>
                center + radius * new Vector3(MathF.Cos(theta), MathF.Sin(theta), 0)));
        Assert.Equal(1.0f * Hz, stm.Freq);
        Assert.Equal(TimeSpan.FromSeconds(1), stm.Period);
        Assert.Equal(20000u, stm.SamplingConfig.Division);
        await autd.SendAsync(stm);

        foreach (var dev in autd.Geometry) Assert.False(autd.Link.IsStmGainMode(dev.Idx, Segment.S0));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal((uint)(dev.SoundSpeed / 1000.0f * 64.0f), autd.Link.StmSoundSpeed(dev.Idx, Segment.S0));
            Assert.Equal(20000u, autd.Link.StmFreqDivision(dev.Idx, Segment.S0));
        }

        stm = FociSTM.Nearest(1.0f * Hz, Enumerable.Range(0, size).Select(i => 2 * MathF.PI * i / size).Select(theta =>
                center + radius * new Vector3(MathF.Cos(theta), MathF.Sin(theta), 0)));
        await autd.SendAsync(stm);
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(20000u, autd.Link.StmFreqDivision(dev.Idx, Segment.S0));
            Assert.Equal(LoopBehavior.Infinite, autd.Link.StmLoopBehavior(dev.Idx, Segment.S0));
        }

        stm = new FociSTM(TimeSpan.FromSeconds(1), Enumerable.Range(0, size).Select(i => 2 * MathF.PI * i / size).Select(theta =>
              center + radius * new Vector3(MathF.Cos(theta), MathF.Sin(theta), 0)));
        await autd.SendAsync(stm);
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(20000u, autd.Link.StmFreqDivision(dev.Idx, Segment.S0));
            Assert.Equal(LoopBehavior.Infinite, autd.Link.StmLoopBehavior(dev.Idx, Segment.S0));
        }

        stm = FociSTM.Nearest(TimeSpan.FromSeconds(1), Enumerable.Range(0, size).Select(i => 2 * MathF.PI * i / size).Select(theta =>
              center + radius * new Vector3(MathF.Cos(theta), MathF.Sin(theta), 0)));
        await autd.SendAsync(stm);
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(20000u, autd.Link.StmFreqDivision(dev.Idx, Segment.S0));
            Assert.Equal(LoopBehavior.Infinite, autd.Link.StmLoopBehavior(dev.Idx, Segment.S0));
        }

        stm = new FociSTM(new SamplingConfig(1), [center, center]).WithLoopBehavior(LoopBehavior.Once);
        await autd.SendAsync(stm);
        Assert.Equal(LoopBehavior.Once, stm.LoopBehavior);
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
                Assert.Contains(phases, p => p != 0);
            }
            {
                var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 1);
                Assert.All(intensities, d => Assert.Equal(0xFF, d));
                Assert.Contains(phases, p => p != 0);
            }
        }
    }

    [Fact]
    public async Task TestChangeFociSTMSegment()
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
        var stm = new FociSTM(1.0f * Hz, Enumerable.Range(0, size).Select(i => 2 * MathF.PI * i / size).Select(theta =>
                center + radius * new Vector3(MathF.Cos(theta), MathF.Sin(theta), 0)));

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

    [Fact]
    public void TestFociSTM1()
    {
        var autd = AUTDTest.CreateControllerSync();
        autd.Send(Silencer.Disable());
        const int size = 100;
        var center = autd.Geometry.Center + new Vector3(0, 0, 150);

        autd.Send(FociSTM.Nearest(1.0f * Hz, Enumerable.Range(0, size).Select(i => center)));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(0xFF, d));

        autd.Send(FociSTM.Nearest(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoint(center))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(0xFF, d));

        autd.Send(FociSTM.Nearest(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints1(center).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));
    }

    [Fact]
    public void TestFociSTMN2()
    {
        var autd = AUTDTest.CreateControllerSync();
        autd.Send(Silencer.Disable());
        const int size = 100;
        var center = autd.Geometry.Center + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints2((center, center)).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));

        autd.Send(FociSTM.Nearest(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints2((center, center)).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));
    }

    [Fact]
    public void TestFociSTM3()
    {
        var autd = AUTDTest.CreateControllerSync();
        autd.Send(Silencer.Disable());
        const int size = 100;
        var center = autd.Geometry.Center + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints3((center, center, center)).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));

        autd.Send(FociSTM.Nearest(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints3((center, center, center)).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));
    }

    [Fact]
    public void TestFociSTM4()
    {
        var autd = AUTDTest.CreateControllerSync();
        autd.Send(Silencer.Disable());
        const int size = 100;
        var center = autd.Geometry.Center + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints4((center, center, center, center)).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));

        autd.Send(FociSTM.Nearest(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints4((center, center, center, center)).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));
    }

    [Fact]
    public void TestFociSTM5()
    {
        var autd = AUTDTest.CreateControllerSync();
        autd.Send(Silencer.Disable());
        const int size = 100;
        var center = autd.Geometry.Center + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints5((center, center, center, center, center)).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));

        autd.Send(FociSTM.Nearest(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints5((center, center, center, center, center)).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));
    }

    [Fact]
    public void TestFociSTM6()
    {
        var autd = AUTDTest.CreateControllerSync();
        autd.Send(Silencer.Disable());
        const int size = 100;
        var center = autd.Geometry.Center + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints6((center, center, center, center, center, center)).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));

        autd.Send(FociSTM.Nearest(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints6((center, center, center, center, center, center)).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));
    }

    [Fact]
    public void TestFociSTM7()
    {
        var autd = AUTDTest.CreateControllerSync();
        autd.Send(Silencer.Disable());
        const int size = 100;
        var center = autd.Geometry.Center + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints7((center, center, center, center, center, center, center)).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));

        autd.Send(FociSTM.Nearest(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints7((center, center, center, center, center, center, center)).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));
    }

    [Fact]
    public void TestFociSTM8()
    {
        var autd = AUTDTest.CreateControllerSync();
        autd.Send(Silencer.Disable());
        const int size = 100;
        var center = autd.Geometry.Center + new Vector3(0, 0, 150);
        autd.Send(new FociSTM(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints8((center, center, center, center, center, center, center, center)).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));

        autd.Send(FociSTM.Nearest(1.0f * Hz, Enumerable.Range(0, size).Select(i => new ControlPoints8((center, center, center, center, center, center, center, center)).WithIntensity((byte)i))));
        foreach (var dev in autd.Geometry) for (var i = 0; i < size; i++) Assert.All(autd.Link.Drives(dev.Idx, Segment.S0, i).Item1, d => Assert.Equal(i, d));
    }
}