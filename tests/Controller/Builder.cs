using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Controller;

public class ControllerBuilderTest
{
    [Fact]
    public void WithUltrasoundFreq()
    {
        var autd = new ControllerBuilder().AddDevice(new AUTD3(Vector3d.Zero)).WithUltrasoundFreq(41000 * Hz).Open(Audit.Builder());

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(41000u, autd.Link.UltrasoundFreq(dev.Idx));
        }
    }
}
