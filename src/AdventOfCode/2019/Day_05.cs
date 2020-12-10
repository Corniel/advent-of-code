using Advent_of_Code;
using Advent_of_Code_2019.Intcoding;
using System.Linq;

namespace Advent_of_Code_2019
{
    public class Day_05
    {
        [Puzzle(answer: 12428642, year: 2019, day: 05)]
        public int part_one(string input)
            => Intcode.Parse(input).Run(1).Outputs.Last();

        [Puzzle(answer: 918655, year: 2019, day: 05)]
        public int part_two(string input)
            => Intcode.Parse(input).Run(5).Outputs.Last();
    }
}