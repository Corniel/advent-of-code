using AdventOfCode.Maths;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public class Day03
    {
        [Puzzle(2019, 03, Part.one)]
        public static int One(string input)
        {
            var wires = input.Lines().Select(line => Move.Parse(line).ToArray()).ToArray();

            var passed = new HashSet<Point>();

            var wire0 = Point.O;

            foreach (var move in wires[0])
            {
                for(var step = 0; step < move.Length; step++)
                {
                    wire0 = move.Step(wire0);
                    passed.Add(wire0);
                }
            }

            var distance = int.MaxValue;

            var wire1 = Point.O;

            foreach (var move in wires[1])
            {
                for (var step = 0; step < move.Length; step++)
                {
                    wire1 = move.Step(wire1);
                    if (passed.Contains(wire1))
                    {
                        distance = Math.Min(Point.O.ManhattanDistance(wire1), distance);
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

            var wire0 = Point.O;
            var steps0 = 1;
            foreach (var move in wires[0])
            {
                for (var step = 0; step < move.Length; step++)
                {
                    wire0 = move.Step(wire0);
                    steps[wire0] = steps0++;
                }
            }

            var distance = int.MaxValue;

            var wire1 = Point.O;
            var steps1 = 1;

            foreach (var move in wires[1])
            {
                for (var step = 0; step < move.Length; step++)
                {
                    wire1 = move.Step(wire1);

                    if (steps.TryGetValue(wire1, out int other))
                    {
                        distance = Math.Min(other + steps1, distance);
                    }
                    steps1++;
                }
            }
            return distance;
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

            public Point Step(Point point)
                => Direction switch
                {
                    Direction.U => new Point(point.X, point.Y + 1),
                    Direction.R => new Point(point.X + 1, point.Y),
                    Direction.D => new Point(point.X, point.Y - 1),
                    Direction.L => new Point(point.X - 1, point.Y),
                    _ => throw new InvalidOperationException(),
                };
        }

        public enum Direction { U, R, D, L }
    }
}