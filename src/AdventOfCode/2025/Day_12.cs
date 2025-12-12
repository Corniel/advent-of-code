namespace Advent_of_Code_2025;

/// <summary>
/// There are areas where presents (Tetris inspired shapes) could be arranged on.
///
/// Part one: Find the areas where the presents fit.
/// </summary>
[Category(Category.Computation)]
public class Day_12
{
    [Puzzle(answer: 521, O.μs100)]
    public int part_one(GroupedLines groups)
    {
        var shapes = groups[..^1].As(ls => ls.Sum(l => l.Count('#')));
        return groups[^1].Count(Fits);

        bool Fits(string line)
        {
            var ns = line.Int32s().Slice();
            // Rows * Cols > space per present * the required amount.
            return ns[0] * ns[1] > Range(0, ns.Count - 2).Sum(i => shapes[i] * ns[i + 2]);
        }
    }

    [Puzzle(answer: "Finish decorating the North Pole")]
    public string part_two(string str) => "Finish decorating the North Pole";
}
