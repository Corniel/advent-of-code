namespace Advent_of_Code_2021;

[Category(Category.Simulation)]
public class Day_07
{
    [Example(answer: 37, "16,1,2,0,4,2,7,1,2,14")]
    [Puzzle(answer: 336701, O.μs100)]
    public int part_one(Ints numbers) => MinimumCosts(numbers, n => n);

    [Example(answer: 168, "16,1,2,0,4,2,7,1,2,14")]
    [Puzzle(answer: 95167302, O.μs100)]
    public int part_two(Ints numbers) => MinimumCosts(numbers, n => n * (n + 1) / 2);

    static int MinimumCosts(Ints crabs, Func<int, int> costs)
    {
        var optimum = int.MaxValue;
        var height = 0;
        while (height++ < int.MaxValue)
        {
            var fuel = crabs.Sum(crab => costs((crab - height).Abs()));
            if (fuel > optimum) return optimum;
            else { optimum = fuel; }
        }
        throw new InfiniteLoop();
    }
}
