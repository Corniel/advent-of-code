using System.Diagnostics.CodeAnalysis;

namespace Advent_of_Code_2023;

[Category(Category.Permutations)]
public class Day_12
{
    [Example(answer: 7, "?.???? 1,1")]
    [Example(answer: 1, "??#.# 2,1")]
    [Example(answer: 1, "#.# 1,1")]
    [Example(answer: 3, "???? 1,1")]
    [Example(answer: 1, "??? 1,1")]
    [Example(answer: 4, "???? 1")]
    [Example(answer: 3, "???? 2")]
    [Example(answer: 1, "???.### 1,1,3")]
    [Example(answer: 2, "????.#...#... 3,1,1")]
    [Example(answer: 1, "????.#...#... 4,1,1")]
    [Example(answer: 8, "#??.???..? 1,1,1")]
    [Example(answer: 4, "?#?.?#? 2,2")]
    [Example(answer: 10, "??????? 2,1")]
    [Example(answer: 11899700525790, "??????????????????????????????????????????????????????????????????????????????? 1,3,1,1,3,1,1,3,1,1,3,1,1,3,1")]
    [Example(answer: 10, "?###???????? 3,2,1")]
    [Example(answer: 4, "????.######..#####. 1,6,5")]
    [Example(answer: 1, ".#?????..???????.? 6,7")]
    [Puzzle(answer: 7541L, O.ms)]
    public long part_one(Lines lines) => lines.As(l => Block.Parse(l, 1)).Sum(b => b.Permutations);

    [Example(answer: 1, "???.### 1,1,3")]
    [Example(answer: 16, "????.#...#... 4,1,1")]
    [Example(answer: 16384, ".??..??...?##. 1,1,3")]
    [Example(answer: 506250, "?###???????? 3,2,1")]
    [Puzzle(answer: 17485169859432, O.s100)]
    public long part_two(Lines lines) => lines.As(l => Block.Parse(l, 5)).Sum(b => b.Permutations);

    record Block(string[] Data, int[] Blocks)
    {
        readonly Dictionary<Hash, long> Cache = [];

        public long Permutations => Next(Data, Blocks);

        long Next(string[] datas, int[] blocks)
        {
            if (datas.Sum(d => d.Length) < blocks.Sum()) return 0;

            var data = datas[0];

            if (datas.Length == 1) return Next(data, blocks);

            var count = 0L;

            for (var l = 0; l <= blocks.Length; l++)
            {
                var n = Next(data, blocks[..l]);
                if (n > 0) count += n * Next(datas[1..], blocks[l..]);
            }
            return count;
        }

        long Next(string s, int[] blocks)
        {
            var hash = new Hash(s, blocks);

            if (Cache.TryGetValue(hash, out var n)) return n;

            if (blocks.Length == 0)
            {
                return (s.Length == 0 || s.All(c => c == '?')) ? 1 : 0;
            }
            if (s.Length == 0) return 0;

            var len = blocks[0];

            // One fit.
            if (blocks.Length == 1 && s.Length == len) return 1;

            var min = blocks.Sum() + blocks.Length - 1;
            // No fit, not enough chars left.
            if (s.Length < min) return 0;

            return Cache[hash] = s.All(c => c == '?') 
                ? Blanks(s, blocks)
                : NoneBlank(s, blocks, len, min);
        }

        long NoneBlank(string s, int[] blocks, int len, int min) => 0L
            // Assume '.'
            + (s[0] == '?' && s.Length > min ? Next(s[1..], blocks) : 0)
            // Split due as s[len] assumed '.'
            + (s[len] != '#'? Next(s[(len + 1)..], blocks[1..]) : 0);

        static long Blanks(string s, int[] blocks)
        {
            var stars = s.Length - blocks.Sum();

            if (stars + 1 >= blocks.Length)
            {
                return Maths.Combination(stars + 1, blocks.Length);
            }
            var bars = blocks.LongLength + 1;

            return Maths.Combination(bars, stars);
        }

        public static Block Parse(string line, int rep = 1) => new(
            string.Join('?', Repeat(line.Split(' ')[0], rep)).Separate('.'), 
            Repeat(line.Int32s(), rep).SelectMany(n => n).ToArray());
    }

    readonly struct Hash(string pattern, int[] blocks) : IEquatable<Hash>
    {
        readonly string Pattern = pattern; readonly int[] Blocks = blocks;

        public override bool Equals([NotNullWhen(true)] object obj) => obj is Hash hash && Equals(hash);

        public bool Equals(Hash other) => Pattern == other.Pattern && Enumerable.SequenceEqual(Blocks, other.Blocks);

        public override int GetHashCode() => Qowaiv.Hashing.Hash.Code(pattern).And(blocks);
    }
}
