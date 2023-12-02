namespace Advent_of_Code_2019;

[Category(Category.IntComputer)]
public class Day_02
{
    [Puzzle(answer: 5110675, O.μs10)]
    public Int part_one(string str)
        => Computer.Parse(str)
            .Update(1, 12)
            .Update(2, 2)
            .Run()
            .Answer;

    [Puzzle(answer: 4847, O.ms)]
    public int part_two(string str)
    {
        var program = Computer.Parse(str);
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
