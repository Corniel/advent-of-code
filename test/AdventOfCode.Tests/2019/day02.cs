using AdventOfCode._2019;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2019
{
    public class day02
    {
        [Test]
        public void part_one_example()
        {
            var input = @"1,9,10,3,2,3,11,0,99,30,40,50";
            Puzzle.HasAnswer(3500, Day02.OneExample, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2019, 02);
            Puzzle.HasAnswer(5110675, Day02.One, with: input);
        }
  
        [Test]
        public void part_two()
        {
            var input = Input.For(2019, 02);
            Puzzle.HasAnswer(4847, Day02.Two, with: input);
        }
    }
}
