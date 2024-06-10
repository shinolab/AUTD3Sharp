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
    public async Task WithRotation()
    {
        {
            var autd = await Open(EulerAngles.ZYZ(90 * deg, 0 * deg, 0 * deg));
            AssertNearVec3(Vector3.UnitY, autd.Geometry[0].XDirection);
            AssertNearVec3(-Vector3.UnitX, autd.Geometry[0].YDirection);
            AssertNearVec3(Vector3.UnitZ, autd.Geometry[0].AxialDirection);
        }
        {
            var autd = await Open(EulerAngles.ZYZ(0 * deg, 90 * deg, 0 * deg));
            AssertNearVec3(-Vector3.UnitZ, autd.Geometry[0].XDirection);
            AssertNearVec3(Vector3.UnitY, autd.Geometry[0].YDirection);
            AssertNearVec3(Vector3.UnitX, autd.Geometry[0].AxialDirection);
        }
        {
            var autd = await Open(EulerAngles.ZYZ(0 * deg, 0 * deg, 90 * deg));
            AssertNearVec3(Vector3.UnitY, autd.Geometry[0].XDirection);
            AssertNearVec3(-Vector3.UnitX, autd.Geometry[0].YDirection);
            AssertNearVec3(Vector3.UnitZ, autd.Geometry[0].AxialDirection);
        }
        {
            var autd = await Open(EulerAngles.ZYZ(0 * deg, 90 * deg, 90 * deg));
            AssertNearVec3(Vector3.UnitY, autd.Geometry[0].XDirection);
            AssertNearVec3(Vector3.UnitZ, autd.Geometry[0].YDirection);
            AssertNearVec3(Vector3.UnitX, autd.Geometry[0].AxialDirection);
        }
        {
            var autd = await Open(EulerAngles.ZYZ(90 * deg, 90 * deg, 0 * deg));
            AssertNearVec3(-Vector3.UnitZ, autd.Geometry[0].XDirection);
            AssertNearVec3(-Vector3.UnitX, autd.Geometry[0].YDirection);
            AssertNearVec3(Vector3.UnitY, autd.Geometry[0].AxialDirection);
        }
        return;

        async Task<Controller<Audit>> Open(Quaternion q) =>
            await Controller.Builder([new AUTD3(Vector3.Zero).WithRotation(q)])
                .OpenAsync(Audit.Builder());

        void AssertNearVec3(Vector3 expected, Vector3 x)
        {
            Assert.True(Math.Abs(expected.X - x.X) < 1e-6);
            Assert.True(Math.Abs(expected.Y - x.Y) < 1e-6);
            Assert.True(Math.Abs(expected.Z - x.Z) < 1e-6);
        }
    }
}
