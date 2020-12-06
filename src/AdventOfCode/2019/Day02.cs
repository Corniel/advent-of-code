using AdventOfCode._2019.Intcoding;

namespace AdventOfCode._2019
{
    public class Day02
    {
        public static int OneExample(string input)
            => Intcode.Parse(input)
            .Run()
            .Answer();

        public static int One(string input)
            => Intcode.Parse(input)
            .Update(1, 12)
            .Update(2, 2)
            .Run()
            .Answer();

        public static int Two(string input)
        {
            var program = Intcode.Parse(input);

            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    var copy = program
                        .Copy()
                        .Update(1, noun)
                        .Update(2, verb)
                        .Run();

                    if (copy?.Answer() == 19690720)
                    {
                        return noun * 100 + verb;
                    }
                }
            }
            throw new NoAnswer();
        }
    }
}