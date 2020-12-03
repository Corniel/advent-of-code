using AdventOfCode._2019.Intcoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public class Day05
    {
        [Puzzle(2019, 05, Part.one)]
        public static int One(string input)
        {
            var program = Intcode.Parse(input);
            program = program.Run(1);
            return program.Outputs.Last();
        }

        [Puzzle(2019, 05, Part.two)]
        public static int Two(string input)
        {
            var program = Intcode.Parse(input);
            program = program.Run(5);
            return program.Outputs.Last();
        }
    }
}