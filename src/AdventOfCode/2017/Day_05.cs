namespace Advent_of_Code_2017;

[Category(Category.Simulation)]
public class Day_05
{
    [Example(answer: 5, "0, 3, 0, 1, -3")]
    [Puzzle(answer: 372671, O.μs100)]
    public int part_one(Ints numbers) => Simulate(numbers);

    [Example(answer: 10, "0, 3, 0, 1, -3")]
    [Puzzle(answer: 25608480, O.ms10)]
    public int part_two(Ints numbers) => Simulate(numbers, 3);

    static int Simulate(Ints input, int threshold = int.MaxValue)
    {
        var numbers = input.Copy();
        var index = 0; var turns = 0;

        while (index.InRange(0, numbers.Length - 1))
        {
            var jump = numbers[index];
            numbers[index] += jump < threshold ? +1 : -1;
            index += jump;
            turns++;
        }
        return turns;
    }
}
