namespace tests.Modulation;

public class StaticTest
{
    [Fact]
    public async Task Static()
    {
        var autd = await new ControllerBuilder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        {
            var m = new AUTD3Sharp.Modulation.Static();
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            await autd.SendAsync(m);
            foreach (var dev in autd.Geometry)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
                var modExpect = new byte[] { 0xFF, 0xFF };
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(0xFFFFFFFFu, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }

        {
            var m = AUTD3Sharp.Modulation.Static.WithIntensity(new EmitIntensity(32)).WithLoopBehavior(LoopBehavior.Once);
            Assert.Equal(LoopBehavior.Once, m.LoopBehavior);
            await autd.SendAsync(m);
            foreach (var dev in autd.Geometry)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
#pragma warning disable IDE0230
                var modExpect = new byte[] { 32, 32 };
#pragma warning restore IDE0230
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Once, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(0xFFFFFFFFu, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }

    }

    [Fact]
    public void StaticDefault()
    {
#pragma warning disable CS8602, CS8605
        var m = new Static();
        var autd = AUTDTest.CreateControllerSync();
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDModulationStaticIsDefault((AUTD3Sharp.NativeMethods.ModulationPtr)typeof(Static).GetMethod("ModulationPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(m,
            [autd.Geometry])));
#pragma warning restore CS8602, CS8605
    }
}