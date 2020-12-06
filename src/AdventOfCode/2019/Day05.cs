using AdventOfCode._2019.Intcoding;
using System.Linq;

namespace AdventOfCode._2019
{
    public class Day05
    {
        public static int One(string input)
        {
            var program = Intcode.Parse(input);
            program = program.Run(1);
            return program.Outputs.Last();
        }

        public static int Two(string input)
        {
            var program = Intcode.Parse(input);
            program = program.Run(5);
            return program.Outputs.Last();
        }
    }
}