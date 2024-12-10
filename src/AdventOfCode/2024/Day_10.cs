namespace Advent_of_Code_2024;

[Category(Category.PathFinding, Category.Grid)]
public class Day_10
{
    [Example(answer: 36, Example._1)]
    [Puzzle(answer: 512, O.μs100)]
    public int part_one(CharGrid map) => Hike(map, p => [p]);

    [Example(answer: 81, Example._1)]
    [Puzzle(answer: 1045, O.μs100)]
    public int part_two(CharGrid map) => Hike(map, p => null);

    static int Hike(CharGrid map, Func<Point, HashSet<Point>> done)
    {
        map.SetNeighbors(Neighbors.Grid, CompassPoints.Primary);
        return map.Positions(p => p == '0').Sum(p => Hike(p, '0', map, done(p)));
    }

    static int Hike(Point start, char h, CharGrid map, HashSet<Point> done)
    {
        var paths = new Queue<Point>().EnqueueRange(start);

        while (h++ < '9')
        {
            foreach (var p in paths.DequeueCurrent())
            {
                paths.EnqueueRange(map.Neighbors[p].Where(n => map[n] == h && done?.Add(n) is not false));
            }
        }
        return paths.Count;
    }
}
