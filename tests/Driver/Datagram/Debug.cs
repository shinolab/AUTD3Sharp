using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Datagram;

public class DebugTest
{
    [Fact]
    public async Task TestDebugOutputIdx()
    {
        using var autd = await CreateController();

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(new byte[] { 0x00, 0x00, 0x00, 0x00 }, autd.Link.DebugTypes(dev.Idx));
            Assert.Equal(new ushort[] { 0x0000, 0x0000, 0x0000, 0x0000 }, autd.Link.DebugValues(dev.Idx));
        }

        Assert.True(await autd.SendAsync(new DebugSettings(_ => new DebugType[] { DebugType.None(), DebugType.BaseSignal(), DebugType.Thermo(), DebugType.ForceFan() })));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(new byte[] { 0x00, 0x01, 0x02, 0x03 }, autd.Link.DebugTypes(dev.Idx));
            Assert.Equal(new ushort[] { 0x0000, 0x0000, 0x0000, 0x0000 }, autd.Link.DebugValues(dev.Idx));
        }

        Assert.True(await autd.SendAsync(new DebugSettings(_ => new DebugType[] { DebugType.Sync(), DebugType.ModSegment(), DebugType.ModIdx(0x01), DebugType.StmSegment() })));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(new byte[] { 0x10, 0x20, 0x21, 0x50 }, autd.Link.DebugTypes(dev.Idx));
            Assert.Equal(new ushort[] { 0x0000, 0x0000, 0x0001, 0x0000 }, autd.Link.DebugValues(dev.Idx));
        }

        Assert.True(await autd.SendAsync(new DebugSettings(dev => new DebugType[] { DebugType.StmIdx(0x02), DebugType.IsStmMode(), DebugType.PwmOut(dev[3]), DebugType.Direct(true) })));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(new byte[] { 0x51, 0x52, 0xE0, 0xF0 }, autd.Link.DebugTypes(dev.Idx));
            Assert.Equal(new ushort[] { 0x0002, 0x0000, 0x0003, 0x0001 }, autd.Link.DebugValues(dev.Idx));
        }
    }
}
