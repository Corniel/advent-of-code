using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override string ToString() => $"({X}, {Y})";

        public int ManhattanDistance(Point other)
            => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

        public override bool Equals(object obj) => obj is Point other && Equals(other);
        public bool Equals(Point other) => X == other.X && Y == other.Y;
        public override int GetHashCode() => X ^ (Y << 16);
    }

}
