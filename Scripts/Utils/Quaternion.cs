using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Utils
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Quaternion : IEquatable<Quaternion>
    {
        #region ctor
        public Quaternion(float w, float x, float y, float z)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }
        #endregion

        #region property
        public float X { get; }
        public float Y { get; }
        public float Z { get; }
        public float W { get; }

        public static Quaternion Identity => new(1, 0, 0, 0);
        #endregion
        public Quaternion Normalized => this / L2Norm;
        public float L2Norm => MathF.Sqrt(L2NormSquared);
        public float L2NormSquared => W * W + X * X + Y * Y + Z * Z;
        #region arithmetic
        public static bool operator ==(Quaternion left, Quaternion right) => left.Equals(right);
        public static bool operator !=(Quaternion left, Quaternion right) => !left.Equals(right);
        public bool Equals(Quaternion other) => W.Equals(other.W) && X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        public override bool Equals(object? obj) => obj is Quaternion other && Equals(other);
        public static Quaternion Divide(Quaternion left, float right) => new(left.W / right, left.X / right, left.Y / right, left.Z / right);
        public static Quaternion operator /(Quaternion left, float right) => Divide(left, right);
        #endregion

        #region util
        [ExcludeFromCodeCoverage] public override int GetHashCode() => HashCode.Combine(W, X, Y, Z);

        public override string ToString() => $"({W}, {X}, {Y}, {Z})";
        #endregion

#if UNITY_2018_3_OR_NEWER
    public static implicit operator UnityEngine.Quaternion(Quaternion v) => new UnityEngine.Quaternion(v.X, v.Y, v.Z, v.W);
    public static implicit operator Quaternion(UnityEngine.Quaternion v) => new Quaternion(v.w, v.x, v.y, v.z);
#endif
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
