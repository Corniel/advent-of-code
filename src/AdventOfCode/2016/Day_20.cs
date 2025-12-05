namespace Advent_of_Code_2016;

[Category(Category.Computation)]
public class Day_20
{
    [Example(answer: 3, "5-8;0-2;4-7")]
    [Puzzle(answer: 31053880L, O.μs100)]
    public long part_one(Int64Ranges ranges) => Process(ranges)[0].Lower;

    [Puzzle(answer: 117L, O.μs100)]
    public long part_two(Int64Ranges ranges) => Process(ranges).Sum(r => r.Size);

    static Int64Ranges Process(Int64Ranges ranges) => Int64Ranges.New(new Int64Range(0, 4294967295)).Except(ranges);
}
