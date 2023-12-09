namespace Advent_of_Code_2023;

[Category(Category.SequenceProgression)]
public class Day_09
{
    [Example(answer: 18, "0 3 6 9 12 15")]
    [Example(answer: 28, "1 3 6 10 15 21")]
    [Example(answer: 114, "0 3 6 9 12 15;1 3 6 10 15 21;10 13 16 21 30 45")]
    [Puzzle(answer: 1479011877, O.μs100)]
    public int part_one(Lines lines) => lines.As(line => line.Int32s().ToList()).Select(One).Sum();

    [Example(answer: 5, "10  13  16  21  30  45")]
    [Puzzle(answer: 973, O.μs100)]
    public int part_two(Lines lines) => lines.As(line => line.Int32s().ToList()).Select(Two).Sum();

    public int One(List<int> numbers) => numbers.Exists(n => n != 0)
        ? numbers[^1] + One(numbers.SelectWithPrevious().Select(p => p.Delta()).ToList()) : 0;

    public int Two(List<int> numbers) => numbers.Exists(n => n != 0)
        ? numbers[0] - Two(numbers.SelectWithPrevious().Select(p => p.Delta()).ToList()) : 0;
}
