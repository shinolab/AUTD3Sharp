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
    public readonly struct Vector3d : IEquatable<Vector3d>, IEnumerable<double>
    {
        #region ctor
        public Vector3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3d(params double[] vector)
        {
            if (vector.Length != 3) throw new InvalidCastException();

            X = vector[0];
            Y = vector[1];
            Z = vector[2];
        }
        #endregion

        #region property
        public static Vector3d UnitX => new Vector3d(1, 0, 0);
        public static Vector3d UnitY => new Vector3d(0, 1, 0);
        public static Vector3d UnitZ => new Vector3d(0, 0, 1);
        public Vector3d Normalized => this / L2Norm;
        public double L2Norm => Math.Sqrt(L2NormSquared);
        public double L2NormSquared => X * X + Y * Y + Z * Z;
        public static Vector3d Zero => new Vector3d(0, 0, 0);

        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        #endregion

        #region indexcer
        public double this[int index]
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
        public static Vector3d Negate(Vector3d operand) => new Vector3d(-operand.X, -operand.Y, -operand.Z);

        public static Vector3d Add(Vector3d left, Vector3d right)
        {
            var v1 = left.X + right.X;
            var v2 = left.Y + right.Y;
            var v3 = left.Z + right.Z;
            return new Vector3d(v1, v2, v3);
        }
        public static Vector3d Subtract(Vector3d left, Vector3d right)
        {
            var v1 = left.X - right.X;
            var v2 = left.Y - right.Y;
            var v3 = left.Z - right.Z;
            return new Vector3d(v1, v2, v3);
        }

        public static Vector3d Divide(Vector3d left, double right)
        {
            var v1 = left.X / right;
            var v2 = left.Y / right;
            var v3 = left.Z / right;
            return new Vector3d(v1, v2, v3);
        }

        public static Vector3d Multiply(Vector3d left, double right)
        {
            var v1 = left.X * right;
            var v2 = left.Y * right;
            var v3 = left.Z * right;
            return new Vector3d(v1, v2, v3);
        }

        public static Vector3d Multiply(double left, Vector3d right) => Multiply(right, left);
        public static Vector3d operator -(Vector3d operand) => Negate(operand);
        public static Vector3d operator +(Vector3d left, Vector3d right) => Add(left, right);
        public static Vector3d operator -(Vector3d left, Vector3d right) => Subtract(left, right);
        public static Vector3d operator *(Vector3d left, double right) => Multiply(left, right);
        public static Vector3d operator *(double left, Vector3d right) => Multiply(right, left);
        public static Vector3d operator /(Vector3d left, double right) => Divide(left, right);
        public static bool operator ==(Vector3d left, Vector3d right) => left.Equals(right);
        public static bool operator !=(Vector3d left, Vector3d right) => !left.Equals(right);
        public bool Equals(Vector3d other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        public override bool Equals(object? obj)
        {
            if (obj is Vector3d vec) return Equals(vec);
            return false;
        }
        #endregion

        #region public methods
        public Vector3d Rectify() => new Vector3d(Math.Max(X, 0), Math.Max(Y, 0), Math.Max(Z, 0));
        public double[] ToArray() => new[] { X, Y, Z };
        #endregion

        #region util
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        [ExcludeFromCodeCoverage] IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public string ToString(string format) => $"3D Column Vector:\n{string.Format(CultureInfo.CurrentCulture, format, X)}\n{string.Format(CultureInfo.CurrentCulture, format, Y)}\n{string.Format(CultureInfo.CurrentCulture, format, Z)}";

        public IEnumerator<double> GetEnumerator()
        {
            yield return X;
            yield return Y;
            yield return Z;
        }

        public override string ToString() => ToString("{0,-20}");
        #endregion

#if UNITY_2018_3_OR_NEWER
    public static implicit operator UnityEngine.Vector3(Vector3d v) => new UnityEngine.Vector3((float)v.X, (float)v.Y, (float)v.Z);
    public static implicit operator Vector3d(UnityEngine.Vector3 v) => new Vector3d(v.x, v.y, v.z);
#endif
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
