namespace Advent_of_Code_2015;

public class Day_02
{
    [Example(answer: 58, "2x3x4")]
    [Example(answer: 43, "1x1x10")]
    [Puzzle(answer: 1586300, year: 2015, day: 02)]
    public int part_one(string input) => input.Lines(WrappingPaper).Sum();

    private static int WrappingPaper(string line)
    {
        var pars = line.Int32s().ToArray();
        var area0 = pars[0] * pars[1];
        var area1 = pars[0] * pars[2];
        var area2 = pars[1] * pars[2];
        return 2 * (area0 + area1 + area2) + Math.Min(area0, Math.Min(area1, area2));
    }

    [Example(answer: 34, "2x3x4")]
    [Example(answer: 14, "1x1x10")]
    [Puzzle(answer: 3737498, year: 2015, day: 02)]
    public int part_two(string input) => input.Lines(WrappingRibbon).Sum();

    private static int WrappingRibbon(string line)
    {
        var pars = line.Int32s().ToArray();
        var bow = pars[0] * pars[1] * pars[2];
        return pars.OrderBy(p => p).Take(2).Sum() * 2 + bow;
  
    }
}
