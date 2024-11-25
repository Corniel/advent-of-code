namespace Advent_of_Code_2017;

[Category(Category.SequenceProgression)]
public class Day_06
{
    [Example(answer: 5, "0 2 7 0")]
    [Puzzle(answer: 6681, "4 1 15 12 0 9 9 5 5 8 7 3 14 5 12 3", O.μs100)]
    public int part_one(Ints numbers)
    {
        var banks = new Banks(numbers.As(n => (byte)n).ToArray());
        var set = new HashSet<Banks>();
        while (set.Add(banks)) { banks = banks.Next(); }
        return set.Count;
    }

    [Example(answer: 4, "0 2 7 0")]
    [Puzzle(answer: 2392, "4 1 15 12 0 9 9 5 5 8 7 3 14 5 12 3", O.μs100)]
    public int part_two(Ints numbers)
    {
        var banks = new Banks(numbers.As(n => (byte)n).ToArray());
        var set = new Dictionary<Banks, int>();
        while (set.TryAdd(banks, set.Count)) { banks = banks.Next(); }
        return set.Count - set[banks];
    }

    private readonly record struct Banks(byte[] Numbers)
    {
        public Banks Next()
        {
            var numbers = Numbers.ToArray();
            var max = numbers[^1];
            var index = numbers.Length - 1;
            for (var i = numbers.Length - 2; i >= 0; i--)
            {
                var val = numbers[i];
                if (val >= max) { index = i; max = val; }
            }
            numbers[index] = 0;
            while (--max != 255)
            {
                index = (index + 1) % numbers.Length;
                numbers[index]++;
            }
            return new(numbers);
        }
    }
}
