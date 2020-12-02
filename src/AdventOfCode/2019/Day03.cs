using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public class Day03
    {
        [Puzzle(2019, 03, Part.one)]
        public static int One(string input)
        {
            var wires = input.Lines().Select(line => Move.Parse(line).ToArray()).ToArray();

            var passed = new HashSet<Point>();

            var wire0 = Point.Start;

            foreach (var move in wires[0])
            {
                for(var step = 0; step < move.Length; step++)
                {
                    wire0 = wire0.Move(move.Direction);
                    passed.Add(wire0);
                }
            }

            var distance = int.MaxValue;

            var wire1 = Point.Start;

            foreach (var move in wires[1])
            {
                for (var step = 0; step < move.Length; step++)
                {
                    wire1 = wire1.Move(move.Direction);
                    if (passed.Contains(wire1))
                    {
                        distance = Math.Min(Point.Start.ManhattanDistance(wire1), distance);
                    }
                }
            }
            return distance;
        }

        [Puzzle(2019, 03, Part.two)]
        public static int Two(string input)
        {
            var wires = input.Lines().Select(line => Move.Parse(line).ToArray()).ToArray();

            var steps = new Dictionary<Point, int>();

            var wire0 = Point.Start;
            var steps0 = 1;
            foreach (var move in wires[0])
            {
                for (var step = 0; step < move.Length; step++)
                {
                    wire0 = wire0.Move(move.Direction);
                    steps[wire0] = steps0++;
                }
            }

            var distance = int.MaxValue;

            var wire1 = Point.Start;
            var steps1 = 1;

            foreach (var move in wires[1])
            {
                for (var step = 0; step < move.Length; step++)
                {
                    wire1 = wire1.Move(move.Direction);

                    if(steps.TryGetValue(wire1, out int other))
                    {
                        distance = Math.Min(other + steps1, distance);
                    }
                    steps1++;
                }
            }
            return distance;
        }

        public readonly struct Point : IEquatable<Point>
        {
            public static readonly Point Start;

            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public int X { get; }
            public int Y { get; }

            public override string ToString() => $"({X}, {Y})";

            public Point Move(Direction direction)
                => direction switch
                {
                    Direction.U => new Point(X, Y + 1),
                    Direction.R => new Point(X + 1, Y),
                    Direction.D => new Point(X, Y - 1),
                    Direction.L => new Point(X - 1, Y),
                    _ => throw new InvalidOperationException(),
                };

            public int ManhattanDistance(Point other)
                => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

            public override bool Equals(object obj) => obj is Point other && Equals(other);
            public bool Equals(Point other) => X == other.X && Y == other.Y;
            public override int GetHashCode() => X ^ (Y << 16);
        }

        public readonly struct Move
        {
            public Move(Direction direction, int length)
            {
                this.Direction = direction;
                this.Length = length;
            }

            public Direction Direction { get; }
            public int Length { get; }
            public override string ToString() => $"{Direction}{Length}";

            public static IEnumerable<Move> Parse(string str)
                => str.CommaSeperated()
                .Select(sub => new Move(
                    direction: Enum.Parse<Direction>(sub.Substring(0, 1)),
                    length: int.Parse(sub.Substring(1))));
        }

        public enum Direction { U, R, D, L }
    }
}