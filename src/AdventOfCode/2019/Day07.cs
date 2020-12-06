using AdventOfCode._2019.Intcoding;
using System;
using System.Linq;

namespace AdventOfCode._2019
{
    public class Day07
    {
        public static int One(string input)
            => new[] { 0, 1, 2, 3, 4 }
            .Permutations()
            .Max(phases => Amplify(Intcode.Parse(input), phases));

        public static int Two(string input)
            => new[] { 5, 6, 7, 8, 9 }
            .Permutations()
            .Max(phases => AmplifyWithFeedback(Intcode.Parse(input), phases));
            

        public static int Amplify(Intcode program, int[] phases)
        {
            var signal = 0;
            foreach(var phase in phases)
            {
                signal = program.Copy().Run(phase, signal).Outputs.First();
            }
            return signal;
        }

        public static int AmplifyWithFeedback(Intcode program, params int[] phases)
        {
            var programs = new [] 
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
                foreach(var prog in programs)
                {
                    prog.Run(true, signal);
                    signal = prog.Outputs.LastOrDefault();
                }
            }
            return signal;
        }
    }
}