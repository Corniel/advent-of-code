using AdventOfCode;
using AdventOfCode._2019;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2019
{
    public class day02
    {
        [Test]
        public void part_one_example_returns_x()
        {
            var input = @"1,9,10,3,2,3,11,0,99,30,40,50";
            Puzzle.HasAnswer(3500, Day02.One, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2019, 02, Part.one);
            Puzzle.HasAnswer(655685, Day02.One, with: input);
        }
  
        [Test]
        public void part_two_example_returns_X()
        {
            var input = @"";
            Puzzle.HasAnswer(0, Day02.Two, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2019, 02, Part.two);
            Puzzle.HasAnswer(0, Day02.Two, with: input);
        }
    }
}
