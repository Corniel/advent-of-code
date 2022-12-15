namespace Advent_of_Code_2020;

[Category(Category.Grid, Category.VectorAlgebra)]
public class Day_20
{
    [Example(answer: 20899048083289, Example._1)]
    [Puzzle(answer: 18449208814679, O.ms10)]
    public long part_one(string input)
        => Tile.Parse(input).Where(t => t.IsCorner).Select(c => c.Id).Distinct().Product();

    [Example(answer: 273, Example._1)]
    [Puzzle(answer: 1559, O.ms10)]
    public int part_two(string input)
    {
        var tiles = Tiles.Create(Tile.Parse(input));
        foreach (var canvas in tiles.Canvases())
        {
            var occupations = Sea.Monster.Occupations(canvas);
            if (occupations != 0) { return canvas.Count(p => p.Value == '#') - occupations; }
        }
        throw new NoAnswer();
    }

    private class Tile
    {
        public Tile(long id, Grid<char> chars)
        {
            Id = id;
            Grid = chars;
            N = Border(Range(0, 10).Select(i => chars[i, 0] == '#' ? 1 : 0).ToArray());
            E = Border(Range(0, 10).Select(i => chars[9, i] == '#' ? 1 : 0).ToArray());
            S = Border(Range(0, 10).Select(i => chars[i, 9] == '#' ? 1 : 0).ToArray());
            W = Border(Range(0, 10).Select(i => chars[0, i] == '#' ? 1 : 0).ToArray());
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
        public Grid<char> Grid { get; }
        public uint N { get; }
        public uint E { get; }
        public uint S { get; }
        public uint W { get; }
        public uint[] Borders { get; }
        public List<Tile> Neighbors { get; } = new();
        public bool IsCorner => Neighbors.Count == 2;

        public Tile Rotate(DiscreteRotation rotation) => new Tile(Id, Grid.Rotate(rotation));
        public Tile Flip() => new Tile(Id, Grid.Flip(true));
        public IEnumerable<Tile> Orientations()
        {
            yield return this;
            yield return Rotate(DiscreteRotation.Deg090);
            yield return Rotate(DiscreteRotation.Deg180);
            yield return Rotate(DiscreteRotation.Deg270);
            yield return Flip();
            yield return Flip().Rotate(DiscreteRotation.Deg090);
            yield return Flip().Rotate(DiscreteRotation.Deg180);
            yield return Flip().Rotate(DiscreteRotation.Deg270);
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
           => new(lines[0].SpaceSeparated()[1][0..^1].Int32(), lines.Skip(1).CharPixels().Grid());
        private static IEnumerable<Tile> Others(IEnumerable<Tile> tiles, Tile exclude)
            => tiles.Where(t => t.Id != exclude.Id);
    }

    private class Tiles : Grid<Tile>
    {
        public Tiles(int size) : base(size, size) => Do.Nothing();

        public IEnumerable<Grid<char>> Canvases()
        {
            var canvas = new Grid<char>(Cols * 8, Rows * 8);
            foreach (var p in Points.Grid(Cols, Rows))
            {
                foreach (var l in Points.Grid(8, 8))
                {
                    var target = l + (p.Vector() * 8);
                    var source = l + Vector.SE;
                    canvas[target] = this[p].Grid[source];
                }
            }
            yield return canvas;
            yield return canvas.Rotate(DiscreteRotation.Deg090);
            yield return canvas.Rotate(DiscreteRotation.Deg180);
            yield return canvas.Rotate(DiscreteRotation.Deg270);
            yield return canvas.Flip(horizontal: true);
            yield return canvas.Flip(horizontal: true).Rotate(DiscreteRotation.Deg090);
            yield return canvas.Flip(horizontal: true).Rotate(DiscreteRotation.Deg180);
            yield return canvas.Flip(horizontal: true).Rotate(DiscreteRotation.Deg270);
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
                if (OnGrid(e) && this[e] is null)
                {
                    this[e] = prev.Neighbors.Single(n => prev.E == n.W);
                    points.Enqueue(e);
                }
                if (OnGrid(s) && this[s] is null)
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
                ".CharPixels().Where(p => p.Value == '#').Select(p => p.Key - Point.O));
        private Sea(IEnumerable<Vector> points)
        {
            AddRange(points);
            Width = this.Max(p => p.X);
            Height = this.Max(p => p.Y);
        }
        public int Width { get; }
        public int Height { get; }
        public int Occupations(Grid<char> image)
            => Points.Grid(image.Cols - Width, image.Rows - Height)
            .Count(offset => this.All(relative => image[offset + relative] == '#')) * Count;
    }
}
