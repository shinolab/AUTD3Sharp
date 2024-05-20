namespace tests.Driver.Datagram.Gain;

[Gain]
partial class ForCacheTestGain
{
    internal int CalcCnt;

    Dictionary<int, Drive[]> Calc(Geometry geometry)
    {
        CalcCnt++;
        return Transform(geometry, (_) => (_) => new Drive { Phase = new Phase(0x90), Intensity = new EmitIntensity(0x80) });
    }
}

public class CacheTest
{
    [Fact]
    public async Task Cache()
    {
        var autd = await AUTDTest.CreateController();

        await autd.SendAsync(new Uniform(new EmitIntensity(0x80)).WithPhase(new Phase(0x90)).WithCache());

        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }
    }

    [Fact]
    public async Task CacheCheckOnce()
    {
        var autd = await AUTDTest.CreateController();
        {
            var g = new ForCacheTestGain();
            await autd.SendAsync(g);
            Assert.Equal(1, g.CalcCnt);
            await autd.SendAsync(g);
            Assert.Equal(2, g.CalcCnt);
        }

        {
            var g = new ForCacheTestGain();
            var gc = g.WithCache();
            await autd.SendAsync(gc);
            Assert.Equal(1, g.CalcCnt);
            await autd.SendAsync(gc);
            Assert.Equal(1, g.CalcCnt);
        }
    }

    [Fact]
    public async Task CacheCheckOnlyForEnabled()
    {
        var autd = await AUTDTest.CreateController();
        autd.Geometry[0].Enable = false;

        var g = new ForCacheTestGain();
        var gc = g.WithCache();
        await autd.SendAsync(gc);

        Assert.False(gc.Drives().ContainsKey(0));
        Assert.True(gc.Drives().ContainsKey(1));

        {
            var (intensities, phases) = autd.Link.Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, phases) = autd.Link.Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }
    }
}