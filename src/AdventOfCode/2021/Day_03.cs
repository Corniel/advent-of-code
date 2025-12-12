namespace Advent_of_Code_2021;

[Category(Category.BitManupilation)]
public class Day_03
{
    [Example(answer: 198, "00100;11110;10110;10111;10101;01111;00111;11100;10000;11001;00010;01010")]
    [Puzzle(answer: 845186UL, O.μs100)]
    public ulong part_one(Lines lines)
    {
        var numbers = lines.Fix(BinaryNumber.Parse);
        var size = numbers[0].Size;
        var threshold = numbers.Length / 2;
        var gamma = BinaryNumber.Empty(size);
        var epsilon = BinaryNumber.Empty(size);

        for (var position = 0; position < size; position++)
        {
            var ones = numbers.Count(line => line.HasFlag(position));
            if (ones > threshold)
            {
                gamma = gamma.Flag(position);
            }
            else
            {
                epsilon = epsilon.Flag(position);
            }
        }
        return gamma.Value * epsilon.Value;
    }

    [Example(answer: 230, "00100;11110;10110;10111;10101;01111;00111;11100;10000;11001;00010;01010")]
    [Puzzle(answer: 4636702UL, O.μs100)]
    public ulong part_two(Lines lines)
    {
        var numbers = lines.Fix(BinaryNumber.Parse);
        var oxygen = Select(numbers, true, numbers[0].Size);
        var co2 = Select(numbers, false, numbers[0].Size);
        return oxygen.Value * co2.Value;
    }

    static BinaryNumber Select(ImmutableArray<BinaryNumber> numbers, bool preferOne, int position)
    {
        position--;
        if (numbers.Length == 1) return numbers[0];
        else
        {
            var ones = numbers.Where(number => number.HasFlag(position)).Fix();
            var zero = numbers.Where(number => !number.HasFlag(position)).Fix();
            var keepOnes = preferOne ? ones.Length >= zero.Length : ones.Length < zero.Length;
            return Select(keepOnes ? ones : zero, preferOne, position);
        }
    }
}
