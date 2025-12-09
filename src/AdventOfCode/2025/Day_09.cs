namespace Advent_of_Code_2025;

/// <summary>
/// There is a polygon defined with about 500 points.
///
/// Part one: Find the biggest square that can be drawn from two points on the polygon.
/// Part two: Find the biggest square that can be drawn from two points that fits polygon.
/// </summary>
[Category(Category._2D, Category.VectorAlgebra)]
public class Day_09
{
    [Example(answer: 50, "7,1 11,1 11,7 9,7 9,5 2,5 2,3 7,3")]
    [Puzzle(answer: 4760959496, O.μs100)]
    public long part_one(Point2Ds points) => points.RoundRobin().Select(p => Rect.New(p).Size).Max();

    [Puzzle(answer: 1343576598L, O.ms10)]
    public long part_two(Point2Ds points)
    {
        // The points are in the right order to describe the polygon.
        var poly = new Polygon(points);
        return points.RoundRobin().Select(Rect.New).OrderByDescending(r => r.Size).First(poly.Contains).Size;
    }
   
    readonly record struct Rect(Point Min, Point Max)
    {
        public long Size => 1L * (Max.X - Min.X + 1) * (Max.Y - Min.Y + 1);

        public static Rect New(Pair<Point> pair)
        {
            var (a, b) = pair;
            return new((int.Min(a.X, b.X), int.Min(a.Y, b.Y)), (int.Max(a.X, b.X), int.Max(a.Y, b.Y)));
        }
    }

    readonly record struct Vertice(bool IsX, int Idx, int Min, int Max)
    {
        public bool Contains(int idx, int val) => Idx == idx && val.InRange(Min, Max);

        // For intersections the upperbound is included to prevent double counting.
        public bool Intersects(int val) => val.InRange(Min, Max - 1);

        public static Vertice New(Point a, Point b) => a.X == b.X
            ? new(true, a.X, int.Min(a.Y, b.Y), int.Max(a.Y, b.Y))
            : new(false, a.Y, int.Min(a.X, b.X), int.Max(a.Y, b.X));
    }

    sealed class Polygon(Point2Ds points)
    {
        private readonly Vertice[] Vertices = [.. Range(0, points.Count).Select(i => Vertice.New(points[i], points[(i + 1) % points.Count]))];
        private readonly HashSet<int> Xs = [.. points.As(v => v.X)];
        private readonly HashSet<int> Ys = [.. points.As(v => v.Y)];
        private readonly Dictionary<Point, bool> Cache = points.ToDictionary(p => p, _ => true);

        public bool Contains(Rect rect)
        {
            foreach (var y in Ys.Where(y => y.InRange(rect.Min.Y, rect.Max.Y)))
                if (!Contains((rect.Min.X, y)) || !Contains((rect.Max.X, y))) return false;

            foreach (var x in Xs.Where(x => x.InRange(rect.Min.X, rect.Max.X)))
                if (!Contains((x, rect.Min.Y)) || !Contains((x, rect.Max.Y))) return false;

            return true;
        }

        bool Contains(Point p) => Cache.TryGetValue(p, out var cache) ? cache : Cache[p] = Contains(p.X, p.Y);

        bool Contains(int x, int y)
        {
            var inter = 0;
            // Rows and cols occur alternatiting in the vertices.
            var skip = Vertices[0].IsX ? 1 : 0;

            // On the horizonatal vetices we just check if we're on it.
            for (var i = skip; i < Vertices.Length; i += 2)
                if (Vertices[i].Contains(y, x)) return true;

            // On the vertical vetices we check if we're on it or count the intersections.
            for (var i = 1 - skip; i < Vertices.Length; i += 2)
            {
                var v = Vertices[i];
                if (v.Contains(x, y)) return true;
                if (x < v.Idx && v.Intersects(y)) inter++;
            }

            // With an odd nummer of intersections we are inside the polygon.
            return inter.IsOdd;
        }
    }
}
