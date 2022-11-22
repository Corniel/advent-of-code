namespace Advent_of_Code_2019;

[Category(Category.IntComputer)]
public class Day_09
{
    [Puzzle(answer: 3780860499L, year: 2019, day: 09)]
    public Int part_one(string input)
        => Computer.Parse(input).Run(new RunArguments(1)).Output.First();

    [Puzzle(answer: 33343, year: 2019, day: 09)]
    public Int part_two(string input)
        => Computer.Parse(input).Run(new RunArguments(2)).Output.First();
}
