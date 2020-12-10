using Advent_of_Code;
using Advent_of_Code_2019.Intcoding;
using System;
using System.Linq;

namespace Advent_of_Code_2019
{
    public class Day_07
    {
        [Example(answer: 43210,"3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0")]
        [Example(answer: 54321,"3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0")]
        [Example(answer: 65210,"3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0")]
        [Puzzle(answer: 101490, year: 2019, day: 07)]
        public int part_one(string input)
            => new[] { 0, 1, 2, 3, 4 }
                .Permutations()
                .Max(phases => Amplify(Intcode.Parse(input), phases));

        [Puzzle(answer: 61019896, year: 2019, day: 07)]
        public int part_two(string input)
            => new[] { 5, 6, 7, 8, 9 }
                .Permutations()
                .Max(phases => AmplifyWithFeedback(Intcode.Parse(input), phases));

        public static int Amplify(Intcode program, int[] phases)
        {
            var signal = 0;
            foreach (var phase in phases)
            {
                signal = program.Copy().Run(phase, signal).Outputs.First();
            }
            return signal;
        }

        public static int AmplifyWithFeedback(Intcode program, params int[] phases)
        {
            var programs = new[]
            {
                program.Copy(),
                program.Copy(),
                program.Copy(),
                program.Copy(),
                program.Copy(),
            };

            for (var i = 0; i < phases.Length; i++)
            {
                programs[i].Inputs.Enqueue(phases[i]);
            }

            var signal = 0;

            while (!programs.Last().Halted())
            {
                foreach (var prog in programs)
                {
                    prog.Run(true, signal);
                    signal = prog.Outputs.LastOrDefault();
                }
            }
            return signal;
        }
    }
}