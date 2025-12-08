namespace Advent_of_Code_2025;

/// <summary>
/// There are 1000 boxes defined as 3D-points
///
/// Part one: The product of the 3 biggest cliques when connecting the 1000 shortest paths.
/// Part two: The product of the X-coordinates for the connection that connects them all.
/// </summary>
[Category(Category._3D)]
public class Day_08
{
    [Puzzle(answer: 181584L, O.ms10)]
    public long part_one(Point3Ds points) => Connect(points, 1000);

    [Puzzle(answer: 8465902405, O.ms10)]
    public long part_two(Point3Ds points) => Connect(points, int.MaxValue);

    static long Connect(Point3Ds points, int take)
    {
        var cs = new Dictionary<Point3D, HashSet<Point3D>>();
        foreach (var point in points) cs[point] = [point];

        foreach (var pair in points.RoundRobin().OrderBy(p => (p.First - p.Second).Length2).Take(take))
        {
            var f = cs[pair.First];

            // Already connected.
            if (f.Contains(pair.Second)) continue;

            var s = cs[pair.Second];
            f.AddRange(s);

            // One clique has occured.
            if (f.Count == points.Count) return pair.First.X.Long() * pair.Second.X.Long();

            foreach (var p in s) cs[p] = f;
            f.Add(pair.Second);
        }

        return cs.Values.Distinct().OrderByDescending(d => d.Count).Take(3).Product(x => x.Count);
    }
}
