using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Datagram;

public class ClearTest
{

    [Fact]
    public void TestClear()
    {
        using var autd = CreateController();
        autd.Send(new Uniform((new Phase(0x90), EmitIntensity.Max)));
        foreach (var dev in autd)
        {
            var m = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }

        autd.Send(new Clear());
        foreach (var dev in autd)
        {
            var m = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
    }
}
