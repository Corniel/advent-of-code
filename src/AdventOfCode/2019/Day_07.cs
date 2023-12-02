namespace Advent_of_Code_2019;

[Category(Category.IntComputer)]
public class Day_07
{
    [Example(answer: 43210, "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0")]
    [Example(answer: 54321, "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0")]
    [Example(answer: 65210, "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0")]
    [Puzzle(answer: 101490, O.ms)]
    public Int part_one(string str)
        => new Int[] { 0, 1, 2, 3, 4 }
            .Permutations()
            .Max(phases => Amplify(Computer.Parse(str), phases));

    [Puzzle(answer: 61019896, O.ms)]
    public Int part_two(string str)
        => new Int[] { 5, 6, 7, 8, 9 }
            .Permutations()
            .Max(phases => AmplifyWithFeedback(Computer.Parse(str), phases));

    static Int Amplify(Computer program, Int[] phases)
    {
        Int signal = 0;
        foreach (var phase in phases)
        {
            signal = program.Copy().Run(new RunArguments(phase, signal)).Output[0];
        }
        return signal;
    }

    static Int AmplifyWithFeedback(Computer program, params Int[] phases)
    {
        var programs = new[]
        {
                program.Copy(),
                program.Copy(),
                program.Copy(),
                program.Copy(),
                program.Copy(),
            };
        var outputs = new List<Int>[] { [], [], [], [], [] };


        Int signal = 0;

        // initial set the configuration.
        for (var i = 0; i < phases.Length; i++)
        {
            programs[i].Inputs.Enqueue(phases[i]);
        }
        while (!programs.Last().Finished)
        {
            var index = 0;
            foreach (var prog in programs)
            {
                var output = outputs[index];
                output.AddRange(prog.Run(new RunArguments(false, true, signal)).Output);
                signal = output.Last();
            }
        }
        return signal;
    }
}
