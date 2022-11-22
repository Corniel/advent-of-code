namespace Advent_of_Code_2019;

[Category(Category.IntComputer)]
public class Day_05
{
    [Puzzle(answer: 12428642, year: 2019, day: 05)]
    public Int part_one(string input)
        => Computer.Parse(input).Run(new RunArguments(1)).Output.Last();

    [Puzzle(answer: 918655, year: 2019, day: 05)]
    public Int part_two(string input)
        => Computer.Parse(input).Run(new RunArguments(5)).Output.Last();
}
