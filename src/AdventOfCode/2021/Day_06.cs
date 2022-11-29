namespace Advent_of_Code_2021;

[Category(Category.Simulation)]
public class Day_06
{
    [Example(answer: 5934, @"3,4,3,1,2")]
    [Puzzle(answer: 372300)]
    public long part_one(string input) => Simulate(input, 80);

    [Example(answer: 26984457539, @"3,4,3,1,2")]
    [Puzzle(answer: 1675781200288)]
    public long part_two(string input) => Simulate(input, 256);

    private static long Simulate(string input, int days)
    {
        var ages = new long[9];
        foreach (var age in input.Int32s())
        {
            ages[age] += 1;
        }
        for (var day = 0; day < days; day++)
        {
            ages[(day + 7) % 9] += ages[day % 9];
        }
        return ages.Sum();
    }
}
