namespace Advent_of_Code_2017;

[Category(Category._3D)]
public class Day_20
{
    [Puzzle(answer: 344, O.μs100)]
    public int part_one(Lines lines)
    {
        var data = lines.As(Data.Parse).ToArray();
        var best = 0;
        for (var test = 1; test < data.Length; test++)
        {
            best = data[best].Compare(data[test]) > 0 ? test : best;
        }
        return best;

    }

    [Puzzle(answer: 404, O.ms)]
    public int part_two(Lines lines)
    {
        var curr = lines.As(Data.Parse).ToDictionary(d => d.P, d => d);
        var next = new Dictionary<Point3D, Data>();
        var dell = new HashSet<Point3D>();

        for (var i = 0; i < 50; i++)
        {
            next.Clear(); dell.Clear();

            foreach (var n in curr.Values)
            {
                if (!next.TryAdd(n.P, n.Next())) dell.Add(n.P);
            }
            foreach (var p in dell) next.Remove(p);

            (next, curr) = (curr, next);
        }

        return curr.Count;
    }

    record struct Data(Point3D P, Vector3D V, Vector3D A)
    {

        public Data Next()
        {
            var v = V + A;
            var p = P + v;
            return new(p, v, A);
        }

        public int Compare(Data other)
            => A.ManhattanDistance(Vector3D.O).ComparesTo(other.A.ManhattanDistance(Vector3D.O))
            ?? V.ManhattanDistance(Vector3D.O).ComparesTo(other.V.ManhattanDistance(Vector3D.O))
            ?? P.ManhattanDistance(Point3D.O).ComparesTo(other.P.ManhattanDistance(Point3D.O))
            ?? 0;

        public static Data Parse(string line)
        {
            var ns = line.Int32s().ToArray();
            return new(Ctor.New<Point3D>(ns[..3]), Ctor.New<Vector3D>(ns[3..6]), Ctor.New<Vector3D>(ns[6..]));
        }
    }
}
