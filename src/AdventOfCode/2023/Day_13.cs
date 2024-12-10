namespace Advent_of_Code_2023;

[Category(Category.Grid)]
public class Day_13
{
    [Example(answer: 405, Example._1)]
    [Puzzle(answer: 37113, O.μs100)]
    public int part_one(GroupedLines groups) => Run(groups, 0);

    [Example(answer: 400, Example._1)]
    [Puzzle(answer: 30449, O.μs100)]
    public int part_two(GroupedLines groups) => Run(groups, 1);

    static int Run(GroupedLines groups, int errors)
        => groups.Select(l => string.Join('\n', l).CharPixels().Grid(c => c == '#')).Sum(m => Reflection(m, errors));

    static int Reflection(Grid<bool> map, int errors)
        => Scan([.. Range(0, map.Rows).Select(map.Row).Select(Values)], errors, 100)
        ?? Scan([.. Range(0, map.Cols).Select(map.Col).Select(Values)], errors, 1)
        ?? throw new NoAnswer();

    static bool[] Values(IEnumerable<KeyValuePair<Point, bool>> cells) => [..cells.Select(c => c.Value)];

    static int? Scan(bool[][] lines, int errors, int factor)
    {
        var index = 0;

        foreach (var pair in lines.SelectWithPrevious())
        {
            index++;
            var deltas = Deltas(pair.Current, pair.Previous);
            if (deltas <= errors)
            {
                var up = index + 1; var dn = index - 2;

                while (up < lines.Length && dn >= 0 && deltas <= errors)
                {
                    deltas += Deltas(lines[up++], lines[dn--]);
                }
                if (deltas == errors) return index * factor;
            }
        }
        return null;
    }

    static int Deltas(bool[] ls, bool[] rs)
    {
        var d = 0;
        for (var i = 0; i < ls.Length && d < 2; i++)
        {
            d += ls[i] != rs[i] ? 1 : 0;
        }
        return d;
    }
}
