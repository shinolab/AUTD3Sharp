namespace tests;

public class GeometryTest
{
    [Fact]
    public void AUTD3Props()
    {
        Assert.Equal(10.16f, AUTD3.TransSpacing);
        Assert.Equal(10.16f, AUTD3.TransSpacingMm);

        Assert.Equal(192.0f, AUTD3.DeviceWidth);
        Assert.Equal(151.4f, AUTD3.DeviceHeight);

        Assert.Equal(18, AUTD3.NumTransX);
        Assert.Equal(14, AUTD3.NumTransY);
        Assert.Equal(249, AUTD3.NumTransInUnit);
    }

    [Fact]
    public void GeometryNumDevices()
    {
        var autd = CreateController();
        Assert.Equal(2, autd.NumDevices());
    }


    [Fact]
    public void GeometryNumTransducers()
    {
        var autd = CreateController();
        Assert.Equal(2 * 249, autd.NumTransducers());
    }

    [Fact]
    public void TestGeometrySoundSpeed()
    {
        var autd = CreateController();
        autd.SetSoundSpeed(350e3f);
        foreach (var dev in autd) Assert.Equal(350e3f, dev.SoundSpeed);
    }

    [Fact]
    public void TestGeometrySetSoundSpeedFromTemp()
    {
        var autd = CreateController();
        autd.SetSoundSpeedFromTemp(15);
        foreach (var dev in autd) Assert.Equal(340.29525e3f, dev.SoundSpeed);
    }

    [Fact]
    public void GeometryCenter()
    {
        var autd = CreateController();
        Assert.Equal(new Vector3(86.625267028808594f, 66.71319580078125f, 0), autd.Center());
    }

    [Fact]
    public void TestDeviceIdx()
    {
        var autd = CreateController();
        Assert.Equal(0, autd[0].Idx());
        Assert.Equal(1, autd[1].Idx());
    }

    [Fact]
    public void TestDeviceSoundSpeed()
    {
        var autd = CreateController();
        foreach (var dev in autd)
        {
            Assert.Equal(340e3f, dev.SoundSpeed);
            dev.SoundSpeed = 350e3f;
            Assert.Equal(350e3f, dev.SoundSpeed);
        }
    }

    [Fact]
    public void TestDeviceSetSoundSpeedFromTemp()
    {
        var autd = CreateController();
        foreach (var dev in autd)
        {
            dev.SetSoundSpeedFromTemp(15);
            Assert.Equal(340.29525e3f, dev.SoundSpeed);
        }
    }

    [Fact]
    public void TestDeviceEnable()
    {
        var autd = CreateController();
        foreach (var dev in autd)
        {
            Assert.True(dev.Enable);
            dev.Enable = false;
            Assert.False(dev.Enable);
        }
    }

    [Fact]
    public void TestDeviceNumTransducers()
    {
        var autd = CreateController();
        foreach (var dev in autd)
        {
            Assert.Equal(249, dev.NumTransducers());
        }
    }

    [Fact]
    public void TestDeviceCenter()
    {
        var autd = CreateController();
        foreach (var dev in autd)
        {
            var center = dev.Center();
            Assert.Equal(86.625267f, center.X);
            Assert.Equal(66.7131958f, center.Y);
            Assert.Equal(0.0f, center.Z);
        }
    }

    [Fact]
    public void TestDeviceTranslate()
    {
        var autd = CreateController();
        foreach (var dev in autd)
        {
            var originalPos = dev.Select(tr => tr.Position()).ToArray();
            var t = new Vector3(1, 2, 3);
            dev.Translate(t);
            foreach (var tr in dev)
                Assert.Equal(tr.Position(), originalPos[tr.Idx()] + t);
        }
    }

    [Fact]
    public void TestDeviceRotate()
    {
        var autd = CreateController();
        foreach (var dev in autd)
        {
            var r = new Quaternion(0.707106829f, 0, 0, 0.707106829f);
            dev.Rotate(r);
            Assert.Equal(r, dev.Rotation());
        }
    }

