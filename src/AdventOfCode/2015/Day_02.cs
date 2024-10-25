namespace Advent_of_Code_2015;

[Category(Category.ExpressionParsing)]
public class Day_02
{
    [Example(answer: 58, "2x3x4")]
    [Example(answer: 43, "1x1x10")]
    [Puzzle(answer: 1586300, O.μs10)]
    public int part_one(Lines lines) => lines.As(WrappingPaper).Sum();

    [Example(answer: 34, "2x3x4")]
    [Example(answer: 14, "1x1x10")]
    [Puzzle(answer: 3737498, O.μs100)]
    public int part_two(Lines lines) => lines.As(WrappingRibbon).Sum();

    static int WrappingPaper(string line)
    {
        var pars = line.Int32s().ToArray();
        int[] areas = [pars[0] * pars[1], pars[0] * pars[2], pars[1] * pars[2]];
        return 2 * areas.Sum() + areas.Min();
    }

    static int WrappingRibbon(string line)
    {
        var pars = line.Int32s().ToArray();
        var bow = pars[0] * pars[1] * pars[2];
        return pars.OrderBy(p => p).Take(2).Sum() * 2 + bow;
  
    }
}
