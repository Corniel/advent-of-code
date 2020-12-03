using AdventOfCode;
using AdventOfCode._2019;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2019
{
    public class day07
    {
        [Test]
        public void part_one()
        {
            var input = Input.For(2019, 07);
            Puzzle.HasAnswer(330, Day07.One, with: input);
        }
  
        [Test]
        public void part_two_example()
        {
            var input = @"";
            Puzzle.HasAnswer(0, Day07.Two, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2019, 07);
            Puzzle.HasAnswer(0, Day07.Two, with: input);
        }
    }
}
