using Advent_of_Code;
using NUnit.Framework;
using SmartAss.Numerics;
using SmartAss.Parsing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_24
    {
        [Test]
        public void nwwswee_makes_roundtrip()
        {
            var location = Point.O;
            foreach(var step in Steps("nwwswee")) { location += step; }
            Assert.AreEqual(Point.O, location);
        }

        [Example(answer: 10, year: 2020, day: 24, example: 1)]
        [Puzzle(answer: 523, year: 2020, day: 24)]
        public int part_one(string input) => new Tiles().Init(input.Lines(Steps)).Flipped;

        [Example(answer: 2208, year: 2020, day: 24, example: 1)]
        [Puzzle(answer: 4225, year: 2020, day: 24)]
        public long part_two(string input)
        {
            var tiles = new Tiles().Init(input.Lines(Steps));
            foreach(var round in Enumerable.Range(0, 100)) tiles.FlipTiles();
            return tiles.Flipped;
        }

        private class Tiles : IEnumerable<Point>
        {
            private readonly Dictionary<Point, bool> tiles = new();
            public bool this[Point point]
            {
                get
                {
                    if (!tiles.TryGetValue(point, out var val)) { tiles[point] = false; }
                    return val;
                }
                set => tiles[point] = value;
            }
            public int Flipped => tiles.Values.Count(v => v);
            
            public IEnumerable<Point> Neighbors(Point tile) => Directions.Select(dir => tile + dir);
            public Tiles Init(IEnumerable<IEnumerable<Vector>> paths)
            {
                foreach(var path in paths)
                {
                    var start = Point.O;
                    foreach (var step in path) { start += step; }
                    var current = this[start];
                    this[start] = !current;
                }
                foreach (var tile in tiles.Keys.SelectMany(t => Neighbors(t)).ToArray())
                {
                    if (!tiles.ContainsKey(tile)) { tiles[tile] = false; }
                }
                return this;
            }
            public void FlipTiles()
            {
                var on = new List<Point>();
                var off = new List<Point>();
                foreach (var tile in this.ToArray())
                {
                    var count = Neighbors(tile).Count(t => this[t]);
                    if (this[tile] && (count == 0 || count > 2)) { off.Add(tile); }
                    else if (!this[tile] && count == 2) { on.Add(tile); }
                }
                foreach (var t in on) { this[t] = true; }
                foreach (var t in off) { this[t] = false; }
            }
            public IEnumerator<Point> GetEnumerator() => tiles.Keys.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            private static Vector[] Directions = new[] { Vector.E * 2, Vector.SE, Vector.NE, Vector.W * 2, Vector.SW, Vector.NW };
        }
        private static IEnumerable<Vector> Steps(string line)
        {
            var prev = ' ';
            foreach (var ch in line)
            {
                if (ch == 'e')
                {
                    if (prev == 's') yield return Vector.SE;
                    else if (prev == 'n') yield return Vector.NE;
                    else yield return Vector.E * 2;
                }
                else if (ch == 'w')
                {
                    if (prev == 's') yield return Vector.SW;
                    else if (prev == 'n') yield return Vector.NW;
                    else yield return Vector.W * 2;
                }
                prev = ch;
            }
        }
    }
}