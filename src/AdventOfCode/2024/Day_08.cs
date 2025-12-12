namespace Advent_of_Code_2024;

[Category(Category.Grid)]
public class Day_08
{
    [Example(answer: 14, Example._1)]
    [Puzzle(answer: 278, O.μs10)]
    public int part_one(CharGrid map) => Scan(map, One).Count;

    [Example(answer: 34, Example._1)]
    [Puzzle(answer: 1067, O.μs10)]
    public int part_two(CharGrid map) => Scan(map, Two).Count;

    static HashSet<Point> Scan(CharGrid map, Func<Point, Point, CharGrid, IEnumerable<Point>> add) => [.. map
        .Positions(p => p != '.').GroupBy(p => map[p])
        .SelectMany(ps => ps.RoundRobin())
        .SelectMany(ps => add(ps.First, ps.Second, map).Concat(add(ps.Second, ps.First, map)))];

    static IEnumerable<Point> One(Point p1, Point p2, CharGrid map) => p1.Repeat(p1 - p2).TakeWhile(map.OnGrid).Take(1);

    static IEnumerable<Point> Two(Point p1, Point p2, CharGrid map) => p1.Repeat(p1 - p2, true).TakeWhile(map.OnGrid);
}
