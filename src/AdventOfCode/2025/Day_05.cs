namespace Advent_of_Code_2025;

/// <summary>
/// There is a list of ingredent ranges and a set of available ingredients.
///
/// Part one: Count the ingredients that are defined in a range.
/// Part two: Count all ingredients defined by the ranges.
/// </summary>
[Category(Category.Computation)]
public class Day_05
{
    [Example(answer: 3, "3-5;10-14;16-20;12-18;;1;5;8;11;17;32")]
    [Puzzle(answer: 525, O.μs100)]
    public int part_one(GroupedLines groups)
    {
        var ranges = Int64Ranges.New(groups[0].Select(Int64Range.Parse));
        return groups[1].Int64s().Count(ranges.Contains);
    }

    [Example(answer: 14, "3-5;10-14;16-20;12-18;;1;5;8;11;17;32")]
    [Puzzle(answer: 333892124923577, O.μs10)]
    public long part_two(GroupedLines groups) => Int64Ranges.New(groups[0].Select(Int64Range.Parse)).Size;
}
