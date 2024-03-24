#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;

namespace AUTD3Sharp.Utils
{
    public readonly struct Quaterniond : IEquatable<Quaterniond>
    {
        #region ctor
        public Quaterniond(double w, double x, double y, double z)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }
        #endregion

        #region property
        public double W { get; }
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public static Quaterniond Identity => new Quaterniond(1, 0, 0, 0);
        #endregion

        public Quaterniond Normalized => this / L2Norm;
        public double L2Norm => Math.Sqrt(L2NormSquared);
        public double L2NormSquared => W * W + X * X + Y * Y + Z * Z;


        #region arithmetic
        public static bool operator ==(Quaterniond left, Quaterniond right) => left.Equals(right);
        public static bool operator !=(Quaterniond left, Quaterniond right) => !left.Equals(right);
        public bool Equals(Quaterniond other) => W.Equals(other.W) && X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        public override bool Equals(object? obj)
        {
            if (obj is Quaterniond qua) return Equals(qua);
            return false;
        }
        public static Quaterniond Divide(Quaterniond left, double right)
        {
            var v1 = left.W / right;
            var v2 = left.X / right;
            var v3 = left.Y / right;
            var v4 = left.Z / right;
            return new Quaterniond(v1, v2, v3, v4);
        }

        public static Quaterniond operator /(Quaterniond left, double right) => Divide(left, right);
        #endregion

        #region util
        public override int GetHashCode() => W.GetHashCode() ^ X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();

        public override string ToString() => $"({W}, {X}, {Y}, {Z})";
        #endregion

#if UNITY_2018_3_OR_NEWER
    public static implicit operator UnityEngine.Quaternion(Quaterniond v) => new UnityEngine.Quaternion((float)v.X, (float)v.Y, (float)v.Z, (float)v.W);
    public static implicit operator Quaterniond(UnityEngine.Quaternion v) => new Quaterniond(v.w, v.x, v.y, v.z);
#endif
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
