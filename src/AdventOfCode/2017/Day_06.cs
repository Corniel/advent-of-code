namespace Advent_of_Code_2017;

[Category(Category.SequenceProgression)]
public class Day_06
{
    [Example(answer: 5, "0 2 7 0")]
    [Puzzle(answer: 6681, "4 1 15 12 0 9 9 5 5 8 7 3 14 5 12 3", O.ms)]
    public int part_one(Ints numbers)
    {
        var banks = new Banks(numbers.ToArray(n => (byte)n));
        var set = new HashSet<Banks>();
        while (set.Add(banks)) banks = banks.Next();
        return set.Count;
    }

    [Example(answer: 4, "0 2 7 0")]
    [Puzzle(answer: 2392, "4 1 15 12 0 9 9 5 5 8 7 3 14 5 12 3", O.ms)]
    public int part_two(Ints numbers)
    {
        var banks = new Banks(numbers.ToArray(n => (byte)n));
        var set = new Dictionary<Banks, int>();
        while (set.TryAdd(banks, set.Count)) banks = banks.Next();
        return set.Count - set[banks];
    }

    readonly struct Banks(byte[] numbers) : IEquatable<Banks>
    {
        readonly byte[] Numbers = numbers;

        public Banks Next()
        {
            byte[] next = [.. Numbers];
            var max = next[^1];
            var index = next.Length - 1;
            for (var i = next.Length - 2; i >= 0; i--)
            {
                var val = next[i];
                if (val >= max) { index = i; max = val; }
            }
            next[index] = 0;
            while (--max != 255)
            {
                index = (index + 1) % next.Length;
                next[index]++;
            }
            return new(next);
        }

        public bool Equals(Banks other) => Numbers.SequenceEqual(other.Numbers);

        public override int GetHashCode()  => Numbers[0] | (Numbers[1] << 8) | (Numbers[2] << 16) | (Numbers[3] << 24);
    }
}
