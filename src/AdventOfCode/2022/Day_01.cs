namespace Advent_of_Code_2022;

[Category(Category.Computation)]
public class Day_01
{
    [Example(answer: 24000, "1000;2000;3000;;4000;;5000;6000;;7000;8000;9000;;10000")]
    [Puzzle(answer: 69528)]
    public long part_one(string input) => input.GroupedLines().Select(lines => lines.Int32s().Sum()).Max();

    [Example(answer: 45000, "1000;2000;3000;;4000;;5000;6000;;7000;8000;9000;;10000")]
    [Puzzle(answer: 206152)]
    public long part_two(string input) => input.GroupedLines().Select(lines => lines.Int32s().Sum()).OrderByDescending(m => m).Take(3).Sum();
}
