using AUTD3Sharp.Gain.Holo;

namespace tests.Gain.Holo;

public class ConstraintTest
{
    [Fact]
    public void Uniform()
    {
        var autd = CreateController();
        var g = new Greedy([(autd.Center() + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Center() + new Vector3(-30, 0, 150), 5e3f * Pa)], new GreedyOption { EmissionConstraint = EmissionConstraint.Uniform(new EmitIntensity(0x80)) });
        autd.Send(g);
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public void Normalize()
    {
        var autd = CreateController();
        var g = new Greedy([(autd.Center() + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Center() + new Vector3(-30, 0, 150), 5e3f * Pa)], new GreedyOption { EmissionConstraint = EmissionConstraint.Normalize });
        autd.Send(g);
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.Contains(intensities, d => d != 0);
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public void Clamp()
    {
        var autd = CreateController();
        {
            var g = new Greedy([(autd.Center() + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Center() + new Vector3(-30, 0, 150), 5e3f * Pa)], new GreedyOption { EmissionConstraint = EmissionConstraint.Clamp(new EmitIntensity(67), new EmitIntensity(85)) });
            autd.Send(g);
            foreach (var dev in autd)
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
                Assert.All(intensities, d => Assert.True(67 <= d));
                Assert.All(intensities, d => Assert.True(d <= 85));
                Assert.Contains(phases, p => p != 0);
            }
        }

        {
            var g = new Greedy([(autd.Center() + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Center() + new Vector3(-30, 0, 150), 5e3f * Pa)], new GreedyOption { EmissionConstraint = EmissionConstraint.Clamp(new EmitIntensity(10), new EmitIntensity(20)) });
            autd.Send(g);
            foreach (var dev in autd)
            {
                var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
                Assert.All(intensities, d => Assert.True(10 <= d));
                Assert.All(intensities, d => Assert.True(d <= 20));
                Assert.Contains(phases, p => p != 0);
            }
        }
    }

    [Fact]
    public void Multiply()
    {
        var autd = CreateController();
        var g = new Greedy([(autd.Center() + new Vector3(30, 0, 150), 5e3f * Pa), (autd.Center() + new Vector3(-30, 0, 150), 5e3f * Pa)], new GreedyOption { EmissionConstraint = EmissionConstraint.Multiply(0) });
        autd.Send(g);
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.Contains(phases, p => p != 0);
        }
    }
}