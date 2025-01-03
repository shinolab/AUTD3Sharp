#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace AUTD3Sharp.Utils
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Point3 : IEquatable<Point3>, IEnumerable<float>
    {
        #region ctor
        public Point3(float x, float y, float z)
        {
            Coords = new(x, y, z);
        }

        public Point3(params float[] vector)
        {
            Coords = new(vector);
        }
        #endregion

        #region property
        public static Point3 Origin => new(0, 0, 0);

        public readonly Vector3 Coords;

        public float X => Coords.X;
        public float Y => Coords.Y;
        public float Z => Coords.Z;
        #endregion

        #region indexcer
        public float this[int index]
        {
            get
            {
                return index switch
                {
                    0 => X,
                    1 => Y,
                    2 => Z,
                    _ => throw new ArgumentOutOfRangeException(nameof(index))
                };
            }
        }
        #endregion

        #region arithmetic
        public static Point3 Negate(Point3 operand) => new(-operand.X, -operand.Y, -operand.Z);

        public static Point3 Add(Point3 left, Vector3 right)
        {
            var v1 = left.X + right.X;
            var v2 = left.Y + right.Y;
            var v3 = left.Z + right.Z;
            return new(v1, v2, v3);
        }
        public static Point3 Subtract(Point3 left, Vector3 right)
        {
            var v1 = left.X - right.X;
            var v2 = left.Y - right.Y;
            var v3 = left.Z - right.Z;
            return new(v1, v2, v3);
        }

        public static Point3 Multiply(Point3 left, float right)
        {
            var v1 = left.X * right;
            var v2 = left.Y * right;
            var v3 = left.Z * right;
            return new(v1, v2, v3);
        }

        public static Point3 Multiply(float left, Point3 right) => Multiply(right, left);
        public static Point3 operator -(Point3 operand) => Negate(operand);
        public static Point3 operator +(Point3 left, Vector3 right) => Add(left, right);
        public static Point3 operator -(Point3 left, Vector3 right) => Subtract(left, right);
        public static Point3 operator *(Point3 left, float right) => Multiply(left, right);
        public static Point3 operator *(float left, Point3 right) => Multiply(right, left);
        public static bool operator ==(Point3 left, Point3 right) => left.Equals(right);
        public static bool operator !=(Point3 left, Point3 right) => !left.Equals(right);
        public bool Equals(Point3 other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        public override bool Equals(object? obj)
        {
            if (obj is Point3 vec) return Equals(vec);
            return false;
        }
        #endregion

        #region public methods
        public float[] ToArray() => new[] { X, Y, Z };
        #endregion

        #region util
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        [ExcludeFromCodeCoverage] IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public string ToString(string format) => $"({string.Format(CultureInfo.CurrentCulture, format, X)}, {string.Format(CultureInfo.CurrentCulture, format, Y)}, {string.Format(CultureInfo.CurrentCulture, format, Z)})";

        public IEnumerator<float> GetEnumerator()
        {
            yield return X;
            yield return Y;
            yield return Z;
        }

        public override string ToString() => ToString("{0,-20}");
        #endregion

#if UNITY_2018_3_OR_NEWER
    public static implicit operator UnityEngine.Vector3(Point3 v) => new(v.X, v.Y, v.Z);
    public static implicit operator Point3(UnityEngine.Vector3 v) => new(v.x, v.y, v.z);
#endif
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
