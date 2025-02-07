namespace tests.Driver;

public class AUTD3DeviceTest
{
    [Fact]
    public void AUTD3()
    {
        var autd = new AUTD3(pos: new Point3(1, 2, 3), rot: new Quaternion(4, 5, 6, 7));
        Assert.Equal(new Point3(1, 2, 3), autd.Pos);
        Assert.Equal(new Quaternion(4, 5, 6, 7), autd.Rot);
    }

    [Fact]
    public void AUTD3Default()
    {
        var autd = new AUTD3();
        Assert.Equal(new Point3(0, 0, 0), autd.Pos);
        Assert.Equal(new Quaternion(1, 0, 0, 0), autd.Rot);
    }
}
