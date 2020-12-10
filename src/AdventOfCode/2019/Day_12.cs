using Advent_of_Code;
using Advent_of_Code.Maths;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2019
{
    public class Day_12
    {
        [Example(answer: 263, @"
            <x=-1, y=0, z=2>
            <x=2, y=-10, z=-7>
            <x=4, y=-8, z=8>
            <x=3, y=5, z=-1>")]
        [Example(answer: 7262, @"
            <x=-8, y=-10, z=0>
            <x=5, y=5, z=10>
            <x=2, y=-7, z=3>
            <x=9, y=-8, z=-3")]
        [Puzzle(answer: 56202, @"
            <x=17, y=-9, z=4>
            <x=2, y=2, z=-13>
            <x=-1, y=5, z=-1>
            <x=4, y=7, z=-7>")]
        public int part_one(string input)
        {
            var moons = Moon.Parse(input).ToArray();

            for (var step = 1; step <= 10_000; step++)
            {
                Moon.SetStep(moons);
            }
            return moons.Sum(moon => moon.TotalEnergy);
        }

        [Example(answer: 2772, @"
            <x=-1, y=0, z=2>
            <x=2, y=-10, z=-7>
            <x=4, y=-8, z=8>
            <x=3, y=5, z=-1>")]
        [Example(answer: 4686774924L, @"
            <x=-8, y=-10, z=0>
            <x=5, y=5, z=10>
            <x=2, y=-7, z=3>
            <x=9, y=-8, z=-3")]
        [Puzzle(answer: 537881600740876L, @"
            <x=17, y=-9, z=4>
            <x=2, y=2, z=-13>
            <x=-1, y=5, z=-1>
            <x=4, y=7, z=-7>")]
        public long part_two(string input)
        {
            var moons = Moon.Parse(input).ToArray();

            var xs = moons.Select(moon => new Tuple<int, int>(moon.Position.X, default)).ToArray();
            var ys = moons.Select(moon => new Tuple<int, int>(moon.Position.Y, default)).ToArray();
            var zs = moons.Select(moon => new Tuple<int, int>(moon.Position.Z, default)).ToArray();

            var x = Cycle(xs);
            var y = Cycle(ys);
            var z = Cycle(zs);

            var xy = x * y / Mathematic.Gcd(x, y);
            return xy * z / Mathematic.Gcd(xy, z);
        }

        internal static long Cycle(Tuple<int, int>[] pairs)
        {
            var initial = pairs.ToArray();
            var steps = 0;

            do
            {
                Step(pairs);
                steps++;
            }
            while (!Enumerable.SequenceEqual(initial, pairs));

            return steps;
        }

        internal static void Step(Tuple<int, int>[] pairs)
        {
            var deltas = new int[pairs.Length];

            for (var i = 0; i < pairs.Length; i++)
            {
                foreach (var other in pairs)
                {
                    deltas[i] += Math.Sign(other.Item1 - pairs[i].Item1);
                }
            }

            for (var i = 0; i < pairs.Length; i++)
            {
                var velocity = deltas[i] + pairs[i].Item2;
                pairs[i] = new(pairs[i].Item1 + velocity, velocity);
            }
        }

        public sealed class Moon : IEquatable<Moon>
        {
            public Moon(Point3d position, Vector3d velocity)
            {
                Position = position;
                Velocity = velocity;
            }

            public int TotalEnergy => PotentialEnergy * KeneticEnergy;

            public int PotentialEnergy
                => Math.Abs(Position.X)
                + Math.Abs(Position.Y)
                + Math.Abs(Position.Z);

            public int KeneticEnergy
                => Math.Abs(Velocity.X)
                + Math.Abs(Velocity.Y)
                + Math.Abs(Velocity.Z);

            public Point3d Position { get; set; }
            public Vector3d Velocity { get; set; }

            public Moon Copy() => new Moon(Position, Velocity);

            public override bool Equals(object obj) => obj is Moon other && Equals(other);
            public bool Equals(Moon other)
                => other != null
                && Position.Equals(other.Position)
                && Velocity.Equals(other.Velocity);

            public override int GetHashCode() => throw new NotSupportedException();


            public override string ToString()
                => $"Pos: {Position}, Vel: {Velocity}";

            public static IEnumerable<Moon> Parse(string str)
                => str.Lines().Select(line =>
                {
                    var xyz = line
                        .Trim('<', '>')
                        .CommaSeperated()
                        .Select(sub => sub.Seperate('=')[1].Int32())
                        .ToArray();

                    return new Moon(new Point3d(xyz[0], xyz[1], xyz[2]), default);
                });

            public static void SetStep(IEnumerable<Moon> moons)
            {
                foreach (var moon in moons)
                {
                    foreach (var other in moons)
                    {
                        var x = Math.Sign(other.Position.X - moon.Position.X);
                        var y = Math.Sign(other.Position.Y - moon.Position.Y);
                        var z = Math.Sign(other.Position.Z - moon.Position.Z);
                        moon.Velocity = moon.Velocity.Adjust(x, y, z);
                    }
                }
                foreach (var moon in moons)
                {
                    moon.Position += moon.Velocity;
                }
            }
        }
    }
}