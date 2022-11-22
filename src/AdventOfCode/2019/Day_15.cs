namespace Advent_of_Code_2019;

[Category(Category.VectorAlgebra, Category.PathFinding)]
public class Day_15
{
    [Puzzle(answer: 222, year: 2019, day: 15)]
    public int part_one(string input)
        => new Space().Exlore(new Computer(input.BigInts()).WarmUp()).O2Distance;

    [Puzzle(answer: 394, year: 2019, day: 15)]
    public int part_two(string input)
        => new Space().Exlore(new Computer(input.BigInts()).WarmUp()).O2Spreading;

    private class Space : Dictionary<Point, Tile>
    {
        public Space() => Add(default, Tile.Empty);
        public int O2Distance => Navigate(Point.O, new Point[] { O2 }).Distance;
        public int O2Spreading => Navigate(new Point(short.MaxValue, short.MaxValue), new Point[] { O2 }).Distance;
        private Point O2 => this.Single(t => t.Value == Tile.Oxygen).Key;
        private IEnumerable<Point> Empties => this.Where(kvp => kvp.Value != Tile.Wall).Select(kvp => kvp.Key);
        private IEnumerable<Point> Unknowns => Empties.SelectMany(tile => Neighbors(tile)).Where(tile => !ContainsKey(tile));

        public Space Exlore(Computer program)
        {
            var droid = Point.O;
            Navigation nav = null;
            while (Unknowns.Any())
            {
                nav = null;
                if (distances.Any())
                {
                    var target = Neighbors(droid).FirstOrDefault(n => distances.ContainsKey(n) && distances[n] < distances[droid]);
                    if (target != default) { nav = new(target - droid, default); }
                }
                if (nav is null) { nav = Navigate(droid, Unknowns); }
                droid = Exlore(droid, nav, program);
            }
            return this;
        }

        private Point Exlore(Point droid, Navigation nav, Computer program)
        {
            var tile = (Tile)(int)program.Run(new RunArguments(true, false, nav.Instruction)).Output.Single();
            droid += nav.Direction;
            this[droid] = tile;
            if (tile == Tile.Wall)
            {
                droid -= nav.Direction;
                distances.Clear();
            }
            return droid;
        }
        private bool Accessable(Point location) => !TryGetValue(location, out var t) || t != Tile.Wall;
        private IEnumerable<Point> Neighbors(Point location)
            => location.Projections(Dirs.Keys).Where(tile => Accessable(tile));
        private Navigation Navigate(Point source, IEnumerable<Point> targets)
        {
            var distance = 0;
            distances.Clear();
            queue.Clear();
            foreach (var target in targets)
            {
                distances[target] = distance;
                queue.Enqueue(target);
            }
            while (queue.Any())
            {
                distance++;
                foreach (var tile in queue.DeuqueCurrent())
                {
                    foreach (var next in Neighbors(tile).Where(n => !distances.ContainsKey(n)))
                    {
                        if (next == source) return new(tile - source, distance);
                        else
                        {
                            distances[next] = distance;
                            queue.Enqueue(next);
                        }
                    }
                }
            }
            return new(Vector.O, distance - 1);
        }
        private readonly Queue<Point> queue = new();
        private readonly Dictionary<Point, int> distances = new Dictionary<Point, int>();
    }
    private enum Tile
    {
        Wall = 0,
        Empty = 1,
        Oxygen = 2,
    }
    private record Navigation(Vector Direction, int Distance)
    {
        public int Instruction => Dirs[Direction];
    }
    private static readonly Dictionary<Vector, int> Dirs = new()
    {
        { Vector.N, 1 },
        { Vector.S, 2 },
        { Vector.W, 3 },
        { Vector.E, 4 },
    };
}
