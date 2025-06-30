namespace tests;

public class EnvironmentTest
{
    [Fact]
    public void TestEnvironmentSoundSpeed()
    {
        var autd = CreateController();
        Assert.Equal(340e3f, autd.Environment.SoundSpeed);
        autd.Environment.SoundSpeed = 350e3f;
        Assert.Equal(350e3f, autd.Environment.SoundSpeed);
    }

    [Fact]
    public void TestEnvironmentSetSoundSpeedFromTemp()
    {
        var autd = CreateController();
        autd.Environment.SetSoundSpeedFromTemp(15);
        Assert.Equal(340.29525e3f, autd.Environment.SoundSpeed);
    }

    [Fact]
    public void TestEnvironmentWavelength()
    {
        var autd = CreateController();
        Assert.Equal(340e3f / 40e3f, autd.Environment.Wavelength());
    }

    [Fact]
    public void TestEnvironmentWavenumber()
    {
        var autd = CreateController();
        Assert.Equal(2.0f * MathF.PI * 40e3f / 340e3f, autd.Environment.Wavenumber());
    }
}