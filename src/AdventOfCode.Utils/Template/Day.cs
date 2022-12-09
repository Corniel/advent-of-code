namespace Advent_of_Code_@Year;

[Category(Category.μs, Category.None)]
public class Day_@Day
{
    [Example(answer: 666, "")]
    [Puzzle(answer: 666)]
    public long part_one(string input)
    {
        var lines = input.Lines();
        var grid = input.CharPixels().Grid();
        var group = input.GroupedLines();
        var numbers = input.Lines().Int32s();

    throw new NoAnswer();
    }

    [Example(answer: 666, "")]
    [Puzzle(answer: 666)]
    public long part_two(string input)
    {
        throw new NoAnswer();
    }
}
