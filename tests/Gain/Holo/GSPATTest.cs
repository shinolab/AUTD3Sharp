using AUTD3Sharp.Gain.Holo;
using static AUTD3Sharp.Units;

namespace tests.Gain.Holo;

public class GSPATTest
{
    [Fact]
    public async Task GSPAT()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.Zero)).OpenAsync(Audit.Builder());

        var backend = new NalgebraBackend();
        var g = new GSPAT<NalgebraBackend>(backend)
            .AddFocus(autd.Geometry.Center + new Vector3d(30, 0, 150), 5e3 * Pa)
            .AddFociFromIter(new double[] { -40 }.Select(x => (autd.Geometry.Center + new Vector3d(x, 0, 150), 5e3 * Pa)))
            .WithRepeat(100)
            .WithConstraint(EmissionConstraint.Uniform(new EmitIntensity(0x80)));

        await autd.SendAsync(g);

        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public async Task GSPATDefault()
    {
#pragma warning disable CS8602, CS8605
        var autd = await AUTDTest.CreateController();
        var backend = new NalgebraBackend();
        var g = new GSPAT<NalgebraBackend>(backend);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsGainHolo.AUTDGainGSPATIsDefault((AUTD3Sharp.NativeMethods.GainPtr)typeof(GSPAT<NalgebraBackend>).GetMethod("GainPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(g,
            [autd.Geometry])));
#pragma warning restore CS8602, CS8605
    }
}