namespace tests.Driver.Common;

using static AUTD3Sharp.Units;

public class RotationTest
{
    [Fact]
    public void AngleTest()
    {

        var angle = 90 * deg;
        Assert.Equal(Math.PI / 2, angle.Radian);

        angle = Math.PI / 2 * rad;
        Assert.Equal(Math.PI / 2, angle.Radian);
    }

    [Fact]
    public async Task WithRotation()
    {
        {
            var autd = await Open(EulerAngles.FromZyz(90 * deg, 0 * deg, 0 * deg));
            AssertNearVec3(Vector3d.UnitY, autd.Geometry[0][0].XDirection);
            AssertNearVec3(-Vector3d.UnitX, autd.Geometry[0][0].YDirection);
            AssertNearVec3(Vector3d.UnitZ, autd.Geometry[0][0].ZDirection);
        }
        {
            var autd = await Open(EulerAngles.FromZyz(0 * deg, 90 * deg, 0 * deg));
            AssertNearVec3(-Vector3d.UnitZ, autd.Geometry[0][0].XDirection);
            AssertNearVec3(Vector3d.UnitY, autd.Geometry[0][0].YDirection);
            AssertNearVec3(Vector3d.UnitX, autd.Geometry[0][0].ZDirection);
        }
        {
            var autd = await Open(EulerAngles.FromZyz(0 * deg, 0 * deg, 90 * deg));
            AssertNearVec3(Vector3d.UnitY, autd.Geometry[0][0].XDirection);
            AssertNearVec3(-Vector3d.UnitX, autd.Geometry[0][0].YDirection);
            AssertNearVec3(Vector3d.UnitZ, autd.Geometry[0][0].ZDirection);
        }
        {
            var autd = await Open(EulerAngles.FromZyz(0 * deg, 90 * deg, 90 * deg));
            AssertNearVec3(Vector3d.UnitY, autd.Geometry[0][0].XDirection);
            AssertNearVec3(Vector3d.UnitZ, autd.Geometry[0][0].YDirection);
            AssertNearVec3(Vector3d.UnitX, autd.Geometry[0][0].ZDirection);
        }
        {
            var autd = await Open(EulerAngles.FromZyz(90 * deg, 90 * deg, 0 * deg));
            AssertNearVec3(-Vector3d.UnitZ, autd.Geometry[0][0].XDirection);
            AssertNearVec3(-Vector3d.UnitX, autd.Geometry[0][0].YDirection);
            AssertNearVec3(Vector3d.UnitY, autd.Geometry[0][0].ZDirection);
        }
        return;

        async Task<Controller<Audit>> Open(Quaterniond q) =>
            await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.Zero).WithRotation(q))
                .OpenAsync(Audit.Builder());

        void AssertNearVec3(Vector3d expected, Vector3d x)
        {
            Assert.True(Math.Abs(expected.X - x.X) < 1e-6);
            Assert.True(Math.Abs(expected.Y - x.Y) < 1e-6);
            Assert.True(Math.Abs(expected.Z - x.Z) < 1e-6);
        }
    }
}
