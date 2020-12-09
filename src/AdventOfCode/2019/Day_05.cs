using Advent_of_Code;
using Advent_of_Code_2019.Intcoding;
using NUnit.Framework;
using System.Linq;

namespace Advent_of_Code_2019
{
    public class Day_05
    {
        [Puzzle(answer: 12428642, year: 2019, day: 05)]
        public void part_one(long answer, string input)
        {
            var outcome = Intcode.Parse(input).Run(1).Outputs.Last();
            Assert.That(outcome, Is.EqualTo(answer));
        }

        [Puzzle(answer: 918655, year: 2019, day: 05)]
        public void part_two(long answer, string input)
        {
            var outcome = Intcode.Parse(input).Run(5).Outputs.Last();
            Assert.That(outcome, Is.EqualTo(answer));
        }
    }
}