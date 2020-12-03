using AdventOfCode;
using AdventOfCode._2020;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2020
{
    public class day03
    {
        [Test]
        public void part_one_example_returns_x()
        {
            var input = @"";
            Puzzle.HasAnswer(false, Day03.One, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2020, 03, Part.one);
            Puzzle.HasAnswer(false, Day03.One, with: input);
        }
  
        [Test]
        public void part_two_example_returns_X()
        {
            var input = @"";
            Puzzle.HasAnswer(false, Day03.Two, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2020, 03, Part.two);
            Puzzle.HasAnswer(false, Day03.Two, with: input);
        }
    }
}
