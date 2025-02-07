namespace tests.Driver.Datagram.STM;

public class ControlPointsTest
{
    [Fact]
    public void ControlPoint()
    {
        var c1 = new ControlPoint();
        var c2 = new ControlPoint
        {
            Point = Point3.Origin,
            PhaseOffset = Phase.Zero,
        };
        var c3 = new ControlPoint
        {
            Point = Point3.Origin,
            PhaseOffset = new Phase(0xFF),
        };
        var c4 = new ControlPoint
        {
            Point = new Point3(0, 0, 1),
            PhaseOffset = Phase.Zero,
        };

        Assert.True(c1 == c2);
        Assert.True(c1 != c3);
        Assert.True(c1 != c4);
        Assert.True(!c1.Equals(null));
        Assert.True(c1.Equals((object?)c2));
        Assert.False(c1.Equals((object?)null));
    }

    [Fact]
    public void ControlPoints()
    {
        var c1 = new ControlPoints(points: [new ControlPoint()], intensity: EmitIntensity.Max);
        var c2 = new ControlPoints(points: [new ControlPoint()], intensity: EmitIntensity.Max);
        var c3 = new ControlPoints(points: [new ControlPoint()], intensity: EmitIntensity.Min);
        var c4 = new ControlPoints(points: [new ControlPoint(), new ControlPoint()], intensity: EmitIntensity.Max);

        Assert.True(c1 == c2);
        Assert.True(c1 != c3);
        Assert.True(c1 != c4);
        Assert.True(!c1.Equals(null));
        Assert.True(c1.Equals((object?)c2));
        Assert.False(c1.Equals((object?)null));
    }
}
