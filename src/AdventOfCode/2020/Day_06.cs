namespace Advent_of_Code_2020;

public class Day_06
{
    [Example(answer: 11, year: 2020, day: 06, example: 1)]
    [Puzzle(answer: 7110, year: 2020, day: 06)]
    public int part_one(string input)
        => input
        .GroupedLines()
        .Sum(group => Characters.a_z
            .Count(ch => group.Any(member => member.Contains(ch))));

    [Example(answer: 6, year: 2020, day: 06, example: 1)]
    [Puzzle(answer: 3628, year: 2020, day: 06)]
    public int part_two(string input)
        => input
            .GroupedLines()
            .Sum(group => Characters.a_z
                .Count(ch => group.All(member => member.Contains(ch))));
}
