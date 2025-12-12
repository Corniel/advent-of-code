namespace Advent_of_Code_2023;

[Category(Category.Grid)]
public class Day_03
{
    [Example(answer: 4361, Example._1)]
    [Example(answer: 517021, Example._2)]
    [Puzzle(answer: 544664, O.ms10)]
    public int part_one(CharGrid map) => Scan(map, 1);

    [Example(answer: 467835, Example._1)]
    [Puzzle(answer: 84495585, O.ms10)]
    public int part_two(CharGrid map) => Scan(map, 2);

    static int Scan(CharGrid map, int part)
    {
        map.SetNeighbors(Neighbors.Grid, CompassPoints.All);

        var span = new List<Point>();
        var parts = new List<Part>();
        var n = 0; var sum = 0; var ratio = 0;

        foreach (var tile in map)
        {
            if (tile.Key.X == 0) sum += Score(map, ref n, span, parts);
            if (tile.Value.TryDigit() is { } d)
            {
                span.Add(tile.Key);
                n = n * 10 + d;
            }
            else sum += Score(map, ref n, span, parts);
        }
        if (part == 1) return sum;

        foreach (var tile in map.Positions(t => t == '*'))
        {
            var neighbors = map.Neighbors[tile].Fix();

            if (parts.Where(p => p.Span.Exists(x => neighbors.Contains(x))).Fix() is { Length: 2 } ps)
                ratio += ps.Product(p => p.Val);
        }
        return ratio;

        int Score(CharGrid grid, ref int n, List<Point> span, List<Part> parts)
        {
            var score = n > 0 && span.SelectMany(n => grid.Neighbors[n]).Any(IsSimbol) ? n : 0;
            if (score > 0) parts.Add(new(n, [.. span]));
            span.Clear();
            n = 0;
            return score;
        }

        bool IsSimbol(Point p) => map[p] is { } c && !c.IsDigit() && c != '.';
    }

    record struct Part(int Val, Point[] Span);
}
