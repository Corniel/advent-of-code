namespace Advent_of_Code_2021;

[Category(Category.Simulation)]
public class Day_06
{
    [Example(answer: 5934, @"3,4,3,1,2")]
    [Puzzle(answer: 372300, O.μs)]
    public long part_one(Ints numbers) => Simulate(numbers, 80);

    [Example(answer: 26984457539, @"3,4,3,1,2")]
    [Puzzle(answer: 1675781200288, O.μs)]
    public long part_two(Ints numbers) => Simulate(numbers, 256);

    static long Simulate(Ints numbers, int days)
    {
        var ages = new long[9];
        foreach (var age in numbers) ages[age] += 1;
        for (var day = 0; day < days; day++) ages[(day + 7) % 9] += ages[day % 9];
        return ages.Sum();
    }
}
