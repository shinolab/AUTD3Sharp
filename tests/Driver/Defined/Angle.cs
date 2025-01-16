namespace tests.Driver.Common;

using static AUTD3Sharp.Units;

public class RotationTest
{
    [Fact]
    public void AngleTest()
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
            AssertNearVec3(Vector3.UnitX, autd[0].XDirection);
            AssertNearVec3(Vector3.UnitZ, autd[0].YDirection);
            AssertNearVec3(-Vector3.UnitY, autd[0].AxialDirection);
        }
        {
            var autd = Open(EulerAngles.Xyz(0 * deg, 90 * deg, 0 * deg));
            AssertNearVec3(-Vector3.UnitZ, autd[0].XDirection);
            AssertNearVec3(Vector3.UnitY, autd[0].YDirection);
            AssertNearVec3(Vector3.UnitX, autd[0].AxialDirection);
        }
        {
            var autd = Open(EulerAngles.Xyz(0 * deg, 0 * deg, 90 * deg));
            AssertNearVec3(Vector3.UnitY, autd[0].XDirection);
            AssertNearVec3(-Vector3.UnitX, autd[0].YDirection);
            AssertNearVec3(Vector3.UnitZ, autd[0].AxialDirection);
        }
        {
            var autd = Open(EulerAngles.Xyz(0 * deg, 90 * deg, 90 * deg));
            AssertNearVec3(Vector3.UnitY, autd[0].XDirection);
            AssertNearVec3(Vector3.UnitZ, autd[0].YDirection);
            AssertNearVec3(Vector3.UnitX, autd[0].AxialDirection);
        }
        {
            var autd = Open(EulerAngles.Xyz(90 * deg, 90 * deg, 0 * deg));
            AssertNearVec3(Vector3.UnitY, autd[0].XDirection);
            AssertNearVec3(Vector3.UnitZ, autd[0].YDirection);
            AssertNearVec3(Vector3.UnitX, autd[0].AxialDirection);
        }
        return;

        Controller<Audit> Open(Quaternion q) =>
            Controller.Builder([new AUTD3(Point3.Origin).WithRotation(q)])
                .Open(Audit.Builder());

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
            AssertNearVec3(Vector3.UnitY, autd[0].XDirection);
            AssertNearVec3(-Vector3.UnitX, autd[0].YDirection);
            AssertNearVec3(Vector3.UnitZ, autd[0].AxialDirection);
        }
        {
            var autd = Open(EulerAngles.Zyz(0 * deg, 90 * deg, 0 * deg));
            AssertNearVec3(-Vector3.UnitZ, autd[0].XDirection);
            AssertNearVec3(Vector3.UnitY, autd[0].YDirection);
            AssertNearVec3(Vector3.UnitX, autd[0].AxialDirection);
        }
        {
            var autd = Open(EulerAngles.Zyz(0 * deg, 0 * deg, 90 * deg));
            AssertNearVec3(Vector3.UnitY, autd[0].XDirection);
            AssertNearVec3(-Vector3.UnitX, autd[0].YDirection);
            AssertNearVec3(Vector3.UnitZ, autd[0].AxialDirection);
        }
        {
            var autd = Open(EulerAngles.Zyz(0 * deg, 90 * deg, 90 * deg));
            AssertNearVec3(Vector3.UnitY, autd[0].XDirection);
            AssertNearVec3(Vector3.UnitZ, autd[0].YDirection);
            AssertNearVec3(Vector3.UnitX, autd[0].AxialDirection);
        }
        {
            var autd = Open(EulerAngles.Zyz(90 * deg, 90 * deg, 0 * deg));
            AssertNearVec3(-Vector3.UnitZ, autd[0].XDirection);
            AssertNearVec3(-Vector3.UnitX, autd[0].YDirection);
            AssertNearVec3(Vector3.UnitY, autd[0].AxialDirection);
        }
        return;

        Controller<Audit> Open(Quaternion q) =>
            Controller.Builder([new AUTD3(Point3.Origin).WithRotation(q)])
                .Open(Audit.Builder());

        void AssertNearVec3(Vector3 expected, Vector3 x)
        {
            Assert.True(Math.Abs(expected.X - x.X) < 1e-6);
            Assert.True(Math.Abs(expected.Y - x.Y) < 1e-6);
            Assert.True(Math.Abs(expected.Z - x.Z) < 1e-6);
        }
    }
}
