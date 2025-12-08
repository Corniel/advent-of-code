namespace Advent_of_Code_2018;

[Category(Category.VectorAlgebra)]
public class Day_06
{
    [Example(answer: 17, "1, 1;1, 6;8, 3;3, 4;5, 5;8, 9")]
    [Puzzle(answer: 4976, O.ms10)]
    public int part_one(Point2Ds points)
    {
        Area[] areas = [.. points.As(p => new Area(p))];
        foreach (var area in areas) { area.Other = [.. areas.Select(a => a.Point).Except(area.Point)]; }

        var dis = 0; var min = areas.Select(a => a.Point).Min(); var max = areas.Select(a => a.Point).Max();

        do
        {
            dis++;

            foreach (var area in areas.Where(a => a.Finate))
            {
                foreach (var p in area.Todo.DequeueCurrent()
                    .SelectMany(Neighbors)
                    .Where(n => n.ManhattanDistance(area.Point) == dis
                        && area.Other.All(o => n.ManhattanDistance(o) > dis)
                        && area.Tiles.Add(n)))
                {
                    area.Finate &= p.X.InRange(min.X, max.X) && p.Y.InRange(min.Y, max.Y);
                    area.Todo.Enqueue(p);
                }
            }
        }
        while (areas.Any(a => a.Finate && a.Todo.Count != 0));

        return areas.Where(a => a.Finate).Max(a => a.Tiles.Count);
    }

    [Example(answer: 16, "1, 1;1, 6;8, 3;3, 4;5, 5;8, 9", 32)]
    [Puzzle(answer: 46462, null, 10_000, O.ms10)]
    public int part_two(Point2Ds points, int max) => Points.Range(points.Min(), points.Max()).Count(p => points.Sum(p.ManhattanDistance) < max);

    static IEnumerable<Point> Neighbors(Point p) => CompassPoints.Primary.Select(c => p + c.ToVector());

    record Area(Point Point)
    {
        public HashSet<Point> Tiles { get; } = new([Point]);
        public bool Finate { get; set; } = true;
        public ImmutableArray<Point> Other { get; set; }
        public Queue<Point> Todo { get; } = new Queue<Point>().EnqueueRange(Point);
    }
}
