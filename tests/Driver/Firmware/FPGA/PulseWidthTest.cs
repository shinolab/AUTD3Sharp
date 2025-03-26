namespace tests.Driver.Firmware.FPGA;

public class PulseWidthTest
{
    [Fact]
    public void PulseWidthNew()
    {
        for (ushort i = 0; i < 512; i++)
            Assert.Equal(i, new PulseWidth(i).Value);

        Assert.Throws<AUTDException>(() => new PulseWidth(512));
    }

    [Fact]
    public void PulseWidthFromDuty()
    {
        for (ushort i = 0; i < 512; i++)
            Assert.Equal(i, PulseWidth.FromDuty((float)i / 512.0f).Value);

        Assert.Throws<AUTDException>(() => PulseWidth.FromDuty(-1.0f));
        Assert.Throws<AUTDException>(() => PulseWidth.FromDuty(1.0f));
    }

    [Fact]
    public void Equals_PulseWidth()
    {
        var m1 = new PulseWidth(1);
        var m2 = new PulseWidth(1);
        var m3 = new PulseWidth(2);

        Assert.True(m1 == m2);
        Assert.True(m1 != m3);
        Assert.True(!m1.Equals(null));
        Assert.True(m1.Equals((object?)m2));
        Assert.True(!m1.Equals((object?)null));
    }
}