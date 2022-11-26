namespace Advent_of_Code_2017;

[Category(Category.Simulation)]
public class Day_05
{
    [Example(answer: 5, "0, 3, 0, 1, -3")]
    [Puzzle(answer: 372671, year: 2017, day: 05)]
    public int part_one(string input) => Simulate(input);

    [Example(answer: 10, "0, 3, 0, 1, -3")]
    [Puzzle(answer: 25608480, year: 2017, day: 05)]
    public long part_two(string input) => Simulate(input, 3);

    private static int Simulate(string input, int threshold = int.MaxValue)
    {
        var jumbers = input.Int32s().ToArray();
        var index = 0; var turns = 0;

        while (index >= 0 && index < jumbers.Length)
        {
            var jump = jumbers[index];
            jumbers[index] += jump < threshold ? +1 : -1;
            index += jump;
            turns++;
        }
        return turns;
    }
}
