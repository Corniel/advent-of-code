namespace Advent_of_Code_2020;

[Category(Category.Cryptography)]
public class Day_02
{
    [Example(answer: 2, "1-3 a: abcde;1-3 b: cdefg;2-9 c: ccccccccc")]
    [Puzzle(answer: 536, O.μs)]
    public int part_one(Inputs<Pass> pass) => pass.Count(p => p.Policy.One(p.Chars));

    [Example(answer: 1, "1-3 a: abcde;1-3 b: cdefg;2-9 c: ccccccccc")]
    [Puzzle(answer: 558, O.μs)]
    public int part_two(Inputs<Pass> pass) => pass.Count(p => p.Policy.Two(p.Chars));

    public record struct Policy(int Min, int Max, char Char)
    {
        public bool One(string str)
        {
            var policy = this;
            return str.Count(policy.Char).InRange(Min, Max);
        }
        public bool Two(string str) => (str[Min - 1] == Char) ^ (str[Max - 1] == Char);
    }
    public record struct Pass(string Chars, Policy Policy)
    {
        public static Pass Parse(string str)
        {
            var split = str.Split(':');
            var pol = split[0].Split('-', ' ');
            var chars = split[1].Trim();
            return new(chars, new(pol[0].Int32(), pol[1].Int32(), pol[2].Char()));
        }
    }
}
