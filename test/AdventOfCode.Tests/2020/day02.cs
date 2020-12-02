using AdventOfCode;
using AdventOfCode._2020;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2020
{
    public class day02
    {
        [Test]
        public void part_one_example_returns_2()
        {
            var input = @"
1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc";
            Puzzle.HasAnswer(2, Day02.One, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2020, 02);
            Puzzle.HasAnswer(536, Day02.One, with: input);
        }
  
        [Test]
        public void part_two_example_returns_1()
        {
            var input = @"
1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc";
            Puzzle.HasAnswer(1, Day02.Two, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2020, 02);
            Puzzle.HasAnswer(558, Day02.Two, with: input);
        }
    }
}
