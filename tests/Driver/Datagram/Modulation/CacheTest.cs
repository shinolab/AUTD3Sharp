namespace tests.Driver.Datagram.Modulation;

public class CacheTest
{
    [Fact]
    public void Cache()
    {
        {
            var autd1 = Controller.Builder([new AUTD3(Point3.Origin)]).Open(Audit.Builder());
            var autd2 = Controller.Builder([new AUTD3(Point3.Origin)]).Open(Audit.Builder());

            var m = new Sine(150 * Hz);
            var mc = m.WithCache().WithLoopBehavior(LoopBehavior.Once);
            Assert.Equal(LoopBehavior.Once, mc.LoopBehavior);
            autd1.Send(m);
            autd2.Send(mc);
            foreach (var dev in autd2)
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
}