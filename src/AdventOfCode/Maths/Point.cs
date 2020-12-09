using System;

namespace Advent_of_Code.Maths
{
    public readonly struct Point : IEquatable<Point>
    {
        public static readonly Point O;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public Point Add(Vector vector) => new Point(X + vector.X, Y + vector.Y);

        public Vector Subrtract(Point other) => new Vector(X - other.X, Y - other.Y);

        public override string ToString() => $"({X}, {Y})";

        public int ManhattanDistance(Point other)
            => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

        public override bool Equals(object obj) => obj is Point other && Equals(other);
        public bool Equals(Point other) => X == other.X && Y == other.Y;
        public override int GetHashCode() => X ^ (Y << 16);

        public static bool operator ==(Point a, Point b) => a.Equals(b);
        public static bool operator !=(Point a, Point b) => !(a == b);

        public static Point operator +(Point point, Vector vector) => point.Add(vector);

        public static Vector operator -(Point point, Point other) => point.Subrtract(other);
    }
}
