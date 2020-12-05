using System;

namespace AdventOfCode.Maths
{
    public readonly struct Vector : IEquatable<Vector>
    {
        public static readonly Vector O;

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public double Angle => Math.Atan2(Y, X);

        public override string ToString() => $"({X}, {Y})";

        public override bool Equals(object obj) => obj is Vector other && Equals(other);
        public bool Equals(Vector other) => X == other.X && Y == other.Y;
        public override int GetHashCode() => X ^ (Y << 16);
    }
}
