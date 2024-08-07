namespace tests.Driver.Datagram;

public class PulseWidthEncoderTest
{
    [Fact]

    public async Task PulseWidthEncoder()
    {
        var autd = await AUTDTest.CreateController();

        var rnd = new Random();
        var buf = Enumerable.Range(0, 256).Select(_ => (byte)rnd.Next(0, 256)).ToArray();

        await autd.SendAsync(new AUTD3Sharp.PulseWidthEncoder(dev => i => buf[i]));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(buf, autd.Link.PulseWidthEncoderTable(dev.Idx));
        }
    }

    [Fact]

    public async Task PulseWidthEncoderDefault()
    {
        var autd = await AUTDTest.CreateController();

        var buf = Enumerable.Range(0, 256).Select(i =>
            (byte)Math.Round(Math.Asin((float)i / 255) / MathF.PI * 256)
        ).ToArray();

        await autd.SendAsync(new AUTD3Sharp.PulseWidthEncoder());
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(buf, autd.Link.PulseWidthEncoderTable(dev.Idx));
        }
    }
}