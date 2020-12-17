using Advent_of_Code;
using SmartAss;
using SmartAss.Parsing;
using SmartAss.Topology;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_17
    {
        [Example(answer: 112, @"
            .#.
            ..#
            ###")]
        [Puzzle(answer: 291, input: @"
            ##.#....
            ...#...#
            .#.#.##.
            ..#.#...
            .###....
            .##.#...
            #.##..##
            #.####..")]
        public int part_one(string input)
        {
            var active = input.CharPixels().Where(p => p.Char == '#').Select(p => p.Position).ToArray();
            var space = new Space();

            foreach (var position in active)
            {
                var cube = space[new Point3D(position.X, position.Y, 0)];
                cube.Active = true;
                foreach (var n in cube.Neighbors) { Do.Nothing(); }
            }

            for (var cycle = 1; cycle <= 6; cycle++)
            {
                var current = space.ToArray();
                foreach (var cube in current)
                {
                    cube.SetNext();
                }
                foreach (var cube in current)
                {
                    cube.UpdateActive();
                }
            }
            return space.Count(v => v.Active);
        }

        [Example(answer: 848, @"
            .#.
            ..#
            ###")]
        [Puzzle(answer: 1524, input: @"
            ##.#....
            ...#...#
            .#.#.##.
            ..#.#...
            .###....
            .##.#...
            #.##..##
            #.####..")]
        public long part_two(string input)
        {
            var active = input.CharPixels().Where(p => p.Char == '#').Select(p => p.Position).ToArray();
            var space = new HyperSpace();

            foreach (var position in active)
            {
                var cube = space[new Point4D(position.X, position.Y, 0, 0)];
                cube.Active = true;
                foreach (var n in cube.Neighbors) { Do.Nothing(); }
            }

            for (var cycle = 1; cycle <= 6; cycle++)
            {
                var current = space.ToArray();
                foreach (var cube in current)
                {
                    cube.SetNext();
                }
                foreach (var cube in current)
                {
                    cube.UpdateActive();
                }
            }
            return space.Count(v => v.Active);
        }

        [DebuggerTypeProxy(typeof(SmartAss.Diagnostics.CollectionDebugView))]
        private class Space : IEnumerable<Cube>
        {
            private readonly Dictionary<Point3D, Cube> cubes = new();
            public Cube this[Point3D position]
            {
                get
                {
                    if (cubes.TryGetValue(position, out var cube)) { return cube; }
                    else
                    {
                        cube = new Cube(position, false, this);
                        this[position] = cube;
                        return cube;
                    }
                }
                set => cubes[value.Position] = value;
            }
            public IEnumerator<Cube> GetEnumerator() => cubes.Values.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        private class Cube
        {
            public Cube(Point3D position, bool active, Space space)
            {
                Position = position;
                Active = active;
                Space = space;
            }
            public Space Space { get; }
            public Point3D Position { get; }
            public int ActiveNeighbors => Neighbors.Count(n => n.Active);
            public bool Active { get; internal set; }
            public bool Next { get; private set; }
            public void SetNext()
            {
                var a = ActiveNeighbors;
                if (Active) { Next = a == 2 || a == 3; }
                else { Next = a == 3; }
            }
            public void UpdateActive() => Active = Next;
            public override string ToString() => $"{Position}, {(Active ? "active" : "inactive")}";
            public IEnumerable<Cube> Neighbors
            {
                get
                {
                    for (var x = -1; x <= 1; x++)
                    {
                        for (var y = -1; y <= 1; y++)
                        {
                            for (var z = -1; z <= 1; z++)
                            {
                                if (x == 0 && y == 0 && z == 0) { continue; }
                                yield return Space[Position + new Vector3D(x, y, z)];
                            }
                        }
                    }
                }
            }
        }

        [DebuggerTypeProxy(typeof(SmartAss.Diagnostics.CollectionDebugView))]
        private class HyperSpace : IEnumerable<HyperCube>
        {
            private readonly Dictionary<Point4D, HyperCube> cubes = new();
            public HyperCube this[Point4D position]
            {
                get
                {
                    if (cubes.TryGetValue(position, out var cube)) { return cube; }
                    else
                    {
                        cube = new HyperCube(position, false, this);
                        this[position] = cube;
                        return cube;
                    }
                }
                set => cubes[value.Position] = value;
            }
            public IEnumerator<HyperCube> GetEnumerator() => cubes.Values.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class HyperCube
        {
            public HyperCube(Point4D position, bool active, HyperSpace space)
            {
                Position = position;
                Active = active;
                Space = space;
            }
            public HyperSpace Space { get; }
            public Point4D Position { get; }
            public int ActiveNeighbors => Neighbors.Count(n => n.Active);
            public bool Active { get; internal set; }
            public bool Next { get; private set; }
            public void SetNext()
            {
                var a = ActiveNeighbors;
                if (Active) { Next = a == 2 || a == 3; }
                else { Next = a == 3; }
            }
            public void UpdateActive() => Active = Next;
            public override string ToString() => $"{Position}, {(Active ? "active" : "inactive")}";

            public IEnumerable<HyperCube> Neighbors
            {
                get
                {
                    for (var x = -1; x <= 1; x++)
                    {
                        for (var y = -1; y <= 1; y++)
                        {
                            for (var z = -1; z <= 1; z++)
                            {
                                for (var w = -1; w <= 1; w++)
                                {
                                    if (x == 0 && y == 0 && z == 0 && w == 0) { continue; }
                                    yield return Space[Position.Add(x, y, z, w)];
                                }
                            }
                        }
                    }
                }
            }
        }
        private readonly struct Point4D : IEquatable<Point4D>
        {
            public Point4D(int x, int y, int z, int w)
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
            }
            public int X { get; }
            public int Y { get; }
            public int Z { get; }
            public int W { get; }
            public Point4D Add(int x, int y, int z, int w) => new Point4D(X + x, Y + y, Z + z, W + w);
            public override bool Equals(object obj) => obj is Point4D other && Equals(other);
            public bool Equals(Point4D other)
                => X == other.X
                && Y == other.Y
                && Z == other.Z
                && W == other.W;
            public override int GetHashCode()
                => X
                ^ (Y << 8)
                ^ (Z << 16)
                ^ (W << 24);
            public override string ToString() => $"({X}, {Y}, {Z}, {W})";
        }
    }
}