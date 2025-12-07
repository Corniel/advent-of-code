namespace Advent_of_Code_2025;

/// <summary>
/// On a grid there a beam that splits on ^ chars.
///
/// Part one: Count the splits that occur.
/// Part two: Count all possible paths.
/// </summary>
[Category(Category.Grid)]
public class Day_07
{
    [Example(answer: 21, Example._1)]
    [Puzzle(answer: 1573, O.μs10)]
    public int part_one(CharGrid map) => Simulate(map).Splits;

    [Example(answer: 40, Example._1)]
    [Puzzle(answer: 15093663987272, O.μs10)]
    public long part_two(CharGrid map) => Simulate(map).Paths;

    static (int Splits, long Paths) Simulate(CharGrid map)
    {
        var cols = new HashSet<int>();
        var paths = new long[map.Cols];
        var split = 0; var s = map.Position(c => c == 'S').X;
        paths[s] = 1; cols.Add(s);

        for (var r = 0; r < map.Rows; r++)
        {
            // As the cols are modified while iterating.
            foreach (var c in cols.ToList())
            {
                var f = paths[c];

                if (map[c, r] is '^')
                {
                    split++;
                    paths[c] = 0; paths[c - 1] += f; paths[c + 1] += f;
                    cols.Add(c - 1); cols.Add(c + 1); cols.Remove(c);
                }
            }
        }
        return (split, paths.Sum());
    }
}
