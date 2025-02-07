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
    }

    [Fact]
    public void ControlPoints1()
    {
        var c1 = new ControlPoints1();
        var c2 = new ControlPoints1
        {
            Points = [new ControlPoint()],
            Intensity = EmitIntensity.Max,
        };
        var c3 = new ControlPoints1
        {
            Points = [new ControlPoint()],
            Intensity = EmitIntensity.Min,
        };
        var c4 = new ControlPoints1(new Point3(0, 0, 1));

        Assert.True(c1 == c2);
        Assert.True(c1 != c3);
        Assert.True(c1 != c4);
        Assert.True(!c1.Equals(null));
        Assert.True(c1.Equals((object?)c2));
    }

    [Fact]
    public void ControlPoints2()
    {
        var c1 = new ControlPoints2();
        var c2 = new ControlPoints2
        {
            Points = [new ControlPoint(), new ControlPoint()],
            Intensity = EmitIntensity.Max,
        };
        var c3 = new ControlPoints2
        {
            Intensity = EmitIntensity.Min,
        };
        var c4 = new ControlPoints2((new Point3(0, 0, 1), Point3.Origin));

        Assert.True(c1 == c2);
        Assert.True(c1 != c3);
        Assert.True(c1 != c4);
        Assert.True(!c1.Equals(null));
        Assert.True(c1.Equals((object?)c2));
    }

    [Fact]
    public void ControlPoints3()
    {
        var c1 = new ControlPoints3();
        var c2 = new ControlPoints3
        {
            Points = [new ControlPoint(), new ControlPoint(), new ControlPoint()],
            Intensity = EmitIntensity.Max,
        };
        var c3 = new ControlPoints3
        {
            Intensity = EmitIntensity.Min,
        };
        var c4 = new ControlPoints3((new Point3(0, 0, 1), Point3.Origin, Point3.Origin));

        Assert.True(c1 == c2);
        Assert.True(c1 != c3);
        Assert.True(c1 != c4);
        Assert.True(!c1.Equals(null));
        Assert.True(c1.Equals((object?)c2));
    }

    [Fact]
    public void ControlPoints4()
    {
        var c1 = new ControlPoints4();
        var c2 = new ControlPoints4
        {
            Points = [new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint()],
            Intensity = EmitIntensity.Max,
        };
        var c3 = new ControlPoints4
        {
            Intensity = EmitIntensity.Min,
        };
        var c4 = new ControlPoints4((new Point3(0, 0, 1), Point3.Origin, Point3.Origin, Point3.Origin));

        Assert.True(c1 == c2);
        Assert.True(c1 != c3);
        Assert.True(c1 != c4);
        Assert.True(!c1.Equals(null));
        Assert.True(c1.Equals((object?)c2));
    }

    [Fact]
    public void ControlPoints5()
    {
        var c1 = new ControlPoints5();
        var c2 = new ControlPoints5
        {
            Points = [new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint()],
            Intensity = EmitIntensity.Max,
        };
        var c3 = new ControlPoints5
        {
            Intensity = EmitIntensity.Min,
        };
        var c4 = new ControlPoints5((new Point3(0, 0, 1), Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin));

        Assert.True(c1 == c2);
        Assert.True(c1 != c3);
        Assert.True(c1 != c4);
        Assert.True(!c1.Equals(null));
        Assert.True(c1.Equals((object?)c2));
    }

    [Fact]
    public void ControlPoints6()
    {
        var c1 = new ControlPoints6();
        var c2 = new ControlPoints6
        {
            Points = [new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint()],
            Intensity = EmitIntensity.Max,
        };
        var c3 = new ControlPoints6
        {
            Intensity = EmitIntensity.Min,
        };
        var c4 = new ControlPoints6((new Point3(0, 0, 1), Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin));

        Assert.True(c1 == c2);
        Assert.True(c1 != c3);
        Assert.True(c1 != c4);
        Assert.True(!c1.Equals(null));
        Assert.True(c1.Equals((object?)c2));
    }

    [Fact]
    public void ControlPoints7()
    {
        var c1 = new ControlPoints7();
        var c2 = new ControlPoints7
        {
            Points = [new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint()],
            Intensity = EmitIntensity.Max,
        };
        var c3 = new ControlPoints7
        {
            Intensity = EmitIntensity.Min,
        };
        var c4 = new ControlPoints7((new Point3(0, 0, 1), Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin));

        Assert.True(c1 == c2);
        Assert.True(c1 != c3);
        Assert.True(c1 != c4);
        Assert.True(!c1.Equals(null));
        Assert.True(c1.Equals((object?)c2));
    }

    [Fact]
    public void ControlPoints8()
    {
        var c1 = new ControlPoints8();
        var c2 = new ControlPoints8
        {
            Points = [new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint()],
            Intensity = EmitIntensity.Max,
        };
        var c3 = new ControlPoints8
        {
            Intensity = EmitIntensity.Min,
        };
        var c4 = new ControlPoints8((new Point3(0, 0, 1), Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin, Point3.Origin));

        Assert.True(c1 == c2);
        Assert.True(c1 != c3);
        Assert.True(c1 != c4);
        Assert.True(!c1.Equals(null));
        Assert.True(c1.Equals((object?)c2));
    }
}