    [Fact]
    public void TestDeviceAffine()
    {
        var autd = CreateController();
        foreach (var dev in autd)
        {
            var originalPos = dev.Select(tr => tr.Position()).ToArray();
            var t = new Vector3(1, 2, 3);
            var r = new Quaternion(0.707106829f, 0, 0, 0.707106829f);
            dev.Affine(t, r);
            foreach (var tr in dev)
            {
                var op = originalPos[tr.Idx()];
                var expected = new Vector3(-op.Y, op.X, op.Z) + t;
                Assert.True(Math.Abs(expected.X - tr.Position().X) < 1e-3);
                Assert.True(Math.Abs(expected.Y - tr.Position().Y) < 1e-3);
                Assert.True(Math.Abs(expected.Z - tr.Position().Z) < 1e-3);
            }
            Assert.Equal(r, dev.Rotation());
        }
    }

    [Fact]
    public void TestDeviceWavelength()
    {
        var autd = CreateController();
        foreach (var dev in autd)
        {
            Assert.Equal(340e3f / 40e3f, dev.Wavelength());
        }
    }

    [Fact]
    public void TestDeviceWavenumber()
    {
        var autd = CreateController();
        foreach (var dev in autd)
        {
            Assert.Equal(2.0f * MathF.PI * 40e3f / 340e3f, dev.Wavenumber());
        }
    }

    [Fact]
    public void TestDeviceRotation()
    {
        var autd = CreateController();
        foreach (var dev in autd)
            Assert.Equal(dev.Rotation(), new Quaternion(1.0f, 0.0f, 0.0f, 0.0f));
    }

    [Fact]
    public void TestDeviceXDirection()
    {
        var autd = CreateController();
        foreach (var dev in autd)
            Assert.Equal(dev.XDirection(), new Vector3(1.0f, 0.0f, 0.0f));
    }

    [Fact]
    public void TestDeviceYDirection()
    {
        var autd = CreateController();
        foreach (var dev in autd)
            Assert.Equal(dev.YDirection(), new Vector3(0.0f, 1.0f, 0.0f));
    }

    [Fact]
    public void TestDeviceAxialDirection()
    {
        var autd = CreateController();
        foreach (var dev in autd)
            Assert.Equal(dev.AxialDirection(), new Vector3(0.0f, 0.0f, 1.0f));
    }

    [Fact]
    public void TestTransducerIdx()
    {
        var autd = CreateController();
        foreach (var dev in autd)
        {
            foreach (var (tr, i) in dev.Select((tr, i) => (tr, i)))
            {
                Assert.Equal(i, tr.Idx());
            }
        }
    }

    [Fact]
    public void TestTransducerDevIdx()
    {
        var autd = CreateController();
        foreach (var dev in autd)
        {
            foreach (var tr in dev)
            {
                Assert.Equal(dev.Idx(), tr.DevIdx());
            }
        }
    }

    [Fact]
    public void TestTransducerPosition()
    {
        var autd = CreateController();
        Assert.Equal(autd[0][0].Position(), new Vector3(0.0f, 0.0f, 0.0f));
        Assert.Equal(autd[0][AUTD3.NumTransInUnit - 1].Position(),
            new Vector3((AUTD3.NumTransX - 1) * AUTD3.TransSpacing, (AUTD3.NumTransY - 1) * AUTD3.TransSpacing, 0.0f));

        Assert.Equal(autd[1][0].Position(), new Vector3(0.0f, 0.0f, 0.0f));
        Assert.Equal(autd[1][AUTD3.NumTransInUnit - 1].Position(),
            new Vector3((AUTD3.NumTransX - 1) * AUTD3.TransSpacing, (AUTD3.NumTransY - 1) * AUTD3.TransSpacing, 0.0f));
    }
}