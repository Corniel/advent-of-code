namespace Advent_of_Code_2015;

[Category(Category.Computation, Category.BitManupilation)]
public class Day_17
{
    [Puzzle(answer: 1304, O.ms10)]
    public int part_one(Ints numbers)
    {
        var ns = numbers.OrderDescending().ToArray();
        return Range(1, (1 << ns.Length) - 2).Count(bits => Fits((uint)bits, ns));
    }

    [Puzzle(answer: 18, O.ms10)]
    public int part_two(Ints numbers)
    {
        var ns = numbers.OrderDescending().ToArray();
        var min = int.MaxValue;
        var count = 0;
        foreach (var containers in Range(1, (1 << ns.Length) - 2)
            .Where(bits => Fits((uint)bits, ns))
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
