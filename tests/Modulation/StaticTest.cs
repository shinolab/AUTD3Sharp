namespace tests.Modulation;

public class StaticTest
{
    [Fact]
    public async Task Static()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).OpenWithAsync(Audit.Builder());

        Assert.True(await autd.SendAsync(AUTD3Sharp.Modulation.Static.WithIntensity(new EmitIntensity(32))));
        foreach (var dev in autd.Geometry)
        {
            var mod = autd.Link.Modulation(dev.Idx);
#pragma warning disable IDE0230
            var modExpect = new byte[] { 32, 32 };
#pragma warning restore IDE0230
            Assert.Equal(modExpect, mod);
            Assert.Equal(0xFFFFFFFFu, autd.Link.ModulationFrequencyDivision(dev.Idx));
        }

        Assert.True(await autd.SendAsync(AUTD3Sharp.Modulation.Static.WithIntensity(32)));
        foreach (var dev in autd.Geometry)
        {
            var mod = autd.Link.Modulation(dev.Idx);
#pragma warning disable IDE0230
            var modExpect = new byte[] { 32, 32 };
#pragma warning restore IDE0230
            Assert.Equal(modExpect, mod);
            Assert.Equal(0xFFFFFFFFu, autd.Link.ModulationFrequencyDivision(dev.Idx));
        }
    }

    [Fact]
    public void StaticDefault()
    {
#pragma warning disable CS8602, CS8605
        var m = new Static();
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDModulationStaticIsDefault((AUTD3Sharp.NativeMethods.ModulationPtr)typeof(Static).GetMethod("ModulationPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(m, new object[] { })));
#pragma warning restore CS8602, CS8605
    }
}