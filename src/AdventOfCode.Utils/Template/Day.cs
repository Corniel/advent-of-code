namespace Advent_of_Code_@Year;

[Category(Category.None)]
public class Day_@Day
{
    [Example(answer: 666, "TEXT")]
    [Puzzle(answer: 666, O.ms)]
    public long part_one(string str)
    {
        var lines = str.Lines();
        var grid = str.CharPixels().Grid();
        var group = str.GroupedLines();
        var numbers = str.Lines().Int32s();

        throw new NoAnswer();
    }

    [Example(answer: 666, "TEXT")]
    [Puzzle(answer: 666, O.ms)]
    public long part_two(string str)
    {
        throw new NoAnswer();
    }
}
