namespace tests.Modulation;

public class StaticTest
{
    [Fact]
    public void Static()
    {
        var autd = CreateController(1);

        {
            var m = new Static();
            autd.Send(m);
            foreach (var dev in autd)
            {
                var mod = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
                var modExpect = new byte[] { 0xFF, 0xFF };
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link<Audit>().ModulationLoopBehavior(dev.Idx(), Segment.S0));
                Assert.Equal(0xFFFFu, autd.Link<Audit>().ModulationFreqDivision(dev.Idx(), Segment.S0));
            }
        }

        {
            var m = new Static()
            {
                Intensity = 32
            };
            autd.Send(m);
            foreach (var dev in autd)
            {
                var mod = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
#pragma warning disable IDE0230
                var modExpect = new byte[] { 32, 32 };
#pragma warning restore IDE0230
                Assert.Equal(modExpect, mod);
                Assert.Equal(0xFFFFu, autd.Link<Audit>().ModulationFreqDivision(dev.Idx(), Segment.S0));
            }
        }
    }

    [Fact]
    public void StaticDefault()
    {
        var m = new Static();
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDModulationStaticIsDefault(m.Intensity));
    }
}