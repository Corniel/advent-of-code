namespace Advent_of_Code_2019;

[Category(Category.IntComputer)]
public class Day_02
{
    [Puzzle(answer: 5110675, year: 2019, day: 02)]
    public Int part_one(string input)
        => Computer.Parse(input)
            .Update(1, 12)
            .Update(2, 2)
            .Run()
            .Answer;

    [Puzzle(answer: 4847, year: 2019, day: 02)]
    public int part_two(string input)
    {
        var program = Computer.Parse(input);
        for (var noun = 0; noun < 100; noun++)
        {
            for (var verb = 0; verb < 100; verb++)
            {
                var results = program
                    .Copy()
                    .Update(1, noun)
                    .Update(2, verb)
                    .Run();

                if (results.Answer == 19690720)
                {
                    return noun * 100 + verb;
                }
            }
        }
        throw new NoAnswer();
    }
}
