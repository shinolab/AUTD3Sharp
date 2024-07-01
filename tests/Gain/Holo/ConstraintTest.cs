using AUTD3Sharp.Gain.Holo;
using static AUTD3Sharp.Units;

namespace tests.Gain.Holo;

public class ConstraintTest
{
    [Fact]
    public async Task Uniform()
    {
        var autd = await AUTDTest.CreateController();

        var backend = new NalgebraBackend();
        var g = new Naive(backend, [(autd.Geometry.Center + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Geometry.Center + new Vector3(-30, 0, 150), 5e3f * Pa)])
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
    public async Task Normalize()
    {
        var autd = await AUTDTest.CreateController();

        var backend = new NalgebraBackend();
        var g = new Naive(backend, [(autd.Geometry.Center + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Geometry.Center + new Vector3(-30, 0, 150), 5e3f * Pa)])
            .WithConstraint(EmissionConstraint.Normalize);

        await autd.SendAsync(g);

        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.Contains(intensities, d => d != 0);
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public async Task Clamp()
    {
        {
            var autd = await AUTDTest.CreateController();

            var backend = new NalgebraBackend();
            var g = new Naive(backend, [(autd.Geometry.Center + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Geometry.Center + new Vector3(-30, 0, 150), 5e3f * Pa)])
                .WithConstraint(EmissionConstraint.Clamp(new EmitIntensity(67), new EmitIntensity(85)));

            await autd.SendAsync(g);

            foreach (var dev in autd.Geometry)
            {
                var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
                Assert.All(intensities, d => Assert.True(67 <= d));
                Assert.All(intensities, d => Assert.True(d <= 85));
                Assert.Contains(phases, p => p != 0);
            }
        }

        {
            var autd = await AUTDTest.CreateController();

            var backend = new NalgebraBackend();
            var g = new Naive(backend, [(autd.Geometry.Center + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Geometry.Center + new Vector3(-30, 0, 150), 5e3f * Pa)])
                .WithConstraint(EmissionConstraint.Clamp(new EmitIntensity(10), new EmitIntensity(20)));

            await autd.SendAsync(g);

            foreach (var dev in autd.Geometry)
            {
                var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
                Assert.All(intensities, d => Assert.True(10 <= d));
                Assert.All(intensities, d => Assert.True(d <= 20));
                Assert.Contains(phases, p => p != 0);
            }
        }
    }

    [Fact]
    public async Task Multyply()
    {
        var autd = await AUTDTest.CreateController();

        var backend = new NalgebraBackend();
        var g = new Naive(backend, [(autd.Geometry.Center + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Geometry.Center + new Vector3(-30, 0, 150), 5e3f * Pa)])
            .WithConstraint(EmissionConstraint.Multiply(0));

        await autd.SendAsync(g);

        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.Contains(phases, p => p != 0);
        }
    }
}