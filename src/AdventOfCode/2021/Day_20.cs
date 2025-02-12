namespace Advent_of_Code_2021;

[Category(Category.GameOfLife, Category.Simulation, Category.BitManupilation)]
public class Day_20
{
    [Example(answer: 35, Example._1)]
    [Puzzle(answer: 5765, O.ms)]
    public int part_one(GroupedLines groups) => Run(groups, 2);

    [Example(answer: 3351, Example._1)]
    [Puzzle(answer: 18509, O.ms100)]
    public int part_two(GroupedLines groups) => Run(groups, 50);

    static int Run(GroupedLines lines, int turns)
    {
        var lookup_odd = lines[0][0].Select(ch => ch == '#').ToArray();
        var lookup_even = lookup_odd.ToArray();

        if (lookup_odd[0]) // all off pixels are on during odd turns
        {
            lookup_odd = lookup_odd.Select(toggle => !toggle).ToArray();
            lookup_even = lookup_odd.Select((toggle, index) => new { toggle, index })
                .ToDictionary(p => 511 ^ p.index, p => !p.toggle) // mirror both the index as the value
                .OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value)
                .ToArray();
        }

        var map = string.Join('\n', lines[1]).CharPixels().ToDictionary(kvp => kvp.Key, kvp => kvp.Value == '#');

        foreach (var point in map.Keys.SelectMany(p => Neighbors.Select(n => p + (n * 2)).Where(n => !map.ContainsKey(n))).ToArray())
        {
            map[point] = false;
        }
        for (var turn = 1; turn <= turns; turn++)
        {
            var temp = new Dictionary<Point, bool>();
            var lookup = turn.IsOdd() ? lookup_odd : lookup_even;
            foreach (var point in map.Keys.OrderBy(p => p.X).ThenBy(p => p.Y))
            {
                var index = 0;
                foreach (var move in Neighbors)
                {
                    var neighbor = point + move;
                    if (!map.TryGetValue(neighbor, out var toggle)) { temp[neighbor] = false; }
                    index = (index << 1) | (toggle ? 1 : 0);
                }
                temp[point] = lookup[index];
            }
            map = temp;
        }
        return map.Values.Count(v => v);
    }

    static readonly Vector[] Neighbors =
    [
        Vector.NW, Vector.N, Vector.NE,
        Vector.W, Vector.O, Vector.E,
        Vector.SW, Vector.S, Vector.SE
    ];
}
