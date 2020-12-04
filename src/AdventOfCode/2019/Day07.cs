using AdventOfCode._2019.Intcoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public class Day07
    {
        [Puzzle(2019, 07, Part.one)]
        public static int One(string input)
            => new[] { 0, 1, 2, 3, 4 }
            .Permutations()
            .Max(phases => Amplify(Intcode.Parse(input), phases));

        [Puzzle(2019, 07, Part.two)]
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

            while (!programs[4].Halted())
            {
                for (var i = 0; i < phases.Length; i++)
                {
                    var phase = phases[i];
                    var selected = programs[i];
                    selected.Run(true, signal);
                    signal = selected.Outputs.LastOrDefault();
                }
            }
            return signal;
        }
    }
}