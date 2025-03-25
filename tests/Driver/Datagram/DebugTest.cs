namespace tests.Driver.Datagram;

public class DebugTest
{
    [Fact]
    public void TestGPIOOutputs()
    {
        using var autd = CreateController();

        foreach (var dev in autd)
        {
            Assert.Equal([0x00, 0x00, 0x00, 0x00], autd.Link<Audit>().GPIOOutputTypes(dev.Idx()));
            Assert.Equal([0x0000, 0x0000, 0x0000, 0x0000], autd.Link<Audit>().DebugValues(dev.Idx()));
        }

        autd.Send(new GPIOOutputs((_, gpio) => gpio switch
        {
            AUTD3Sharp.GPIOOut.O0 => GPIOOutputType.None,
            AUTD3Sharp.GPIOOut.O1 => GPIOOutputType.BaseSignal,
            AUTD3Sharp.GPIOOut.O2 => GPIOOutputType.Thermo,
            AUTD3Sharp.GPIOOut.O3 => GPIOOutputType.ForceFan,
            _ => throw new ArgumentOutOfRangeException(nameof(gpio), gpio, null)
        }));

        foreach (var dev in autd)
        {
            Assert.Equal([0x00, 0x01, 0x02, 0x03], autd.Link<Audit>().GPIOOutputTypes(dev.Idx()));
            Assert.Equal([0x0000, 0x0000, 0x0000, 0x0000], autd.Link<Audit>().DebugValues(dev.Idx()));
        }

        autd.Send(new GPIOOutputs((_, gpio) => gpio switch
        {
            AUTD3Sharp.GPIOOut.O0 => GPIOOutputType.Sync,
            AUTD3Sharp.GPIOOut.O1 => GPIOOutputType.ModSegment,
            AUTD3Sharp.GPIOOut.O2 => GPIOOutputType.ModIdx(0x01),
            AUTD3Sharp.GPIOOut.O3 => GPIOOutputType.StmSegment,
            _ => throw new ArgumentOutOfRangeException(nameof(gpio), gpio, null)
        }));
        foreach (var dev in autd)
        {
            Assert.Equal([0x10, 0x20, 0x21, 0x50], autd.Link<Audit>().GPIOOutputTypes(dev.Idx()));
            Assert.Equal([0x0000, 0x0000, 0x0001, 0x0000], autd.Link<Audit>().DebugValues(dev.Idx()));
        }

        autd.Send(new GPIOOutputs((dev, gpio) => gpio switch
        {
            AUTD3Sharp.GPIOOut.O0 => GPIOOutputType.StmIdx(0x02),
            AUTD3Sharp.GPIOOut.O1 => GPIOOutputType.IsStmMode,
            AUTD3Sharp.GPIOOut.O2 => GPIOOutputType.PwmOut(dev[3]),
            AUTD3Sharp.GPIOOut.O3 => GPIOOutputType.Direct(true),
            _ => throw new ArgumentOutOfRangeException(nameof(gpio), gpio, null)
        }));
        foreach (var dev in autd)
        {
            Assert.Equal([0x51, 0x52, 0xE0, 0xF0], autd.Link<Audit>().GPIOOutputTypes(dev.Idx()));
            Assert.Equal([0x0002, 0x0000, 0x0003, 0x0001], autd.Link<Audit>().DebugValues(dev.Idx()));
        }

        var sysTime = DcSysTime.Now;
        autd.Send(new GPIOOutputs((dev, gpio) => gpio switch
                {

                    AUTD3Sharp.GPIOOut.O0 => GPIOOutputType.SysTimeEq(sysTime),
                    AUTD3Sharp.GPIOOut.O1 => GPIOOutputType.None,
                    AUTD3Sharp.GPIOOut.O2 => GPIOOutputType.None,
                    AUTD3Sharp.GPIOOut.O3 => GPIOOutputType.None,
                    _ => throw new ArgumentOutOfRangeException(nameof(gpio), gpio, null)
                }));
        foreach (var dev in autd)
        {
            Assert.Equal([0x60, 0x00, 0x00, 0x00], autd.Link<Audit>().GPIOOutputTypes(dev.Idx()));
            Assert.Equal([(sysTime.SysTime / 3125) >> 3, 0x0000, 0x0000, 0x0000], autd.Link<Audit>().DebugValues(dev.Idx()));
        }
    }
}
