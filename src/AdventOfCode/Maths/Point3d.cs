using System;

namespace AdventOfCode.Maths
{
    public readonly struct Point3d : IEquatable<Point3d>
    {
        public static readonly Point3d O;

        public Point3d(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Point3d Add(Vector3d vector)
            => new Point3d(X + vector.X, Y + vector.Y, Z + vector.Z);

        public override string ToString() => $"({X}, {Y}, {Z})";

        public override bool Equals(object obj) => obj is Point3d other && Equals(other);
        public bool Equals(Point3d other) 
            => X == other.X 
            && Y == other.Y
            && Z == other.Z;

        public override int GetHashCode() => X ^ (Y << 11) ^ (Z << 20);

        public static bool operator ==(Point3d a, Point3d b) => a.Equals(b);
        public static bool operator !=(Point3d a, Point3d b) => !(a == b);

        public static Point3d operator +(Point3d point, Vector3d vector) => point.Add(vector);
    }
}
