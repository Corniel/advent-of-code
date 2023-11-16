namespace Advent_of_Code_2020;

[Category(Category.SequenceProgression)]
public class Day_01
{
    [Example(answer: 514579, "1721, 979, 366, 299, 675, 1456")]
    [Puzzle(answer: 786811, O.μs)]
    public int part_one(string input)
    {
        const int sum = 2020;
        var numbers = UniqueNumbers.Parse(input);

        foreach (var number0 in numbers.Range(max: sum / 2))
        {
            var number1 = sum - number0;
            if (numbers.Contains(number1))
            {
                return number0 * number1;
            }
        }
        throw new NoAnswer();
    }

    [Example(answer: 241861950L, "1721, 979, 366, 299, 675, 1456")]
    [Puzzle(answer: 199068980L, O.μs)]
    public long part_two(string input)
    {
        const int sum = 2020;
        var numbers = UniqueNumbers.Parse(input);

        foreach (var number0 in numbers.Range(max: sum / 3))
        {
            foreach (var number1 in numbers.Range(min: number0 + 1, max: (sum - number0) / 2))
            {
                var number2 = sum - number0 - number1;
                if (numbers.Contains(number2))
                {
                    return (long)number0 * (long)number1 * (long)number2;
                }
            }
        }
        throw new NoAnswer();
    }
}
