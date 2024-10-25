namespace Advent_of_Code_2023;

[Category(Category.Grid)]
public class Day_11
{
    [Example(answer: 374, Example._1)]
    [Puzzle(answer: 9605127L, O.ms10)]
    public long part_one(CharGrid map) => Distances(map, 1);

    [Example(answer: 1030, null, 10, Example._1)]
    [Example(answer: 8410, null, 100, Example._1)]
    [Puzzle(answer: 458191688761, null, 1_000_000, O.ms10)]
    public long part_two(CharGrid map, int expand) => Distances(map, expand - 1);

    static long Distances(CharGrid map, int expand)
    {
        var emptyCols = Range(0, map.Cols).Select(r => map.Col(r).All(t => t.Value == '.')).ToArray();
        var emptyRows = Range(0, map.Rows).Select(r => map.Row(r).All(t => t.Value == '.')).ToArray();

        return map.Positions(c => c == '#').ToArray().RoundRobin().Sum(Distance);

        long Distance(Pair<Point> p)
        {
            var dis = p.First.ManhattanDistance(p.Second);

            var min = Points.Min(p.First, p.Second);
            var max = Points.Max(p.First, p.Second);

            for (var x = min.X + 1; x < max.X; x++) dis += emptyCols[x] ? expand : 0;
            for (var y = min.Y + 1; y < max.Y; y++) dis += emptyRows[y] ? expand : 0;

            return dis;
        }
    }
}
