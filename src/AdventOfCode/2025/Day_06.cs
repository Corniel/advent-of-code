namespace Advent_of_Code_2025;

/// <summary>
/// There is a sheet of arithmetic tasks that has to be completed.
///
/// Part one: Sum all results read numbers from left to right.
/// Part two: Sum all resullts read top to bottom.
/// </summary>
[Category(Category.ExpressionEvaluation)]
public class Day_06
{
    [Example(answer: 4277556, Example._1)]
    [Puzzle(answer: 6209956042374, O.μs10)]
    public long part_one(Lines lines)
    {
        var tab = lines[..^1].Select(l => l.Int64s().ToArray()).ToArray();
        return Op(lines[^1]).Select((op, col) => Exe(op, tab.Select(v => v[col]))).Sum();
    }

    [Example(answer: 3263827, Example._1)]
    [Puzzle(answer: 12608160008022, O.μs10)]
    public long part_two(string str)
    {
        // To keep white space.
        var lines = str.Lines(default);
        
        var total = 0L; var rows = lines.Count - 1; var i = 0;
        var ops = Op(lines[^1]);
        List<long> nums = [];
       
        for (var col = 0; col < lines[0].Length; col++)
        {
            var num = 0;

            // read top top bottom and skip non-digits
            for (var r = 0; r < rows; r++)
                if (lines[r][col].TryDigit() is { } d) num = num * 10 + d;

            if (num is 0)
            {
                total += Exe(ops[i++], nums);
                nums.Clear();
            }
            else nums.Add(num);
        }
        return total + Exe(ops[i], nums);
    }

    static long Exe(char op, IEnumerable<long> vs) => op == '+' ? vs.Sum() : vs.Product();
    static char[] Op(string s) => [..s.Where(c => c != ' ')];
}
