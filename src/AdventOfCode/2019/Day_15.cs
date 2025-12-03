namespace Advent_of_Code_2019;

[Category(Category.VectorAlgebra, Category.PathFinding)]
public class Day_15
{
    [Puzzle(answer: 222, O.ms100)]
    public int part_one(string str) => new Space().Exlore(new Computer(str.BigInts()).WarmUp()).O2Distance;

    [Puzzle(answer: 394, O.ms100)]
    public int part_two(string str) => new Space().Exlore(new Computer(str.BigInts()).WarmUp()).O2Spreading;

    class Space : Dictionary<Point, Tile>
    {
        public Space() => Add(default, Tile.Empty);
        public int O2Distance => Navigate((0, 0), [O2]).Distance;
        public int O2Spreading => Navigate((short.MaxValue, short.MaxValue), [O2]).Distance;
        Point O2 => this.Single(t => t.Value == Tile.Oxygen).Key;
        IEnumerable<Point> Empties => this.Where(kvp => kvp.Value != Tile.Wall).Select(kvp => kvp.Key);
        IEnumerable<Point> Unknowns => Empties.SelectMany(Neighbors).Where(tile => !ContainsKey(tile));

        public Space Exlore(Computer program)
        {
            var droid = Point.O;
            Navigation nav = null;
            while (Unknowns.NotEmpty)
            {
                nav = null;
                if (distances.NotEmpty)
                {
                    var target = Neighbors(droid).FirstOrDefault(n => distances.ContainsKey(n) && distances[n] < distances[droid]);
                    if (target != default) { nav = new(target - droid, default); }
                }
                nav ??= Navigate(droid, Unknowns);
                droid = Exlore(droid, nav, program);
            }
            return this;
        }

        Point Exlore(Point droid, Navigation nav, Computer program)
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
        bool Accessable(Point location) => !TryGetValue(location, out var t) || t != Tile.Wall;
        IEnumerable<Point> Neighbors(Point location)
            => location.Projections(Dirs.Keys).Where(Accessable);
        Navigation Navigate(Point source, IEnumerable<Point> targets)
        {
            var distance = 0;
            distances.Clear();
            queue.Clear();
            foreach (var target in targets)
            {
                distances[target] = distance;
                queue.Enqueue(target);
            }
            while (queue.NotEmpty)
            {
                distance++;
                foreach (var tile in queue.DequeueCurrent())
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
        readonly Queue<Point> queue = new();
        readonly Dictionary<Point, int> distances = [];
    }
    enum Tile { Wall = 0, Empty = 1, Oxygen = 2 }

    record Navigation(Vector Direction, int Distance) { public int Instruction => Dirs[Direction]; }

    static readonly Dictionary<Vector, int> Dirs = new()
    {
        [Vector.N] = 1,
        [Vector.S] = 2,
        [Vector.W] = 3,
        [Vector.E] = 4,
    };
}
