namespace Advent_of_Code_2015;

[Category(Category.ExpressionParsing)]
public class Day_01
{
    [Example(answer: 3, "))(((((")]
    [Puzzle(answer: 232, O.ns100)]
    public int part_one(string str) => str.Count(c => c == '(') * 2 - str.Length;

    [Example(answer: 5, "()())")]
    [Puzzle(answer: 1783, O.ns100)]
    public int part_two(string str)
    {
        var level = 0;
        for (var i = 0; i < str.Length; i++)
        {
            level += str[i] is '(' ? 1 : -1;
            if (level == -1) return i + 1;
        }
        return -1;
    }
}
