namespace tests.Driver.Datagram;

public class GroupTest
{
    [Fact]
    public void TestGroup()
    {
        var autd = CreateController();

        autd.Send(new AUTD3Sharp.Group(dev => dev.Idx(), new GroupDictionary
        {
            { 0, (new Static(), new Null()) },
            { 1, (new Sine(freq: 150 * Hz, option: new SineOption()), new Uniform(intensity: Intensity.Max, phase: Phase.Zero)) }
        }));

        {
            var m = autd.Link<Audit>().Modulation(0, Segment.S0);
            Assert.Equal(2, m.Length);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link<Audit>().Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var m = autd.Link<Audit>().Modulation(1, Segment.S0);
            Assert.Equal(80, m.Length);
            var (intensities, phases) = autd.Link<Audit>().Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }

        autd.Send(new AUTD3Sharp.Group(dev => dev.Idx(), new GroupDictionary()
            {
                { 1, new Null() }, { 0, new Uniform(intensity: Intensity.Max, phase: Phase.Zero) }
            }));

        {
            var (intensities, phases) = autd.Link<Audit>().Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, _) = autd.Link<Audit>().Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
        }

        autd.Send(new AUTD3Sharp.Group(dev => dev.Idx() switch
        {
            0 => 0,
            _ => null
        }, new GroupDictionary()
    {
            {0, new Uniform(intensity: Intensity.Max, phase: Phase.Zero)}
    }));
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, _) = autd.Link<Audit>().Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
        }
    }

    [Fact]
    public void TestGroupCheckOnlyForEnabled()
    {
        using var autd = CreateController();

        autd.Send(new AUTD3Sharp.Group(dev =>
        {
            return dev.Idx() == 0 ? null : 0;
        }, new GroupDictionary()
    {
            {0, (new Sine(freq: 150 * Hz, option: new SineOption()), new Uniform(intensity:new Intensity(0x80), phase: new Phase(0x90)))}
    }));

        {
            var (intensities, phases) = autd.Link<Audit>().Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }
    }
}
