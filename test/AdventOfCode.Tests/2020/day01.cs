using AdventOfCode;
using AdventOfCode._2020;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2020
{
    public class day01
    {
        [Test]
        public void part_one_example_returns_514579()
        {
            var numbers = @"
                1721
                979
                366
                299
                675
                1456";
            Puzzle.HasAnswer(514579, Day01.One, with: numbers);
        }

        [Test]
        public void part_one()
        {
            var numbers = Input.For(2020, 01, Part.one);
            Puzzle.HasAnswer(786811, Day01.One, with: numbers);
        }
  
        [Test]
        public void part_two_returns_241861950()
        {
            var numbers = @"
                1721
                979
                366
                299
                675
                1456";
            Puzzle.HasAnswer(241861950L, Day01.Two, with: numbers);
        }

        [Test]
        public void part_two()
        {
            var numbers = Input.For(2020, 01, Part.two);
            Puzzle.HasAnswer(199068980L, Day01.Two, with: numbers);
        }
    }
}
