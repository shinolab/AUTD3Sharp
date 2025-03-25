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
    public readonly struct Vector3 : IEquatable<Vector3>, IEnumerable<float>
    {
        #region ctor
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(params float[] vector)
        {
            if (vector.Length != 3) throw new InvalidCastException();
            X = vector[0];
            Y = vector[1];
            Z = vector[2];
        }
        #endregion

        #region property
        public static Vector3 UnitX => new(1, 0, 0);
        public static Vector3 UnitY => new(0, 1, 0);
        public static Vector3 UnitZ => new(0, 0, 1);
        public Vector3 Normalized => this / L2Norm;
        public float L2Norm => MathF.Sqrt(L2NormSquared);
        public float L2NormSquared => X * X + Y * Y + Z * Z;
        public static Vector3 Zero => new(0, 0, 0);

        public float X { get; }
        public float Y { get; }
        public float Z { get; }
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
        public static Vector3 Negate(Vector3 operand) => new(-operand.X, -operand.Y, -operand.Z);

        public static Vector3 Add(Vector3 left, Vector3 right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        public static Vector3 Subtract(Vector3 left, Vector3 right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        public static Vector3 Divide(Vector3 left, float right) => new(left.X / right, left.Y / right, left.Z / right);
        public static Vector3 Multiply(Vector3 left, float right) => new(left.X * right, left.Y * right, left.Z * right);
        public static Vector3 Multiply(float left, Vector3 right) => Multiply(right, left);
        public static Vector3 operator -(Vector3 operand) => Negate(operand);
        public static Vector3 operator +(Vector3 left, Vector3 right) => Add(left, right);
        public static Vector3 operator -(Vector3 left, Vector3 right) => Subtract(left, right);
        public static Vector3 operator *(Vector3 left, float right) => Multiply(left, right);
        public static Vector3 operator *(float left, Vector3 right) => Multiply(right, left);
        public static Vector3 operator /(Vector3 left, float right) => Divide(left, right);
        public static bool operator ==(Vector3 left, Vector3 right) => left.Equals(right);
        public static bool operator !=(Vector3 left, Vector3 right) => !left.Equals(right);
        public bool Equals(Vector3 other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        public override bool Equals(object? obj) => obj is Vector3 other && Equals(other);
        #endregion

        #region public methods
        public Vector3 Rectify() => new(Math.Max(X, 0), Math.Max(Y, 0), Math.Max(Z, 0));
        public float[] ToArray() => new[] { X, Y, Z };
        #endregion

        #region util
        public override int GetHashCode() => HashCode.Combine(X, Y, Z);
        [ExcludeFromCodeCoverage] IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public string ToString(string format) => $"({string.Format(CultureInfo.CurrentCulture, format, X)}, {string.Format(CultureInfo.CurrentCulture, format, Y)}, {string.Format(CultureInfo.CurrentCulture, format, Z)})";

        public IEnumerator<float> GetEnumerator()
        {
            yield return X;
            yield return Y;
            yield return Z;
        }

        public override string ToString() => ToString("{0}");
        #endregion

#if UNITY_2018_3_OR_NEWER
    public static implicit operator UnityEngine.Vector3(Vector3 v) => new(v.X, v.Y, v.Z);
    public static implicit operator Vector3(UnityEngine.Vector3 v) => new(v.x, v.y, v.z);
#endif
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
