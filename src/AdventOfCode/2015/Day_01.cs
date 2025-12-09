namespace Advent_of_Code_2015;

[Category(Category.ExpressionParsing)]
public class Day_01
{
    [Example(answer: 3, "))(((((")]
    [Puzzle(answer: 232, O.ns100)]
    public int part_one(string str) => str.Sum(ch => ch == '(' ? 1 : -1);

    [Example(answer: 5, "()())")]
    [Puzzle(answer: 1783, O.μs)]
    public int part_two(string str)
    {
        var level = 0;
        for (var pos = 0; pos < str.Length; pos++)
        {
            level += str[pos] == '(' ? 1 : -1;
            if (level == -1) return pos + 1;
        }
        throw new NoAnswer();
    }
}
