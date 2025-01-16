using AUTD3Sharp.Gain.Holo;
using static AUTD3Sharp.Units;

namespace tests.Gain.Holo;

public class ConstraintTest
{
    [Fact]
    public void Uniform()
    {
        var autd = AUTDTest.CreateController();

        var backend = new NalgebraBackend();
        var g = new Naive(backend, [(autd.Center + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Center + new Vector3(-30, 0, 150), 5e3f * Pa)])
            .WithConstraint(EmissionConstraint.Uniform(new EmitIntensity(0x80)));

        autd.Send(g);

        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public void Normalize()
    {
        var autd = AUTDTest.CreateController();

        var backend = new NalgebraBackend();
        var g = new Naive(backend, [(autd.Center + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Center + new Vector3(-30, 0, 150), 5e3f * Pa)])
            .WithConstraint(EmissionConstraint.Normalize);

        autd.Send(g);

        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.Contains(intensities, d => d != 0);
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public void Clamp()
    {
        {
            var autd = AUTDTest.CreateController();

            var backend = new NalgebraBackend();
            var g = new Naive(backend, [(autd.Center + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Center + new Vector3(-30, 0, 150), 5e3f * Pa)])
                .WithConstraint(EmissionConstraint.Clamp(new EmitIntensity(67), new EmitIntensity(85)));

            autd.Send(g);

            foreach (var dev in autd)
            {
                var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
                Assert.All(intensities, d => Assert.True(67 <= d));
                Assert.All(intensities, d => Assert.True(d <= 85));
                Assert.Contains(phases, p => p != 0);
            }
        }

        {
            var autd = AUTDTest.CreateController();

            var backend = new NalgebraBackend();
            var g = new Naive(backend, [(autd.Center + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Center + new Vector3(-30, 0, 150), 5e3f * Pa)])
                .WithConstraint(EmissionConstraint.Clamp(new EmitIntensity(10), new EmitIntensity(20)));

            autd.Send(g);

            foreach (var dev in autd)
            {
                var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
                Assert.All(intensities, d => Assert.True(10 <= d));
                Assert.All(intensities, d => Assert.True(d <= 20));
                Assert.Contains(phases, p => p != 0);
            }
        }
    }

    [Fact]
    public void Multyply()
    {
        var autd = AUTDTest.CreateController();

        var backend = new NalgebraBackend();
        var g = new Naive(backend, [(autd.Center + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Center + new Vector3(-30, 0, 150), 5e3f * Pa)])
            .WithConstraint(EmissionConstraint.Multiply(0));

        autd.Send(g);

        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.Contains(phases, p => p != 0);
        }
    }
}