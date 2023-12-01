namespace Advent_of_Code_2017;

[Category(Category.BitManupilation)]
public class Day_15
{
    [Example(answer: 588, "Generator A starts with 65;Generator B starts with 8921")]
    [Puzzle(answer: 597, "Generator A starts with 516;Generator B starts with 190", O.ms100)]
    public int part_one(Lines input) => Match(input, 40_000_000, n => true, n => true);

    [Example(answer: 309, "Generator A starts with 65;Generator B starts with 8921")]
    [Puzzle(answer: 303, "Generator A starts with 516;Generator B starts with 190", O.ms100)]
    public int part_two(Lines input) => Match(input, 5_000_000, n => (n & 3) == 0, n => (n & 7) == 0);

    private static int Match(Lines input, int pairs, Predicate<long> ca, Predicate<long> cb)
    {
        var numbers = input.Int32s().ToArray();
        var ag = new Generator(numbers[0], 16807, ca);
        var bg = new Generator(numbers[1], 48271, cb);
        var match = 0;

        for (var i = 0; i < pairs; i++)
        {
            ag.MoveNext(); bg.MoveNext();
            match += (ag.Current & 0xFFFF) == (bg.Current & 0xFFFF) ? 1 : 0;
        }
        return match;
    }

    struct Generator(long value, int factor, Predicate<long> condition) : Iterator<long>
    {
        private readonly int Factor = factor;
        private readonly Predicate<long> Condition = condition;

        public long Current { get; private set; } = value;

        public bool MoveNext()
        {
            do { Current = Current * Factor % 0x7FFF_FFFF; }
            while (!Condition(Current));
            return true;
        }
        public void Dispose() => Do.Nothing();
        public void Reset() => Do.Nothing();
    }
}
