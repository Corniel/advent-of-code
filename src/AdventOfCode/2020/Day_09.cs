namespace Advent_of_Code_2020;

[Category(Category.Simulation)]
public class Day_09
{
    [Puzzle(answer: 144381670, O.μs10)]
    public int part_one(Ints numbers)
    {
        for (var i = 25; i < numbers.Length; i++)
        {
            var number = numbers[i];
            if (!Matches(numbers, i, number))
            {
                return number;
            }
        }
        throw new NoAnswer();
    }

    [Puzzle(answer: 20532569, O.ns100)]
    public int part_two(Ints numbers)
    {
        var sum = 144381670;

        var lo = 0;
        var hi = 1;
        var bottom = numbers[lo];
        var top = numbers[hi];
        var test = bottom + top;

        while (hi < numbers.Length)
        {
            if (test == sum)
            {
                var min = numbers[lo..hi].Min();
                var max = numbers[lo..hi].Max();
                return min + max;
            }
            else if (test < sum)
            {
                top = numbers[++hi];
                test += top;
            }
            else
            {
                test -= bottom;
                bottom = numbers[++lo];
            }
        }
        throw new NoAnswer();
    }

    static bool Matches(Ints numbers, int index, long number)
    {
        for (var p0 = index - 25; p0 < index; p0++)
        {
            var n0 = numbers[p0];

            for (var p1 = p0 + 1; p1 < index; p1++)
            {
                var n1 = numbers[p1];
                if (n0 + n1 == number)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
