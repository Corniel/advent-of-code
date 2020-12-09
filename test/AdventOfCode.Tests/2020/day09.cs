using AdventOfCode._2020;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2020
{
    public class day09
    {
        [Test]
        public void part_one_example()
        {
            var input = @"
                35
                20
                15
                25
                47
                40
                62
                55
                65
                95
                102
                117
                150
                182
                127
                219
                299
                277
                309
                576";
            Puzzle.HasAnswer(127, Day09.One, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2020, 09);
            Puzzle.HasAnswer(0, Day09.One, with: input);
        }
  
        [Test]
        public void part_two_example()
        {
            var input = @"";
            Puzzle.HasAnswer(0, Day09.Two, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2020, 09);
            Puzzle.HasAnswer(0, Day09.Two, with: input);
        }
    }
}
