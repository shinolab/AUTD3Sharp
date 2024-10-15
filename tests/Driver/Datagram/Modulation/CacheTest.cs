namespace tests.Driver.Datagram.Modulation;

[Modulation]
public partial class ForCacheTest
{
    internal int CalcCnt;

    private byte[] Calc()
    {
        CalcCnt++;
        return [0xFF, 0xFF];
    }
}

public class CacheTest
{
    [Fact]
    public async Task Cache()
    {
        {
            var autd1 = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());
            var autd2 = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

            var m = new Sine(150 * Hz);
            var mc = m.WithCache().WithLoopBehavior(LoopBehavior.Once);
            Assert.Equal(LoopBehavior.Once, mc.LoopBehavior);
            await autd1.SendAsync(m);
            await autd2.SendAsync(mc);
            foreach (var dev in autd2.Geometry)
            {
                var modExpect = autd1.Link.Modulation(dev.Idx, Segment.S0);
                var mod = autd2.Link.Modulation(dev.Idx, Segment.S0);
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd1.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(LoopBehavior.Once, autd2.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(autd1.Link.ModulationFreqDivision(dev.Idx, Segment.S0), autd2.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }

            _ = m.WithCache();
        }

        GC.Collect();
    }

    [Fact]
    public async Task CacheCheckOnce()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        {
            var m = new ForCacheTest();
            await autd.SendAsync(m);
            Assert.Equal(1, m.CalcCnt);
            await autd.SendAsync(m);
            Assert.Equal(2, m.CalcCnt);
        }

        {
            var m = new ForCacheTest();
            var mc = m.WithCache();
            await autd.SendAsync(mc);
            Assert.Equal(1, m.CalcCnt);
            await autd.SendAsync(mc);
            Assert.Equal(1, m.CalcCnt);
        }
    }

    [Fact]
    public async Task CacheCheckFree()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var mc = new ForCacheTest().WithCache();
        {
            await autd.SendAsync(mc);
        }

        await autd.SendAsync(mc);
    }
}