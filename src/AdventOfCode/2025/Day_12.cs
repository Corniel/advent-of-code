
namespace Advent_of_Code_2025;

/// <summary>
/// There are areas where presents (Tetris inspired shapes) could be arranged on.
///
/// Part one: Find the areas where the presents fit.
/// </summary>
[Category(Category.Computation)]
public class Day_12
{
    [Puzzle(answer: 521, O.μs)] // We skip the first 6 numbers (id's of the shapes)
    public int part_one(Ints numbers)
    {
        var count = 0; var n = numbers.AsSpan()[6..];
        while (n.Length >= 8)
        {
            // Given the data it is safe to say that if fits as all would fit if 3x3
            if (n[0] * n[1] >= 9 * (n[2] + n[3] + n[4] + n[5] + n[6] + n[7])) count++;
            n = n[8..];
        }
        return count;
    }

    [Puzzle(answer: 24, "Finish decorating the North Pole")]
    public int part_two(string _) => 24;
}
