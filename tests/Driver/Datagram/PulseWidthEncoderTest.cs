namespace tests.Driver.Datagram;

public class PulseWidthEncoderTest
{
    [Fact]

    public void PulseWidthEncoder()
    {
        var autd = CreateController();

        var rnd = new Random();
        var buf = Enumerable.Range(0, 256).Select(_ => (byte)rnd.Next(0, 256)).ToArray();

        autd.Send(new AUTD3Sharp.PulseWidthEncoder(dev => i => buf[i]));
        foreach (var dev in autd)
        {
            Assert.Equal(buf, autd.Link<Audit>().PulseWidthEncoderTable(dev.Idx()));
        }
    }

    [Fact]

    public void PulseWidthEncoderDefault()
    {
        var autd = CreateController();

        var buf = Enumerable.Range(0, 256).Select(i =>
            (byte)Math.Round(Math.Asin((float)i / 255) / MathF.PI * 256)
        ).ToArray();

        autd.Send(new AUTD3Sharp.PulseWidthEncoder());
        foreach (var dev in autd)
        {
            Assert.Equal(buf, autd.Link<Audit>().PulseWidthEncoderTable(dev.Idx()));
        }
    }
}