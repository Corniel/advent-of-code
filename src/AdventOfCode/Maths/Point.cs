using System;

namespace AdventOfCode.Maths
{

    public readonly struct Point : IEquatable<Point>
    {
        public static readonly Point O;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public Point Add(Vector vector) => new Point(X + vector.X, Y + vector.Y);

        public override string ToString() => $"({X}, {Y})";

        public int ManhattanDistance(Point other)
            => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

        public override bool Equals(object obj) => obj is Point other && Equals(other);
        public bool Equals(Point other) => X == other.X && Y == other.Y;
        public override int GetHashCode() => X ^ (Y << 16);

        public static Point operator +(Point point, Vector vector) => point.Add(vector);
    }
}
