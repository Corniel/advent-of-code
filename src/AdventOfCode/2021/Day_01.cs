namespace Advent_of_Code_2021;

[Category(Category.Simulation)]
public class Day_01
{
    [Example(answer: 7, "199,200,208,210,200,207,240,269,260,263")]
    [Puzzle(answer: 1292, O.μs10)]
    public int part_one(Ints numbers) => numbers.SelectWithPrevious().Count(t => t.Previous < t.Current);

    [Example(answer: 5, "199,200,208,210,200,207,240,269,260,263")]
    [Puzzle(answer: 1262, O.μs10)]
    public int part_two(Ints numbers) => numbers
        .SelectWithPrevious(size: 3)
        .Select(group => group.Sum())
        .SelectWithPrevious().Count(t => t.Previous < t.Current);
}
