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
        {
            var program = Intcode.Parse(input);
            return new[] { 0, 1, 2, 3, 4 }
                .Permutations()
                .Max(input => program
                        .Copy()
                        .Run(input)
                        .Outputs.FirstOrDefault());
        }

        [Puzzle(2019, 07, Part.two)]
        public static int Two(string input)
            => throw new NoAnswer();
    }
}