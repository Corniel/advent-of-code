namespace Advent_of_Code_@Year;

[Category(Category.None)]
public class Day_@Day
{
    [Example(answer: 17, Example._1)]
    [Example(answer: 17, "TEXT")]
    [Puzzle(answer: 42, O.ms)]
    public long part_one(string str)
    {
        var lines = str.Lines();
        var grid = str.CharPixels().Grid();
        var group = str.GroupedLines();
        var numbers = str.Lines().Int32s();

        throw new NoAnswer();
    }

    [Puzzle(answer: 69, O.ms)]
    public long part_two(string str)
    {
        throw new NoAnswer();
    }
}
