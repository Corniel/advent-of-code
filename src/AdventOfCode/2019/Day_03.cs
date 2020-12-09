using Advent_of_Code;
using Advent_of_Code.Maths;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2019
{
    public class Day_03
    {
        [Example(answer: 159, @"
            R75,D30,R83,U83,L12,D49,R71,U7,L72
            U62,R66,U55,R34,D71,R55,D58,R83")]
        [Example(answer: 135, @"
            R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51
            U98,R91,D20,R16,D67,R40,U7,R15,U6,R7")]
        [Puzzle(answer: 1195, year: 2019, day: 03)]
        public void part_one(long answer, string input)
        {
            var wires = input.Lines().Select(line => Move.Parse(line).ToArray()).ToArray();
            var passed = new HashSet<Point>();
            var wire0 = Point.O;

            foreach (var move in wires[0])
            {
                for (var step = 0; step < move.Length; step++)
                {
                    wire0 = move.Step(wire0);
                    passed.Add(wire0);
                }
            }

            long distance = int.MaxValue;
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
            Assert.That(distance, Is.EqualTo(answer));
        }

        [Example(answer: 610, @"
            R75,D30,R83,U83,L12,D49,R71,U7,L72
            U62,R66,U55,R34,D71,R55,D58,R83")]
        [Example(answer: 410, @"
            R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51
            U98,R91,D20,R16,D67,R40,U7,R15,U6,R7")]
        [Puzzle(answer: 91518, year: 2019, day: 03)]
        public void part_two(long answer, string input)
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

            long distance = int.MaxValue;

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
            Assert.That(distance, Is.EqualTo(answer));
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