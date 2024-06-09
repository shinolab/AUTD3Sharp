#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace AUTD3Sharp.Utils
{
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
        public static Vector3 UnitX => new Vector3(1, 0, 0);
        public static Vector3 UnitY => new Vector3(0, 1, 0);
        public static Vector3 UnitZ => new Vector3(0, 0, 1);
        public Vector3 Normalized => this / L2Norm;
        public float L2Norm => MathF.Sqrt(L2NormSquared);
        public float L2NormSquared => X * X + Y * Y + Z * Z;
        public static Vector3 Zero => new Vector3(0, 0, 0);

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
        public static Vector3 Negate(Vector3 operand) => new Vector3(-operand.X, -operand.Y, -operand.Z);

        public static Vector3 Add(Vector3 left, Vector3 right)
        {
            var v1 = left.X + right.X;
            var v2 = left.Y + right.Y;
            var v3 = left.Z + right.Z;
            return new Vector3(v1, v2, v3);
        }
        public static Vector3 Subtract(Vector3 left, Vector3 right)
        {
            var v1 = left.X - right.X;
            var v2 = left.Y - right.Y;
            var v3 = left.Z - right.Z;
            return new Vector3(v1, v2, v3);
        }

        public static Vector3 Divide(Vector3 left, float right)
        {
            var v1 = left.X / right;
            var v2 = left.Y / right;
            var v3 = left.Z / right;
            return new Vector3(v1, v2, v3);
        }

        public static Vector3 Multiply(Vector3 left, float right)
        {
            var v1 = left.X * right;
            var v2 = left.Y * right;
            var v3 = left.Z * right;
            return new Vector3(v1, v2, v3);
        }

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
        public override bool Equals(object? obj)
        {
            if (obj is Vector3 vec) return Equals(vec);
            return false;
        }
        #endregion

        #region public methods
        public Vector3 Rectify() => new Vector3(Math.Max(X, 0), Math.Max(Y, 0), Math.Max(Z, 0));
        public float[] ToArray() => new[] { X, Y, Z };
        #endregion

        #region util
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        [ExcludeFromCodeCoverage] IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public string ToString(string format) => $"3D Column Vector:\n{string.Format(CultureInfo.CurrentCulture, format, X)}\n{string.Format(CultureInfo.CurrentCulture, format, Y)}\n{string.Format(CultureInfo.CurrentCulture, format, Z)}";

        public IEnumerator<float> GetEnumerator()
        {
            yield return X;
            yield return Y;
            yield return Z;
        }

        public override string ToString() => ToString("{0,-20}");
        #endregion

#if UNITY_2018_3_OR_NEWER
    public static implicit operator UnityEngine.Vector3(Vector3 v) => new UnityEngine.Vector3(v.X, v.Y, v.Z);
    public static implicit operator Vector3(UnityEngine.Vector3 v) => new Vector3(v.x, v.y, v.z);
#endif
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
