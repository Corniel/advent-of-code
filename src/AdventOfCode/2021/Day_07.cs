namespace Advent_of_Code_2021;

[Category(Category.Simulation)]
public class Day_07
{
    [Example(answer: 37, "16,1,2,0,4,2,7,1,2,14")]
    [Puzzle(answer: 336701, year: 2021, day: 07)]
    public int part_one(string input) => MinimumCosts(input.Int32s().ToArray(), n => n);

    [Example(answer: 168, "16,1,2,0,4,2,7,1,2,14")]
    [Puzzle(answer: 95167302, year: 2021, day: 07)]
    public int part_two(string input) => MinimumCosts(input.Int32s().ToArray(), n => n * (n + 1) / 2);

    private static int MinimumCosts(int[] crabs, Func<int, int> costs)
    {
        var optimum = int.MaxValue;
        var height = 0;
        while (height++ < int.MaxValue)
        {
            var fuel = crabs.Sum(crab => costs(Math.Abs(crab - height)));
            if (fuel > optimum) return optimum;
            else { optimum = fuel; }
        }
        throw new InfiniteLoop();
    }
}
