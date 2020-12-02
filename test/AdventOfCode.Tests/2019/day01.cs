using AdventOfCode;
using AdventOfCode._2019;
using AdventOfCode.Tests;
using NUnit.Framework;

namespace Advent_of_Code_2019
{
    public class day01
    {
        [TestCase(12, 2)]
        [TestCase(14, 2)]
        [TestCase(1969, 654)]
        [TestCase(100756, 33583)]
        public void part_one_example(int input, int fuel)
        {
            Puzzle.HasAnswer(fuel, Day01.One, with: input.ToString());
        }

        [Test]
        public void part_one()
        {
            var input = Input.For(2019, 01);
            Puzzle.HasAnswer(3291356, Day01.One, with: input);
        }

        [TestCase(12, 2)]
        [TestCase(14, 2)]
        [TestCase(1969, 966)]
        [TestCase(100756, 50346)]
        public void part_two_example(int input, int fuel)
        {
            Puzzle.HasAnswer(fuel, Day01.Two, with: input.ToString());
        }

        [Test]
        public void part_two()
        {
            var input = Input.For(2019, 01);
            Puzzle.HasAnswer(4934153, Day01.Two, with: input);
        }
    }
}
