namespace Advent_of_Code_2022;

[Category(Category.Computation)]
public class Day_01
{
    [Example(answer: 24000, "1000;2000;3000;;4000;;5000;6000;;7000;8000;9000;;10000")]
    [Puzzle(answer: 69528, O.μs10)]
    public int part_one(GroupedLines groups) => groups.Select(lines => lines.Int32s().Sum()).Max();

    [Example(answer: 45000, "1000;2000;3000;;4000;;5000;6000;;7000;8000;9000;;10000")]
    [Puzzle(answer: 206152, O.μs100)]
    public int part_two(GroupedLines groups) => groups.Select(lines => lines.Int32s().Sum()).OrderDescending().Take(3).Sum();
}
