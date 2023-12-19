namespace Advent_of_Code_2020;

[Category(Category.SequenceProgression)]
public class Day_01
{
    [Example(answer: 514579, "1721, 979, 366, 299, 675, 1456")]
    [Puzzle(answer: 786811, O.μs)]
    public int part_one(Ints input)
    {
        var numbers = new UniqueNumbers(input);
        var n = numbers.Range(max: 2020 / 2).First(n => numbers.Contains(2020 - n));
        return n * (2020 - n);
    }

    [Example(answer: 241861950L, "1721, 979, 366, 299, 675, 1456")]
    [Puzzle(answer: 199068980L, O.μs)]
    public long part_two(Ints input)
    {
        var numbers = new UniqueNumbers(input);
        foreach (var n0 in numbers.Range(max: 2020 / 3))
        {
            if (numbers.Range(min: n0 + 1, max: (2020 - n0) / 2).FirstOrNone(n => numbers.Contains(2020 - n0 - n)) is { } n1)
                return 1L * n0 * n1 * (2020 - n0 - n1);
        }
        throw new NoAnswer();
    }
}
