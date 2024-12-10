namespace Advent_of_Code_2020;

[Category(Category.GameOfLife, Category._3D)]
public class Day_17
{
    [Example(answer: 112, @".#.
..#
###")]
    [Puzzle(answer: 291, @"##.#....
...#...#
.#.#.##.
..#.#...
.###....
.##.#...
#.##..##
#.####..", O.ms)]
    public int part_one(CharPixels chars)
    {
        var space = new Space();
        space.AddRange(chars.Where(p => p.Value == '#').Select(p => new Point3D(p.Key.X, p.Key.Y, 0)));
        space.Generations(6);
        return space.Count;
    }

    [Example(answer: 848, @"
        .#.
        ..#
        ###")]
    [Puzzle(answer: 1524, @"
        ##.#....
        ...#...#
        .#.#.##.
        ..#.#...
        .###....
        .##.#...
        #.##..##
        #.####..", O.ms100)]
    public int part_two(CharPixels chars)
    {
        var space = new HyperSpace();
        space.AddRange(chars.Where(p => p.Value == '#').Select(p => new Point4D(p.Key.X, p.Key.Y, 0, 0)));
        space.Generations(6);
        return space.Count;
    }

    class Space : GameOfLife<Point3D>
    {
        protected override bool Dies(int living) => living < 2 || living > 3;
        protected override bool IntoExistence(int living) => living == 3;
        public override IEnumerable<Point3D> Neighbors(Point3D cell)
        {
            for (var x = -1; x <= 1; x++)
                for (var y = -1; y <= 1; y++)
                    for (var z = -1; z <= 1; z++)
                    {
                        if (x == 0 && y == 0 && z == 0) { continue; }
                        yield return cell + new Vector3D(x, y, z);
                    }
        }
    }

    class HyperSpace : GameOfLife<Point4D>
    {
        protected override bool Dies(int living) => living < 2 || living > 3;
        protected override bool IntoExistence(int living) => living == 3;
        public override IEnumerable<Point4D> Neighbors(Point4D cell)
        {
            for (var x = -1; x <= 1; x++)
                for (var y = -1; y <= 1; y++)
                    for (var z = -1; z <= 1; z++)
                        for (var w = -1; w <= 1; w++)
                        {
                            if (x == 0 && y == 0 && z == 0 && w == 0) { continue; }
                            yield return cell.Add(x, y, z, w);
                        }
        }
    }
    readonly struct Point4D(int x, int y, int z, int w) : IEquatable<Point4D>
    {
        public readonly int X = x;
        public readonly int Y = y;
        public readonly int Z = z;
        public readonly int W = w;
        public Point4D Add(int x, int y, int z, int w) => new(X + x, Y + y, Z + z, W + w);
        public override bool Equals(object obj) => obj is Point4D other && Equals(other);
        public bool Equals(Point4D other)
            => X == other.X
            && Y == other.Y
            && Z == other.Z
            && W == other.W;
        public override int GetHashCode() => X ^ (Y << 8) ^ (Z << 16) ^ (W << 24);
        public override string ToString() => $"({X}, {Y}, {Z}, {W})";
    }
}
