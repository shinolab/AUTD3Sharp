using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Datagram;

public class DebugTest
{
    [Fact]
    public async Task TestDebugSettings()
    {
        using var autd = await CreateController();

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal([0x00, 0x00, 0x00, 0x00], autd.Link.DebugTypes(dev.Idx));
            Assert.Equal([0x0000, 0x0000, 0x0000, 0x0000], autd.Link.DebugValues(dev.Idx));
        }

        await autd.SendAsync(new DebugSettings((_, gpio) => gpio switch
        {
            GPIOOut.O0 => DebugType.None,
            GPIOOut.O1 => DebugType.BaseSignal,
            GPIOOut.O2 => DebugType.Thermo,
            GPIOOut.O3 => DebugType.ForceFan,
            _ => throw new ArgumentOutOfRangeException(nameof(gpio), gpio, null)
        }));

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal([0x00, 0x01, 0x02, 0x03], autd.Link.DebugTypes(dev.Idx));
            Assert.Equal([0x0000, 0x0000, 0x0000, 0x0000], autd.Link.DebugValues(dev.Idx));
        }

        await autd.SendAsync(new DebugSettings((_, gpio) => gpio switch
        {
            GPIOOut.O0 => DebugType.Sync,
            GPIOOut.O1 => DebugType.ModSegment,
            GPIOOut.O2 => DebugType.ModIdx(0x01),
            GPIOOut.O3 => DebugType.StmSegment,
            _ => throw new ArgumentOutOfRangeException(nameof(gpio), gpio, null)
        }));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal([0x10, 0x20, 0x21, 0x50], autd.Link.DebugTypes(dev.Idx));
            Assert.Equal([0x0000, 0x0000, 0x0001, 0x0000], autd.Link.DebugValues(dev.Idx));
        }

        await autd.SendAsync(new DebugSettings((dev, gpio) => gpio switch
        {
            GPIOOut.O0 => DebugType.StmIdx(0x02),
            GPIOOut.O1 => DebugType.IsStmMode,
            GPIOOut.O2 => DebugType.PwmOut(dev[3]),
            GPIOOut.O3 => DebugType.Direct(true),
            _ => throw new ArgumentOutOfRangeException(nameof(gpio), gpio, null)
        }));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal([0x51, 0x52, 0xE0, 0xF0], autd.Link.DebugTypes(dev.Idx));
            Assert.Equal([0x0002, 0x0000, 0x0003, 0x0001], autd.Link.DebugValues(dev.Idx));
        }
    }
}
