namespace Advent_of_Code_2019;

[Category(Category.IntComputer)]
public class Day_05
{
    [Puzzle(answer: 12428642, O.μs10)]
    public Int part_one(string str)
        => Computer.Parse(str).Run(new RunArguments(1)).Output[^1];

    [Puzzle(answer: 918655, O.μs10)]
    public Int part_two(string str)
        => Computer.Parse(str).Run(new RunArguments(5)).Output[^1];
}
