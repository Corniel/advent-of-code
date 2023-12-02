using SmartAss.Numerics;

namespace Advent_of_Code_2020;

[Category(Category.Simulation, Category.SequenceProgression)]
public class Day_15
{
    [Example(answer: 436, @"0,3,6")]
    [Puzzle(answer: 620, "0,12,6,13,20,1,17", O.μs)]
    public int part_one(Ints numbers) => MemoryGame(numbers, 2020);

    [Puzzle(answer: 110871, "0,12,6,13,20,1,17", O.ms100)]
    public int part_two(Ints numbers) => MemoryGame(numbers, 30000000);

    static int MemoryGame(Ints starting, int rounds)
    {
        var round = 1;
        var previous = 0;
        var recents = new int[rounds];
        var befores = new int[rounds];

        foreach (var n in starting)
        {
            recents[n] = round++;
            previous = n;
        }
        while (round <= rounds)
        {
            var before = befores[previous];
            var number = before == 0 ? 0 : round - before - 1;
            befores[number] = recents[number];
            recents[number] = round++;
            previous = number;
        }
        return previous;
    }
}
