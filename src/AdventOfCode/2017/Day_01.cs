namespace Advent_of_Code_2017;

[Category(Category.SequenceProgression)]
public class Day_01
{
    [Example(answer: 3, "1122")]
    [Example(answer: 4, "1111")]
    [Example(answer: 0, "1234")]
    [Example(answer: 9, "91212129")]
    [Puzzle(answer: 1141, year: 2017, day: 01)]
    public int part_one(string input)
        => (input + input[0]).Digits().SelectWithPrevious().Where(pair => pair.Unchanged()).Sum(pair => pair.Current);

    [Example(answer: 6, "1212")]
    [Example(answer: 0, "1221")]
    [Example(answer: 4, "123425")]
    [Example(answer: 12, "123123")]
    [Example(answer: 4, "12131415")]
    [Puzzle(answer: 950, year: 2017, day: 01)]
    public int part_two(string input)
    {
        var digits = input.Digits().ToArray();
        var half = digits.Length / 2;
        return digits.Where((digit, index) => digits[(index + half) % digits.Length] == digit).Sum();
    }
}
