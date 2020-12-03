using AdventOfCode;
using AdventOfCode._2019;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2019
{
    public class day05
    {
        [Test]
        public void part_one()
        {
            var input = Input.For(2019, 05);
            Puzzle.HasAnswer(0, Day05.One, with: input);
        }
  
        [Test]
        public void part_two_example()
        {
            var input = @"";
            Puzzle.HasAnswer(0, Day05.Two, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2019, 05);
            Puzzle.HasAnswer(0, Day05.Two, with: input);
        }
    }
}
