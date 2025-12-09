namespace Advent_of_Code_2017;

[Category(Category._3D)]
public class Day_20
{
    [Puzzle(answer: 344, O.μs10)]
    public int part_one(Point3Ds points) => Init(points).Index().OrderBy(i => i.Item).First().Index;

    [Puzzle(answer: 404, O.μs100)]
    public int part_two(Point3Ds points)
    {
        var cur = Init(points).ToDictionary(d => d.P, d => d);
        var nxt = new Dictionary<Point3D, Data>();
        var del = new HashSet<Point3D>();

        for (var i = 0; i < 50; i++)
        {
            nxt.Clear(); del.Clear();

            foreach (var n in cur.Values)
            {
                if (!nxt.TryAdd(n.P, n.Next())) del.Add(n.P);
            }
            foreach (var p in del) nxt.Remove(p);

            (nxt, cur) = (cur, nxt);
        }
        return cur.Count;
    }

    static IEnumerable<Data> Init(Point3Ds points) => points.ChunkBy(3).Select(c => new Data(c[0], c[1].Vector(), c[2].Vector()));

    readonly record struct Data(Point3D P, Vector3D V, Vector3D A) : IComparable<Data>
    {
        public Data Next()
        {
            var v = V + A;
            var p = P + v;
            return new(p, v, A);
        }

        public int CompareTo(Data other)
            => A.ManhattanDistance(Vector3D.O).ComparesTo(other.A.ManhattanDistance(Vector3D.O))
            ?? V.ManhattanDistance(Vector3D.O).ComparesTo(other.V.ManhattanDistance(Vector3D.O))
            ?? P.ManhattanDistance(Point3D.O).CompareTo(other.P.ManhattanDistance(Point3D.O));
    }
}
