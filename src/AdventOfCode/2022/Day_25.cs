namespace Advent_of_Code_2022;

[Category(Category.ExpressionParsing)]
public class Day_25
{
    [Example(answer: "1=-0-2", "1=-0-2")]
    [Puzzle(answer: "2=222-2---22=1=--1-2", O.μs)]
    public string part_one(Lines lines) => SNAFU(lines.As(Number).Sum());

    [Puzzle(answer: "You only need 49 stars to boost it", "You only need 49 stars to boost it")]
    public string part_two(string str) => str;

    static long Number(string str)
    {
        long n = 0;
        foreach (var ch in str) n = n * 5 + "=-012".IndexOf(ch) - 2;
        return n;
    }

    public static string SNAFU(long n)
    {
        var sb = new StringBuilder();
        while (n > 0)
        {
            n += 2;
            sb.Insert(0, "=-012"[(int)(n % 5)]);
            n /= 5;
        }
        return sb.ToString();
    }
}
