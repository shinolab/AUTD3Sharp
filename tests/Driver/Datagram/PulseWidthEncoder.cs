namespace tests.Driver.Datagram;

public class PulseWidthEncoderTest
{
    [Fact]

    public async Task PulseWidthEncoder()
    {
        var autd = await AUTDTest.CreateController();

        var rnd = new Random();
        var buf = Enumerable.Range(0, 65536).Select(_ => (ushort)rnd.Next(0, 257)).OrderBy(i => i).ToArray();

        await autd.SendAsync(new AUTD3Sharp.PulseWidthEncoder(buf));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(buf, autd.Link.PulseWidthEncoderTable(dev.Idx));
        }
    }

    [Fact]

    public async Task PulseWidthEncoderDefault()
    {
        var autd = await AUTDTest.CreateController();

        var buf = Enumerable.Range(0, 255 * 255).Select(i =>
            (ushort)Math.Round(Math.Asin((double)i / 255 / 255) / Math.PI * 512)
        ).Concat(Enumerable.Repeat((ushort)256, 65536 - 255 * 255)).ToArray();

        await autd.SendAsync(new AUTD3Sharp.PulseWidthEncoder());
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(buf, autd.Link.PulseWidthEncoderTable(dev.Idx));
        }
    }
}