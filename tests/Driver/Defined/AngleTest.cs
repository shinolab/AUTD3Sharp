namespace tests.Driver.Defined;

public class AngleTest
{
    [Fact]
    public void Constructor()
    {

        var angle = 90 * deg;
        Assert.Equal(MathF.PI / 2, angle.Radian);

        angle = MathF.PI / 2 * rad;
        Assert.Equal(MathF.PI / 2, angle.Radian);
    }

    [Fact]
    public void WithRotationXyz()
    {
        {
            var autd = Open(EulerAngles.Xyz(90 * deg, 0 * deg, 0 * deg));
            AssertNearVec3(Vector3.UnitX, autd[0].XDirection());
            AssertNearVec3(Vector3.UnitZ, autd[0].YDirection());
            AssertNearVec3(-Vector3.UnitY, autd[0].AxialDirection());
        }
        {
            var autd = Open(EulerAngles.Xyz(0 * deg, 90 * deg, 0 * deg));
            AssertNearVec3(-Vector3.UnitZ, autd[0].XDirection());
            AssertNearVec3(Vector3.UnitY, autd[0].YDirection());
            AssertNearVec3(Vector3.UnitX, autd[0].AxialDirection());
        }
        {
            var autd = Open(EulerAngles.Xyz(0 * deg, 0 * deg, 90 * deg));
            AssertNearVec3(Vector3.UnitY, autd[0].XDirection());
            AssertNearVec3(-Vector3.UnitX, autd[0].YDirection());
            AssertNearVec3(Vector3.UnitZ, autd[0].AxialDirection());
        }
        {
            var autd = Open(EulerAngles.Xyz(0 * deg, 90 * deg, 90 * deg));
            AssertNearVec3(Vector3.UnitY, autd[0].XDirection());
            AssertNearVec3(Vector3.UnitZ, autd[0].YDirection());
            AssertNearVec3(Vector3.UnitX, autd[0].AxialDirection());
        }
        {
            var autd = Open(EulerAngles.Xyz(90 * deg, 90 * deg, 0 * deg));
            AssertNearVec3(Vector3.UnitY, autd[0].XDirection());
            AssertNearVec3(Vector3.UnitZ, autd[0].YDirection());
            AssertNearVec3(Vector3.UnitX, autd[0].AxialDirection());
        }
        return;

        AUTD3Sharp.Controller Open(Quaternion q) => AUTD3Sharp.Controller.Open([new AUTD3 { Pos = Point3.Origin, Rot = q }], new Audit());

        void AssertNearVec3(Vector3 expected, Vector3 x)
        {
            Assert.True(Math.Abs(expected.X - x.X) < 1e-6);
            Assert.True(Math.Abs(expected.Y - x.Y) < 1e-6);
            Assert.True(Math.Abs(expected.Z - x.Z) < 1e-6);
        }
    }

    [Fact]
    public void WithRotationZyz()
    {
        {
            var autd = Open(EulerAngles.Zyz(90 * deg, 0 * deg, 0 * deg));
            AssertNearVec3(Vector3.UnitY, autd[0].XDirection());
            AssertNearVec3(-Vector3.UnitX, autd[0].YDirection());
            AssertNearVec3(Vector3.UnitZ, autd[0].AxialDirection());
        }
        {
            var autd = Open(EulerAngles.Zyz(0 * deg, 90 * deg, 0 * deg));
            AssertNearVec3(-Vector3.UnitZ, autd[0].XDirection());
            AssertNearVec3(Vector3.UnitY, autd[0].YDirection());
            AssertNearVec3(Vector3.UnitX, autd[0].AxialDirection());
        }
        {
            var autd = Open(EulerAngles.Zyz(0 * deg, 0 * deg, 90 * deg));
            AssertNearVec3(Vector3.UnitY, autd[0].XDirection());
            AssertNearVec3(-Vector3.UnitX, autd[0].YDirection());
            AssertNearVec3(Vector3.UnitZ, autd[0].AxialDirection());
        }
        {
            var autd = Open(EulerAngles.Zyz(0 * deg, 90 * deg, 90 * deg));
            AssertNearVec3(Vector3.UnitY, autd[0].XDirection());
            AssertNearVec3(Vector3.UnitZ, autd[0].YDirection());
            AssertNearVec3(Vector3.UnitX, autd[0].AxialDirection());
        }
        {
            var autd = Open(EulerAngles.Zyz(90 * deg, 90 * deg, 0 * deg));
            AssertNearVec3(-Vector3.UnitZ, autd[0].XDirection());
            AssertNearVec3(-Vector3.UnitX, autd[0].YDirection());
            AssertNearVec3(Vector3.UnitY, autd[0].AxialDirection());
        }
        return;

        AUTD3Sharp.Controller Open(Quaternion q) => AUTD3Sharp.Controller.Open([new AUTD3 { Pos = Point3.Origin, Rot = q }], new Audit());

        void AssertNearVec3(Vector3 expected, Vector3 x)
        {
            Assert.True(Math.Abs(expected.X - x.X) < 1e-6);
            Assert.True(Math.Abs(expected.Y - x.Y) < 1e-6);
            Assert.True(Math.Abs(expected.Z - x.Z) < 1e-6);
        }
    }

    [Fact]
    public void Equals_Angle()
    {
        var m1 = 0 * deg;
        var m2 = 0 * deg;
        var m3 = 1 * deg;

        Assert.True(m1 == m2);
        Assert.True(m1 != m3);
        Assert.True(!m1.Equals(null));
        Assert.True(m1.Equals((object?)m2));
    }
}
