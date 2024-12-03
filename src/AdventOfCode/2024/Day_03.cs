namespace Advent_of_Code_2024;

[Category(Category.ExpressionParsing)]
public partial class Day_03
{
    [Example(answer: 161, "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))")]
    [Puzzle(answer: 183788984, O.μs10)]
    public int part_one(string str) => Parse(str, true);

    [Example(answer: 48, "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))")]
    [Puzzle(answer: 62098619, O.μs10)]
    public int part_two(string str) => Parse(str, false);

    static int Parse(string str, bool dont)
    {
        var sum = 0; var enable = true;

        foreach (var m in Reg().Matches(str).Select(m => m.Value))
        {
            if (m == "don't()") enable = dont;
            else if (m == "do()") enable = true;
            else if (enable) sum += m.Int32s().Product();
        }
        return sum;
    }

    [GeneratedRegex(@"(mul\([0-9]{1,3},[0-9]{1,3}\)|do\(\)|don't\(\))", RegexOptions.ExplicitCapture)]
    private static partial Regex Reg();
}
