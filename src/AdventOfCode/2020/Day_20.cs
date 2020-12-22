using Advent_of_Code;
using SmartAss;
using SmartAss.LinearAlgebra;
using SmartAss.Parsing;
using SmartAss.Text;
using SmartAss.Topology;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_20
    {
        [Example(answer: 20899048083289, year: 2020, day: 20, example: 1)]
        [Puzzle(answer: 18449208814679, year: 2020, day: 20)]
        public long part_one(string input)
            => Tile.Parse(input).Where(t => t.IsCorner).Select(c => c.Id).Distinct().Product();

        [Example(answer: 273, year: 2020, day: 20, example: 1)]
        [Puzzle(answer: 1559, year: 2020, day: 20)]
        public int part_two(string input)
        {
            var tiles = Tiles.Create(Tile.Parse(input));
            foreach (var canvas in tiles.Canvases())
            {
                var occupations = Sea.Monster.Occupations(canvas);
                if (occupations != 0) { return canvas.Count(p => p.Char == '#') - occupations; }
            }
            throw new NoAnswer();
        }

        private class Tile
        {
            public Tile(long id, CharGrid chars)
            {
                Id = id;
                Grid = chars;
                N = Border(Enumerable.Range(0, 10).Select(i => chars[i, 0] == '#' ? 1 : 0).ToArray());
                E = Border(Enumerable.Range(0, 10).Select(i => chars[9, i] == '#' ? 1 : 0).ToArray());
                S = Border(Enumerable.Range(0, 10).Select(i => chars[i, 9] == '#' ? 1 : 0).ToArray());
                W = Border(Enumerable.Range(0, 10).Select(i => chars[0, i] == '#' ? 1 : 0).ToArray());
                Borders = new[] { N, E, S, W };
            }
            private static uint Border(int[] bits)
            {
                uint pattern = default;
                for (var i = 0; i < bits.Length; i++)
                {
                    if (bits[i] != 0) { pattern = Bits.UInt32.Flag(pattern, i); }
                }
                return pattern;
            }
            public long Id { get; }
            public CharGrid Grid { get; }
            public uint N { get; }
            public uint E { get; }
            public uint S { get; }
            public uint W { get; }
            public uint[] Borders { get; }
            public List<Tile> Neighbors { get; } = new();
            public bool IsCorner => Neighbors.Count == 2;

            public Tile CCW() => new Tile(Id, Grid.RotateCCW());
            public Tile Flip() => new Tile(Id, Grid.FlipHorizontal());
            public IEnumerable<Tile> Orientations()
            {
                yield return this;
                yield return CCW();
                yield return CCW().CCW();
                yield return CCW().CCW().CCW();
                yield return Flip();
                yield return Flip().CCW();
                yield return Flip().CCW().CCW();
                yield return Flip().CCW().CCW().CCW();
            }
            public override string ToString() => $"ID: {Id}, N: {N:000}, E: {E:000}, S: {S:000}, W: {W:000}";

            public static Tile[] Parse(string input)
            {
                var tiles = input.GroupedLines().Select(Parse)
                    .SelectMany(i => i.Orientations())
                    .ToArray();
                foreach (var tile in tiles)
                {
                    tile.Neighbors.AddRange(Others(tiles, tile).Where(o => tile.N == o.S));
                    tile.Neighbors.AddRange(Others(tiles, tile).Where(o => tile.E == o.W));
                    tile.Neighbors.AddRange(Others(tiles, tile).Where(o => tile.S == o.N));
                    tile.Neighbors.AddRange(Others(tiles, tile).Where(o => tile.W == o.E));
                }
                return tiles;
            }
            private static Tile Parse(string[] lines)
               => new Tile(lines[0].Seperate(' ')[1][0..^1].Int32(), lines.Skip(1).CharPixels().Grid());
            private static IEnumerable<Tile> Others(IEnumerable<Tile> tiles, Tile exclude) 
                => tiles.Where(t => t.Id != exclude.Id);
        }

        private class Tiles : Matrix<Tile>
        {
            public Tiles(int size) : base(size, size) => Do.Nothing();
     
            public IEnumerable<CharGrid> Canvases()
            {
                var canvas = new CharGrid(Cols * 8, Rows * 8);
                foreach(var p in Points.Grid(Cols, Rows))
                {
                    foreach(var l in Points.Grid(8, 8))
                    {
                        var target = l + (p.Vector() * 8);
                        var source = l + Vector.SE;
                        canvas[target] = this[p].Grid[source];
                    }
                }
                yield return canvas;
                yield return canvas.RotateCCW();
                yield return canvas.RotateCCW().RotateCCW();
                yield return canvas.RotateCCW().RotateCCW().RotateCCW();
                yield return canvas.FlipHorizontal();
                yield return canvas.FlipHorizontal().RotateCCW();
                yield return canvas.FlipHorizontal().RotateCCW().RotateCCW();
                yield return canvas.FlipHorizontal().RotateCCW().RotateCCW().RotateCCW();
            }
            private void FillO(Tile[] tiles)
            {
                this[Point.O] = tiles.First(t => t.IsCorner &&
                    !t.Neighbors.Any(n => t.N == n.S) &&
                    !t.Neighbors.Any(n => t.W == n.E));
            }
            private void Fill()
            {
                var points = new Queue<Point>(new[] { Point.O });
                
                while (points.Any())
                {
                    var point = points.Dequeue();
                    var prev = this[point];
                    var e = point + Vector.E;
                    var s = point + Vector.S;
                    if (OnMatrix(e) && this[e] is null)
                    {
                        this[e] = prev.Neighbors.Single(n => prev.E == n.W);
                        points.Enqueue(e);
                    }
                    if (OnMatrix(s) && this[s] is null)
                    {
                        this[s] = prev.Neighbors.Single(n => prev.S == n.N);
                        points.Enqueue(s);
                    }
                }
            }

            public static Tiles Create(Tile[] tiles)
            {
                var matrix = new Tiles((int)Math.Sqrt(tiles.Length / 8d));
                matrix.FillO(tiles);
                matrix.Fill();
                return matrix;
            }
        }
        private sealed class Sea : List<Vector>
        {
            public static readonly Sea Monster = new Sea(@"
                ..................#.
                #....##....##....###
                .#..#..#..#..#..#...
                ".CharPixels().Where(p => p.Char == '#').Select(p => p.Position - Point.O));
            private Sea(IEnumerable<Vector> points)
            {
                AddRange(points);
                Width = this.Max(p => p.X);
                Height = this.Max(p => p.Y);
            }
            public int Width { get; }
            public int Height { get; }
            public int Occupations(CharGrid image)
                => Points.Grid(image.Cols - Width, image.Rows - Height)
                .Count(offset => this.All(relative => image[offset + relative] == '#')) * Count;
        }
    }
}