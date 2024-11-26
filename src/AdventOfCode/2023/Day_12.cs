namespace Advent_of_Code_2023;

[Category(Category.Permutations)]
public class Day_12
{
    [Example(answer: 7, "?.???? 1,1")]
    [Example(answer: 1, "??#.# 2,1")]
    [Example(answer: 1, "???.### 1,1,3")]
    [Example(answer: 2, "????.#...#... 3,1,1")]
    [Example(answer: 8, "#??.???..? 1,1,1")]
    [Example(answer: 4, "?#?.?#? 2,2")]
    [Example(answer: 11899700525790, "??????????????????????????????????????????????????????????????????????????????? 1,3,1,1,3,1,1,3,1,1,3,1,1,3,1")]
    [Example(answer: 10, "?###???????? 3,2,1")]
    [Example(answer: 4, "????.######..#####. 1,6,5")]
    [Puzzle(answer: 7541L, O.ms)]
    public long part_one(Lines lines) => lines.As(l => Condition.Parse(l, 1)).Sum(c => c.Arrangements());

    [Example(answer: 1, "???.### 1,1,3")]
    [Example(answer: 16, "????.#...#... 4,1,1")]
    [Example(answer: 16384, ".??..??...?##. 1,1,3")]
    [Example(answer: 506250, "?###???????? 3,2,1")]
    [Puzzle(answer: 17485169859432, O.ms10)]
    public long part_two(Lines lines) => lines.As(l => Condition.Parse(l, 5)).Sum(c => c.Arrangements());

    record Condition(string Pattern, int[] Frequencies)
    {
        public static Condition Parse(string line, int rep) => new(
            string.Join('?', Repeat(Trim(line), rep)),
            Repeat(line.Int32s(), rep).SelectMany(n => n).ToArray());

        // This replacement is a modification of @Renzo Baasdam's. Mine was
        // correct, but did not scale, the part two variant took 2 minutes
        // instead of the current 10 ms.
        public long Arrangements()
        {
            if (Pattern.All(c => c == '?')) return Blanks();

            var curr = new States();
            var next = new States();

            // Start with on arrangement.
            curr.Init(Frequencies);
            curr[0].Add(idx: 0, 1);

            foreach (var ch in Pattern)
            {
                next.Init(Frequencies);

                switch (ch)
                {
                    case '.': curr.Dot(next); break;
                    case '#': curr.Hash(next); break;
                    default: curr.Dot(next); curr.Hash(next); break; // ?
                }
                (curr, next) = (next, curr);
            }
            return curr[^2..].Sum(s => s.Last);
        }

        // This is part of my old implementation; it uses some combination theory
        // and is even (just slightly) faster then the fall back)
        long Blanks()
        {
            var stars = Pattern.Length - Frequencies.Sum();
            return stars + 1 >= Frequencies.Length
                ? Maths.Choose(stars + 1, Frequencies.Length)
                : Maths.Choose(Frequencies.Length + 1L, stars);
        }

        static string Trim(string line) => line.Split(' ')[0].RegReplace("\\.{2,}", ".");
    }

    sealed class States : List<State>
    {
        public void Init(int[] frequencies)
        {
            Clear();
            AddRange(frequencies.Select(f => new State(f + 1)));
            Add(new(1));
        }

        public void Dot(States next)
        {
            next[0].Add(idx: 0, this[0].First);

            for (var st = 1; st < Count; st++)
            {
                next[st].Add(idx: 0, this[st].First + this[st - 1].Last);
            }
        }

        public void Hash(States next)
        {
            for (var st = 0; st < Count; st++)
            {
                for (var freq = 0; freq < this[st].Count - 1; freq++)
                {
                    next[st].Add(idx: freq + 1, this[st][freq]);
                }
            }
        }
    }

    readonly struct State(int length)
    {
        private readonly long[] Counts = new long[length];

        public long this[int index] => Counts[index];

        public int Count => Counts.Length;

        public void Add(int idx, long val) => Counts[idx] += val;

        public long First => this[0];

        public long Last => this[^1];

        public override string ToString() => string.Join(", ", Counts);
    }
}
