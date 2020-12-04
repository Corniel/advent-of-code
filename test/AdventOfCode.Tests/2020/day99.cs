using AdventOfCode._2020;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2020
{
    [TestFixture(Ignore = "Template")]
    public class day99
    {
        [Test]
        public void part_one_example()
        {
            var input = @"";
            Puzzle.HasAnswer(0, Day99.One, with: input);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2020, 99);
            Puzzle.HasAnswer(0, Day99.One, with: input);
        }
  
        [Test]
        public void part_two_example()
        {
            var input = @"";
            Puzzle.HasAnswer(0, Day99.Two, with: input);
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2020, 99);
            Puzzle.HasAnswer(0, Day99.Two, with: input);
        }
    }
}
