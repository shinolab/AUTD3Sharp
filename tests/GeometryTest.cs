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

        Assert.Equal(18, AUTD3.NumTransInX);
        Assert.Equal(14, AUTD3.NumTransInY);
        Assert.Equal(249, AUTD3.NumTransInUnit);
    }

    [Fact]
    public async Task GeometryNumDevices()
    {
        var autd = await AUTDTest.CreateController();
        Assert.Equal(2, autd.Geometry.NumDevices);
    }


    [Fact]
    public async Task GeometryNumTransducers()
    {
        var autd = await AUTDTest.CreateController();
        Assert.Equal(2 * 249, autd.Geometry.NumTransducers);
    }

    [Fact]
    public async Task TestGeometrySoundSpeed()
    {
        var autd = await AUTDTest.CreateController();
        autd.Geometry.SetSoundSpeed(350e3f);
        foreach (var dev in autd.Geometry) Assert.Equal(350e3f, dev.SoundSpeed);
    }

    [Fact]
    public async Task TestGeometrySetSoundSpeedFromTemp()
    {
        var autd = await AUTDTest.CreateController();
        autd.Geometry.SetSoundSpeedFromTemp(15);
        foreach (var dev in autd.Geometry) Assert.Equal(340.29525e3f, dev.SoundSpeed);
    }

    [Fact]
    public async Task GeometryCenter()
    {
        var autd = await AUTDTest.CreateController();
        Assert.Equal(new Vector3(86.625267028808594f, 66.71319580078125f, 0), autd.Geometry.Center);
    }

    [Fact]
    public async Task TestDeviceIdx()
    {
        var autd = await AUTDTest.CreateController();
        Assert.Equal(0, autd.Geometry[0].Idx);
        Assert.Equal(1, autd.Geometry[1].Idx);
    }

    [Fact]
    public async Task TestDeviceSoundSpeed()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(340e3f, dev.SoundSpeed);
            dev.SoundSpeed = 350e3f;
            Assert.Equal(350e3f, dev.SoundSpeed);
        }
    }

    [Fact]
    public async Task TestDeviceSetSoundSpeedFromTemp()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
        {
            dev.SetSoundSpeedFromTemp(15);
            Assert.Equal(340.29525e3f, dev.SoundSpeed);
        }
    }

    [Fact]
    public async Task TestDeviceEnable()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
        {
            Assert.True(dev.Enable);
            dev.Enable = false;
            Assert.False(dev.Enable);
        }
    }

    [Fact]
    public async Task TestDeviceNumTransducers()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(249, dev.NumTransducers);
        }
    }

    [Fact]
    public async Task TestDeviceCenter()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
        {
            var center = dev.Center;
            Assert.Equal(86.625267f, center.X);
            Assert.Equal(66.7131958f, center.Y);
            Assert.Equal(0.0f, center.Z);
        }
    }

    [Fact]
    public async Task TestDeviceTranslate()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
        {
            var originalPos = dev.Select(tr => tr.Position).ToArray();
            var t = new Vector3(1, 2, 3);
            dev.Translate(t);
            foreach (var tr in dev)
            {
                Assert.Equal(tr.Position, originalPos[tr.Idx] + t);
            }
        }
    }

    [Fact]
    public async Task TestDeviceRotate()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
        {
            var r = new Quaternion(0.707106829f, 0, 0, 0.707106829f);
            dev.Rotate(r);
            foreach (var tr in dev)
                Assert.Equal(r, dev.Rotation);
        }
    }

    [Fact]
    public async Task TestDeviceAffine()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
        {
            var originalPos = dev.Select(tr => tr.Position).ToArray();
            var t = new Vector3(1, 2, 3);
            var r = new Quaternion(0.707106829f, 0, 0, 0.707106829f);
            dev.Affine(t, r);
            foreach (var tr in dev)
            {
                var op = originalPos[tr.Idx];
                var expected = new Vector3(-op.Y, op.X, op.Z) + t;
                Assert.True(Math.Abs(expected.X - tr.Position.X) < 1e-3);
                Assert.True(Math.Abs(expected.Y - tr.Position.Y) < 1e-3);
                Assert.True(Math.Abs(expected.Z - tr.Position.Z) < 1e-3);
            }
            Assert.Equal(r, dev.Rotation);
        }
    }

    [Fact]
    public async Task TestDeviceWavelength()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(340e3f / 40e3f, dev.Wavelength);
        }
    }

    [Fact]
    public async Task TestDeviceWavenumber()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(2.0f * MathF.PI * 40e3f / 340e3f, dev.Wavenumber);
        }
    }



    [Fact]
    public async Task TestDeviceRotation()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
            Assert.Equal(dev.Rotation, new Quaternion(1.0f, 0.0f, 0.0f, 0.0f));
    }

    [Fact]
    public async Task TestDeviceXDirection()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
            Assert.Equal(dev.XDirection, new Vector3(1.0f, 0.0f, 0.0f));
    }

    [Fact]
    public async Task TestDeviceYDirection()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
            Assert.Equal(dev.YDirection, new Vector3(0.0f, 1.0f, 0.0f));
    }

    [Fact]
    public async Task TestDeviceAxialDirection()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
            Assert.Equal(dev.AxialDirection, new Vector3(0.0f, 0.0f, 1.0f));
    }

    [Fact]
    public async Task TestTransducerIdx()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
        {
            foreach (var (tr, i) in dev.Select((tr, i) => (tr, i)))
            {
                Assert.Equal(i, tr.Idx);
            }
        }
    }

    [Fact]
    public async Task TestTransducerDevIdx()
    {
        var autd = await AUTDTest.CreateController();
        foreach (var dev in autd.Geometry)
        {
            foreach (var tr in dev)
            {
                Assert.Equal(dev.Idx, tr.DevIdx);
            }
        }
    }

    [Fact]
    public async Task TestTransducerPosition()
    {
        var autd = await AUTDTest.CreateController();
        Assert.Equal(autd.Geometry[0][0].Position, new Vector3(0.0f, 0.0f, 0.0f));
        Assert.Equal(autd.Geometry[0][AUTD3.NumTransInUnit - 1].Position,
            new Vector3((AUTD3.NumTransInX - 1) * AUTD3.TransSpacing, (AUTD3.NumTransInY - 1) * AUTD3.TransSpacing, 0.0f));

        Assert.Equal(autd.Geometry[1][0].Position, new Vector3(0.0f, 0.0f, 0.0f));
        Assert.Equal(autd.Geometry[1][AUTD3.NumTransInUnit - 1].Position,
            new Vector3((AUTD3.NumTransInX - 1) * AUTD3.TransSpacing, (AUTD3.NumTransInY - 1) * AUTD3.TransSpacing, 0.0f));
    }
}