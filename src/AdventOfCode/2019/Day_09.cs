namespace Advent_of_Code_2019;

[Category(Category.IntComputer)]
public class Day_09
{
    [Puzzle(answer: 3780860499L, O.μs100)]
    public Int part_one(string input)
        => Computer.Parse(input).Run(new RunArguments(1)).Output[0];

    [Puzzle(answer: 33343, O.ms)]
    public Int part_two(string input)
        => Computer.Parse(input).Run(new RunArguments(2)).Output[0];
}
