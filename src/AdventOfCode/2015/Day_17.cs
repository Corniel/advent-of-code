namespace Advent_of_Code_2015;

[Category(Category.Computation, Category.BitManupilation)]
public class Day_17
{
    [Puzzle(answer: 1304)]
    public int part_one(string input)
    {
        var numbers = input.Int32s().OrderByDescending(n => n).ToArray();
        return Enumerable.Range(1, (1 << numbers.Length) - 2).Count(bits => Fits((uint)bits, numbers));
    }

    [Puzzle(answer: 18)]
    public int part_two(string input)
    {
        var numbers = input.Int32s().OrderByDescending(n => n).ToArray();
        var min = int.MaxValue;
        var count = 0;
        foreach (var containers in Enumerable.Range(1, (1 << numbers.Length) - 2)
            .Where(bits => Fits((uint)bits, numbers))
            .Select(bits => Bits.UInt32.Count((uint)bits)))
        {
            if (containers < min)
            {
                min = containers;
                count = 1;
            }
            else
            {
                count += containers == min ? 1 : 0;
            }
        }
        return count;
    }

    static bool Fits(uint distribution, int[] numbers)
    {
        var sum = 150;
        var remainder = distribution;
        for (var i = 0; i < numbers.Length; i++)
        {
            sum -= (remainder & 1) == 1 ? numbers[i] : 0;
            remainder >>= 1;
            if (sum <= 0) { break; }
        }
        return sum == 0 && remainder == 0;
    }
}
