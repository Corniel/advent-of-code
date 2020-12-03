using AdventOfCode._2019;
using AdventOfCode._2019.Intcoding;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2019
{
    public class day05
    {
        [Test]
        public void part_one_example()
        {
            var program = Intcode.Parse("1002,4,3,4,33");
            Assert.AreEqual(99, program.Run().Memory[4]);
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2019, 05);
            Puzzle.HasAnswer(12428642, Day05.One, with: input);
        }
  
        [Test]
        public void part_two()
        {
            var input = Input.For(2019, 05);
            Puzzle.HasAnswer(918655, Day05.Two, with: input);
        }
    }
}
