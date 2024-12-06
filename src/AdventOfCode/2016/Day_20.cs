namespace Advent_of_Code_2016;

[Category(Category.Computation)]
public class Day_20
{
    [Example(answer: 3, "5-8;0-2;4-7")]
    [Puzzle(answer: 31053880, O.μs100)]
    public int part_one(Longs numbers) => (int)Process(numbers)[0].Lower;

    [Puzzle(answer: 117, O.μs100)]
    public int part_two(Longs numbers) => (int)Process(numbers).Sum(r => r.Size);

    static Int64Ranges Process(Longs ns) => Int64Ranges
        .New(new Int64Range(0, 4294967295))
        .Except(ns.ChunkBy(2).Select(c => new Int64Range(c[0], -c[1])));
}
