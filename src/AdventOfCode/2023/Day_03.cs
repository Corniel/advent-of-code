namespace Advent_of_Code_2023;

[Category(Category.Grid)]
public class Day_03
{
    [Example(answer: 4361, Example._1)]
    [Example(answer: 517021, Example._2)]
    [Puzzle(answer: 544664, O.ms10)]
    public int part_one(CharGrid grid) => Scan(grid).Parts;

    [Example(answer: 467835, Example._1)]
    [Puzzle(answer: 84495585, O.ms10)]
    public int part_two(CharGrid grid) => Scan(grid).Ratio;

    private static Result Scan(CharGrid grid)
    {
        grid.SetNeighbors(Neighbors.Grid, CompassPoints.All);

        var span = new List<Point>();
        var parts = new List<Part>();
        var n = 0; var sum = 0; var ratio = 0;

        foreach (var tile in grid)
        {
            if (tile.Key.X == 0) sum += Score(grid, ref n, span, parts);
            if (tile.Value.TryDigit() is { } d)
            {
                span.Add(tile.Key);
                n = n * 10 + d;
            }
            else sum += Score(grid, ref n, span, parts);
        }

        foreach (var tile in grid.Where(p => p.Value == '*'))
        {
            var neighbors = grid.Neighbors[tile.Key].ToArray();

            if (parts.Where(p => p.Span.Exists(x => neighbors.Contains(x))).ToArray() is { Length: 2 } ps)
            {
                ratio += ps.Product(p => p.Val);
            }
        }
        return new(sum, ratio);

        int Score(CharGrid grid, ref int n, List<Point> span, List<Part> parts)
        {
            var score = n > 0 && span.SelectMany(n => grid.Neighbors[n]).Any(IsSimbol) ? n : 0;
            if (score > 0) parts.Add(new(n, [.. span]));
            span.Clear();
            n = 0;
            return score;
        }

        bool IsSimbol(Point p) => grid[p] is { } c && !c.IsDigit() && c != '.';
    }

    record struct Part(int Val, Point[] Span);

    record struct Result(int Parts, int Ratio);
}
