namespace Advent_of_Code_2022;

[Category(Category.SequenceProgression)]
public class Day_20
{
    [Example(answer: 3, "1,2,-3,3,-2,0,4")]
    [Puzzle(answer: 5498L, O.ms10)]
    public long part_one(Longs numbers) => Decrypt(numbers, 1, 1);

    [Example(answer: 1623178306, "1,2,-3,3,-2,0,4")]
    [Puzzle(answer: 3390007892081, O.ms100)]
    public long part_two(Longs numbers) => Decrypt(numbers, 811589153, 10);

    static long Decrypt(Longs numbers, int key, int times)
    {
        var ns = Loop.NewRange(numbers.As(n => n * key));
        var sort = ns.ToArray();
        var zero = ns.First(n => n.Value == 0);

        foreach (var nr in Repeat(sort, times).SelectMany(n => n)) nr.Move(nr.Value);

        return zero.Skip(1000).Value + zero.Skip(2000).Value + zero.Skip(3000).Value;
    }
}
