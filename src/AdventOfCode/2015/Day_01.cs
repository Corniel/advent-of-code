namespace Advent_of_Code_2015;

[Category(Category.ExpressionParsing)]
public class Day_01
{
    [Example(answer: 3, "))(((((")]
    [Puzzle(answer: 232, year: 2015, day: 01)]
    public int part_one(string input) => input.Sum(ch => ch == '(' ? 1 : -1);

    [Example(answer: 5, "()())")]
    [Puzzle(answer: 1783, year: 2015, day: 01)]
    public int part_two(string input)
    {
        var level = 0;
        for(var pos = 0; pos < input.Length; pos++)
        {
            level += input[pos] == '(' ? 1 : -1;
            if (level == -1) return pos + 1;
        }
        throw new NoAnswer();
    }
}
