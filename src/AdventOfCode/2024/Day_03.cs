namespace Advent_of_Code_2024;

[Category(Category.ExpressionParsing)]
public class Day_03
{
    [Example(answer: 161, "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))")]
    [Puzzle(answer: 183788984, O.μs100)]
    public int part_one(string str) => Parse(str, new(@"mul\(\d+,\d+\)"));

    [Example(answer: 48, "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))")]
    [Puzzle(answer: 62098619, O.μs100)]
    public int part_two(string str) => Parse(str, new(@"(mul\(\d+,\d+\)|do\(\)|don't\(\))"));

    static int Parse(string str, Regex pattern)
    {
        var sum = 0; var enable = true;

        foreach (Group m in pattern.Matches(str))
        {
            if (m.Value == "don't()") enable = false;
            else if (m.Value == "do()") enable = true;
            else if (enable) sum += m.Value.Int32s().Product();
        }
        return sum;
    }
}
