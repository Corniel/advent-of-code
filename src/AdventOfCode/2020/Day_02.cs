namespace Advent_of_Code_2020;

[Category(Category.Cryptography)]
public class Day_02
{
    [Example(answer: 2, "1-3 a: abcde;1-3 b: cdefg;2-9 c: ccccccccc")]
    [Puzzle(answer: 536, O.μs100)]
    public int part_one(Lines lines) => lines.As(Password.Parse).Count(p => p.Policy.ValidForOne(p.Chars));

    [Example(answer: 1, "1-3 a: abcde;1-3 b: cdefg;2-9 c: ccccccccc")]
    [Puzzle(answer: 558, O.μs100)]
    public int part_two(Lines lines) => lines.As(Password.Parse).Count(p => p.Policy.ValidForTwo(p.Chars));

    record PasswordPolicy(int Min, int Max, char Char)
    {
        public bool ValidForOne(string str)
        {
            var policy = this;
            return str.Count(policy.Char).InRange(Min, Max);
        }
        public bool ValidForTwo(string str) => (str[Min - 1] == Char) ^ (str[Max - 1] == Char);
    }
    record Password(string Chars, PasswordPolicy Policy)
    {
        public static Password Parse(string str)
        {
            var split = str.Split(':');
            var pol = split[0].Split('-', ' ');
            var chars = split[1].Trim();
            return new Password(chars,
                new PasswordPolicy(
                    Min: pol[0].Int32(),
                    Max: pol[1].Int32(),
                    Char: pol[2].Char()));
        }
    }
}
