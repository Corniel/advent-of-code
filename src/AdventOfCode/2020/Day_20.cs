namespace Advent_of_Code_2020;

[Category(Category.Grid, Category.VectorAlgebra)]
public class Day_20
{
    [Example(answer: 20899048083289, Example._1)]
    [Puzzle(answer: 18449208814679, O.ms10)]
    public long part_one(GroupedLines groups)
        => Tile.Parse(groups).Where(t => t.IsCorner).Select(c => c.Id).Distinct().Product();

    [Example(answer: 273, Example._1)]
    [Puzzle(answer: 1559, O.ms10)]
    public int part_two(GroupedLines groups)
    {
        var tiles = Tiles.Create(Tile.Parse(groups));
        foreach (var canvas in tiles.Canvases())
        {
            var occupations = Sea.Monster.Occupations(canvas);
            if (occupations != 0) { return canvas.Count(p => p.Value == '#') - occupations; }
        }
        throw new NoAnswer();
    }

    class Tile
    {
        public Tile(long id, CharGrid map)
        {
            Id = id;
            Grid = map;
            N = Border(Range(0, 10).Select(i => map[i, 0] == '#' ? 1 : 0).ToArray());
            E = Border(Range(0, 10).Select(i => map[9, i] == '#' ? 1 : 0).ToArray());
            S = Border(Range(0, 10).Select(i => map[i, 9] == '#' ? 1 : 0).ToArray());
            W = Border(Range(0, 10).Select(i => map[0, i] == '#' ? 1 : 0).ToArray());
            Borders = [N, E, S, W];
        }
        static uint Border(int[] bits)
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
        public List<Tile> Neighbors { get; } = [];
        public bool IsCorner => Neighbors.Count == 2;

        public Tile Rotate(DiscreteRotation rotation) => new(Id, Grid.Rotate(rotation));
        public Tile Flip() => new(Id, Grid.Flip(true));
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

        public static Tile[] Parse(GroupedLines groups)
        {
            var tiles = groups.Select(Parse)
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
        static Tile Parse(string[] lines)
           => new(lines[0].SpaceSeparated()[1][0..^1].Int32(), lines.Skip(1).CharPixels().Grid());
        static IEnumerable<Tile> Others(IEnumerable<Tile> tiles, Tile exclude)
            => tiles.Where(t => t.Id != exclude.Id);
    }

    class Tiles : Grid<Tile>
    {
        public Tiles(int size) : base(size, size) => Do.Nothing();

        public IEnumerable<CharGrid> Canvases()
        {
            var canvas = new CharGrid(Cols * 8, Rows * 8);
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
                !t.Neighbors.Exists(n => t.N == n.S) &&
                !t.Neighbors.Exists(n => t.W == n.E));
        }
        private void Fill()
        {
            var points = new Queue<Point>([Point.O]);

            while (System.Collections.CollectionExtensions.NotEmpty(points))
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
            var matrix = new Tiles((tiles.Length / 8d).Sqrt().Floor());
            matrix.FillO(tiles);
            matrix.Fill();
            return matrix;
        }
    }
    private sealed class Sea : List<Vector>
    {
        public static readonly Sea Monster = new(@"
                ..................#.
                #....##....##....###
                .#..#..#..#..#..#...
                ".CharPixels(true).Where(p => p.Value == '#').Select(p => p.Key - Point.O));
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
            .Count(offset => TrueForAll(relative => image[offset + relative] == '#')) * Count;
    }
}
