namespace Advent_of_Code_2017;

[Category(Category.SequenceProgression)]
public class Day_17
{
    [Example(answer: 638, 3)]
    [Puzzle(answer: 1025, 366, O.ms)]
    public int part_one(int input)
    {
        var current = Loop.New(0);
        for (var value = 1; value <= 2017; value++)
        {
            current = current.Skip(input).InsertAfter(value);
        }
        return current.Next.Value;
    }

    [Puzzle(answer: 37_803_463, 366, O.ms100)]
    public int part_two(int input)
    {
        var length = 1; var index = 0; var next = 0;
        for (var value = 1; value <= 50_000_000; value++)
        {
            index = ((index + input) % length++) + 1;
            next = index == 1 ? value : next;
        }
        return next;
    }
}
