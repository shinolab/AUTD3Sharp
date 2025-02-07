namespace tests.Utils;

public class Point3Test
{
    [Fact]
    public void Constructor_WithThreeArguments_SetsPropertiesCorrectly()
    {
        var vector = new Point3(1, 2, 3);

        Assert.Equal(1, vector.X);
        Assert.Equal(2, vector.Y);
        Assert.Equal(3, vector.Z);
    }

    [Fact]
    public void Constructor_WithArrayArgument_SetsPropertiesCorrectly()
    {
        var vector = new Point3([1, 2, 3]);

        Assert.Equal(1, vector.X);
        Assert.Equal(2, vector.Y);
        Assert.Equal(3, vector.Z);

        Assert.Throws<InvalidCastException>(() => new Point3([]));
    }


    [Fact]
    public void NegatePoint3Correctly()
    {
        var vector = new Point3(1, 2, 3);
        {
            var result = Point3.Negate(vector);
            Assert.Equal(-1, result.X);
            Assert.Equal(-2, result.Y);
            Assert.Equal(-3, result.Z);
        }

        {
            var result = -vector;
            Assert.Equal(-1, result.X);
            Assert.Equal(-2, result.Y);
            Assert.Equal(-3, result.Z);
        }
    }


    [Fact]
    public void Add_AddsPointsCorrectly()
    {
        var vector1 = new Point3(1, 2, 3);
        var vector2 = new Vector3(4, 5, 6);

        var result = Point3.Add(vector1, vector2);

        Assert.Equal(5, result.X);
        Assert.Equal(7, result.Y);
        Assert.Equal(9, result.Z);
    }

    [Fact]
    public void Subtract_SubtractsPointsCorrectly()
    {
        var vector1 = new Point3(1, 2, 3);
        var vector2 = new Vector3(4, 5, 6);

        {
            var result = Point3.Subtract(vector1, vector2);

            Assert.Equal(-3, result.X);
            Assert.Equal(-3, result.Y);
            Assert.Equal(-3, result.Z);
        }

        {
            var result = vector1 - vector2;

            Assert.Equal(-3, result.X);
            Assert.Equal(-3, result.Y);
            Assert.Equal(-3, result.Z);
        }
    }

    [Fact]
    public void GetHashCode_ReturnsConsistentHashCodes()
    {
        var vector1 = new Point3(1, 2, 3);
        var vector2 = new Point3(1, 2, 3);

        Assert.Equal(vector1.GetHashCode(), vector2.GetHashCode());
    }

    [Fact]
    public void GetEnumerator_ReturnsCorrectValues()
    {
        var vector = new Point3(1, 2, 3);

        using var enumerator = vector.GetEnumerator();

        Assert.True(enumerator.MoveNext());
        Assert.Equal(1, enumerator.Current);

        Assert.True(enumerator.MoveNext());
        Assert.Equal(2, enumerator.Current);

        Assert.True(enumerator.MoveNext());
        Assert.Equal(3, enumerator.Current);

        Assert.False(enumerator.MoveNext());
    }

    [Fact]
    public void Indexer_ReturnsCorrectValues()
    {
        var vector = new Point3(1, 2, 3);

        Assert.Equal(1, vector[0]);
        Assert.Equal(2, vector[1]);
        Assert.Equal(3, vector[2]);
        Assert.Throws<ArgumentOutOfRangeException>(() => vector[3]);
    }

    [Fact]
    public void Multiply_MultipliesCorrectly()
    {
        var vector = new Point3(1, 2, 3);

        var result = Point3.Multiply(2, vector);
        Assert.Equal(2, result.X);
        Assert.Equal(4, result.Y);
        Assert.Equal(6, result.Z);
    }

    [Fact]
    public void OperatorMultiply_MultipliesCorrectly()
    {
        var vector = new Point3(1, 2, 3);

        {
            var result = vector * 2;
            Assert.Equal(2, result.X);
            Assert.Equal(4, result.Y);
            Assert.Equal(6, result.Z);
        }

        {
            var result = 2 * vector;
            Assert.Equal(2, result.X);
            Assert.Equal(4, result.Y);
            Assert.Equal(6, result.Z);
        }
    }

    [Fact]
    public void Divide_DividesCorrectly()
    {
        var vector = new Point3(2, 4, 6);

        var result = Point3.Divide(vector, 2);
        Assert.Equal(1, result.X);
        Assert.Equal(2, result.Y);
        Assert.Equal(3, result.Z);
    }

    [Fact]
    public void OperatorDivide_DividesCorrectly()
    {
        var vector = new Point3(2, 4, 6);

        var result = vector / 2;
        Assert.Equal(1, result.X);
        Assert.Equal(2, result.Y);
        Assert.Equal(3, result.Z);
    }

    [Fact]
    public void Equals_ReturnsTrueForEqualPoints()
    {
        var vector1 = new Point3(1, 2, 3);
        var vector2 = new Point3(1, 2, 3);
        var vector3 = new Point3(2, 3, 4);

        Assert.True(vector1 == vector2);
        Assert.True(vector1 != vector3);
        Assert.True(vector1.Equals(vector2));
        Assert.True(!vector1.Equals(vector3));
        Assert.True(vector1.Equals((object)vector2));
        Assert.True(!vector1.Equals((object)vector3));
        Assert.True(!vector1.Equals(null));
    }

    [Fact]
    public void ToArray_ReturnsCorrectArray()
    {
        var vector = new Point3(1, 2, 3);

        var result = vector.ToArray();

        Assert.Equal([1, 2, 3], result);
    }

    [Fact]
    public void ToString_ReturnsCorrectString()
    {
        var vector = new Point3(1, 2, 3);

        var result = vector.ToString();

        Assert.Equal("(1, 2, 3)", result);
    }
}