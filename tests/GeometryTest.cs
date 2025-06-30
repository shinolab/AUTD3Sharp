namespace tests;

public class GeometryTest
{
    [Fact]
    public void TestAUTD3Props()
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
    public void TestGeometryNumDevices()
    {
        var autd = CreateController();
        Assert.Equal(2, autd.NumDevices());
    }


    [Fact]
    public void TestGeometryNumTransducers()
    {
        var autd = CreateController();
        Assert.Equal(2 * 249, autd.NumTransducers());
    }

    [Fact]
    public void TestGeometryCenter()
    {
        var autd = CreateController();
        Assert.Equal(new Point3(86.625267028808594f, 66.71319580078125f, 0), autd.Center());
    }

    [Fact]
    public void TestGeometryReconfigure()
    {
        var autd = CreateController();
        Assert.Equal(new Point3(0, 0, 0), autd[0][0].Position());
        Assert.Equal(new Quaternion(1, 0, 0, 0), autd[0].Rotation());
        Assert.Equal(new Point3(0, 0, 0), autd[1][0].Position());
        Assert.Equal(new Quaternion(1, 0, 0, 0), autd[1].Rotation());
        autd.Reconfigure(d =>
            d.Idx() switch
            {
                0 => new AUTD3(pos: new Point3(1, 2, 3), rot: new Quaternion(0.1825742f, 0.3651484f, 0.5477226f, 0.7302968f)),
                _ => new AUTD3(pos: new Point3(4, 5, 6), rot: new Quaternion(0.37904903f, 0.45485884f, 0.53066862f, 0.60647845f)),
            }
            );
        Assert.Equal(new Point3(1, 2, 3), autd[0][0].Position());
        Assert.Equal(new Quaternion(0.1825742f, 0.3651484f, 0.5477226f, 0.7302968f), autd[0].Rotation());
        Assert.Equal(new Point3(4, 5, 6), autd[1][0].Position());
        Assert.Equal(new Quaternion(0.37904903f, 0.45485884f, 0.53066862f, 0.60647845f), autd[1].Rotation());
    }

    [Fact]
    public void TestDeviceIdx()
    {
        var autd = CreateController();
        Assert.Equal(0, autd[0].Idx());
        Assert.Equal(1, autd[1].Idx());
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